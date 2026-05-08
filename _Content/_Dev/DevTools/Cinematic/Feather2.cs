using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.DevTools.Cinematic
{
    public class Feather2 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 24, 26);
        }

        public override bool Update(Dust dust)
        {
            if (Collision.SolidCollision(dust.position, 16, 16) && dust.fadeIn == 0f)
            {
                dust.velocity *= 0.10f;
            }
            else
            {
                int p = BaseAI.GetPlayer(dust.position, -1);
                Player player = Main.player[p];
                Vector2 target = player.Center;
                float moveIntervalX = 0.1f, moveIntervalY = 0.02f, maxSpeedX = 5f;

                dust.position += dust.velocity;

                dust.velocity.Y += moveIntervalY;
                if (dust.velocity.Y < 0f) dust.velocity.Y *= 0.99f;
                if (dust.velocity.Y > 1f) dust.velocity.Y = 1f;
                if (dust.position.X + 24 < target.X - 8)
                {
                    if (dust.velocity.X < 0) dust.velocity.X *= 0.98f;
                    dust.velocity.X += moveIntervalX;
                }
                else if (dust.position.X > target.X + 8)
                {
                    if (dust.velocity.X > 0) dust.velocity.X *= 0.98f;
                    dust.velocity.X -= moveIntervalX;
                }
                if (dust.velocity.X > maxSpeedX || dust.velocity.X < -maxSpeedX) dust.velocity.X *= 0.97f;
                dust.rotation = dust.velocity.X * 0.05f;
            }

            return false;
        }
    }
}