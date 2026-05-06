using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima
{
    public class RaiderUltima_RocketStrike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rocket Strike");     
            Main.projFrames[Projectile.type] = 7;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
            Projectile.scale *= 1.2f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150);
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 7)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
