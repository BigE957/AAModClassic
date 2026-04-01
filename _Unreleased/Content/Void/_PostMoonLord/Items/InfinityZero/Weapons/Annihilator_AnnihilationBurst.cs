using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Projectiles.Infinity
{
    public class AnhialationBurst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Singulatiy");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 4;     //The recording mode
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
            Projectile.localNPCHitCooldown = 6;
            Projectile.scale *= 1.3f;
            Projectile.alpha = 255;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Projectile.alpha -= 1;
            Projectile.scale -= .05f;
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;

                }
            }
            if (Projectile.scale <= 0)
            {
                Projectile.Kill();
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }
    }
}
