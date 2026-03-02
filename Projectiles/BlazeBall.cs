using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class BlazeBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blaze Ball");
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.DD2FlameBurstShot;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            int num = 1;
            if (Projectile.type == ProjectileID.DD2FlameBurstTowerT2Shot)
            {
                num = 2;
            }
            if (Projectile.type == ProjectileID.DD2FlameBurstTowerT3Shot)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                if (Main.rand.Next(2) != 0)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2f);
                    dust.noGravity = true;
                    dust.velocity *= 0.3f;
                    if (Main.rand.Next(1) == 0)
                    {
                        Dust expr_131_cp_0 = dust;
                        expr_131_cp_0.velocity.Y += Math.Sign(dust.velocity.Y) * 1.2f;
                        dust.fadeIn += 0.5f;
                    }
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 600);
        }

        public override void OnKill(int timeleft)
        {
            int num20 = 4;
            int num21 = 20;
            int num22 = 10;
            int num23 = 20;
            int num24 = 20;
            int num25 = 4;
            float num26 = 1.5f;
            int num27 = 6;
            int num28 = 6;
            if (Main.player[Projectile.owner].setApprenticeT3)
            {
                num20 += 4;
                num24 += 10;
                num21 += 20;
                num23 += 30;
                num22 /= 2;
                num25 += 4;
                num26 += 0.5f;
                num27 += 7;
                num28 = 270;
            }
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 16 * num27;
            Projectile.Center = Projectile.position;
            Projectile.Damage();
            SoundEngine.PlaySound(SoundID.Item100, Projectile.position);
            for (int num29 = 0; num29 < num20; num29++)
            {
                int num30 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                Main.dust[num30].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
            }
            for (int num31 = 0; num31 < num21; num31++)
            {
                Dust dust5 = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 200, default, 1f);
                dust5.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 10f;
                dust5.velocity *= 16f;
                if (dust5.velocity.Y > -2f)
                {
                    Dust expr_FA2_cp_0 = dust5;
                    expr_FA2_cp_0.velocity.Y *= -0.4f;
                }
                dust5.noLight = true;
                dust5.noGravity = true;
            }
            for (int num32 = 0; num32 < num23; num32++)
            {
                Dust dust6 = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num28, 0f, 0f, 100, default, 1f);
                dust6.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
                dust6.velocity *= 2f;
                dust6.noGravity = true;
                dust6.fadeIn = num26;
            }
            for (int num33 = 0; num33 < num22; num33++)
            {
                int num34 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 1f);
                Main.dust[num34].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * Projectile.width / 2f;
                Main.dust[num34].noGravity = true;
                Main.dust[num34].velocity *= 3f;
            }
            for (int num35 = 0; num35 < num24; num35++)
            {
                int num36 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, default, 1f);
                Main.dust[num36].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * Projectile.width / 2f;
                Main.dust[num36].noGravity = true;
                Main.dust[num36].velocity *= 3f;
            }
            for (int num37 = 0; num37 < num25; num37++)
            {
                int num38 = Gore.NewGore(Projectile.position + new Vector2(Projectile.width * Main.rand.Next(100) / 100f, Projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default, Main.rand.Next(61, 64), 1f);
                Main.gore[num38].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
                Main.gore[num38].position -= Vector2.One * 16f;
                if (Main.rand.Next(2) == 0)
                {
                    Gore expr_13A4_cp_0 = Main.gore[num38];
                    expr_13A4_cp_0.position.Y -= 30f;
                }
                Main.gore[num38].velocity *= 0.3f;
                Gore expr_13DF_cp_0 = Main.gore[num38];
                expr_13DF_cp_0.velocity.X += Main.rand.Next(-10, 11) * 0.05f;
                Gore expr_140D_cp_0 = Main.gore[num38];
                expr_140D_cp_0.velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
            }
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y - 51, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("BlazeBlast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
