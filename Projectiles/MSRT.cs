using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MSRT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reality Tear");     
            Main.projFrames[Projectile.type] = 10;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 42;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 30;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 9)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

    }
}
