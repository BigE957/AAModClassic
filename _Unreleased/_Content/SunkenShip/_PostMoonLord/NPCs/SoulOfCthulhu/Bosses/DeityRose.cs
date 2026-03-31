using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using AAModClassic._Unreleased;
using AAModClassic.Globals;
using AAModClassic.Dusts;

namespace AAModClassic._Unreleased.NPCs.Bosses.SoC.Bosses
{
    [AutoloadBossHead]
    public class DeityRose : ModNPC
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ei'lor");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.width = 86;
            NPC.height = 86;
            NPC.aiStyle = 51;
            NPC.damage = 90;
            NPC.defense = 100;
            NPC.lifeMax = 150000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.boss = true;
            NPC.npcSlots = 16f;
            NPC.buffImmune[20] = true;
        }

        //TODOSOC
        /*
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (AAWorld_Unreleased.Anticheat == true)
            {
                if (damage > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    damage = 0;
                }

                return false;
            }

            return true;
        }
        */

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            int num = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
            if (NPC.frameCounter > 6.0)
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = NPC.frame.Y + num;
            }
            if (NPC.life > NPC.lifeMax / 2)
            {
                if (NPC.frame.Y > num * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (NPC.frame.Y < num * 4)
                {
                    NPC.frame.Y = num * 4;
                }
                if (NPC.frame.Y > num * 7)
                {
                    NPC.frame.Y = num * 4;
                }
            }
        }

        public override void AI()
        {
            bool flag45 = false;
            bool flag46 = false;
            NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead)
            {
                flag46 = true;
                flag45 = true;
            }
            if (Main.netMode != 1)
            {
                int num703 = 6000;
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)num703)
                {
                    NPC.active = false;
                    NPC.life = 0;
                }
            }
            AAModGlobalNPC.Rose = NPC.whoAmI;
            if (NPC.localAI[0] == 0f && Main.netMode != 1)
            {
                NPC.localAI[0] = 1f;
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseHook>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseHook>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseHook>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
            }
            int[] array2 = new int[3];
            float num704 = 0f;
            float num705 = 0f;
            int num706 = 0;
            for (int num707 = 0; num707 < 200; num707++)
            {
                if (Main.npc[num707].active && Main.npc[num707].aiStyle == 52)
                {
                    num704 += Main.npc[num707].Center.X;
                    num705 += Main.npc[num707].Center.Y;
                    array2[num706] = num707;
                    num706++;
                    if (num706 > 2)
                    {
                        break;
                    }
                }
            }
            num704 /= (float)num706;
            num705 /= (float)num706;
            float num708 = 2.5f;
            float num709 = 0.05f;
            if (NPC.life < NPC.lifeMax / 2)
            {
                num708 = 5f;
                num709 = 0.05f;
            }
            if (NPC.life < NPC.lifeMax / 4)
            {
                num708 = 7f;
            }
            if (!Main.player[NPC.target].ZoneBeach || (double)Main.player[NPC.target].position.Y < Main.worldSurface * 16.0 || Main.player[NPC.target].position.Y > (float)((Main.maxTilesY - 200) * 16))
            {
                flag45 = true;
                num708 += 8f;
                num709 = 0.3f;
            }
            if (Main.expertMode)
            {
                num708 += 1f;
                num708 *= 1.1f;
                num709 += 0.01f;
                num709 *= 1.1f;
            }
            Vector2 vector87 = new Vector2(num704, num705);
            float num710 = Main.player[NPC.target].Center.X - vector87.X;
            float num711 = Main.player[NPC.target].Center.Y - vector87.Y;
            if (flag46)
            {
                num711 *= -1f;
                num710 *= -1f;
                num708 += 8f;
            }
            float num712 = (float)Math.Sqrt((double)(num710 * num710 + num711 * num711));
            int num713 = 500;
            if (flag45)
            {
                num713 += 350;
            }
            if (Main.expertMode)
            {
                num713 += 150;
            }
            if (num712 >= (float)num713)
            {
                num712 = (float)num713 / num712;
                num710 *= num712;
                num711 *= num712;
            }
            num704 += num710;
            num705 += num711;
            vector87 = new Vector2(NPC.Center.X, NPC.Center.Y);
            num710 = num704 - vector87.X;
            num711 = num705 - vector87.Y;
            num712 = (float)Math.Sqrt((double)(num710 * num710 + num711 * num711));
            if (num712 < num708)
            {
                num710 = NPC.velocity.X;
                num711 = NPC.velocity.Y;
            }
            else
            {
                num712 = num708 / num712;
                num710 *= num712;
                num711 *= num712;
            }
            if (NPC.velocity.X < num710)
            {
                NPC.velocity.X = NPC.velocity.X + num709;
                if (NPC.velocity.X < 0f && num710 > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + num709 * 2f;
                }
            }
            else if (NPC.velocity.X > num710)
            {
                NPC.velocity.X = NPC.velocity.X - num709;
                if (NPC.velocity.X > 0f && num710 < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - num709 * 2f;
                }
            }
            if (NPC.velocity.Y < num711)
            {
                NPC.velocity.Y = NPC.velocity.Y + num709;
                if (NPC.velocity.Y < 0f && num711 > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + num709 * 2f;
                }
            }
            else if (NPC.velocity.Y > num711)
            {
                NPC.velocity.Y = NPC.velocity.Y - num709;
                if (NPC.velocity.Y > 0f && num711 < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - num709 * 2f;
                }
            }
            Vector2 vector88 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num714 = Main.player[NPC.target].Center.X - vector88.X;
            float num715 = Main.player[NPC.target].Center.Y - vector88.Y;
            NPC.rotation = (float)Math.Atan2((double)num715, (double)num714) + 1.57f;
            if (NPC.life > NPC.lifeMax / 2)
            {
                NPC.defense = 36;
                NPC.damage = (int)(50f * Main.GameModeInfo.EnemyDamageMultiplier);
                if (flag45)
                {
                    NPC.defense *= 2;
                    NPC.damage *= 2;
                }
                if (Main.netMode != 1)
                {
                    NPC.localAI[1] += 1f;
                    if ((double)NPC.life < (double)NPC.lifeMax * 0.9)
                    {
                        NPC.localAI[1] += 1f;
                    }
                    if ((double)NPC.life < (double)NPC.lifeMax * 0.8)
                    {
                        NPC.localAI[1] += 1f;
                    }
                    if ((double)NPC.life < (double)NPC.lifeMax * 0.7)
                    {
                        NPC.localAI[1] += 1f;
                    }
                    if ((double)NPC.life < (double)NPC.lifeMax * 0.6)
                    {
                        NPC.localAI[1] += 1f;
                    }
                    if (flag45)
                    {
                        NPC.localAI[1] += 3f;
                    }
                    if (Main.expertMode)
                    {
                        NPC.localAI[1] += 1f;
                    }
                    if (Main.expertMode && NPC.justHit && Main.rand.Next(2) == 0)
                    {
                        NPC.localAI[3] = 1f;
                    }
                    if (NPC.localAI[1] > 80f)
                    {
                        NPC.localAI[1] = 0f;
                        bool flag47 = Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height);
                        if (NPC.localAI[3] > 0f)
                        {
                            flag47 = true;
                            NPC.localAI[3] = 0f;
                        }
                        if (flag47)
                        {
                            Vector2 vector89 = new Vector2(NPC.Center.X, NPC.Center.Y);
                            float num716 = 15f;
                            if (Main.expertMode)
                            {
                                num716 = 17f;
                            }
                            float num717 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector89.X;
                            float num718 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector89.Y;
                            float num719 = (float)Math.Sqrt((double)(num717 * num717 + num718 * num718));
                            num719 = num716 / num719;
                            num717 *= num719;
                            num718 *= num719;
                            int num720 = 22;
                            int num721 = 275;
                            int maxValue2 = 4;
                            int maxValue3 = 8;
                            if (Main.expertMode)
                            {
                                maxValue2 = 2;
                                maxValue3 = 6;
                            }
                            if ((double)NPC.life < (double)NPC.lifeMax * 0.8 && Main.rand.Next(maxValue2) == 0)
                            {
                                num720 = 27;
                                NPC.localAI[1] = -30f;
                                num721 = 276;
                            }
                            else if ((double)NPC.life < (double)NPC.lifeMax * 0.8 && Main.rand.Next(maxValue3) == 0)
                            {
                                num720 = 31;
                                NPC.localAI[1] = -120f;
                                num721 = 277;
                            }
                            if (flag45)
                            {
                                num720 *= 2;
                            }
                            if (Main.expertMode)
                            {
                                num720 = (int)((double)num720 * 0.9);
                            }
                            vector89.X += num717 * 3f;
                            vector89.Y += num718 * 3f;
                            int num722 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector89.X, vector89.Y, num717, num718, num721, num720, 0f, Main.myPlayer, 0f, 0f);
                            if (num721 != 277)
                            {
                                Main.projectile[num722].timeLeft = 300;
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                NPC.defense = 10;
                NPC.damage = (int)(70f * Main.GameModeInfo.EnemyDamageMultiplier);
                if (flag45)
                {
                    NPC.defense *= 4;
                    NPC.damage *= 2;
                }
                if (Main.netMode != 1)
                {
                    if (NPC.localAI[0] == 1f)
                    {
                        NPC.localAI[0] = 2f;
                        for (int num723 = 0; num723 < 8; num723++)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseClaws>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        if (Main.expertMode)
                        {
                            for (int num724 = 0; num724 < 200; num724++)
                            {
                                if (Main.npc[num724].active && Main.npc[num724].aiStyle == 52)
                                {
                                    for (int num725 = 0; num725 < 3; num725++)
                                    {
                                        int num726 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseClaws>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                                        Main.npc[num726].ai[3] = (float)(num724 + 1);
                                    }
                                }
                            }
                        }
                    }
                    else if (Main.expertMode && Main.rand.Next(60) == 0)
                    {
                        int num727 = 0;
                        for (int num728 = 0; num728 < 200; num728++)
                        {
                            if (Main.npc[num728].active && Main.npc[num728].type == 264 && Main.npc[num728].ai[3] == 0f)
                            {
                                num727++;
                            }
                        }
                        if (num727 < 8 && Main.rand.Next((num727 + 1) * 10) <= 1)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseClaws>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                    }
                }
                if (NPC.localAI[2] == 0f)
                {
                    Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)Main.rand.Next(NPC.width), NPC.position.Y + (float)Main.rand.Next(NPC.height)), NPC.velocity, 378, NPC.scale);
                    Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)Main.rand.Next(NPC.width), NPC.position.Y + (float)Main.rand.Next(NPC.height)), NPC.velocity, 379, NPC.scale);
                    Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)Main.rand.Next(NPC.width), NPC.position.Y + (float)Main.rand.Next(NPC.height)), NPC.velocity, 380, NPC.scale);
                    NPC.localAI[2] = 1f;
                }
                NPC.localAI[1] += 1f;
                if ((double)NPC.life < (double)NPC.lifeMax * 0.4)
                {
                    NPC.localAI[1] += 1f;
                }
                if ((double)NPC.life < (double)NPC.lifeMax * 0.3)
                {
                    NPC.localAI[1] += 1f;
                }
                if ((double)NPC.life < (double)NPC.lifeMax * 0.2)
                {
                    NPC.localAI[1] += 1f;
                }
                if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
                {
                    NPC.localAI[1] += 1f;
                }
                if (NPC.localAI[1] >= 350f)
                {
                    float num729 = 8f;
                    Vector2 vector90 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float num730 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector90.X + (float)Main.rand.Next(-10, 11);
                    float num731 = Math.Abs(num730 * 0.2f);
                    float num732 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector90.Y + (float)Main.rand.Next(-10, 11);
                    if (num732 > 0f)
                    {
                        num731 = 0f;
                    }
                    num732 -= num731;
                    float num733 = (float)Math.Sqrt((double)(num730 * num730 + num732 * num732));
                    num733 = num729 / num733;
                    num730 *= num733;
                    num732 *= num733;
                    int num734 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityRoseSpore>(), 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num734].velocity.X = num730;
                    Main.npc[num734].velocity.Y = num732;
                    Main.npc[num734].netUpdate = true;
                    NPC.localAI[1] = 0f;
                    return;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                SoC.ComeBack = true;
                int num440 = 0;
                while ((double)num440 < hit.Damage / (double)NPC.lifeMax * 100.0)
                {
                    if (NPC.life > NPC.lifeMax / 2 && Main.rand.Next(3) != 0)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), (float)hit.HitDirection, -1f, 0, default(Color), 1f);
                    }
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                if (Main.rand.Next(3) != 0)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), (float)(2 * hit.HitDirection), -2f, 0, default(Color), 1f);
                }
                else
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), (float)(2 * hit.HitDirection), -2f, 0, default(Color), 1f);
                }
            }
            
        }
    }
}