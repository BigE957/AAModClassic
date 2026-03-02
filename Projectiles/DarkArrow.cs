using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DarkArrow : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Arrow");
		}

		public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.UnholyArrow);
			AIType = ProjectileID.UnholyArrow;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, DustID.Electric, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}
