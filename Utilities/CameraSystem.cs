using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{

    [Autoload(Side = ModSide.Client)]
    public class CameraSystem : ModSystem
    {
        public static Vector2 CameraCenter
        {
            get => cameraPosition + Main.ScreenSize.ToVector2() * 0.5f;
            set
            {
                setCameraPosition = cameraPosition = value - Main.ScreenSize.ToVector2() * 0.5f;
                cameraModified = true;
            }
        }
        private static Vector2 oldCameraPosition = Main.screenPosition;
        private static Vector2 cameraPosition = Main.screenPosition;
        private static Vector2 setCameraPosition = Main.screenPosition;
        private static bool cameraModified = false;
        public static bool CameraModified => cameraModified;
        private static int ResetTimer = 60;

        public static float Zoom
        {
            get => zoom;
            set
            {
                setZoom = zoom = value;
                cameraModified = true;
            }
        }
        private static float zoom = 0f;
        private static float setZoom = 0f;

        private static bool releaseRequested = false;
        private static bool releasingCamera = false;
        private static Vector2 releaseOffset = Vector2.Zero;

        public override void ModifyScreenPosition()
        {
            if (ResetTimer > 30)
            {
                oldCameraPosition = setCameraPosition = Main.screenPosition;
                setZoom = 0f;
                zoom = 0f;

                releasingCamera = false;
                releaseRequested = false;

                return;
            }

            if ((Main.LocalPlayer.dead && !Main.gamePaused) || !cameraModified)
            {
                if (releaseRequested)
                {
                    releaseOffset = setCameraPosition - Main.screenPosition;

                    releaseOffset.X = 0f;

                    releasingCamera = true;
                    releaseRequested = false;
                }

                float lerp = MathUtils.SineInOutEasing(ResetTimer / 30f);

                if (releasingCamera)
                    cameraPosition = Main.screenPosition + new Vector2(0f, releaseOffset.Y * (1f - lerp));
                else
                    cameraPosition = Vector2.Lerp(setCameraPosition, Main.screenPosition, lerp);

                zoom = MathHelper.Lerp(setZoom, 0f, lerp);
            }

            Main.screenPosition = cameraPosition;
        }

        public override void PostUpdateEverything()
        {
            if (cameraModified && (!Main.LocalPlayer.dead || Main.gamePaused))
            {
                ResetTimer = 0;
                releasingCamera = false;
                releaseRequested = false;
            }
            else
                ResetTimer++;

            cameraModified = false;
        }

        public override void ModifyTransformMatrix(ref SpriteViewMatrix transform)
        {
            transform.Zoom *= 1f + Zoom;
        }

        public static void StartScreenShake(Vector2 startPosition, Vector2 direction, float strength, float vibrationCyclesPerSecond, int frames, float distanceFalloff = -1f, string uniqueIdentity = null)
        {
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(startPosition, direction, strength, vibrationCyclesPerSecond, frames, distanceFalloff, uniqueIdentity));
        }

        public static void InterpolateCamera(Vector2 goalCenter, float interpolant) => CameraCenter = Vector2.Lerp(oldCameraPosition + Main.ScreenSize.ToVector2() * 0.5f, goalCenter, MathHelper.Clamp(interpolant, 0f, 1f));

        public static void InterpolateCamera(Vector2 goalCenter, float xInterpolant, float yInterpolant)
        {
            Vector2 startCenter = oldCameraPosition + Main.ScreenSize.ToVector2() * 0.5f;

            float newX = MathHelper.Lerp(startCenter.X, goalCenter.X, MathHelper.Clamp(xInterpolant, 0f, 1f));
            float newY = MathHelper.Lerp(startCenter.Y, goalCenter.Y, MathHelper.Clamp(yInterpolant, 0f, 1f));

            CameraCenter = new Vector2(newX, newY);
        }

        public static void ResetCamera()
        {
            ResetTimer = 0;
            releaseRequested = true;
        }
    }
}
