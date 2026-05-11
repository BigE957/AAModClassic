using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Biomes
{
    public class ShipBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer_Unreleased>().ZoneShip = AAWorld_Unreleased.ShipTiles > 1 && player.wet;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Ship"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

    public class CthulhuSky : CustomSky
    {
        private readonly CthulhuSky_Clouds BGClouds = new(true);

        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;
        private int _fogTimer = 300;
        private int _fogTimer2 = 300;
        private static readonly Asset<Texture2D> texture = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/CthulhuSky_Clouds");

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;

        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }
            _fogTimer--;
            _fogTimer2 -= 3;
            if (_fogTimer <= 0)
            {
                _fogTimer = texture.Width();
            }

            if (_fogTimer2 <= 0)
            {
                _fogTimer2 = texture.Width();
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            var planetPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(texture.Value, planetPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(texture.Width() >> 1, texture.Height() >> 1), 1f, SpriteEffects.None, 1f);

            }
            BGClouds.Update(texture.Value);
            BGClouds.Draw(texture.Value, true, new Color(130, 130, 130));
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
        }

        public override void Reset()
        {
            Active = false;
        }

        public override bool IsActive()
        {
            return Active || Intensity > 0.001f;
        }
    }

    public class CthulhuSkyData(string passName) : ScreenShaderData(passName)
    {
        private int SoCIndex;

        private void UpdateCthulhuSky()
        {

            int SoCType = ModContent.NPCType<SoulOfCthulhu>();
            if (SoCIndex >= 0 && Main.npc[SoCIndex].active && Main.npc[SoCIndex].type == SoCType)
            {
                return;
            }
            SoCIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == SoCType)
                {
                    SoCIndex = i;
                    break;
                }
            }
            if (Main.LocalPlayer.ZoneBeach && !AAWorld_Unreleased.downedSoC && AAWorld.downedAllAncients)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateCthulhuSky();
            if (SoCIndex != -1)
            {
                UseTargetPosition(Main.npc[SoCIndex].Center);
            }
            base.Apply();
        }
    }

    public class CthulhuSky_Handler : ModSystem
    {
        private static readonly CthulhuSky_Clouds CthulhuFog = new(false);

        public override void PostDrawTiles()
        {
            CthulhuFog.Update(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/CthulhuSky_Clouds").Value);
            CthulhuFog.Draw(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/CthulhuSky_Clouds").Value, false, Color.White, true);
        }
    }

    //TODO: Turn this into a ModSceneEffect and/or merge it into CthulhuSky
    public class CthulhuSky_Clouds
    {
        public int fogOffsetX = 0;
        public float fadeOpacity = 0f;
        public float dayTimeOpacity = 0f;
        public bool backgroundFog = false;

        public CthulhuSky_Clouds(bool bg)
        {
            backgroundFog = bg;
        }

        public void Update(Texture2D texture)
        {
            if (Main.netMode == NetmodeID.Server || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

            bool CthulhuTime = Main.LocalPlayer.ZoneBeach && !Main.LocalPlayer.ZoneSkyHeight && AAWorld.downedAllAncients && !AAWorld_Unreleased.downedSoC;
            if (!backgroundFog && (BasePlayer.HasAccessory(Main.LocalPlayer, ModContent.ItemType<Lantern>(), true, false) || AAWorld_Unreleased.downedSoC)) CthulhuTime = false;

            fogOffsetX += 1;
            if (fogOffsetX >= texture.Width) fogOffsetX = 0;
            if (CthulhuTime)
            {
                fadeOpacity += 0.05f;
                if (fadeOpacity > 1f) fadeOpacity = 1f;
            }
            else
            {
                fadeOpacity -= 0.05f;
                if (fadeOpacity < 0f) fadeOpacity = 0f;
            }
            if (backgroundFog)
            {
                dayTimeOpacity = BaseUtility.MultiLerp((float)Main.time / 52000f, 0.5f, 1f, 1f, 1f, 1f, 1f, 0.5f);
                dayTimeOpacity *= 0.7f; //make it fadier as it's in the background
            }
            else
            {
                dayTimeOpacity = BaseUtility.MultiLerp((float)Main.time / 52000f, 0.3f, 1f, 1f, 1f, 1f, 1f, 0.3f);
            }
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
            if (fadeOpacity == 0f) return; //don't draw if no fog
            if (setSB) Main.spriteBatch.Begin();

            Color bgColor = GetAlpha(defaultColor, 0.2f * fadeOpacity * dayTimeOpacity);
            Color fogColor = GetAlpha(defaultColor, 0.4f * fadeOpacity * dayTimeOpacity);
            int minX = -texture.Width;
            int minY = -texture.Height;
            int maxX = Main.screenWidth + texture.Width;
            int maxY = Main.screenHeight + texture.Height;


            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
            if (setSB) Main.spriteBatch.End();
        }

        public Color GetAlpha(Color newColor, float alph)
        {
            int alpha = 255 - (int)(255 * alph);
            float alphaDiff = (255 - alpha) / 255f;
            int newR = (int)(newColor.R * alphaDiff);
            int newG = (int)(newColor.G * alphaDiff);
            int newB = (int)(newColor.B * alphaDiff);
            int newA = newColor.A - alpha;
            if (newA < 0) newA = 0;
            if (newA > 255) newA = 255;
            return new Color(newR, newG, newB, newA);
        }
    }

}
