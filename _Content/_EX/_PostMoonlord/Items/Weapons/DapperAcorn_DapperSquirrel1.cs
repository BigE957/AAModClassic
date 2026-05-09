using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class DapperAcorn_DapperSquirrel1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dapper Squirrel");
            Main.projFrames[Projectile.type] = 18;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 44;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return true;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
        
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = true;
			
            if (Projectile.lavaWet)
            {
                Projectile.ai[0] = 1f;
                Projectile.ai[1] = 0f;
            }
            if (player.dead)
            {
                modPlayer.DapperSquirrel = false;
            }
            if (modPlayer.DapperSquirrel)
            {
                Projectile.timeLeft = Main.rand.Next(2, 10);
            }
            int num = 10;
            int num2 = 40 * (Projectile.minionPos + 1) * player.direction;
            if (player.position.X + player.width / 2 < Projectile.position.X + Projectile.width / 2 - num + num2)
            {
                flag = true;
            }
            else if (player.position.X + player.width / 2 > Projectile.position.X + Projectile.width / 2 + num + num2)
            {
                flag2 = true;
            }
            else if (player.position.X + player.width / 2 < Projectile.position.X + Projectile.width / 2 - num)
            {
                flag = true;
            }
            else if (player.position.X + player.width / 2 > Projectile.position.X + Projectile.width / 2 + num)
            {
                flag2 = true;
            }
            if (Projectile.ai[1] == 0f)
            {
				Projectile.tileCollide = true;
                int num36 = 500;
                num36 += 40 * Projectile.minionPos;
                if (Projectile.localAI[0] > 0f)
                {
                    num36 += 500;
                }
                if (player.rocketDelay2 > 0)
                {
                    Projectile.ai[0] = 1f;
                }
                Vector2 vector6 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num37 = player.position.X + player.width / 2 - vector6.X;
                float num38 = player.position.Y + player.height / 2 - vector6.Y;
                float num39 = (float)Math.Sqrt(num37 * num37 + num38 * num38);
                if (num39 > 2000f)
                {
                    Projectile.position.X = player.position.X + player.width / 2 - Projectile.width / 2;
                    Projectile.position.Y = player.position.Y + player.height / 2 - Projectile.height / 2;
                }
                else if (num39 > num36 || (Math.Abs(num38) > 300f && (Projectile.localAI[0] <= 0f)))
                {
                    if (num38 > 0f && Projectile.velocity.Y < 0f)
                    {
                        Projectile.velocity.Y = 0f;
                    }
                    if (num38 < 0f && Projectile.velocity.Y > 0f)
                    {
                        Projectile.velocity.Y = 0f;
                    }
                    Projectile.ai[0] = 1f;
                }
            }
            if (Projectile.ai[0] != 0f)
            {
                int num41 = 100;
                Projectile.tileCollide = false;
                Vector2 vector7 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num42 = player.position.X + player.width / 2 - vector7.X;
                num42 -= 40 * player.direction;
                float num43 = 700f;
                if (flag5)
                {
                    num43 += 100f;
                }
                bool flag6 = false;
                int num44 = -1;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].CanBeChasedBy(this, false))
                    {
                        float num45 = Main.npc[j].position.X + Main.npc[j].width / 2;
                        float num46 = Main.npc[j].position.Y + Main.npc[j].height / 2;
                        float num47 = Math.Abs(player.position.X + player.width / 2 - num45) + Math.Abs(player.position.Y + player.height / 2 - num46);
                        if (num47 < num43)
                        {
                            if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                            {
                                num44 = j;
                            }
                            flag6 = true;
                            break;
                        }
                    }
                }
                if (!flag6)
                {
                    num42 -= 40 * Projectile.minionPos * player.direction;
                }
                if (flag6 && num44 >= 0)
                {
                    Projectile.ai[0] = 0f;
                }
                float num48 = player.position.Y + player.height / 2 - vector7.Y;
                float num49 = (float)Math.Sqrt(num42 * num42 + num48 * num48);
                float num40 = 0.4f;
                float num50 = 12f;
                if (num50 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
                {
                    num50 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
                }
                if (num49 < num41 && player.velocity.Y == 0f && Projectile.position.Y + Projectile.height <= player.position.Y + player.height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    if (Projectile.velocity.Y < -6f)
                    {
                        Projectile.velocity.Y = -6f;
                    }
                }
                if (num49 < 60f)
                {
                    num42 = Projectile.velocity.X;
                    num48 = Projectile.velocity.Y;
                }
                else
                {
                    num49 = num50 / num49;
                    num42 *= num49;
                    num48 *= num49;
                }
                if (Projectile.velocity.X < num42)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num40;
                    if (Projectile.velocity.X < 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num40 * 1.5f;
                    }
                }
                if (Projectile.velocity.X > num42)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num40;
                    if (Projectile.velocity.X > 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num40 * 1.5f;
                    }
                }
                if (Projectile.velocity.Y < num48)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num40;
                    if (Projectile.velocity.Y < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num40 * 1.5f;
                    }
                }
                if (Projectile.velocity.Y > num48)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num40;
                    if (Projectile.velocity.Y > 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num40 * 1.5f;
                    }
                }
                if (Projectile.frame < 12)
                {
                    Projectile.frame = Main.rand.Next(12, 18);
                    Projectile.frameCounter = 0;
                }
                if (Projectile.velocity.X > 0.5)
                {
                    Projectile.spriteDirection = -1;
                }
                else if (Projectile.velocity.X < -0.5)
                {
                    Projectile.spriteDirection = 1;
                }
                if (Projectile.spriteDirection == -1)
                {
                    Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
                }
                else
                {
                    Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
                }
                return;
            }
            else
            {
                float num57 = 40 * Projectile.minionPos;
                int num58 = 30;
                int num59 = 60;
                Projectile.localAI[0] -= 1f;
                if (Projectile.localAI[0] < 0f)
                {
                    Projectile.localAI[0] = 0f;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                else
                {
                    float num60 = Projectile.position.X;
                    float num61 = Projectile.position.Y;
                    float num62 = 100000f;
                    float num63 = num62;
                    int num64 = -1;
                    NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
                    if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
                    {
                        float num65 = ownerMinionAttackTargetNPC.position.X + ownerMinionAttackTargetNPC.width / 2;
                        float num66 = ownerMinionAttackTargetNPC.position.Y + ownerMinionAttackTargetNPC.height / 2;
                        float num67 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num65) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num66);
                        if (num67 < num62)
                        {
                            if (num64 == -1 && num67 <= num63)
                            {
                                num63 = num67;
                                num60 = num65;
                                num61 = num66;
                            }
                            if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                            {
                                num62 = num67;
                                num60 = num65;
                                num61 = num66;
                                num64 = ownerMinionAttackTargetNPC.whoAmI;
                            }
                        }
                    }
                    if (num64 == -1)
                    {
                        
                        for (int l = 0; l < 200; l++)
                        {
                            if (Main.npc[l].CanBeChasedBy(this, false))
                            {
                                float num68 = Main.npc[l].position.X + Main.npc[l].width / 2;
                                float num69 = Main.npc[l].position.Y + Main.npc[l].height / 2;
                                float num70 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num68) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num69);
                                if (num70 < num62)
                                {
                                    if (num64 == -1 && num70 <= num63)
                                    {
                                        num63 = num70;
                                        num60 = num68;
                                        num61 = num69;
                                    }
                                    if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[l].position, Main.npc[l].width, Main.npc[l].height))
                                    {
                                        num62 = num70;
                                        num60 = num68;
                                        num61 = num69;
                                        num64 = l;
                                    }
                                }
                            }
                        }
                    }
                    if (num64 == -1 && num63 < num62)
                    {
                        num62 = num63;
                    }
                    float num71 = 400f;
                    if (Projectile.position.Y > Main.worldSurface * 16.0)
                    {
                        num71 = 200f;
                    }
                    if (num62 < num71 + num57 && num64 == -1)
                    {
                        float num72 = num60 - (Projectile.position.X + Projectile.width / 2);
                        if (num72 < -5f)
                        {
                            flag = true;
                            flag2 = false;
                        }
                        else if (num72 > 5f)
                        {
                            flag2 = true;
                            flag = false;
                        }
                    }
                    else if (num64 >= 0 && num62 < 800f + num57)
                    {
                        Projectile.localAI[0] = num59;
                        float num73 = num60 - (Projectile.position.X + Projectile.width / 2);
                        if (num73 > 300f || num73 < -300f)
                        {
                            if (num73 < -50f)
                            {
                                flag = true;
                                flag2 = false;
                            }
                            else if (num73 > 50f)
                            {
                                flag2 = true;
                                flag = false;
                            }
                        }
                        else if (Projectile.owner == Main.myPlayer)
                        {
                            Projectile.ai[1] = num58;
                            float num74 = 12f;
                            Vector2 vector8 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height / 2 - 8f);
                            float num75 = num60 - vector8.X + Main.rand.Next(-20, 21);
                            float num76 = Math.Abs(num75) * 0.1f;
                            num76 = num76 * Main.rand.Next(0, 100) * 0.001f;
                            float num77 = num61 - vector8.Y + Main.rand.Next(-20, 21) - num76;
                            float num78 = (float)Math.Sqrt(num75 * num75 + num77 * num77);
                            num78 = num74 / num78;
                            num75 *= num78;
                            num77 *= num78;
                            int num79 = Projectile.damage;
                            int num80 = ModContent.ProjectileType<DapperAcorn_TopHat>();
                            int num81 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector8.X, vector8.Y, num75 * 2, num77 * 2, num80, num79, Projectile.knockBack, Main.myPlayer, 0f, num64);
                            Main.projectile[num81].timeLeft = 300;
                            if (num75 < 0f)
                            {
                                Projectile.direction = -1;
                            }
                            if (num75 > 0f)
                            {
                                Projectile.direction = 1;
                            }
                            Projectile.netUpdate = true;
                        }
                    }
                }
                if (Projectile.localAI[0] == 0f)
                {
                    Projectile.direction = player.direction;
                }
                Projectile.rotation = 0f;
                float num104 = 6f;
                float num103 = 0.2f;
                if (num104 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
                {
                    num104 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
                    num103 = 0.3f;
                }
                if (flag)
                {
                    if (Projectile.velocity.X > -3.5)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num103;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num103 * 0.25f;
                    }
                }
                else if (flag2)
                {
                    if (Projectile.velocity.X < 3.5)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num103;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num103 * 0.25f;
                    }
                }
                else
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.9f;
                    if (Projectile.velocity.X >= -num103 && Projectile.velocity.X <= num103)
                    {
                        Projectile.velocity.X = 0f;
                    }
                }
                if (flag || flag2)
                {
                    int num105 = (int)(Projectile.position.X + Projectile.width / 2) / 16;
                    int j2 = (int)(Projectile.position.Y + Projectile.height / 2) / 16;
                    if (flag)
                    {
                        num105--;
                    }
                    if (flag2)
                    {
                        num105++;
                    }
                    num105 += (int)Projectile.velocity.X;
                    if (WorldGen.SolidTile(num105, j2))
                    {
                        flag4 = true;
                    }
                }
                if (player.position.Y + player.height - 8f > Projectile.position.Y + Projectile.height)
                {
                    flag3 = true;
                }
                Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY, 1, false, 0);
                if (Projectile.velocity.Y == 0f)
                {
                    if (!flag3 && (Projectile.velocity.X < 0f || Projectile.velocity.X > 0f))
                    {
                        int num106 = (int)(Projectile.position.X + Projectile.width / 2) / 16;
                        int j3 = (int)(Projectile.position.Y + Projectile.height / 2) / 16 + 1;
                        if (flag)
                        {
                            num106--;
                        }
                        if (flag2)
                        {
                            num106++;
                        }
                        WorldGen.SolidTile(num106, j3);
                    }
                    if (flag4)
                    {
                        int num107 = (int)(Projectile.position.X + Projectile.width / 2) / 16;
                        int num108 = (int)(Projectile.position.Y + Projectile.height) / 16 + 1;
                        if (WorldGen.SolidTile(num107, num108) || Main.tile[num107, num108].IsHalfBlock || Main.tile[num107, num108].Slope > 0)
                        {
                            try
                            {
                                num107 = (int)(Projectile.position.X + Projectile.width / 2) / 16;
                                num108 = (int)(Projectile.position.Y + Projectile.height / 2) / 16;
                                if (flag)
                                {
                                    num107--;
                                }
                                if (flag2)
                                {
                                    num107++;
                                }
                                num107 += (int)Projectile.velocity.X;
                                if (!WorldGen.SolidTile(num107, num108 - 1) && !WorldGen.SolidTile(num107, num108 - 2))
                                {
                                    Projectile.velocity.Y = -5.1f;
                                }
                                else if (!WorldGen.SolidTile(num107, num108 - 2))
                                {
                                    Projectile.velocity.Y = -7.1f;
                                }
                                else if (WorldGen.SolidTile(num107, num108 - 5))
                                {
                                    Projectile.velocity.Y = -11.1f;
                                }
                                else if (WorldGen.SolidTile(num107, num108 - 4))
                                {
                                    Projectile.velocity.Y = -10.1f;
                                }
                                else
                                {
                                    Projectile.velocity.Y = -9.1f;
                                }
                            }
                            catch
                            {
                                Projectile.velocity.Y = -9.1f;
                            }
                        }
                    }
                }
                if (Projectile.velocity.X > num104)
                {
                    Projectile.velocity.X = num104;
                }
                if (Projectile.velocity.X < -num104)
                {
                    Projectile.velocity.X = -num104;
                }
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.direction = -1;
                }
                if (Projectile.velocity.X > 0f)
                {
                    Projectile.direction = 1;
                }
                if (Projectile.velocity.X > num103 && flag2)
                {
                    Projectile.direction = 1;
                }
                if (Projectile.velocity.X < -num103 && flag)
                {
                    Projectile.direction = -1;
                }
                if (Projectile.direction == -1)
                {
                    Projectile.spriteDirection = 1;
                }
                if (Projectile.direction == 1)
                {
                    Projectile.spriteDirection = -1;
                }
                if (Projectile.ai[1] > 0f)
                {
                    if (Projectile.localAI[1] == 0f)
                    {
                        Projectile.localAI[1] = 1f;
                        Projectile.frame = 1;
                    }
                    if (Projectile.frame != 0)
                    {
                        Projectile.frameCounter++;
                        if (Projectile.frameCounter > 4)
                        {
                            Projectile.frame++;
                            Projectile.frameCounter = 0;
                        }
                        if (Projectile.frame == 4)
                        {
                            Projectile.frame = 0;
                        }
                    }
                }
                else if (Projectile.velocity.Y == 0f)
                {
                    Projectile.localAI[1] = 0f;
                    if (Projectile.velocity.X == 0f)
                    {
                        Projectile.frame = 0;
                        Projectile.frameCounter = 0;
                    }
                    else if (Projectile.velocity.X < -0.8 || Projectile.velocity.X > 0.8)
                    {
                        Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
                        Projectile.frameCounter++;
                        if (Projectile.frameCounter > 6)
                        {
                            Projectile.frame++;
                            Projectile.frameCounter = 0;
                        }
                        if (Projectile.frame < 5)
                        {
                            Projectile.frame = 5;
                        }
                        if (Projectile.frame >= 11)
                        {
                            Projectile.frame = 5;
                        }
                    }
                    else
                    {
                        Projectile.frame = 0;
                        Projectile.frameCounter = 0;
                    }
                }
                else if (Projectile.velocity.Y < 0f)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = 4;
                }
                else if (Projectile.velocity.Y > 0f)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = 4;
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
                if (Projectile.velocity.Y > 10f)
                {
                    Projectile.velocity.Y = 10f;
                }
                return;
            }
        }
    }
}
