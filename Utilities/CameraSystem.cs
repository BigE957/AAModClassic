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
        private static Vector2 cameraPosition = Main.screenPosition;
        private static Vector2 setCameraPosition = Main.screenPosition;
        private static bool cameraModified = false;
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

        public override void ModifyScreenPosition()
        {
            if (ResetTimer > 30)
                return;

            if ((Main.LocalPlayer.dead && !Main.gamePaused) || !cameraModified)
            {
                float lerp = MathUtils.SineInOutEasing(ResetTimer / 30f);
                cameraPosition = Vector2.Lerp(setCameraPosition, Main.screenPosition, lerp);
                zoom = MathHelper.Lerp(setZoom, 0f, lerp);
            }

            Main.screenPosition = cameraPosition;
        }

        public override void PostUpdateEverything()
        {
            if (cameraModified && (!Main.LocalPlayer.dead || Main.gamePaused))
                ResetTimer = 0;
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

        public static void InterpolateCamera(Vector2 goalCenter, float interpolant) => CameraCenter = Vector2.Lerp(Main.screenPosition + Main.ScreenSize.ToVector2() * 0.5f, goalCenter, MathHelper.Clamp(interpolant, 0f, 1f));
    }
}
