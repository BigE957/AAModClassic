using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Equinox
{
    public class NightcrawlerNothing : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nightclawer Nothing");
            Main.projFrames[Projectile.type] = 5;
		}

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 200;
		}
        public override void AI()
		{
			Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), .37f, .8f, .89f);
			Projectile.ai[0] += 1f;
			int num123 = Player.FindClosest(Projectile.Center, 1, 1);
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] < 110f && Projectile.ai[1] > 30f)
			{
				float scaleFactor2 = Projectile.velocity.Length();
				Vector2 vector17 = Main.player[num123].Center - Projectile.Center;
				vector17.Normalize();
				vector17 *= scaleFactor2;
				Projectile.velocity = (Projectile.velocity * 24f + vector17) / 25f;
				Projectile.velocity.Normalize();
				Projectile.velocity *= scaleFactor2;
			}
			if (Projectile.velocity.Length() < 18f)
			{
				Projectile.velocity *= 1.02f;
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
				for (int num124 = 0; num124 < 10; num124++)
				{
					int num125 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<DarkmatterDust>(), Projectile.velocity.X, Projectile.velocity.Y, 100, Color.White, 2f);
					Main.dust[num125].noGravity = true;
					Main.dust[num125].velocity = Projectile.Center - Main.dust[num125].position;
					Main.dust[num125].velocity.Normalize();
					Main.dust[num125].velocity *= -5f;
					Main.dust[num125].velocity += Projectile.velocity / 2f;
				}
			}

			Projectile.frame++;

			if (Projectile.frame > 4)
			{
				Projectile.frame = 0;
			}
			if (Projectile.ai[0] < 0f)
			{
				for (int num155 = 0; num155 < 2; num155++)
				{
					int num156 = Dust.NewDust(new Vector2(Projectile.position.X + 4f, Projectile.position.Y + 4f), Projectile.width - 8, Projectile.height - 8, ModContent.DustType<DarkmatterDust>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
					Main.dust[num156].position -= Projectile.velocity;
					Main.dust[num156].noGravity = true;
					Dust expr_7ED9_cp_0 = Main.dust[num156];
					expr_7ED9_cp_0.velocity.X *= 0.3f;
					Dust expr_7EF7_cp_0 = Main.dust[num156];
					expr_7EF7_cp_0.velocity.Y *= 0.3f;
				}
			}
			else
			{
				for (int num157 = 0; num157 < 2; num157++)
				{
					int num158 = Dust.NewDust(new Vector2(Projectile.position.X + 4f, Projectile.position.Y + 4f), Projectile.width - 8, Projectile.height - 8, ModContent.DustType<DarkmatterDust>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2f);
					Main.dust[num158].position -= Projectile.velocity * 2f;
					Main.dust[num158].noGravity = true;
					Dust expr_7FDC_cp_0 = Main.dust[num158];
					expr_7FDC_cp_0.velocity.X *= 0.3f;
					Dust expr_7FFA_cp_0 = Main.dust[num158];
					expr_7FFA_cp_0.velocity.Y *= 0.3f;
				}
			}

			if (Projectile.ai[0] >= 15f)
			{
				Projectile.ai[0] = 15f;
				Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
			}

			Projectile.spriteDirection = Projectile.direction;
			if (Projectile.direction < 0)
			{
				Projectile.rotation = (float)Math.Atan2(-Projectile.velocity.Y, -Projectile.velocity.X);
			}
			else
			{
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
			}

			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;

			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return new Color(95, 205, 228, 200);
        }

        public override void OnKill(int timeLeft)
        {
            SpawnDust();
            Projectile.active = false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Obstructed, 60);
        }

        public void SpawnDust()
        {
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }
    }
}