using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;
using AAModClassic.Projectiles.Zero;
using AAModClassic.Buffs;
using AAModClassic.CrossMod;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using AAModClassic.Items.Blocks;
using AAModClassic.___Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Items.Boss.Athena.Olympian;
using AAModClassic.Items.Boss.Greed.WKG;
using AAModClassic.NPCs.Enemies.Sky;
using AAModClassic.___Content.Mire.Buffs;

namespace AAModClassic.Projectiles.Greed.WKG
{
    public class OreChunk : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
			Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 6;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ore");
		}

        public override void AI()
        {
            OreEffect();
            if (Projectile.velocity.X > 0)
            {
                Projectile.direction = 1;
            }
            else
            {
                Projectile.direction = -1;
            }
            Projectile.rotation += .2f * Projectile.direction;

            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

            int k = (int)Projectile.ai[1];
            if(k == ItemID.SilverOre)
            {
                bool flag = false;
                Vector2 velocity = Collision.TileCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, true, true, 1);;
                if (velocity != Projectile.velocity)
				{
					flag = true;
				}
                if (flag && ProjectileLoader.OnTileCollide(Projectile, Projectile.velocity))
			    {
                    Projectile.velocity = - Projectile.velocity;
                    Projectile.penetrate--;
                }
            }
            else if(k == ItemID.TungstenOre)
            {
                Projectile.penetrate = -1;
                Projectile.GetGlobalProjectile<ImplaingProjectile>().CanImpale = true;
                Projectile.GetGlobalProjectile<ImplaingProjectile>().damagePerImpaler = 30;
                if (Projectile.ai[0] == 1f)
                {
                    Projectile.rotation = 0;
                    Projectile.tileCollide = false;
                    int num6 = 15;
                    bool flag = false;
                    bool flag2 = false;
                    float[] localAI = Projectile.localAI;
                    int num7 = 0;
                    float num8 = localAI[num7];
                    localAI[num7] = num8 + 1f;
                    if (Projectile.localAI[0] % 30f == 0f)
                    {
                        flag2 = true;
                    }
                    int num9 = (int)Projectile.localAI[1];
                    if (Projectile.localAI[0] >= 60 * num6)
                    {
                        flag = true;
                    }
                    else if (num9 < 0 || num9 >= 200)
                    {
                        flag = true;
                    }
                    else if (Main.npc[num9].active && !Main.npc[num9].dontTakeDamage)
                    {
                        Projectile.Center = Main.npc[num9].Center - Projectile.velocity * 2f;
                        Projectile.gfxOffY = Main.npc[num9].gfxOffY;
                        Projectile.alpha = Main.npc[num9].alpha;
                        if (flag2)
                        {
                            Main.npc[num9].HitEffect(0, 1.0);
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        Projectile.Kill();
                    }
                }
            }
            else if(k == ModContent.ItemType<AbyssiumOre>())
            {
                if(Projectile.ai[0]++ > 800)
                {
                    Projectile.Kill();
                }
                if(Projectile.ai[0] % 30 == 15)
                {
                    for(int shoot = 0; shoot < 6; shoot ++)
                    {
                        Vector2 vector17 = Projectile.velocity;
                        vector17.Normalize();
                        vector17 *= Main.rand.Next(70, 91) * 0.1f;
                        vector17.X += Main.rand.Next(-30, 31) * 0.04f;
                        vector17.Y += Main.rand.Next(-30, 31) * 0.03f;
                        NewProjectile(Projectile.position.X, Projectile.position.Y, vector17.X, vector17.Y, 523, Projectile.damage, 0, Main.myPlayer, Main.rand.Next(20), 0f);
                    }
                }
            }
            else if(k == ItemID.Hellstone)
            {
                if(Projectile.ai[0]++ > 800)
                {
                    Projectile.Kill();
                }
                if(Projectile.ai[0] % 20 == 10)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        Vector2 vector109 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f + 30f);
                        float num824 = Projectile.position.X - vector109.X;
                        float num825 = Projectile.position.Y - vector109.Y;
                        num824 += Main.rand.Next(-20, 51);
                        num825 += Main.rand.Next(20, 51);
                        num825 *= 0.2f;
                        float num826 = (float)Math.Sqrt(num824 * num824 + num825 * num825);
                        num824 *= num826;
                        num825 *= num826;
                        num824 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        num825 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        int p = NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[p].DamageType = DamageClass.Ranged;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
            }
            else if(k == ItemID.CobaltOre)
            {
                bool flag = false;
                Vector2 velocity = Collision.TileCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, true, true, 1);;
                if (velocity != Projectile.velocity)
				{
					flag = true;
				}
                if (flag && ProjectileLoader.OnTileCollide(Projectile, Projectile.velocity))
			    {
                    Projectile.velocity = - Projectile.velocity;
                }
            }
            else if(k == ItemID.AdamantiteOre)
            {
                bool flag = false;
                if(Projectile.velocity == Vector2.Zero) Projectile.Kill();
                else if(Projectile.velocity.Length() < 8f) Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 8f;
                Vector2 velocity = Collision.TileCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, true, true, 1);;
                if (velocity != Projectile.velocity)
				{
					flag = true;
				}
                if (flag && ProjectileLoader.OnTileCollide(Projectile, Projectile.velocity))
			    {
                    if(velocity.Y != Projectile.velocity.Y) Projectile.velocity.Y = 0;
                    if(velocity.X != Projectile.velocity.X) Projectile.velocity.X = 0;
                }
            }
            else if(k == ModContent.ItemType<DarkmatterOre>())
            {
                int num5 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width * 3, Projectile.height * 3, ModContent.DustType<Dusts.DarkmatterDust>() , 0f, 0f, 200, default, 0.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.75f;
                Main.dust[num5].fadeIn = 1.3f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(50, 100) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 34f;
                Main.dust[num5].position = Projectile.Center - vector;

                if(Projectile.ai[0]++ > 800)
                {
                    Projectile.Kill();
                }

                for (int i = 0; i < 20; i++)
                {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 200);
                    offset.Y += (float)(Math.Cos(angle) * 200);
                    Dust dust = Main.dust[Dust.NewDust(Projectile.Center - Projectile.velocity + offset, 0, 0,  ModContent.DustType<Dusts.DarkmatterDust>(), 0, 0, 100, default, 1f)];
                    dust.velocity = Projectile.velocity;
                    dust.noGravity = true;
                }

                if(Projectile.ai[0] % 20 == 10)
                {
                    for(int n = 0; n < 200; n++)
                    {
                        if(!Main.npc[n].townNPC && !Main.npc[n].dontTakeDamage && (Main.npc[n].position - Projectile.position).Length() < 200)
                        {
                            Main.player[Projectile.owner].ApplyDamageToNPC(Main.npc[n], Projectile.damage / 10, 0, 1, false);
                        }
                    }
                }
            }
            else if(k == ModContent.ItemType<DaybreakIncineriteOre>())
            {
                if(Projectile.ai[0] == 1f)
                {
                    if(Projectile.localAI[0]++ >= 15f)
                    {
                        Projectile.localAI[0] = 0f;
                        NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), Projectile.damage, Projectile.knockBack * 3, Main.myPlayer, 0, 0);
                    }
                    else if(Projectile.localAI[0] <= 0f)
                    {
                        Projectile.localAI[0] = 0f;
                    }
                }
            }
            else if(k == ModContent.ItemType<RadiumOre>())
            {
                Projectile.ai[0] ++;
                if(Projectile.ai[0] > 600)
                {
                    Projectile.ai[0] = 600;
                }
                else
                {
                    Projectile.damage += 4;
                }
                Projectile.velocity += Vector2.Normalize(Projectile.velocity) * 0.03f;
            }
            else if(k == ModContent.ItemType<EventideAbyssiumOre>())
            {
                if(Projectile.localAI[0] == 1)
                {
                    const int homingDelay = 20;
                    const float desiredFlySpeedInPixelsPerFrame = 60;
                    const float amountOfFramesToLerpBy = 20;

                    Projectile.ai[0]++;
                    if (Projectile.ai[0] > homingDelay)
                    {
                        Projectile.ai[0] = homingDelay;

                        int foundTarget = HomeOnTarget();
                        if (foundTarget != -1)
                        {
                            NPC n = Main.npc[foundTarget];
                            Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                            Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                        }
                    }
                }
                else if(Projectile.localAI[0] >= 2)
                {
                    Projectile.ai[0]++;
                    if (Projectile.ai[0] > 20)
                    {
                        Projectile.localAI[0] = 1;
                    }
                }
            }
            else if(k == ModContent.ItemType<Apocalyptite>())
            {
                if((Projectile.ai[0] ++) % 40 == 20 && Projectile.localAI[0] < 3)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        Vector2 vector82 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                        float ai = Main.rand.Next(100);
                        Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(3.1415f * 2));
                        Vector2 vector84 = Vector2.Normalize(vector83.RotatedByRandom(0.8)) * 14f;
                        int id = NewProjectile(Projectile.position.X + Projectile.velocity.X, Projectile.position.Y  + Projectile.velocity.Y, vector84.X * 2, vector84.Y * 2, ModContent.ProjectileType<ZeroTaze>(), (int) (Projectile.damage * .02f), 0f, Main.myPlayer, vector83.ToRotation(), ai);
                        Main.projectile[id].timeLeft = 30;
                    }
                    Projectile.localAI[0] ++;
                }
                if(Projectile.ai[0] > 800)
                {
                    Projectile.Kill();
                }
            }
            else if(ModSupport.GetMod("CalamityMod") != null)
            {
                if (Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "ChaoticOre").Item.type)
                {
                    if(Projectile.ai[0]++ > 800)
                    {
                        Projectile.Kill();
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "LavaChunk").Projectile.type;
                        int p = NewProjectile(Projectile.Center.X + Projectile.velocity.X, Projectile.Center.Y + Projectile.velocity.Y, 0f, 0.1f, projtype, Projectile.damage, 2f, Projectile.owner, 0f, 0f);
                        Main.projectile[p].DamageType = DamageClass.Ranged;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
                else if(Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "AstralOre").Item.type)
                {
                    if(Projectile.ai[0]++ > 800)
                    {
                        Projectile.Kill();
                    }
                    if (Main.rand.Next(40) == 0)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            float num13 = Projectile.position.X + Main.rand.Next(-400, 400);
                            float num14 = Projectile.position.Y - Main.rand.Next(500, 800);
                            Vector2 vector2 = new Vector2(num13, num14);
                            float num15 = Projectile.position.X + Projectile.width / 2 - vector2.X;
                            float num16 = Projectile.position.Y + Projectile.height / 2 - vector2.Y;
                            num15 += Main.rand.Next(-100, 101);
                            float num17 = 25f;
                            int num18 = Main.rand.Next(3);
                            if (num18 == 0)
                            {
                                num18 = ModSupport.GetModProjectile("CalamityMod", "AstralStar").Projectile.type;
                            }
                            else if (num18 == 1)
                            {
                                num18 = 92;
                            }
                            else
                            {
                                num18 = 12;
                            }
                            float num19 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
                            num19 = num17 / num19;
                            num15 *= num19;
                            num16 *= num19;
                            int num20 = NewProjectile(num13, num14, num15, num16, num18, Projectile.damage, 5f, Projectile.owner, 0f, 0f);
                            Main.projectile[num20].DamageType = DamageClass.Ranged;
                        }
                    }
                }
            }
            else if(ModSupport.GetMod("Redemption") != null)
            {
                
            }
            else if(Projectile.ai[1] > 3930 && ItemLoader.GetItem((int) Projectile.ai[1]).Mod != null)
			{
                try
                {
                    ItemLoader.GetItem((int) Projectile.ai[1]).Mod.Call(new object[]
                    {
                        "AAOreCannonOreAI",
                        Projectile.ai[1]
                    });
                }
                catch
                {
                    return;
                }
			}
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Item[(int)Projectile.ai[1]].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < 3; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((3 - k) / (float)3);
				Main.spriteBatch.Draw(TextureAssets.Item[(int)Projectile.ai[1]].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}

            if (Projectile.ai[1] == ItemID.DemoniteOre || Projectile.ai[1] == ModContent.ItemType<AbyssiumOre>() || Projectile.ai[1] == ItemID.LunarOre || Projectile.ai[1] == ModContent.ItemType<EventideAbyssiumOre>())
            {
                Main.spriteBatch.Draw(TextureAssets.Item[(int)Projectile.ai[1]].Value, Projectile.position, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            else if(Projectile.ai[1] > 3930 && ItemLoader.GetItem((int) Projectile.ai[1]).Mod != null)
			{
                try
                {
                    ItemLoader.GetItem((int) Projectile.ai[1]).Mod.Call(new object[]
                    {
                        "AAOreCannonOreDraw",
                        Projectile.ai[1]
                    });
                }
                catch
                {
                    return false;
                }
			}
            
            /*
            Rectangle frame = BaseDrawing.GetFrame(1, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);

            if (projectile.ai[1] == ItemID.DemoniteOre || projectile.ai[1] == mod.ItemType("Abyssium") || projectile.ai[1] == ItemID.LunarOre || projectile.ai[1] == mod.ItemType("EventideAbyssiumOre"))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.oldPos, 1, projectile.rotation, projectile.direction, 1, frame, .8f, 1, 4, true, 0, 0, lightColor);
            }
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            */
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            int DustType = DType();
            for (int num468 = 0; num468 < 5; num468++)
            {
                float VelX = -Projectile.velocity.X * 0.2f;
                float VelY = -Projectile.velocity.Y * 0.2f;
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustType, VelX, VelY);
            }
            if (Projectile.ai[1] == ItemID.Meteorite)
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.1f);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if (Projectile.ai[1] == ModContent.ItemType<AbyssiumOre>())
            {
                for(int shoot = 0; shoot < 3; shoot ++)
                {
                    Vector2 vector17 = Projectile.velocity;
                    vector17.Normalize();
                    vector17 *= Main.rand.Next(70, 91) * 0.1f;
                    vector17.X += Main.rand.Next(-30, 31) * 0.04f;
                    vector17.Y += Main.rand.Next(-30, 31) * 0.03f;
                    int id = NewProjectile(Projectile.position.X, Projectile.position.Y, vector17.X, vector17.Y, 523, Projectile.damage, 0, Main.myPlayer, Main.rand.Next(20), 0f);
                    Main.projectile[id].tileCollide = false;
                }
            }
            else if (Projectile.ai[1] == ItemID.ChlorophyteOre)
            {
                for (int s = 0; s < 3; s++)
                {
                    NewProjectile(Projectile.position, Vector2.Zero, ModContent.ProjectileType<OreSpores>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0, s);
                }
            }
            else if (Projectile.ai[1] == ItemID.LunarOre)
            {
                NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0, 0);
            }
            else if (Projectile.ai[1] == ModContent.ItemType<DaybreakIncineriteOre>())
            {
                NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), Projectile.damage, Projectile.knockBack * 3, Main.myPlayer, 0, 0);
            }
            else if (Projectile.ai[1] == ModContent.ItemType<Apocalyptite>())
            {
                for (int v = 0; v < 4; v++)
                {
                    int x = Main.rand.Next(-6, 6);
                    int y = -Main.rand.Next(3, 5);
                    int p = NewProjectile(Projectile.position, new Vector2(x, y), ModContent.ProjectileType<AFrag>(), Projectile.damage, 0, Projectile.owner, 0, Main.rand.Next(23));
                    Main.projectile[p].Center = Projectile.Center;
                }
            }
            else if(ModSupport.GetMod("CalamityMod") != null)
            {
                if(Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "CryonicOre").Item.type)
                {
                    SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
					float num36 = 0.783f;
					double num37 = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - num36 / 2f;
					double num38 = num36 / 8f;
					for (int num40 = 0; num40 < 8; num40++)
                    {
                        float num41 = Main.rand.Next(1, 7);
                        float num42 = Main.rand.Next(1, 7);
                        double num43 = num37 + num38 * (num40 + num40 * num40) / 2.0 + 32f * num40;
                        int num44 = NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(num43) * 5.0), (float)(Math.Cos(num43) * 5.0) + num41, 90, Projectile.damage, 1f, Projectile.owner, 0f, 0f);
                        int num45 = NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(-(float)Math.Sin(num43) * 5.0), (float)(-(float)Math.Cos(num43) * 5.0) + num42, 90, Projectile.damage, 1f, Projectile.owner, 0f, 0f);
                        Main.projectile[num44].DamageType = DamageClass.Ranged;
                        Main.projectile[num45].DamageType = DamageClass.Ranged;
                    }
                    return;
                }
                else if (Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "ChaoticOre").Item.type)
                {
                    SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                    int projtype = ModSupport.GetModProjectile("CalamityMod", "ChaosBlaze").Projectile.type;
					int p = NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, projtype, Projectile.damage / 3, 1f, Projectile.owner, 0f, 0f);
                    Main.projectile[p].DamageType = DamageClass.Ranged;
					return;
                }
                else if(Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "CharredOre").Item.type)
                {
					Vector2 vector5 = new Vector2(Projectile.position.X, Projectile.position.Y);
                    int num40 = ModSupport.GetModProjectile("CalamityMod", "BrimstoneHellblast").Projectile.type;
                    float num35 = Projectile.velocity.X;
                    float num37 = Projectile.velocity.Y;
                    for (int m = 0; m < 6; m++)
                    {
                        Vector2 vector6 = Vector2.Normalize(new Vector2(num35 + Main.rand.Next(-4, 4), num37 + Main.rand.Next(-4, 4))) * Main.rand.Next(6, 12);
                        int num41 = NewProjectile(vector5.X, vector5.Y, vector6.X, vector6.Y, num40, Projectile.damage, 0f, Projectile.owner, 1f, 0f);
                        Main.projectile[num41].timeLeft = 300;
                        Main.projectile[num41].tileCollide = false;
                        Main.projectile[num41].hostile = false;
                        Main.projectile[num41].friendly = true;
                        Main.projectile[num41].DamageType = DamageClass.Ranged;
                    }
                    int num42 = 12;
                    float num43 = MathHelper.ToRadians(30f);
                    double num44 = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - num43 / 2f;
                    double num45 = num43 / num42;
                    float num46 = 6f;
                    for (int n = 0; n < 6; n++)
                    {
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "BrimstoneBarrage").Projectile.type;
                        double num47 = num44 + num45 * (n + n * n) / 2.0 + 32f * n + 0.5f * Main.rand.NextDouble();
                        int id1 = NewProjectile(vector5.X, vector5.Y, (float)(Math.Sin(num47) * num46), (float)(Math.Cos(num47) * num46), projtype, Projectile.damage, 0f, Projectile.owner, 1f, 0f);
                        int id2 = NewProjectile(vector5.X, vector5.Y, (float)(-(float)Math.Sin(num47) * (double)num46), (float)(-(float)Math.Cos(num47) * (double)num46), projtype, Projectile.damage, 0f, Projectile.owner, 1f, 0f);
                        Main.projectile[id1].hostile = false;
                        Main.projectile[id1].friendly = true;
                        Main.projectile[id1].DamageType = DamageClass.Ranged;
                        Main.projectile[id2].hostile = false;
                        Main.projectile[id2].friendly = true;
                        Main.projectile[id2].DamageType = DamageClass.Ranged;
                    }
                    return;
                }
                else if(Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "PerennialOre").Item.type)
                {
                    int projtype = ModSupport.GetModProjectile("CalamityMod", "ReaverBlast").Projectile.type;
                    int id = NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, projtype, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
                    Main.projectile[id].DamageType = DamageClass.Ranged;
                    return;
                }
                else if(Projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "UelibloomOre").Item.type)
                {
                    int num21 = Main.rand.Next(2, 4);
					for (int i = 0; i < num21; i++)
					{
						Vector2 vector3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						while (vector3.X == 0f && vector3.Y == 0f)
						{
							vector3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						}
						vector3.Normalize();
						vector3 *= Main.rand.Next(70, 101) * 0.1f;
						int num22 = NewProjectile(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2, vector3.X, vector3.Y, 206, Projectile.damage / 2, 0f, Projectile.owner, 0f, 0f);
                        Main.projectile[num22].DamageType = DamageClass.Ranged;
						Main.projectile[num22].netUpdate = true;
					}
                }
            }
            else if(Projectile.ai[1] > 3930 && ItemLoader.GetItem((int)Projectile.ai[1]).Mod != null)
			{
                try
                {
                    ItemLoader.GetItem((int)Projectile.ai[1]).Mod.Call(new object[]
                    {
                        "AAOreCannonOreKill",
                        Projectile.ai[1]
                    });
                }
                catch
                {
                    return;
                }
			}
            else
            {
                return;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            int k = (int)Projectile.ai[1];
            if(k == ItemID.CopperOre)
            {
                modifiers.TargetDamageMultiplier *= 1.1f;
            }
            else if(k == ItemID.IronOre)
            {
               target.AddBuff(BuffID.BrokenArmor, 180);
            }
            else if(k == ItemID.LeadOre)
            {
                target.AddBuff(BuffID.Weak, 180);
            }
            if(k == ItemID.TungstenOre)
            {
                target.AddBuff(ModContent.BuffType<Impaled_Buff>(), 900);
                Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                if (Projectile.owner == Main.myPlayer)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && ((Projectile.friendly && (!Main.npc[i].friendly || Projectile.type == ProjectileID.RottenEgg || (Main.npc[i].type == NPCID.Guide && Projectile.owner < 255 && Main.player[Projectile.owner].killGuide) || (Main.npc[i].type == NPCID.Clothier && Projectile.owner < 255 && Main.player[Projectile.owner].killClothier))) || (Projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (Projectile.owner < 0 || Main.npc[i].immune[Projectile.owner] == 0 || Projectile.maxPenetrate == 1) && (Main.npc[i].noTileCollide || !Projectile.ownerHitCheck || Projectile.CanHitWithOwnBody(Main.npc[i])))
                        {
                            bool flag;
                            if (Main.npc[i].type == NPCID.SolarCrawltipedeTail)
                            {
                                Rectangle rect = Main.npc[i].getRect();
                                int num = 8;
                                rect.X -= num;
                                rect.Y -= num;
                                rect.Width += num * 2;
                                rect.Height += num * 2;
                                flag = Projectile.Colliding(rectangle, rect);
                            }
                            else
                            {
                                flag = Projectile.Colliding(rectangle, Main.npc[i].getRect());
                            }
                            if (flag)
                            {
                                if (Main.npc[i].reflectsProjectiles && Projectile.CanBeReflected())
                                {
                                    Main.npc[i].ReflectProjectile(Projectile);
                                    return;
                                }
                                Projectile.ai[0] = 1f;
                                Projectile.localAI[1] = i;
                                Projectile.velocity = (Main.npc[i].Center - Projectile.Center) * 0.75f;
                                Projectile.netUpdate = true;
                                Projectile.StatusNPC(i);
                                Projectile.damage = 0;
                                Projectile.timeLeft = 1200;
                            }
                        }
                    }
                }
            }
            else if(k == ItemID.GoldOre || k == ItemID.PlatinumOre)
            {
                target.AddBuff(BuffID.Midas, 180);
                if(k == ItemID.GoldOre)
                {
                    modifiers.FlatBonusDamage += (int)(target.defense * (Main.expertMode? 0.75f : 0.5f));
                }
                if(k == ItemID.PlatinumOre && Main.rand.Next(5) == 0)
                {
                    int itemcreat = 0;
                    itemcreat = Item.NewItem(Projectile.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, 16, 16, ItemID.SilverCoin, Main.rand.Next(15, 20), false, 0, false, false);
                    if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            else if(k == ItemID.DemoniteOre)
            {
                modifiers.FlatBonusDamage += 50;
                if (Main.rand.Next(5) == 0)
                {
                    target.AddBuff(BuffID.ShadowFlame, 180);
                }
            }
            else if(k == ItemID.CrimtaneOre)
            {
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= (float)(modifiers.FinalDamage.Flat * 0.02);
                Projectile.NewProjectile(Projectile.GetSource_Death(), target.position.X, target.position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, Projectile.owner, Projectile.owner, (float)(modifiers.FinalDamage.Flat * 0.02));
                if (Main.rand.Next(5) == 0)
                {
                    target.AddBuff(BuffID.Confused, 180);
                }
            }
            else if(k == ModContent.ItemType<IncineriteOre>())
            {
                target.AddBuff(BuffID.OnFire, 240);
                if (Main.rand.Next(5) == 0)
                {
                    for(int shoot = 0; shoot < 3; shoot ++)
                    {
                        Vector2 vector109 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f + 30f);
                        float num824 = Projectile.position.X - vector109.X;
                        float num825 = Projectile.position.Y - vector109.Y;
                        num824 += Main.rand.Next(-20, 51);
                        num825 += Main.rand.Next(20, 51);
                        num825 *= 0.2f;
                        float num826 = (float)Math.Sqrt(num824 * num824 + num825 * num825);
                        num824 *= num826;
                        num825 *= num826;
                        num824 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        num825 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        int p = NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), (int)modifiers.FinalDamage.Flat, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[p].DamageType = DamageClass.Ranged;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
            }
            else if(k == ModContent.ItemType<AbyssiumOre>())
            {
                target.AddBuff(BuffID.Venom, 180);
            }
            else if(k == ModContent.ItemType<DynaskullOre>())
            {
                if(Projectile.ai[0] != 1f)
                {
                    Vector2 shoot = Vector2.Zero;
                    int projType = Projectile.type;
                    for(int shootid = 0; shootid < 16; shootid++)
                    {
                        shoot = new Vector2((float)Math.Sin(shootid * 0.125f * Math.PI), (float)Math.Cos(shootid * 0.125f * Math.PI));
                        shoot *= 10f;
                        int p = NewProjectile(Projectile.position.X, Projectile.position.Y, shoot.X, shoot.Y, projType, (int)(modifiers.FinalDamage.Flat /2), 5, Main.myPlayer, 0, ModContent.ItemType<DynaskullOre>());
                        Main.projectile[p].ai[0] = 1f;
                        Main.projectile[p].scale /= 2;
                        Main.projectile[p].width /= 2;
                        Main.projectile[p].height /= 2;
                    }
                }
            }
            else if(k == ItemID.Hellstone)
            {
                target.AddBuff(BuffID.OnFire, 1200);
                for(int shoot = 0; shoot < 7; shoot ++)
                {
                    Vector2 vector109 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f + 30f);
                    float num824 = Projectile.position.X - vector109.X;
                    float num825 = Projectile.position.Y - vector109.Y;
                    num824 += Main.rand.Next(-20, 51);
                    num825 += Main.rand.Next(20, 51);
                    num825 *= 0.2f;
                    float num826 = (float)Math.Sqrt(num824 * num824 + num825 * num825);
                    num824 *= num826;
                    num825 *= num826;
                    num824 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                    num825 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                    int p = NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), (int)modifiers.FinalDamage.Flat, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[p].DamageType = DamageClass.Ranged;
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].friendly = true;
                }
            }
            else if(k == ItemID.CobaltOre)
            {
                if(Projectile.tileCollide)
                {
                    Projectile.velocity = - Projectile.velocity;
                }
            }
            else if(k == ItemID.PalladiumOre)
            {
                if(Projectile.damage / 2 > 100f)
                NewProjectile(Projectile.position.X, Projectile.position.Y, -Projectile.velocity.X, -Projectile.velocity.Y, ModContent.ProjectileType<OreChunk>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, ItemID.PalladiumOre);
            }
            else if(k == ItemID.MythrilOre || k == ItemID.OrichalcumOre)
            {
                if(k == ItemID.MythrilOre)
                {
                    target.AddBuff(BuffID.CursedInferno, 600);
                }
                else if(k == ItemID.OrichalcumOre)
                {
                    target.AddBuff(BuffID.Ichor, 600);
                }

                for(int i = 0; i < 200; i++)
                {
                    if((Main.npc[i].Center - target.Center).Length() < 200f && !Main.npc[i].friendly && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i] != target)
                    {
                        Projectile.velocity = target.DirectionTo(Main.npc[i].Center) * Projectile.velocity.Length();
                        break;
                    }
                }
            }
            else if(k == ItemID.AdamantiteOre)
            {
                Projectile.scale = (float)(Projectile.scale / 1.3);
                Projectile.width = (int)(Projectile.width / 1.3);
                Projectile.height = (int)(Projectile.height / 1.3);
                Projectile.damage = (int)(Projectile.damage / 1.3);
            }
            else if(k == ModContent.ItemType<HallowedOre>())
            {
                //target.AddBuff(BuffID.Slow, 180);
                Player player = Main.player[Projectile.owner];
                if(Projectile.ai[0] < 2f)
                {
                    int p = NewProjectile(player.Center.X, player.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<OreChunk>(), Projectile.damage, Projectile.knockBack, Projectile.owner, ++Projectile.ai[0], ModContent.ItemType<HallowedOre>());
                }
            }
            else if(k == ItemID.ChlorophyteOre)
            {
                for(int shootid = 0; shootid < 4; shootid++)
                {
                    NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * Main.rand.Next(-3, 3) * 0.1f, Projectile.velocity.Y * Main.rand.Next(-3, 3) * 0.1f, 228, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
                target.AddBuff(BuffID.Poisoned, 240);
                target.AddBuff(BuffID.Venom, 240);
            }
            else if(k == ItemID.LunarOre)
            {
                if(Projectile.damage / 2 > 100f)
                {
                    Vector2 vector = Projectile.velocity.RotatedBy(Math.PI /2);
                    vector = Vector2.Normalize(vector);
                    for(int newone = -1; newone <= 1; newone += 2)
                    {
                        int p = NewProjectile(Projectile.Center.X + vector.X * 40f * newone, Projectile.Center.Y + vector.Y * 40f * newone, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<OreChunk>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, ItemID.LunarOre);
                        Main.projectile[p].scale /= 2;
                        Main.projectile[p].width /= 2;
                        Main.projectile[p].height /= 2;
                        Main.projectile[p].ai[0] = 1f;
                    }
                }
                if(Projectile.ai[0] != 1f) NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), (int)(Projectile.damage / 2.5), Projectile.knockBack, Projectile.owner, 0, 0);
            }
            else if(k == ModContent.ItemType<SkyCrystal>())
            {
                int num90 = 3;
                if (Main.rand.Next(3) == 0)
                {
                    num90 ++;
                }
                for (int num91 = 0; num91 < num90; num91++)
                {
                    Vector2 vector2 = new Vector2(Projectile.position.X + Projectile.width * 0.5f + Main.rand.Next(201) * -(float)Projectile.direction + (Projectile.Center.X - Projectile.position.X), Projectile.Center.Y - 600f);
                    vector2.X = (vector2.X * 10f + Projectile.Center.X) / 11f + Main.rand.Next(-100, 101);
                    vector2.Y -= 150 * num91;
                    float num82 = Projectile.Center.X - vector2.X;
                    float num83 = Projectile.Center.Y - vector2.Y;
                    if (num83 < 0f)
                    {
                        num83 *= -1f;
                    }
                    if (num83 < 20f)
                    {
                        num83 = 20f;
                    }
                    float num92 = num82 + Main.rand.Next(-40, 41) * 0.03f;
                    float speedY2 = num83 + Main.rand.Next(-40, 41) * 0.03f;
                    num92 *= Main.rand.Next(75, 150) * 0.01f;
                    vector2.X += Main.rand.Next(-50, 51);
                    Vector2 speedfinal = Vector2.Normalize(new Vector2(num92, speedY2)) * Projectile.velocity.Length();
                    NewProjectile(vector2.X, vector2.Y, speedfinal.X, speedfinal.Y, ModContent.ProjectileType<SeraphFeather>(), Projectile.damage, 0, Projectile.owner, 0f, 1f);
                }
            }
            else if(k == ModContent.ItemType<CovetiteOre>())
            {
                for(int i = 0; i < 12; i++)
                {
                    NewProjectile(Projectile.position.X + 30f, Projectile.position.Y + 30f, Main.rand.Next(-3, 4), Main.rand.Next(-3, 10), ModContent.ProjectileType<Gold>(), Projectile.damage / 2, 1, Projectile.owner, 0, 1);
                }
            }
            else if(k == ModContent.ItemType<DarkmatterOre>())
            {
                target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 180);
            }
            else if(k == ModContent.ItemType<DaybreakIncineriteOre>())
            {
                target.AddBuff(BuffID.Daybreak, 400);
                NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<DaybreakBlast>(), (int)(Projectile.damage / 2.5), Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.ai[0] = 1f;
            }
            else if(k == ModContent.ItemType<EventideAbyssiumOre>())
            {
                target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 400);

                Projectile.localAI[0] ++;

                if(Projectile.velocity.Length() < 10f) Projectile.velocity = 10 * Vector2.Normalize(Projectile.velocity);
            }
            else if(ModSupport.GetMod("CalamityMod") != null)
            {
                if(k == ModSupport.GetModItem("CalamityMod", "AerialiteOre").Item.type)
                {
                    for (int i = 0; i < 4; i++)
					{
						float num = target.position.X + Main.rand.Next(-400, 400);
						float num2 = target.position.Y - Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(num, num2);
						float num3 = target.position.X + target.width / 2 - vector.X;
						float num4 = target.position.Y + target.height / 2 - vector.Y;
						num3 += Main.rand.Next(-100, 101);
						float num5 = 20;
						float num6 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
						num6 = num5 / num6;
						num3 *= num6;
						num4 *= num6;
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "StickyFeatherAero").Projectile.type;
						NewProjectile(num, num2, num3, num4, projtype, Projectile.damage, 1f, Projectile.owner, 0f, 0f);
					}
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CryonicOre").Item.type)
                {
                    target.AddBuff(BuffID.OnFire, 240, false);
                    target.AddBuff(BuffID.Frostburn, 240, false);
                    int bufftype = ModSupport.GetModBuff("CalamityMod", "GlacialState").Type;
                    target.AddBuff(bufftype, 120, false);
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "AstralOre").Item.type)
                {
                    int bufftype = ModSupport.GetModBuff("CalamityMod", "AstralInfectionDebuff").Type;
                    target.AddBuff(bufftype, 360, false);
                    for (int j = 0; j < 6; j++)
					{
						float num13 = target.position.X + Main.rand.Next(-400, 400);
						float num14 = target.position.Y - Main.rand.Next(500, 800);
						Vector2 vector2 = new Vector2(num13, num14);
						float num15 = target.position.X + target.width / 2 - vector2.X;
						float num16 = target.position.Y + target.height / 2 - vector2.Y;
						num15 += Main.rand.Next(-100, 101);
						float num17 = 25f;
						int num18 = Main.rand.Next(3);
						if (num18 == 0)
						{
							num18 = ModSupport.GetModProjectile("CalamityMod", "AstralStar").Projectile.type;
						}
						else if (num18 == 1)
						{
							num18 = 92;
						}
						else
						{
							num18 = 12;
						}
						float num19 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
						num19 = num17 / num19;
						num15 *= num19;
						num16 *= num19;
						int num20 = NewProjectile(num13, num14, num15, num16, num18, Projectile.damage, 5f, Projectile.owner, 0f, 0f);
						Main.projectile[num20].DamageType = DamageClass.Ranged;
                        Main.projectile[num20].noDropItem = true;
					}
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "ChaoticOre").Item.type)
                {
                    target.AddBuff(BuffID.OnFire, 720, false);
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CharredOre").Item.type)
                {
                    int bufftype = ModSupport.GetModBuff("CalamityMod", "BrimstoneFlames").Type;
                    target.AddBuff(bufftype, 720, false);
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "PerennialOre").Item.type)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.position);
					float num46 = 0.783f;
					double num47 = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - num46 / 2f;
					double num48 = num46 / 8f;
                    for (int num50 = 0; num50 < 4; num50++)
                    {
                        float x2 = Utils.NextBool(Main.rand, 2) ? (Projectile.Center.X + 100f) : (Projectile.Center.X - 100f);
                        Vector2 vector5 = new Vector2(x2, Projectile.Center.Y + Main.rand.Next(-100, 101));
                        double num51 = num47 + num48 * (num50 + num50 * num50) / 2.0 + 32f * num50;
                        int num52 = NewProjectile(vector5.X, vector5.Y, (float)(Math.Sin(num51) * 5.0), (float)(Math.Cos(num51) * 5.0), 567, Projectile.damage, 2f, Projectile.owner, 0f, 0f);
                        Main.projectile[num52].DamageType = DamageClass.Ranged;
                        Main.projectile[num52].usesLocalNPCImmunity = true;
                        Main.projectile[num52].localNPCHitCooldown = 60;
                        int num53 = NewProjectile(vector5.X, vector5.Y, (float)(-(float)Math.Sin(num51) * 5.0), (float)(-(float)Math.Cos(num51) * 5.0), 568, Projectile.damage, 2f, Projectile.owner, 0f, 0f);
                        Main.projectile[num53].DamageType = DamageClass.Ranged;
                        Main.projectile[num53].usesLocalNPCImmunity = true;
                        Main.projectile[num53].localNPCHitCooldown = 60;
                    }
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "UelibloomOre").Item.type)
                {
                    int num3 = 9 + Main.rand.Next(3);
                    for (int i = 0; i < num3; i++)
                    {
                        float num4 = 0.025f * i;
                        float num5 = Projectile.velocity.X + Main.rand.Next(-25, 26) * num4;
                        float num6 = Projectile.velocity.Y + Main.rand.Next(-25, 26) * num4;
                        float num7 = Projectile.velocity.Length();
                        num7 = 14f / num7;
                        num5 *= num7;
                        num6 *= num7;
                        int id = NewProjectile(Main.player[Projectile.owner].position.X, Main.player[Projectile.owner].position.Y, num5, num6, 206, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
                        Main.projectile[id].DamageType = DamageClass.Ranged;
                    }
                    if(!target.SpawnedFromStatue && (target.damage > 5 || target.boss) && target.lifeMax > 100 && Main.rand.Next(5) == 0)
                    {
                        int itemcreat = 0;
                        itemcreat = Item.NewItem(Projectile.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, 16, 16, 58, 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                        }
                        if(Main.bloodMoon)
                        {
                            int droptype = ModSupport.GetModItem("CalamityMod", "BloodOrb").Item.type;
                            itemcreat = Item.NewItem(Projectile.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, 16, 16, droptype, 1, false, 0, false, false);
                            if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
                            {
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                            }
                        }
                    }
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "ExodiumClusterOre").Item.type)
                {
                    int bufftype1 = ModSupport.GetModBuff("CalamityMod", "Horror").Type;
                    int bufftype2 = ModSupport.GetModBuff("CalamityMod", "MarkedforDeath").Type;
                    target.AddBuff(bufftype1, 240, false);
                    target.AddBuff(bufftype2, 240, false);
                    if(!target.immortal)
                    {
                        int rangedLevel = (int)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "rangedLevel", false, false);
                        if(rangedLevel < 12500)
                        {
                            rangedLevel += 2;
                            ModSupport.SetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "rangedLevel", rangedLevel, false, false);
                        }
                    }
                    bool revenge = (bool)ModSupport.GetModWorldConditions("CalamityMod", "CalamityWorld", "revenge", false, true);
                    if(revenge)
                    {
                        bool Death = (bool)ModSupport.GetModWorldConditions("CalamityMod", "CalamityWorld", "death", false, true);
                        int stress = (int)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "stress", false, false);
                        bool rageMode = (bool)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "rageMode", false, false);
                        int adrenaline = (int)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "adrenaline", false, false);
                        bool adrenalineMode = (bool)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "adrenalineMode", false, false);
                        if(stress < 10000 && !rageMode)
                        {
                            stress += Death? 350 : 150;
                            ModSupport.SetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "stress", stress, false, false);
                        }
                        if(adrenaline < 10000 && !adrenalineMode)
                        {
                            adrenaline += Death? 350 : 150;
                            ModSupport.SetModPlayerConditions("CalamityMod", Main.player[Projectile.owner], "CalamityPlayer", "adrenaline", adrenaline, false, false);
                        }
                    }
                    return;
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "AuricOre").Item.type)
                {
                    float num2 = Main.rand.Next(22, 30);
                    int num6 = 4;
                    for (int i = 0; i < num6; i++)
                    {
                        Vector2 vector = Projectile.Center;
                        vector.X = (vector.X + Projectile.Center.X) / 2f;
                        vector.Y -= 100 * i;
                        float num3 = Projectile.position.X - vector.X;
                        float num4 = Projectile.position.X - vector.Y;
                        float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                        num5 = num2 / num5;
                        num3 *= num5;
                        num4 *= num5;
                        float num7 = num3 + Main.rand.Next(-360, 361) * 0.02f;
                        float num8 = num4 + Main.rand.Next(-360, 361) * 0.02f;
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "ElementBall").Projectile.type;
                        NewProjectile(vector.X, vector.Y, num7, num8, projtype, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, Main.rand.Next(3));
                    }
                }
            }
            else
            {
                return;
            }
        }

        public void OreEffect()
        {
            int k = (int)Projectile.ai[1];
            Item item = new Item();
            if(k > 0)
            {
                item.SetDefaults(k, false);
            }
            if(k == ItemID.DemoniteOre || k == ModContent.ItemType<AbyssiumOre>() || k == ModContent.ItemType<RadiumOre>())
            {
                Projectile.extraUpdates = 1;
            }
            else if(k == ItemID.Hellstone || k == ModContent.ItemType<IncineriteOre>())
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if(k == ItemID.LunarOre)
            {
                Projectile.extraUpdates = 2;
            }
            else if(k == ModContent.ItemType<EventideAbyssiumOre>())
            {
                Projectile.extraUpdates = 2;
                Projectile.tileCollide = false;
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.Moonraze>(), 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if(ModSupport.GetMod("CalamityMod") != null)
            {
                if(k == ModSupport.GetModItem("CalamityMod", "AerialiteOre").Item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.t_Slime, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CryonicOre").Item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "AstralOre").Item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int dustType = ModSupport.GetModDust("Calamity", "AstralChunkDust").Type;
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "ChaoticOre").Item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CharredOre").Item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, 0f, -1f, 90, default, 3f);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "PerennialOre").Item.type)
                {
                    for (int num291 = 0; num291 < 3; num291++)
                    {
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenFairy, Projectile.velocity.X * 0.2f + Projectile.direction * 3, Projectile.velocity.Y * 0.2f, 100, default, 0.75f);
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "UelibloomOre").Item.type)
                {
                    for (int num291 = 0; num291 < 2; num291++)
                    {
                        int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ChlorophyteWeapon, 0f, -1f, 90, default, 3f);
                        Main.dust[num292].noGravity = true;
                    };
                }
            }
            else if(k > 3930 && Config.LuckyOre[k] > 650 && item.ModItem.Mod != AAMod.instance)
            {
                int dustid = DustID.Copper;
                switch (WorldGen.genRand.Next(10))
                {
                    case 0:
                        dustid = DustID.Copper; break;
                    case 1:
                        dustid = DustID.Tin; break;
                    case 2:
                        dustid = DustID.Iron; break;
                    case 3:
                        dustid = DustID.Lead; break;
                    case 4:
                        dustid = DustID.Silver; break;
                    case 5:
                        dustid = DustID.Tungsten; break;
                    case 6:
                        dustid = DustID.Gold; break;
                    case 7:
                        dustid = DustID.Platinum; break;
                    case 8:
                        dustid = DustID.t_Meteor; break;
                    case 9:
                        dustid = DustID.Torch; break;
                }
                for (int num291 = 0; num291 < 3; num291++)
                {
                    int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustid, 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else
            {
                return;
            }
        }

        public int Damage()
        {
            int orevalue = 0;
            if(Config.LuckyOre.TryGetValue((int)Projectile.ai[1], out orevalue))
            {
                return (int)Math.Exp(orevalue * 0.67/100);
            }
            else if((int)Projectile.ai[1] == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.67/100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.67/100);
            }
            /* 
            switch ((int)projectile.ai[1])
            {
                case 0:
                    return 8;
                case 1:
                    return 9;
                case 2:
                    return 10;
                case 3:
                case 4:
                    return 11;
                case 5:
                    return 12;
                case 6:
                    return 13;
                case 7:
                    return 15;
                case 8:
                    return 21;
                case 9:
                    return 19;
                case 10:
                    return 22;
                case 11:
                    return 14;
                case 12:
                    return 26;
                case 13:
                    return 36;
                case 14:
                    return 39;
                case 15:
                    return 41;
                case 16:
                    return 44;
                case 17:
                    return 47;
                case 18:
                    return 50;
                case 19:
                    return 52;
                case 20:
                    return 57;
                case 21:
                    return 75;
                case 22:
                    return 110;
                case 23:
                    return 130;
                case 24:
                    return 170;
                case 25:
                    return 160;
                case 26:
                    return 130;
                case 27:
                    return 150;
                default:
                    goto case 0;
            }
            */
        }

        public int DType()
        {
            int k = (int)Projectile.ai[1];
            if(k == ItemID.CopperOre)
            {
                return DustID.Copper;
            }
            else if(k == ItemID.TinOre)
            {
                return DustID.Tin;
            }
            else if(k == ItemID.IronOre)
            {
                return DustID.Iron;
            }
            else if(k == ItemID.LeadOre)
            {
                return DustID.Lead;
            }
            else if(k == ItemID.SilverOre)
            {
                return DustID.Silver;
            }
            else if(k == ItemID.TungstenOre)
            {
                return DustID.Tungsten;
            }
            else if(k == ItemID.GoldOre)
            {
                return DustID.Gold;
            }
            else if(k == ItemID.PlatinumOre)
            {
                return DustID.Platinum;
            }
            else if(k == ItemID.Meteorite)
            {
                return DustID.t_Meteor;
            }
            else if (k == ItemID.DemoniteOre)
            {
                return 14;
            }
            else if (k == ItemID.CrimtaneOre)
            {
                return 117;
            }
            else if (k == ModContent.ItemType<AbyssiumOre>())
            {
                return ModContent.DustType<Dusts.AbyssiumDust>();
            }
            else if (k == ModContent.ItemType<IncineriteOre>())
            {
                return ModContent.DustType<Dusts.IncineriteDust>();
            }
            else if (k == ItemID.Hellstone)
            {
                return DustID.Torch;
            }
            else if (k == ItemID.CobaltOre)
            {
                return 48;
            }
            else if (k == ItemID.PalladiumOre)
            {
                return 144;
            }
            else if (k == ItemID.MythrilOre)
            {
                return 49;
            }
            else if (k == ItemID.OrichalcumOre)
            {
                return 145;
            }
            else if (k == ItemID.AdamantiteOre)
            {
                return 50;
            }
            else if (k == ItemID.TitaniumOre)
            {
                return 146;
            }
            else if (k == ModContent.ItemType<HallowedOre>())
            {
                return DustID.Gold;
            }
            else if (k == ItemID.ChlorophyteOre)
            {
                return 128;
            }
            else if (k == ItemID.LunarOre)
            {
                return ModContent.DustType<Dusts.LuminiteDust>();
            }
            else if (k == ModContent.ItemType<DarkmatterOre>())
            {
                return ModContent.DustType<Dusts.DarkmatterDust>();
            }
            else if (k == ModContent.ItemType<RadiumOre>())
            {
                return ModContent.DustType<Dusts.RadiumDust>();
            }
            else if (k == ModContent.ItemType<DaybreakIncineriteOre>())
            {
                return ModContent.DustType<Dusts.DaybreakIncineriteDust>();
            }
            else if (k == ModContent.ItemType<EventideAbyssiumOre>())
            {
                return ModContent.DustType<Dusts.YamataDust>();
            }
            else if (k == ModContent.ItemType<Apocalyptite>())
            {
                return ModContent.DustType<Dusts.VoidDust>();
            }
            else if (Config.LuckyOre[k] <= 300)
            {
                return DustID.Copper;
            }
            else if (Config.LuckyOre[k] <= 700)
            {
                return DustID.Gold;
            }
            else
            {
                switch (WorldGen.genRand.Next(18))
                {
                    case 0:
                        return DustID.Copper;
                    case 1:
                        return DustID.Tin;
                    case 2:
                        return DustID.Iron;
                    case 3:
                        return DustID.Lead;
                    case 4:
                        return DustID.Silver;
                    case 5:
                        return DustID.Tungsten;
                    case 6:
                        return DustID.Gold;
                    case 7:
                        return DustID.Platinum;
                    case 8:
                        return DustID.t_Meteor;
                    case 9:
                        return ModContent.DustType<Dusts.LuminiteDust>();
                    case 10:
                        return ModContent.DustType<Dusts.DarkmatterDust>();
                    case 11:
                        return ModContent.DustType<Dusts.RadiumDust>();
                    case 12:
                        return ModContent.DustType<Dusts.DaybreakIncineriteDust>();
                    case 13:
                        return ModContent.DustType<Dusts.YamataDust>();
                    case 14:
                        return ModContent.DustType<Dusts.VoidDust>();
                    case 15:
                        return ModContent.DustType<Dusts.IncineriteDust>();
                    case 16:
                        return ModContent.DustType<Dusts.AbyssiumDust>();
                    case 17:
                        return DustID.Torch;
                }
            }

            switch ((int)Projectile.ai[1])
            {
                case 0:
                    return DustID.Copper;
                case 1:
                    return DustID.Tin;
                case 2:
                    return DustID.Iron;
                case 3:
                    return DustID.Lead;
                case 4:
                    return DustID.Silver;
                case 5:
                    return DustID.Tungsten;
                case 6:
                    return DustID.Gold;
                case 7:
                    return DustID.Platinum;
                case 8:
                    return DustID.t_Meteor;
                case 9:
                    return 14;
                case 10:
                    return 117;
                case 11:
                    return ModContent.DustType<Dusts.IncineriteDust>();
                case 12:
                    return ModContent.DustType<Dusts.AbyssiumDust>();
                case 13:
                    return DustID.Torch;
                case 14:
                    return 48;
                case 15:
                    return 144;
                case 16:
                    return 49;
                case 17:
                    return 145;
                case 18:
                    return 50;
                case 19:
                    return 146;
                case 20:
                    return DustID.Gold;
                case 21:
                    return 128;
                case 22:
                    return ModContent.DustType<Dusts.LuminiteDust>();
                case 23:
                    return ModContent.DustType<Dusts.DarkmatterDust>();
                case 24:
                    return ModContent.DustType<Dusts.RadiumDust>();
                case 25:
                    return ModContent.DustType<Dusts.DaybreakIncineriteDust>();
                case 26:
                    return ModContent.DustType<Dusts.YamataDust>();
                case 27:
                    return ModContent.DustType<Dusts.VoidDust>();
                default:
                    goto case 0;
            }

        }

        private int NewProjectile(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float Knockback, int Owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), X, Y, SpeedX, SpeedY, Type, Damage, Knockback, Owner, ai0, ai1);
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].DamageType = DamageClass.Ranged;
            Main.projectile[proj].minion = false;
            Main.projectile[proj].sentry = false;
            return proj;
        }

        private int NewProjectile(Vector2 position, Vector2 velocity, int Type, int Damage, float Knockback, int Owner = 255, float ai0 = 0f, float ai1 = 0f)
		{
            int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), position, velocity, Type, Damage, Knockback, Owner, ai0, ai1);
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].DamageType = DamageClass.Ranged;
            Main.projectile[proj].minion = false;
            Main.projectile[proj].sentry = false;
            return proj;
        }
        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance)
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}
