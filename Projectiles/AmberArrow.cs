using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class AmberArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Amber Arrow");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BoneArrow);
			Projectile.width = 14;
			Projectile.height = 18;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 600;
			AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.arrow = true;
        }


        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, DustID.Dirt, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}