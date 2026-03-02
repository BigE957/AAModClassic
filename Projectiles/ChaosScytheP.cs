using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosScytheP : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("CHAOS CHAOS");
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.alpha = 254;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public bool CHAOSCHAOS = false;

        public override void AI()
        {
            if (CHAOSCHAOS == false && Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
            if (CHAOSCHAOS == false && Projectile.alpha <= 0)
            {
                Projectile.alpha = 0;
                CHAOSCHAOS = true;
            }
            if (CHAOSCHAOS == true && Projectile.alpha < 255)
            {
                Projectile.alpha += 5;
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
            }
            Projectile.rotation += Projectile.direction * 0.2f;
        }
    }
}