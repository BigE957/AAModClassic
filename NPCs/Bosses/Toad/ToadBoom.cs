using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Toad
{
    public class ToadBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boom");     
            Main.projFrames[Projectile.type] = 7;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.alpha -= 10;
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Shroomed_Buff>(), 600);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
