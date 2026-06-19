using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Inferno.World.Biomes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened.Skies
{
    public class AkumaASkyScene : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player) => NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()) || player.GetModPlayer<AAPlayer>().AkumaAltar;

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:AkumaASky", isActive);
            player.ManageSpecialBiomeVisuals("HeatDistortion", isActive && Main.UseHeatDistortion);
        }
    }

    public class AkumaASky : CustomSky
    {
        private struct Meteor
        {
            public Vector2 Position;

            public float Depth;

            public int FrameCounter;

            public float Scale;

            public float StartX;
        }

        private Meteor[] Meteors;

        public bool Active;
        public float Intensity;
        private readonly UnifiedRandom _random = new();

        private readonly float num = 1200f;

        private static Asset<Texture2D> SunTex;
        private static Asset<Texture2D> MeteorTex;
        private static Asset<Texture2D> SkyTex;

        public override void OnLoad()
        {
            SunTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/Skies/AkumaASky_Sun");
            MeteorTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/Skies/AkumaASky_Meteor");
            SkyTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/SkyTex");
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
                Intensity = Math.Min(1f, 0.01f + Intensity);
            else
                Intensity = Math.Max(0f, Intensity - 0.01f);

            for (int i = 0; i < Meteors.Length; i++)
            {
                Meteor[] expr_60_cp_0_cp_0 = Meteors;
                int expr_60_cp_0_cp_1 = i;
                expr_60_cp_0_cp_0[expr_60_cp_0_cp_1].Position.X = expr_60_cp_0_cp_0[expr_60_cp_0_cp_1].Position.X - num * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Meteor[] expr_8E_cp_0_cp_0 = Meteors;
                int expr_8E_cp_0_cp_1 = i;
                expr_8E_cp_0_cp_0[expr_8E_cp_0_cp_1].Position.Y = expr_8E_cp_0_cp_0[expr_8E_cp_0_cp_1].Position.Y + num * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Meteors[i].Position.Y > Main.worldSurface * 16.0)
                {
                    Meteors[i].Position.X = Meteors[i].StartX;
                    Meteors[i].Position.Y = -10000f;
                }
            }
        }

        public override Color OnTileColor(Color inColor) => Color.Lerp(inColor, Color.White, Intensity * 0.5f);

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Texture2D sun = SunTex.Value;
            Texture2D meteor = MeteorTex.Value;
            Texture2D sky = SkyTex.Value;

            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (Main.dayTime || Main.LocalPlayer.GetModPlayer<AAPlayer>().AkumaAltar)
                {
                    spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    spriteBatch.Draw(sky, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.DeepSkyBlue * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * Intensity));
                    float sunOpacity = 1f;
                    sunOpacity -= Main.cloudAlpha * 1.5f;
                    if (sunOpacity < 0f)
                        sunOpacity = 0f;
                    
                    int sunX = (int)(Main.time / 54000.0 * (Main.screenWidth + TextureAssets.Sun.Value.Width * 2)) - TextureAssets.Sun.Value.Width;
                    int sunY = 0;
                    float sunScale = 1f;
                    float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;
                    double bgTop = 0;
                    if (!Main.gameMenu)
                    {
                        float clampedScreenH = Math.Min(PlayerInput.RealScreenHeight, Main.LogicCheckScreenHeight);
                        float screenMidY = Main.screenPosition.Y + Main.screenHeight / 2f - clampedScreenH / 2f;
                        bgTop = (int)((-screenMidY) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    }
                    if (Main.dayTime)
                    {
                        double timeMult;
                        if (Main.time < 27000.0)
                        {
                            timeMult = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
                            sunY = (int)(bgTop + timeMult * 250.0 + 180.0);
                        }
                        else
                        {
                            timeMult = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
                            sunY = (int)(bgTop + timeMult * 250.0 + 180.0);
                        }
                        sunScale = (float)(1.2 - timeMult * 0.4);
                    }

                    InfernoSky infernoSky = ModContent.GetInstance<InfernoSky>();
                    if (Main.gameMenu || infernoSky == null || !infernoSky.IsActive())
                        sunOpacity *= Intensity;
                    else
                        sunScale = MathHelper.Lerp(0.25f, sunScale, Intensity);

                    spriteBatch.Draw(sun, new Vector2(sunX, sunY + Main.sunModY), null, Color.White * sunOpacity, rotation, sun.Size() / 2f, sunScale, SpriteEffects.None, 0f);
                }
            }

            int num = -1;
            int num2 = 0;

            for (int i = 0; i < Meteors.Length; i++)
            {
                float depth = Meteors[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int j = num; j < num2; j++)
            {
                Vector2 value4 = new Vector2(1f / Meteors[j].Depth, 0.9f / Meteors[j].Depth);
                Vector2 position = (Meteors[j].Position - value3) * value4 + value3 - Main.screenPosition;
                int num3 = Meteors[j].FrameCounter / 3;
                Meteors[j].FrameCounter = (Meteors[j].FrameCounter + 1) % 12;
                if (rectangle.Contains((int)position.X, (int)position.Y))
                {
                    spriteBatch.Draw(meteor, position, new Rectangle?(new Rectangle(0, num3 * (meteor.Height / 4), meteor.Width, meteor.Height / 4)), Color.White * scale * Intensity, 0f, Vector2.Zero, value4.X * 5f * Meteors[j].Scale, SpriteEffects.None, 0f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
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

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            Meteors = new Meteor[150];
            for (int i = 0; i < Meteors.Length; i++)
            {
                float num = i / (float)Meteors.Length;
                Meteors[i].Position.X = num * (Main.maxTilesX * 16f) + _random.NextFloat() * 40f - 20f;
                Meteors[i].Position.Y = _random.NextFloat() * -((float)Main.worldSurface * 16f + 10000f) - 10000f;
                if (_random.NextBool(3))
                {
                    Meteors[i].Depth = _random.NextFloat() * 3f + 1.8f;
                }
                else
                {
                    Meteors[i].Depth = _random.NextFloat() * 5f + 4.8f;
                }
                Meteors[i].FrameCounter = _random.Next(12);
                Meteors[i].Scale = _random.NextFloat() * 0.5f + 1f;
                Meteors[i].StartX = Meteors[i].Position.X;
            }
            Array.Sort(Meteors, new Comparison<Meteor>(SortMethod));
        }
        private int SortMethod(Meteor meteor1, Meteor meteor2)
        {
            return meteor2.Depth.CompareTo(meteor1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
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

    public class AkumaASkyData : ScreenShaderData
    {
        private int AkumaIndex;

        public AkumaASkyData(string passName) : base(passName)
        {
        }

        private void UpdateAkumaIndex()
        {
            int AkumaType = ModContent.NPCType<AkumaAHead>();
            if (AkumaIndex >= 0 && Main.npc[AkumaIndex].active && Main.npc[AkumaIndex].type == AkumaType)
            {
                return;
            }
            AkumaIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == AkumaType)
                {
                    AkumaIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateAkumaIndex();
            if (AkumaIndex != -1)
            {
                UseTargetPosition(Main.npc[AkumaIndex].Center);
            }
            base.Apply();
        }
    }
}