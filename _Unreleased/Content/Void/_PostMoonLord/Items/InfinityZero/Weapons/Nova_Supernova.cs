using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Nova_Supernova : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Supernova");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 7;     //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 4;
            Projectile.scale *= .2f;
            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;
            Projectile.alpha = 0;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.alpha += 10;
            Projectile.scale += .2f;
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X = 0.00f;
            Projectile.velocity.Y -= .6f;

        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
