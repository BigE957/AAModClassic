using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class AshRain : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
            if (!Main.dayTime && !AAWorld.downedAkuma)
            {
                dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
                dust.velocity.X *= 0.3f;
                dust.scale *= 1.3f;
                dust.noGravity = false;
            }
		}

		public override bool MidUpdate(Dust dust)
        {
            if (!Main.dayTime && !AAWorld.downedAkuma)
            {
                AAPlayer.Ashes = 0;
                if (!dust.noGravity)
                {
                    dust.velocity.Y += 0.2f;
                }
                if (!dust.noLight)
                {
                    float strength = dust.scale * 1.4f;
                    if (strength > 1f)
                    {
                        strength = 1f;
                    }
                    Lighting.AddLight(dust.position, 0.5f * strength, 0.2f * strength, 0.2f * strength);
                }
                AAPlayer.Ashes++;
                dust.scale += 0.009f;
                float y = Main.LocalPlayer.velocity.Y;
                if (y > 0f && dust.fadeIn == 0f && dust.velocity.Y < y)
                {
                    dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y, 0.04f);
                }
                if (!dust.noLight && y > 0f)
                {
                    Dust expr_3604_cp_0 = dust;
                    expr_3604_cp_0.position.Y += Main.LocalPlayer.velocity.Y * 0.2f;
                }
                if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                {
                    dust.scale *= 0.9f;
                    dust.velocity *= 0.25f;
                }
            }
            return false;
		}

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(190, 30, 30, 25);
        }
    }
}