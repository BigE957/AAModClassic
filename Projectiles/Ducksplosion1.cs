using AAModClassic.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Ducksplosion1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dayfire");     
            Main.projFrames[Projectile.type] = 5;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 176;
            Projectile.height = 230;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 9)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 5)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Electrified>(), 200);
        }


    }
}
