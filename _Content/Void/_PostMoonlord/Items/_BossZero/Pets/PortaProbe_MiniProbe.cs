using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Pets
{
    /// <summary>
    /// ALPHA Projectile IS NOT AN ITEM
    /// </summary>
	// Porting Note: Lol??
    public class PortaProbe_MiniProbe : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Mini Probe"); // Automatic from .lang files
            Main.projFrames[Projectile.type] = 6;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EyeOfCthulhuPet);
            AIType = ProjectileID.None;// ProjectileID.EyeOfCthulhuPet;
            Projectile.width = Projectile.height = 14;

        }

        public override bool PreAI()
        {
            //Player player = Main.player[Projectile.owner];
            //player.petFlagEyeOfCthulhuPet = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            if (player.dead)
            {
                modPlayer.MiniProbe = false;
                Projectile.active = false;
                return;
            }
            if (modPlayer.MiniProbe)
            {
                Projectile.timeLeft = 2;
            }

            Projectile.direction = Projectile.spriteDirection = player.direction;
            Vector3 v3_ = new Vector3(0.5f, 0.9f, 1f) * 2f;
            DelegateMethods.v3_1 = v3_;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 6f, 20f, DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(Projectile.Left, Projectile.Right, 20f, DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(player.Center, player.Center + player.velocity * 6f, 40f, DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(player.Left, player.Right, 40f, DelegateMethods.CastLightOpen);

            float num964 = 30f;
            float y7 = -20f;
            int num965 = player.direction;
            if (player.ownedProjectileCounts[650] > 0)
            {
                num965 *= -1;
            }

            Vector2 vector158 = new Vector2((float)num965 * num964, y7);
            Vector2 vector159 = player.MountedCenter + vector158;
            float num966 = Vector2.Distance(Projectile.Center, vector159);
            if (num966 > 1000f)
            {
                Projectile.Center = player.Center + vector158;
            }
            Vector2 vector160 = vector159 - Projectile.Center;
            float num967 = 4f;
            if (num966 < num967)
            {
                Projectile.velocity *= 0.25f;
            }
            if (vector160 != Vector2.Zero)
            {
                if (vector160.Length() < num967)
                {
                    Projectile.velocity = vector160;
                }
                else
                {
                    Projectile.velocity = vector160 * 0.1f;
                }
            }

            if (Projectile.velocity.Length() > 6f)
            {
                float num968 = Projectile.velocity.ToRotation() + MathHelper.Pi / 2f;
                if (Math.Abs(Projectile.rotation - num968) >= MathHelper.Pi)
                {
                    if (num968 < Projectile.rotation)
                    {
                        Projectile.rotation -= MathHelper.Pi * 2f;
                    }
                    else
                    {
                        Projectile.rotation += MathHelper.Pi * 2f;
                    }
                }
                float num969 = 12f;
                Projectile.rotation = (Projectile.rotation * (num969 - 1f) + num968) / num969;
                if (++Projectile.frameCounter >= 4)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= Main.projFrames[Type])
                    {
                        Projectile.frame = 0;
                    }
                }
            }
            else
            {
                if (Projectile.rotation > MathHelper.Pi)
                {
                    Projectile.rotation -= MathHelper.Pi * 2f;
                }
                if (Projectile.rotation > -0.005f && Projectile.rotation < 0.005f)
                {
                    Projectile.rotation = 0f;
                }
                else
                {
                    Projectile.rotation *= 0.96f;
                }
                if (++Projectile.frameCounter >= 6)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= Main.projFrames[Type])
                    {
                        Projectile.frame = 0;
                    }
                }
            }

            if (Projectile.ai[0] > 0f && (Projectile.ai[0] += 1f) >= 60f)
            {
                Projectile.ai[0] = 0f;
                Projectile.ai[1] = 0f;
            }

            if (Main.rand.NextBool(15))
            {
                int num988 = -1;
                int num989 = -1;
                float num990 = -1f;
                int num991 = 17;
                if ((Projectile.Center - player.Center).Length() < (float)Main.screenWidth)
                {
                    int num992 = (int)Projectile.Center.X / 16;
                    int num993 = (int)Projectile.Center.Y / 16;
                    num992 = (int)MathHelper.Clamp(num992, num991 + 1, Main.maxTilesX - num991 - 1);
                    num993 = (int)MathHelper.Clamp(num993, num991 + 1, Main.maxTilesY - num991 - 1);
                    for (int tX = num992 - num991; tX <= num992 + num991; tX++)
                    {
                        for (int tY = num993 - num991; tY <= num993 + num991; tY++)
                        {
                            int rand = Main.rand.Next(8);
                            if (rand < 4 && new Vector2(num992 - tX, num993 - tY).Length() < (float)num991 && Main.tile[tX, tY] != null && Main.tile[tX, tY].HasTile && Main.IsTileSpelunkable(tX, tY))
                            {
                                float num997 = Projectile.Distance(new Vector2(tX * 16 + 8, tY * 16 + 8));
                                if (num997 < num990 || num990 == -1f)
                                {
                                    num990 = num997;
                                    num988 = tX;
                                    num989 = tY;
                                    Projectile.ai[0] = 1f;
                                    Projectile.ai[1] = Projectile.AngleTo(new Vector2(tX * 16 + 8, tY * 16 + 8));
                                }
                                if (rand < 2)
                                {
                                    int num998 = Dust.NewDust(new Vector2(tX * 16, tY * 16), 16, 16, DustID.TreasureSparkle, 0f, 0f, 150, default(Color), 0.3f);
                                    Main.dust[num998].fadeIn = 0.75f;
                                    Dust dust2 = Main.dust[num998];
                                    dust2.velocity *= 0.1f;
                                }
                            }
                        }
                    }
                }
            }

            float f3 = Projectile.localAI[0] % ((float)Math.PI * 2f) - (float)Math.PI;
            float num999 = (float)Math.IEEERemainder(Projectile.localAI[1], 1.0);
            if (num999 < 0f)
            {
                num999 += 1f;
            }
            float num1000 = (float)Math.Floor(Projectile.localAI[1]);
            float max = 0.999f;
            float num1001 = 0f;
            int num1002 = 0;
            float amount2 = 0.1f;
            bool flag67 = player.velocity.Length() > 3f;
            int num1003 = -1;
            int num1004 = -1;
            float num1005 = 300f;
            float num1006 = 500f;
            for (int num1007 = 0; num1007 < 200; num1007++)
            {
                NPC nPC16 = Main.npc[num1007];
                if (!nPC16.active || !nPC16.chaseable || nPC16.dontTakeDamage || nPC16.immortal)
                {
                    continue;
                }
                float num1008 = Projectile.Distance(nPC16.Center);
                if (nPC16.friendly || nPC16.lifeMax <= 5)
                {
                    if (num1008 < num1005 && !flag67)
                    {
                        num1005 = num1008;
                        num1004 = num1007;
                    }
                }
                else if (num1008 < num1006)
                {
                    num1006 = num1008;
                    num1003 = num1007;
                }
            }
            if (flag67)
            {
                num1001 = Projectile.AngleTo(Projectile.Center + player.velocity);
                num1002 = 1;
                num999 = MathHelper.Clamp(num999 + 0.05f, 0f, max);
                num1000 += (float)Math.Sign(-10f - num1000);
            }
            else if (num1003 != -1)
            {
                num1001 = Projectile.AngleTo(Main.npc[num1003].Center);
                num1002 = 2;
                num999 = MathHelper.Clamp(num999 + 0.05f, 0f, max);
                num1000 += (float)Math.Sign(-12f - num1000);
            }
            else if (num1004 != -1)
            {
                num1001 = Projectile.AngleTo(Main.npc[num1004].Center);
                num1002 = 3;
                num999 = MathHelper.Clamp(num999 + 0.05f, 0f, max);
                num1000 += (float)Math.Sign(6f - num1000);
            }
            else if (Projectile.ai[0] > 0f)
            {
                num1001 = Projectile.ai[1];
                num999 = MathHelper.Clamp(num999 + (float)Math.Sign(0.75f - num999) * 0.05f, 0f, max);
                num1002 = 4;
                num1000 += (float)Math.Sign(10f - num1000);
                if (Main.rand.Next(10) == 0)
                {
                    int num1009 = Dust.NewDust(Projectile.Center + f3.ToRotationVector2() * 6f * num999 - Vector2.One * 4f, 8, 8, DustID.TreasureSparkle, 0f, 0f, 150, default(Color), 0.3f);
                    Main.dust[num1009].fadeIn = 0.75f;
                    Dust dust2 = Main.dust[num1009];
                    dust2.velocity *= 0.1f;
                }
            }
            else
            {
                num1001 = ((player.direction == 1) ? 0f : 3.1416028f);
                num999 = MathHelper.Clamp(num999 + (float)Math.Sign(0.75f - num999) * 0.05f, 0f, max);
                num1000 += (float)Math.Sign(0f - num1000);
                amount2 = 0.12f;
            }
            Vector2 value19 = num1001.ToRotationVector2();
            num1001 = Vector2.Lerp(f3.ToRotationVector2(), value19, amount2).ToRotation();
            Projectile.localAI[0] = num1001 + (float)num1002 * ((float)Math.PI * 2f) + (float)Math.PI;
            Projectile.localAI[1] = num1000 + num999;
        }
    }
}