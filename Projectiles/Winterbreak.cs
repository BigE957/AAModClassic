using AAModClassic.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Winterbreak : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }

        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.SnowDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<SnowDustLight>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 45f)
                {
                    float num975 = 0.98f;
                    float num976 = 0.35f;
                    Projectile.ai[1] = 45f;
                    Projectile.velocity.X = Projectile.velocity.X * num975;
                    Projectile.velocity.Y = Projectile.velocity.Y + num976;
                }
                Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            }
        }
    }
}
