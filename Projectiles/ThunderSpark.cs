using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class ThunderSpark : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Thunder Spark");
            Main.projFrames[Projectile.type] = 4;

		}

		public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
			Projectile.width = 14;
			Projectile.height = 18;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
        }

        public override void PostAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }
    }
}