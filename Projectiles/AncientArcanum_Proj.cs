using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class AncientArcanum_Proj : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Quazar");
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 60;
			Projectile.height = 60;
			Projectile.friendly = true;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.hide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.tileCollide = false;
        }

        public override void AI()
        {
			Vector2 velocity = Projectile.velocity;
			if (Projectile.velocity.X != velocity.X)
			{
				Projectile.velocity.X = -velocity.X * 0.35f;
			}
			if (Projectile.velocity.Y != velocity.Y)
			{
				Projectile.velocity.Y = -velocity.Y * 0.35f;
			}
			float[] var_2_2DDF8_cp_0 = Projectile.ai;
			int var_2_2DDF8_cp_1 = 0;
			float num73 = var_2_2DDF8_cp_0[var_2_2DDF8_cp_1];
			var_2_2DDF8_cp_0[var_2_2DDF8_cp_1] = num73 + 1f;
			int num1013 = 0;
			if (Projectile.velocity.Length() <= 4f)
			{
				num1013 = 1;
			}
			Projectile.alpha -= 15;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (num1013 == 0)
			{
				Projectile.rotation -= 0.104719758f;
				if (Main.rand.NextBool(3))
				{
					if (Main.rand.NextBool(2))
					{
						Vector2 vector140 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust28 = Main.dust[Dust.NewDust(Projectile.Center - vector140 * 30f, 0, 0, Utils.SelectRandom(Main.rand, new int[]
						{
							88,
							92
						}), 0f, 0f, 0, default, 1f)];
						dust28.noGravity = true;
						dust28.position = Projectile.Center - vector140 * Main.rand.Next(10, 21);
						dust28.velocity = vector140.RotatedBy(1.5707963705062866, default) * 6f;
						dust28.scale = 0.5f + Main.rand.NextFloat();
						dust28.fadeIn = 0.5f;
						dust28.customData = Projectile;
					}
					else
					{
						Vector2 vector141 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust29 = Main.dust[Dust.NewDust(Projectile.Center - vector141 * 30f, 0, 0, DustID.Marble, 0f, 0f, 0, default, 1f)];
						dust29.noGravity = true;
						dust29.position = Projectile.Center - vector141 * 30f;
						dust29.velocity = vector141.RotatedBy(-1.5707963705062866, default) * 3f;
						dust29.scale = 0.5f + Main.rand.NextFloat();
						dust29.fadeIn = 0.5f;
						dust29.customData = Projectile;
					}
				}
				if (Projectile.ai[0] >= 30f)
				{
					Projectile.velocity *= 0.98f;
					Projectile.scale += 0.00744680827f;
					if (Projectile.scale > 1.3f)
					{
						Projectile.scale = 1.3f;
					}
					Projectile.rotation -= 0.0174532924f;
				}
				if (Projectile.velocity.Length() < 4.1f)
				{
					Projectile.velocity.Normalize();
					Projectile.velocity *= 4f;
					Projectile.ai[0] = 0f;
				}
			}
			else if (num1013 == 1)
			{
				Projectile.rotation -= 0.104719758f;
				int num3;
				for (int num1014 = 0; num1014 < 1; num1014 = num3 + 1)
				{
					if (Main.rand.NextBool(2))
					{
						Vector2 vector142 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust30 = Main.dust[Dust.NewDust(Projectile.Center - vector142 * 30f, 0, 0, DustID.GemSapphire, 0f, 0f, 0, default, 1f)];
						dust30.noGravity = true;
						dust30.position = Projectile.Center - vector142 * Main.rand.Next(10, 21);
						dust30.velocity = vector142.RotatedBy(1.5707963705062866, default) * 6f;
						dust30.scale = 0.9f + Main.rand.NextFloat();
						dust30.fadeIn = 0.5f;
						dust30.customData = Projectile;
						vector142 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						dust30 = Main.dust[Dust.NewDust(Projectile.Center - vector142 * 30f, 0, 0, DustID.Frost, 0f, 0f, 0, default, 1f)];
						dust30.noGravity = true;
						dust30.position = Projectile.Center - vector142 * Main.rand.Next(10, 21);
						dust30.velocity = vector142.RotatedBy(1.5707963705062866, default) * 6f;
						dust30.scale = 0.9f + Main.rand.NextFloat();
						dust30.fadeIn = 0.5f;
						dust30.customData = Projectile;
						dust30.color = Color.Crimson;
					}
					else
					{
						Vector2 vector143 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust31 = Main.dust[Dust.NewDust(Projectile.Center - vector143 * 30f, 0, 0, DustID.Marble, 0f, 0f, 0, default, 1f)];
						dust31.noGravity = true;
						dust31.position = Projectile.Center - vector143 * Main.rand.Next(20, 31);
						dust31.velocity = vector143.RotatedBy(-1.5707963705062866, default) * 5f;
						dust31.scale = 0.9f + Main.rand.NextFloat();
						dust31.fadeIn = 0.5f;
						dust31.customData = Projectile;
					}
					num3 = num1014;
				}
				if (Projectile.ai[0] % 30f == 0f && Projectile.ai[0] < 241f && Main.myPlayer == Projectile.owner)
				{
					Vector2 vector144 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * 12f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector144.X, vector144.Y, ProjectileID.NebulaArcanumSubshot, Projectile.damage / 2, 0f, Projectile.owner, 0f, Projectile.whoAmI);
				}
				Vector2 vector145 = Projectile.Center;
				float num1015 = 800f;
				bool flag59 = false;
				int num1016 = 0;
				if (Projectile.ai[1] == 0f)
				{
					for (int num1017 = 0; num1017 < 200; num1017 = num3 + 1)
					{
						if (Main.npc[num1017].CanBeChasedBy(Projectile, false))
						{
							Vector2 center13 = Main.npc[num1017].Center;
							if (Projectile.Distance(center13) < num1015 && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[num1017].position, Main.npc[num1017].width, Main.npc[num1017].height))
							{
								num1015 = Projectile.Distance(center13);
								vector145 = center13;
								flag59 = true;
								num1016 = num1017;
							}
						}
						num3 = num1017;
					}
					if (flag59)
					{
						if (Projectile.ai[1] != num1016 + 1)
						{
							Projectile.netUpdate = true;
						}
						Projectile.ai[1] = num1016 + 1;
					}
					flag59 = false;
				}
				if (Projectile.ai[1] != 0f)
				{
					int num1018 = (int)(Projectile.ai[1] - 1f);
					if (Main.npc[num1018].active && Main.npc[num1018].CanBeChasedBy(Projectile, true) && Projectile.Distance(Main.npc[num1018].Center) < 1000f)
					{
						flag59 = true;
						vector145 = Main.npc[num1018].Center;
					}
				}
				if (!Projectile.friendly)
				{
					flag59 = false;
				}
				if (flag59)
				{
					float num1019 = 4f;
					int num1020 = 8;
					Vector2 vector146 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
					float num1021 = vector145.X - vector146.X;
					float num1022 = vector145.Y - vector146.Y;
					float num1023 = (float)Math.Sqrt(num1021 * num1021 + num1022 * num1022);
					num1023 = num1019 / num1023;
					num1021 *= num1023;
					num1022 *= num1023;
					Projectile.velocity.X = (Projectile.velocity.X * (num1020 - 1) + num1021) / num1020;
					Projectile.velocity.Y = (Projectile.velocity.Y * (num1020 - 1) + num1022) / num1020;
				}
			}
			if (Projectile.alpha < 150)
			{
				Lighting.AddLight(Projectile.Center, 0.7f, 0.2f, 0.6f);
			}
			if (Projectile.ai[0] >= 600f)
			{
				Projectile.Kill();
				return;
			}
        }

		public override Color? GetAlpha(Color newColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
		}

        public override void OnKill(int timeLeft)
        {
            Projectile.position = Projectile.Center;
			Projectile.width = Projectile.height = 176;
			Projectile.Center = Projectile.position;
			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;
			Projectile.Damage();
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			int num3;
			for (int num95 = 0; num95 < 4; num95 = num3 + 1)
			{
				int num96 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Marble, 0f, 0f, 100, default, 1.5f);
				Main.dust[num96].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				num3 = num95;
			}
			for (int num97 = 0; num97 < 30; num97 = num3 + 1)
			{
				int num98 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 200, default, 3.7f);
				Main.dust[num98].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				Main.dust[num98].noGravity = true;
				Dust dust = Main.dust[num98];
				dust.velocity *= 3f;
				num98 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Frost, 0f, 0f, 100, default, 1.5f);
				Main.dust[num98].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				dust = Main.dust[num98];
				dust.velocity *= 2f;
				Main.dust[num98].noGravity = true;
				Main.dust[num98].fadeIn = 1f;
				Main.dust[num98].color = Color.Crimson * 0.5f;
				num3 = num97;
			}
			for (int num99 = 0; num99 < 10; num99 = num3 + 1)
			{
				int num100 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 0, default, 2.7f);
				Main.dust[num100].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * Projectile.width / 2f;
				Main.dust[num100].noGravity = true;
				Dust dust = Main.dust[num100];
				dust.velocity *= 3f;
				num3 = num99;
			}
			for (int num101 = 0; num101 < 10; num101 = num3 + 1)
			{
				int num102 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Marble, 0f, 0f, 0, default, 1.5f);
				Main.dust[num102].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * Projectile.width / 2f;
				Main.dust[num102].noGravity = true;
				Dust dust = Main.dust[num102];
				dust.velocity *= 3f;
				num3 = num101;
			}
			for (int num103 = 0; num103 < 2; num103 = num3 + 1)
			{
				int num104 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position + new Vector2(Projectile.width * Main.rand.Next(100) / 100f, Projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default, Main.rand.Next(61, 64), 1f);
				Main.gore[num104].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				Gore gore = Main.gore[num104];
				gore.velocity *= 0.3f;
				Gore expr_3C56_cp_0_cp_0 = Main.gore[num104];
				expr_3C56_cp_0_cp_0.velocity.X += Main.rand.Next(-10, 11) * 0.05f;
				Gore expr_3C81_cp_0_cp_0 = Main.gore[num104];
				expr_3C81_cp_0_cp_0.velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
				num3 = num103;
			}
			if (Main.myPlayer == Projectile.owner)
			{
				for (int num105 = 0; num105 < 1000; num105 = num3 + 1)
				{
					if (Main.projectile[num105].active && Main.projectile[num105].type == ProjectileID.NebulaArcanumSubshot && Main.projectile[num105].ai[1] == Projectile.whoAmI)
					{
						Main.projectile[num105].Kill();
					}
					num3 = num105;
				}
				int num106 = Main.rand.Next(5, 9);
				int num107 = Main.rand.Next(5, 9);
				int num108 = Utils.SelectRandom(Main.rand, new int[]
				{
					86,
					90
				});
				int num109 = (num108 == 86) ? 90 : 86;
				for (int num110 = 0; num110 < num106; num110 = num3 + 1)
				{
					Vector2 vector4 = Projectile.Center + Utils.RandomVector2(Main.rand, -30f, 30f);
					Vector2 vector5 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					while (vector5.X == 0f && vector5.Y == 0f)
					{
						vector5 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					}
					vector5.Normalize();
					if (vector5.Y > 0.2f)
					{
						vector5.Y *= -1f;
					}
					vector5 *= Main.rand.Next(70, 101) * 0.1f;
					Projectile.NewProjectile(Projectile.GetSource_Death(), vector4.X, vector4.Y, vector5.X, vector5.Y, ProjectileID.NebulaArcanumExplosionShotShard, (int)(Projectile.damage * 0.65), Projectile.knockBack * 0.8f, Projectile.owner, num108, 0f);
					num3 = num110;
				}
				for (int num111 = 0; num111 < num107; num111 = num3 + 1)
				{
					Vector2 vector6 = Projectile.Center + Utils.RandomVector2(Main.rand, -30f, 30f);
					Vector2 vector7 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					while (vector7.X == 0f && vector7.Y == 0f)
					{
						vector7 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					}
					vector7.Normalize();
					if (vector7.Y > 0.4f)
					{
						vector7.Y *= -1f;
					}
					vector7 *= Main.rand.Next(40, 81) * 0.1f;
					Projectile.NewProjectile(Projectile.GetSource_Death(), vector6.X, vector6.Y, vector7.X, vector7.Y, ProjectileID.NebulaArcanumExplosionShotShard, (int)(Projectile.damage * 0.65), Projectile.knockBack * 0.8f, Projectile.owner, num109, 0f);
					num3 = num111;
				}
			}
        }

		public override void PostDraw(Color lightColor)
		{
			Vector2 vector53 = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
			Texture2D texture2D31 = TextureAssets.Projectile[Projectile.type].Value;
            Color color25 = new Color(250 - 5 * 10, 250 - 5 * 10, 250 - 5 * 10, 150 - 5 * 10);
			Color alpha4 = Projectile.GetAlpha(color25);
			SpriteEffects spriteEffects = SpriteEffects.None;
			Vector2 origin8 = new Vector2(texture2D31.Width, texture2D31.Height) / 2f;
            Color color57 = alpha4 * 0.8f;
			color57.A /= 2;
			Color color58 = Color.Lerp(alpha4, Color.Black, 0.5f);
			color58.A = alpha4.A;
			float num274 = 0.95f + (Projectile.rotation * 0.75f).ToRotationVector2().Y * 0.1f;
			color58 *= num274;
			float scale13 = 0.6f + Projectile.scale * 0.6f * num274;
			SpriteBatch arg_DA77_0 = Main.spriteBatch;
			Texture2D arg_DA77_1 = TextureAssets.Extra[ExtrasID.VortexBlack].Value;
			Vector2 arg_DA77_2 = vector53;
			Rectangle? sourceRectangle2 = null;
			arg_DA77_0.Draw(arg_DA77_1, arg_DA77_2, sourceRectangle2, color58, -Projectile.rotation + 0.35f, origin8, scale13, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
			SpriteBatch arg_DAC3_0 = Main.spriteBatch;
			Texture2D arg_DAC3_1 = TextureAssets.Extra[ExtrasID.VortexBlack].Value;
			Vector2 arg_DAC3_2 = vector53;
			sourceRectangle2 = null;
			arg_DAC3_0.Draw(arg_DAC3_1, arg_DAC3_2, sourceRectangle2, alpha4, -Projectile.rotation, origin8, Projectile.scale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
			SpriteBatch arg_DB13_0 = Main.spriteBatch;
			Texture2D arg_DB13_1 = texture2D31;
			Vector2 arg_DB13_2 = vector53;
			sourceRectangle2 = null;
			arg_DB13_0.Draw(arg_DB13_1, arg_DB13_2, sourceRectangle2, color57, -Projectile.rotation * 0.7f, origin8, Projectile.scale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
			SpriteBatch arg_DB72_0 = Main.spriteBatch;
			Texture2D arg_DB72_1 = TextureAssets.Extra[ExtrasID.VortexBlack].Value;
			Vector2 arg_DB72_2 = vector53;
			sourceRectangle2 = null;
			arg_DB72_0.Draw(arg_DB72_1, arg_DB72_2, sourceRectangle2, alpha4 * 0.8f, Projectile.rotation * 0.5f, origin8, Projectile.scale * 0.9f, spriteEffects, 0f);
			alpha4.A = 0;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.immune[Projectile.owner] = 1;
            Projectile.Kill();
        }
    }
}