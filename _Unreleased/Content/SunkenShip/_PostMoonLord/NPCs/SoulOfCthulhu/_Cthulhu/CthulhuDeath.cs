using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu
{
    public class CthulhuDeath : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reality Explosion");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 7;     //The recording mode
        }

        public override Color? GetAlpha(Color lightColor) => AAColor.Cthulhu2;

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
        }

        public override void AI()
        {

            Projectile.alpha += 5;
            Projectile.scale += .1f;
            if (++Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X = 0.00f;
            Projectile.velocity.Y -= .4f;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
