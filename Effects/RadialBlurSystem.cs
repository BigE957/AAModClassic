using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAModClassic.Effects
{
    public class RadialBlurSystem : ModSystem
    {
        private static float CurrentScreenBlurStrength = 0f;
        private static bool BlurSetThisFrame = false;
        private static Vector2 ScreenBlurSource = Vector2.Zero;
        private static int BlurTimer = 0;

        public static void SetRadialBlur(float intensity, Vector2 source)
        {
            if(!BlurSetThisFrame || intensity > CurrentScreenBlurStrength)
            {
                CurrentScreenBlurStrength = intensity;
                ScreenBlurSource = source;
            }

            BlurSetThisFrame = true;
        }

        public override void PostDrawTiles()
        {
            if (Main.dedServ)
                return;

            if(CurrentScreenBlurStrength == 0f)
            {
                if (!Filters.Scene["AAModClassic:RadialBlur"].IsActive())
                    Filters.Scene.Deactivate("AAModClassic:RadialBlur");
            }

            if (!BlurSetThisFrame)
            {
                CurrentScreenBlurStrength = Filters.Scene["AAModClassic:RadialBlur"].GetShader().Intensity * 0.9f;
                Filters.Scene["AAModClassic:RadialBlur"].GetShader().UseIntensity(CurrentScreenBlurStrength);
                if (CurrentScreenBlurStrength < 0.01f)
                {
                    CurrentScreenBlurStrength = 0f;
                    BlurTimer = 0;
                    ScreenBlurSource = Vector2.Zero;
                    Filters.Scene["AAModClassic:RadialBlur"].GetShader().UseIntensity(0f);
                }
                return;
            }

            if (Filters.Scene["AAModClassic:RadialBlur"].IsLoaded)
            {
                if (!Filters.Scene["AAModClassic:RadialBlur"].IsActive())
                    Filters.Scene.Activate("AAModClassic:RadialBlur");

                float distSQ = Main.LocalPlayer.DistanceSQ(ScreenBlurSource);
                float distanceScaleFactor = 1;
                if (distSQ > 592900) //770^2
                    distanceScaleFactor = 1 / (1 + (((float)Math.Sqrt(distSQ) - 770) / 32f));

                Filters.Scene["AAModClassic:RadialBlur"].GetShader().UseIntensity((CurrentScreenBlurStrength + (((float)Math.Cos(BlurTimer * MathHelper.TwoPi / 15f) / 2f + 0.5f) * (0.4f * CurrentScreenBlurStrength))) * distanceScaleFactor);
                Filters.Scene["AAModClassic:RadialBlur"].GetShader().Shader.Parameters["uSaturation"].SetValue(20);

                Vector2 targetPos = Vector2.Transform(ScreenBlurSource - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.ScreenSize.ToVector2();

                Filters.Scene["AAModClassic:RadialBlur"].GetShader().UseDirection(targetPos);

                BlurTimer++;
            }

            BlurSetThisFrame = false;
        }
    }
}
