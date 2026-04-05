using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Dusts;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose
{
    public class DeityRoseHook: ModNPC
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ei'Lor's Claw");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.width = 40;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.PlanteraHook;
            NPC.damage = 60;
            NPC.defense = 24;
            NPC.lifeMax = 4000;
            NPC.dontTakeDamage = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        public override void AI()
        {
            bool flag48 = false;
            bool flag49 = false;
            if (AAModGlobalNPC.Rose < 0)
            {
                //TODOSOC
                //NPC.StrikeNPCNoInteraction(9999, 0f, 0, false, false, false);
                NPC.netUpdate = true;
                return;
            }
            if (Main.player[Main.npc[AAModGlobalNPC.Rose].target].dead)
            {
                flag49 = true;
            }
            if (AAModGlobalNPC.Rose != -1 && !Main.player[Main.npc[AAModGlobalNPC.Rose].target].ZoneBeach || Main.player[Main.npc[AAModGlobalNPC.Rose].target].position.Y < Main.worldSurface * 16.0 || Main.player[Main.npc[AAModGlobalNPC.Rose].target].position.Y > (Main.maxTilesY - 200) * 16 || flag49)
            {
                NPC.localAI[0] -= 4f;
                flag48 = true;
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0f)
                {
                    NPC.ai[0] = (int)(NPC.Center.X / 16f);
                }
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[1] = (int)(NPC.Center.X / 16f);
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0f || NPC.ai[1] == 0f)
                {
                    NPC.localAI[0] = 0f;
                }
                NPC.localAI[0] -= 1f;
                if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 2)
                {
                    NPC.localAI[0] -= 2f;
                }
                if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 4)
                {
                    NPC.localAI[0] -= 2f;
                }
                if (flag48)
                {
                    NPC.localAI[0] -= 6f;
                }
                if (!flag49 && NPC.localAI[0] <= 0f && NPC.ai[0] != 0f)
                {
                    for (int num735 = 0; num735 < 200; num735++)
                    {
                        if (num735 != NPC.whoAmI && Main.npc[num735].active && Main.npc[num735].type == NPC.type && (Main.npc[num735].velocity.X != 0f || Main.npc[num735].velocity.Y != 0f))
                        {
                            NPC.localAI[0] = Main.rand.Next(60, 300);
                        }
                    }
                }
                if (NPC.localAI[0] <= 0f)
                {
                    NPC.localAI[0] = Main.rand.Next(300, 600);
                    bool flag50 = false;
                    int num736 = 0;
                    while (!flag50 && num736 <= 1000)
                    {
                        num736++;
                        int num737 = (int)(Main.player[Main.npc[AAModGlobalNPC.Rose].target].Center.X / 16f);
                        int num738 = (int)(Main.player[Main.npc[AAModGlobalNPC.Rose].target].Center.Y / 16f);
                        if (NPC.ai[0] == 0f)
                        {
                            num737 = (int)((Main.player[Main.npc[AAModGlobalNPC.Rose].target].Center.X + Main.npc[AAModGlobalNPC.Rose].Center.X) / 32f);
                            num738 = (int)((Main.player[Main.npc[AAModGlobalNPC.Rose].target].Center.Y + Main.npc[AAModGlobalNPC.Rose].Center.Y) / 32f);
                        }
                        if (flag49)
                        {
                            num737 = (int)Main.npc[AAModGlobalNPC.Rose].position.X / 16;
                            num738 = (int)(Main.npc[AAModGlobalNPC.Rose].position.Y + 400f) / 16;
                        }
                        int num739 = 20;
                        num739 += (int)(100f * (num736 / 1000f));
                        int num740 = num737 + Main.rand.Next(-num739, num739 + 1);
                        int num741 = num738 + Main.rand.Next(-num739, num739 + 1);
                        if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 2 && Main.rand.Next(6) == 0)
                        {
                            NPC.TargetClosest(true);
                            int num742 = (int)(Main.player[NPC.target].Center.X / 16f);
                            int num743 = (int)(Main.player[NPC.target].Center.Y / 16f);
                            if (Main.tile[num742, num743].WallType > WallID.None)
                            {
                                num740 = num742;
                                num741 = num743;
                            }
                        }
                        try
                        {
                            if (WorldGen.SolidTile(num740, num741) || Main.tile[num740, num741].WallType > WallID.None && (num736 > 500 || Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 2))
                            {
                                flag50 = true;
                                NPC.ai[0] = num740;
                                NPC.ai[1] = num741;
                                NPC.netUpdate = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (NPC.ai[0] > 0f && NPC.ai[1] > 0f)
            {
                float num744 = 6f;
                if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 2)
                {
                    num744 = 8f;
                }
                if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 4)
                {
                    num744 = 10f;
                }
                if (Main.expertMode)
                {
                    num744 += 1f;
                }
                if (Main.expertMode && Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax / 2)
                {
                    num744 += 1f;
                }
                if (flag48)
                {
                    num744 *= 2f;
                }
                if (flag49)
                {
                    num744 *= 2f;
                }
                Vector2 vector91 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num745 = NPC.ai[0] * 16f - 8f - vector91.X;
                float num746 = NPC.ai[1] * 16f - 8f - vector91.Y;
                float num747 = (float)Math.Sqrt((double)(num745 * num745 + num746 * num746));
                if (num747 < 12f + num744)
                {
                    NPC.velocity.X = num745;
                    NPC.velocity.Y = num746;
                }
                else
                {
                    num747 = num744 / num747;
                    NPC.velocity.X = num745 * num747;
                    NPC.velocity.Y = num746 * num747;
                }
                Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num748 = Main.npc[AAModGlobalNPC.Rose].Center.X - vector92.X;
                float num749 = Main.npc[AAModGlobalNPC.Rose].Center.Y - vector92.Y;
                NPC.rotation = (float)Math.Atan2((double)num749, (double)num748) - 1.57f;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                int num440 = 0;
                while (num440 < hit.Damage / (double)NPC.lifeMax * 100.0)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), hit.HitDirection, -1f, 0, default, 1f);
                    
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 2 * hit.HitDirection, -2f, 0, default, 1f);
                
            }
        }
    }
}