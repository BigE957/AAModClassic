using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
    public class CthulhuCannon_CthulhuBomb : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhu Bomb");
            Main.projFrames[Projectile.type] = 6;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.aiStyle = 1;
            
		}

        public override void AI()
        {
            if (++Projectile.frameCounter >= 12)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.frame = 0;

                }
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2((double)-(double)Projectile.velocity.Y, (double)-(double)Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 55;
                Projectile.scale = 1.3f;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                    float num109 = 4f;
                    int num110 = 0;
                    while (num110 < num109)
                    {
                        Vector2 vector14 = Vector2.UnitX * 0f;
                        vector14 += -Vector2.UnitY.RotatedBy((double)(num110 * (6.28318548f / num109)), default) * new Vector2(1f, 4f);
                        vector14 = vector14.RotatedBy((double)Projectile.velocity.ToRotation(), default);
                        int num111 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<CthulhuDust>(), 0f, 0f, 0, default, 1f);
                        Main.dust[num111].scale = 1.5f;
                        Main.dust[num111].noLight = true;
                        Main.dust[num111].noGravity = true;
                        Main.dust[num111].position = Projectile.Center + vector14;
                        Main.dust[num111].velocity = Main.dust[num111].velocity * 4f + Projectile.velocity * 0.3f;
                        num110++;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 160;
            Projectile.Center = Projectile.position;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.Damage();
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<CthulhuCannon_CthulhuBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Vector2 position = Projectile.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 4; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 5; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<CthulhuDust>(), 0f, 0f, 200, default, 3.7f);
                Main.dust[num88].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num88].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Crimson * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 5; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<CthulhuDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)Projectile.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += Projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 70; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)Projectile.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += Projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }
    }
}
