using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.Projectiles
{
    public class Snowflake : ModProjectile
    {
        public override void SetDefaults()
        {
			Projectile.CloneDefaults(344);
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			AIType = ProjectileID.NorthPoleSnowflake;
			Main.projFrames[Projectile.type] = 3;
			Projectile.light = 1f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Axis Snowflake");
        }
		
		public override void OnKill(int timeLeft)
		{
			int num3;
			for (int num367 = 0; num367 < 3; num367 = num3 + 1)
			{
				int num368 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0);
				Main.dust[num368].noGravity = true;
				Main.dust[num368].scale = Projectile.scale;
				num3 = num367;
			}
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.immune[Projectile.owner] = 1;
        }
		
		public override Color? GetAlpha(Color newColor)
		{
			float num6 = 1f - Projectile.alpha / 255f;
			return new Color((int)(250f * num6), (int)(250f * num6), (int)(250f * num6), (int)(100f * num6));
		}
    }
}
