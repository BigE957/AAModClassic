using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class FreedomBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Freedom Plasma Ball");
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 130;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            Projectile.alpha += 10;
            Projectile.scale += 0.3f;
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}
