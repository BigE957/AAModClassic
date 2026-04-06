using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using AAModClassic.Dusts;

namespace AAModClassic.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    public class Star1 : ModProjectile
	{
        public override void SetDefaults()
		{
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.alpha = 30;
            Projectile.light = 0.2f;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Projectile.rotation += .1f;
            int stardust = ModContent.DustType<Dusts.StarDust>();
            int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, stardust, Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, stardust, Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId3].noGravity = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int n = 0; n < 5; n++)
            {
                float x = Projectile.position.X + Main.rand.Next(-400, 400);
                float y = Projectile.position.Y - Main.rand.Next(500, 800);
                Vector2 vector = new Vector2(x, y);
                float num13 = Projectile.position.X + (Projectile.width / 2) - vector.X;
                float num14 = Projectile.position.Y + (Projectile.height / 2) - vector.Y;
                num13 += Main.rand.Next(-100, 101);
                int num15 = 23;
                float num16 = (float)Math.Sqrt(num13 * num13 + num14 * num14);
                num16 = num15 / num16;
                num13 *= num16;
                num14 *= num16;
                int num17 = Projectile.NewProjectile(Projectile.GetSource_OnHit(target), x, y, num13, num14, ModContent.ProjectileType<Stars>(), 70, 5f, Main.myPlayer, 0f, 0f);
                Main.projectile[num17].ai[1] = Projectile.position.Y;
            }
        }

        public override void OnKill(int timeleft)
        {
            int stardust = ModContent.DustType<Dusts.StarDust>();
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, stardust, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, stardust, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
