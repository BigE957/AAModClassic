using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace AAModClassic.Backgrounds
{
    public class MireSky : CustomSky
    {
        public bool Active;
        public float Intensity;

        public override void OnLoad()
        {
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
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            bool SilouetteMode = !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().Clueless;
            if (SilouetteMode)
            {
                return new Color(1,1,1);
            }
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Texture2D PlanetTexture = AAMod.GetTexture("Backgrounds/Moon");
            Texture2D SkyTexture = AAMod.GetTexture("Backgrounds/MireSky");
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (!Main.dayTime)
                {
                    spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * Intensity);
                    double bgTop = (int)((-Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    Main.ColorOfTheSkies = Color.White;
                    if (Main.gameMenu || Main.netMode == NetmodeID.Server)
                    {
                        bgTop = -200;
                    }
                    int num23 = (int)(Main.time / 32400.0 * (Main.screenWidth + TextureAssets.Moon[Main.moonType].Width() * 2)) - TextureAssets.Moon[Main.moonType].Width();
                    int num24 = 0;
                    Color white2 = Color.White;
                    float num25 = 1f;
                    float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
                    if (!Main.dayTime)
                    {
                        double num27;
                        if (Main.time < 16200.0)
                        {
                            num27 = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
                            num24 = (int)(bgTop + num27 * 250.0 + 180.0);
                        }
                        else
                        {
                            num27 = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
                            num24 = (int)(bgTop + num27 * 250.0 + 180.0);
                        }
                        num25 = (float)(1.2 - num27 * 0.4);
                    }
                    float num65 = 1f - Main.cloudAlpha * 1.5f;
                    if (num65 < 0f)
                    {
                        num65 = 0f;
                    }

                    num25 = MathHelper.Lerp(0.25f, num25, Intensity);

                    white2.R = (byte)(white2.R * num65);
                    white2.G = (byte)(white2.G * num65);
                    white2.B = (byte)(white2.B * num65);
                    white2.A = (byte)(white2.A * num65);
                    Main.spriteBatch.Draw(PlanetTexture, new Vector2(num23, num24 + (Main.gameMenu ? Main.moonModY + 200 : Main.moonModY)), new Rectangle?(new Rectangle(0, 0, PlanetTexture.Width, PlanetTexture.Width)), white2 * Intensity, rotation2, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Width / 2), num25, SpriteEffects.None, 0f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
        }

        public override void Deactivate(params object[] args)
        {
            if(!Main.gameMenu || (args.Length > 0 && (bool)args[0] == true))
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

    public class MireSkyData : ScreenShaderData
    {
        public MireSkyData(string passName) : base(passName)
        {
        }

        private static void UpdateMireSky()
        {
            if (AAWorld.mireTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateMireSky();
            base.Apply();
        }
    }
}