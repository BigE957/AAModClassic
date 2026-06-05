using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
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
    public class SunkenShipBiome : ModBiome
    {
        private static readonly CthulhuSky_Clouds CthulhuFog = new(false);

        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer_Unreleased>().ZoneShip = AAWorld_Unreleased.ShipTiles > 1;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool useCthulhu = 
                NPC.AnyNPCs(ModContent.NPCType<SoulOfCthulhu>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEaterTail>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) ||
                NPC.AnyNPCs(ModContent.NPCType<CthulhuPortal>()) ||
                NPC.AnyNPCs(ModContent.NPCType<Cthulhu>()) ||
                (Main.LocalPlayer.GetModPlayer<AAPlayer_Unreleased>().ZoneShip && AAWorld.downedAllAncients && !AAWorld_Unreleased.DownedSoC);

            if (SkyManager.Instance["AAModClassic:CthulhuSky"] != null && ((isActive && useCthulhu) != SkyManager.Instance["AAModClassic:CthulhuSky"].IsActive()))
            {
                if (isActive && useCthulhu)
                    SkyManager.Instance.Activate("AAModClassic:CthulhuSky");
                else
                    SkyManager.Instance.Deactivate("AAModClassic:CthulhuSky");
            }

            CthulhuFog.Update(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/CthulhuSky_Clouds").Width(), 1);
        }

        public override int Music => (AAWorld.downedAllAncients && !AAWorld_Unreleased.DownedSoC) ? MusicManagementSystem.MusicSlots["SunkenShip_PreSoC"] : MusicManagementSystem.MusicSlots["SunkenShip"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

        private class CthulhuForegroundFogHandler : ModSystem
        {
            public override void Load()
            {
                On_Main.DrawInfernoRings += DrawFog;
            }

            private void DrawFog(On_Main.orig_DrawInfernoRings orig, Main self)
            {
                orig(self);

                CthulhuFog.Draw(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/CthulhuSky_Clouds").Value, Color.White);
            }
        }
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

            BGClouds.Update(texture.Width(), -1);
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
            BGClouds.Draw(texture.Value, new Color(130, 130, 130));
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
                return;

            SoCIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == SoCType)
                {
                    SoCIndex = i;
                    break;
                }
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

    //TODO: Turn this into a ModSceneEffect and/or merge it into CthulhuSky
    public class CthulhuSky_Clouds(bool bg)
    {
        public float fogOffsetX = 0;
        public float fadeOpacity = 0f;
        public float dayTimeOpacity = 0f;
        public bool backgroundFog = bg;

        public void Update(int width, float mult = 1f)
        {
            if (Main.netMode == NetmodeID.Server || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

            bool useCthulhu =
                NPC.AnyNPCs(ModContent.NPCType<SoulOfCthulhu>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEaterTail>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) ||
                NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) ||
                NPC.AnyNPCs(ModContent.NPCType<CthulhuPortal>()) ||
                NPC.AnyNPCs(ModContent.NPCType<Cthulhu>()) ||
                (Main.LocalPlayer.GetModPlayer<AAPlayer_Unreleased>().ZoneShip && AAWorld.downedAllAncients && !AAWorld_Unreleased.DownedSoC);

            if (!backgroundFog && BasePlayer.HasAccessory(Main.LocalPlayer, ModContent.ItemType<Lantern>(), true, false)) 
                useCthulhu = false;

            fogOffsetX = fogOffsetX + mult;
            if (fogOffsetX >= width) 
                fogOffsetX = fogOffsetX - width;
            if (fogOffsetX <= -width)
                fogOffsetX = fogOffsetX + width * 2;
            if (useCthulhu)
            {
                fadeOpacity += 0.05f;
                if (fadeOpacity > 1f) 
                    fadeOpacity = 1f;
            }
            else
            {
                fadeOpacity -= 0.05f;
                if (fadeOpacity < 0f) 
                    fadeOpacity = 0f;
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

        public void Draw(Texture2D texture, Color defaultColor)
        {
            if (fadeOpacity == 0f) return; //don't draw if no fog

            Color fogColor = GetAlpha(defaultColor, 0.4f * fadeOpacity * dayTimeOpacity);
            int minX = -texture.Width;
            int minY = -texture.Height;
            int maxX = Main.screenWidth + texture.Width;
            int maxY = Main.screenHeight + texture.Height;

            for (int i = minX; i < maxX; i += texture.Width)
                for (int j = minY; j < maxY; j += texture.Height)
                    Main.spriteBatch.Draw(texture, new Vector2(i + fogOffsetX, j), null, fogColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public static Color GetAlpha(Color newColor, float alph)
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
