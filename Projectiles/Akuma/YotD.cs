
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class YotD : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Year of the Dragon");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = ProjAIStyleID.FireWork;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }
        public override void AI()
        {
			
			float num1 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
			float num2 = Projectile.localAI[0];
			if (num2 == 0.0)
			{
				Projectile.localAI[0] = num1;
				num2 = num1;
			}
			float num3 = Projectile.position.X;
			float num4 = Projectile.position.Y;
			float num5 = 300f;
			bool flag2 = false;
			int num6 = 0;
			if (Projectile.ai[1] == 0.0)
			{
				for (int index = 0; index < 200; ++index)
				{
					if (Main.npc[index].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0.0 || Projectile.ai[1] == (double)(index + 1)))
					{
						float num7 = Main.npc[index].position.X + Main.npc[index].width / 2;
						float num8 = Main.npc[index].position.Y + Main.npc[index].height / 2;
						float num9 = Math.Abs(Projectile.position.X + (Projectile.width / 2) - num7) + Math.Abs(Projectile.position.Y + (Projectile.height / 2) - num8);
						if (num9 < num5 && Collision.CanHit(new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2)), 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height))
						{
							num5 = num9;
							num3 = num7;
							num4 = num8;
							flag2 = true;
							num6 = index;
						}
					}
				}
				if (flag2)
					Projectile.ai[1] = num6 + 1;
				flag2 = false;
			}
			if (Projectile.ai[1] > 0.0)
			{
				int index = (int)(Projectile.ai[1] - 1.0);
				if (Main.npc[index].active && Main.npc[index].CanBeChasedBy(this, true) && !Main.npc[index].dontTakeDamage)
				{
					if (Math.Abs(Projectile.position.X + (Projectile.width / 2) - (Main.npc[index].position.X + Main.npc[index].width / 2)) + Math.Abs(Projectile.position.Y + (Projectile.height / 2) - (Main.npc[index].position.Y + Main.npc[index].height / 2)) < 1000.0)
					{
						flag2 = true;
						num3 = Main.npc[index].position.X + Main.npc[index].width / 2;
						num4 = Main.npc[index].position.Y + Main.npc[index].height / 2;
					}
				}
				else
					Projectile.ai[1] = 0.0f;
			}
			if (!Projectile.friendly)
				flag2 = false;
			if (flag2)
			{
				float num7 = num2;
				Vector2 vector2 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
				float num8 = num3 - vector2.X;
				float num9 = num4 - vector2.Y;
				float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
				float num11 = num7 / num10;
				float num12 = num8 * num11;
				float num13 = num9 * num11;
				int num14 = 8;
				Projectile.velocity.X = (Projectile.velocity.X * (num14 - 1) + num12) / num14;
				Projectile.velocity.Y = (Projectile.velocity.Y * (num14 - 1) + num13) / num14;
			}			
			
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            if (Projectile.ai[1] == 1f)
            {
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] == 1f)
                {
                    for (int num352 = 0; num352 < 8; num352++)
                    {
                        int num353 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 1.8f);
                        Main.dust[num353].noGravity = true;
                        Main.dust[num353].velocity *= 3f;
                        Main.dust[num353].fadeIn = 0.5f;
                        Main.dust[num353].position += Projectile.velocity / 2f;
                        Main.dust[num353].velocity += Projectile.velocity / 4f + Main.player[Projectile.owner].velocity * 0.1f;
                    }
                }
                if (Projectile.ai[0] > 2f)
                {
                    int num354 = Dust.NewDust(new Vector2(Projectile.position.X + 2f, Projectile.position.Y + 20f), 8, 8, ModContent.DustType<Dusts.AkumaDust>(), Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(Projectile.rotation, Projectile.Center);
                    num354 = Dust.NewDust(new Vector2(Projectile.position.X + 2f, Projectile.position.Y + 15f), 8, 8, ModContent.DustType<Dusts.AkumaDust>(), Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(Projectile.rotation, Projectile.Center);
                    num354 = Dust.NewDust(new Vector2(Projectile.position.X + 2f, Projectile.position.Y + 10f), 8, 8, ModContent.DustType<Dusts.AkumaDust>(), Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(Projectile.rotation, Projectile.Center);
                    return;
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] > 4f)
                {
                    int num356 = Dust.NewDust(new Vector2(Projectile.position.X + 2f, Projectile.position.Y + 20f), 8, 8, ModContent.DustType<Dusts.AkumaADust>(), Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num356].noGravity = true;
                    Main.dust[num356].velocity *= 0.2f;
                    Main.dust[num356].position = Main.dust[num356].position.RotatedBy(Projectile.rotation, Projectile.Center);
                    return;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 vector13 = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2();
            float num653 = Main.rand.Next(5, 9);
            float num654 = Main.rand.Next(12, 17);
            float value26 = Main.rand.Next(3, 7);
            float num655 = 20f;
            for (float num656 = 0f; num656 < num653; num656 += 1f)
            {
                for (int num657 = 0; num657 < 2; num657++)
                {
                    Vector2 value27 = vector13.RotatedBy(((num657 == 0) ? 1f : -1f) * 6.28318548f / (num653 * 2f));
                    for (float num658 = 0f; num658 < num655; num658 += 1f)
                    {
                        Vector2 value28 = Vector2.Lerp(vector13, value27, num658 / num655);
                        float scaleFactor2 = MathHelper.Lerp(num654, value26, num658 / num655);
                        int num659 = Dust.NewDust(Projectile.Center, 6, 6, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 1.3f);
                        Main.dust[num659].velocity *= 0.1f;
                        Main.dust[num659].noGravity = true;
                        Main.dust[num659].velocity += value28 * scaleFactor2;
                    }
                }
                vector13 = vector13.RotatedBy(6.28318548f / num653);
            }
            for (float num660 = 0f; num660 < num653; num660 += 1f)
            {
                for (int num661 = 0; num661 < 2; num661++)
                {
                    Vector2 value29 = vector13.RotatedBy(((num661 == 0) ? 1f : -1f) * 6.28318548f / (num653 * 2f));
                    for (float num662 = 0f; num662 < num655; num662 += 1f)
                    {
                        Vector2 value30 = Vector2.Lerp(vector13, value29, num662 / num655);
                        float scaleFactor3 = MathHelper.Lerp(num654, value26, num662 / num655) / 2f;
                        int num663 = Dust.NewDust(Projectile.Center, 6, 6, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 1.3f);
                        Main.dust[num663].velocity *= 0.1f;
                        Main.dust[num663].noGravity = true;
                        Main.dust[num663].velocity += value30 * scaleFactor3;
                    }
                }
                vector13 = vector13.RotatedBy(6.28318548f / num653);
            }
            for (int num664 = 0; num664 < 100; num664++)
            {
                float num665 = num654;
                int num667 = Dust.NewDust(Projectile.Center, 6, 6, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
                float num668 = Main.dust[num667].velocity.X;
                float num669 = Main.dust[num667].velocity.Y;
                if (num668 == 0f && num669 == 0f)
                {
                    num668 = 1f;
                }
                float num670 = (float)Math.Sqrt(num668 * num668 + num669 * num669);
                num670 = num665 / num670;
                num668 *= num670;
                num669 *= num670;
                Main.dust[num667].velocity *= 0.5f;
                Dust expr_15A4E_cp_0 = Main.dust[num667];
                expr_15A4E_cp_0.velocity.X += num668;
                Dust expr_15A6D_cp_0 = Main.dust[num667];
                expr_15A6D_cp_0.velocity.Y += num669;
                Main.dust[num667].scale = 1.3f;
                Main.dust[num667].noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, false);
            return false;
        }
    }
}
