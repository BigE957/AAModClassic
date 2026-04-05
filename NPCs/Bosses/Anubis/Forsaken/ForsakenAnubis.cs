using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.Anubis.Forsaken;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    [AutoloadBossHead]
    public class ForsakenAnubis : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis; Forsaken Judge");
            Main.npcFrameCount[NPC.type] = 12;
        }

        public override void SetDefaults()
        {
            NPC.width = 88;
            NPC.height = 180;
            NPC.aiStyle = -1;
            NPC.damage = 55;
            NPC.defense = 60;
            NPC.lifeMax = 150000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AnubisA");
            NPC.value = Item.sellPrice(0, 10, 0, 0);
        }

        public float[] internalAI = new float[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
            NPC.damage = (int)(NPC.damage * 0.85f);
        }

        public int RuneCount = 9;

        bool text = false;

        public override void AI()
        {
            int TeleportCount = 0;

            if (NPC.life < (int)(NPC.lifeMax * .75f))
            {
                TeleportCount = 1;
            }

            if (NPC.life < (int)(NPC.lifeMax * .5f))
            {
                TeleportCount = 2;
            }

            if (NPC.life < (int)(NPC.lifeMax * .25f))
            {
                TeleportCount = 3;
            }

            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (player.Center.X < NPC.Center.X)
            {
                NPC.direction = NPC.spriteDirection = 1;
            }
            else
            {
                NPC.direction = NPC.spriteDirection = -1;
            }

            NPC.dontTakeDamage = false;
            NPC.noGravity = true;

            if (internalAI[0] == 0)
            {
                NPC.velocity.Y += 0.002f;
                if (NPC.velocity.Y > .1f)
                {
                    internalAI[0] = 1f;
                    NPC.netUpdate = true;
                }
            }
            else
            if (internalAI[0] == 1)
            {
                NPC.velocity.Y -= 0.002f;
                if (NPC.velocity.Y < -.1f)
                {
                    internalAI[0] = 0f;
                    NPC.netUpdate = true;
                }
            }

            if (NPC.life < NPC.lifeMax / 3)
            {
                if (internalAI[2] == 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int m = 0; m < RuneCount; m++)
                        {
                            int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("CurseGlyphs").Type, NPC.damage / 2, 0, Main.myPlayer);
                            Main.projectile[p].Center = NPC.Center;
                            Main.projectile[p].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                            Main.projectile[p].velocity *= 8f;
                            Main.projectile[p].ai[0] = m;
                            Main.projectile[p].netUpdate2 = true;
                            int dustType = ModContent.DustType<Dusts.JudgementDust>();
                            int pieCut = 20;
                            for (int i = 0; i < pieCut; i++)
                            {
                                int dustID = Dust.NewDust(Main.projectile[p].position, Main.projectile[p].width, Main.projectile[p].height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), i / (float)pieCut * 6.28f);
                                Main.dust[dustID].noLight = false;
                                Main.dust[dustID].noGravity = true;
                            }
                            for (int i = 0; i < pieCut; i++)
                            {
                                int dustID = Dust.NewDust(Main.projectile[p].position, Main.projectile[p].width, Main.projectile[p].height, dustType, 0f, 0f, 100, Color.White, 2f);
                                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), i / (float)pieCut * 6.28f);
                                Main.dust[dustID].noLight = false;
                                Main.dust[dustID].noGravity = true;
                            }
                        }
                    }
                    internalAI[2] = 1;
                }
                NPC.damage = 70;
            }

            NPC.ai[1]++;

            switch (NPC.ai[0])
            {
                case 0:
                    if (!AliveCheck(player))
                        break;

                    if (NPC.ai[1] >= 200)
                    {
                        internalAI[1]++;
                        if (internalAI[3] < TeleportCount && internalAI[1] >= 50)
                        {
                            internalAI[3]++;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        else
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            internalAI[3] = 0;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        return;
                    }

                    int proj = Main.rand.Next(2) == 0 ? ModContent.ProjectileType<ForsakenBlast>() : ModContent.ProjectileType<ForsakenSkull>();

                    int damage = NPC.damage / 2;

                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, proj, ref NPC.ai[3], 60, damage, 10, true);

                    if (NPC.ai[3] == 30)
                    {
                        Teleport();
                    }
                    break;
                case 1:
                    if (!AliveCheck(player))
                        break;

                    if (NPC.ai[1] >= 130)
                    {
                        internalAI[1]++;
                        if (internalAI[3] < TeleportCount && internalAI[1] >= 50)
                        {
                            internalAI[3]++;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        else
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            internalAI[3] = 0;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        return;
                    }

                    if (!text)
                    {
                        text = true;
                        CombatText.NewText(NPC.Hitbox, Color.ForestGreen, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.FAnubisCombat"), true);
                    }

                    if (NPC.ai[1] == 10)
                    {
                        if (Main.rand.Next(2) == 0 && NPC.life < NPC.lifeMax * (2/3))
                        {
                            if (NPC.life < NPC.lifeMax / 3)
                            {
                                int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X - 150, NPC.Center.Y);
                                Main.npc[a].Center = NPC.Center;
                                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X + 150, NPC.Center.Y);
                                Main.npc[b].Center = NPC.Center;
                                int c = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X, NPC.Center.Y - 150);
                                Main.npc[c].Center = NPC.Center;
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X, NPC.Center.Y + 150);
                                Main.npc[d].Center = NPC.Center;
                            }
                            else
                            {
                                int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X - 180, NPC.Center.Y - 60);
                                Main.npc[a].Center = NPC.Center;
                                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X + 180, NPC.Center.Y - 60);
                                Main.npc[b].Center = NPC.Center;
                                int c = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<HorusSummon>(), 0, 0, Main.myPlayer, NPC.Center.X, NPC.Center.Y - 200);
                                Main.npc[c].Center = NPC.Center;
                            }
                        }
                        else
                        {
                            if (NPC.life < NPC.lifeMax / 2)
                            {
                                int m = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 130, (int)NPC.position.Y, ModContent.NPCType<CurseCircle>());
                                Main.npc[m].Center = new Vector2(NPC.Center.X + 130, NPC.Center.Y);

                                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 130, (int)NPC.position.Y, ModContent.NPCType<CurseCircle>());
                                Main.npc[n].Center = new Vector2(NPC.Center.X - 130, NPC.Center.Y);

                                int o = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 130, ModContent.NPCType<CurseCircle>());
                                Main.npc[o].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 130);

                                int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y - 130, ModContent.NPCType<CurseCircle>());
                                Main.npc[p].Center = new Vector2(NPC.Center.X, NPC.Center.Y - 130);
                            }
                            else
                            {
                                int m = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 130, (int)NPC.position.Y, ModContent.NPCType<CurseCircle>());
                                Main.npc[m].Center = new Vector2(NPC.Center.X + 130, NPC.Center.Y - 60);

                                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 130, (int)NPC.position.Y, ModContent.NPCType<CurseCircle>());
                                Main.npc[n].Center = new Vector2(NPC.Center.X - 130, NPC.Center.Y - 60);

                                int o = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 130, ModContent.NPCType<CurseCircle>());
                                Main.npc[o].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 130);
                            }
                        }
                    }
                    break;
                case 2:
                    if (!AliveCheck(player))
                        break;

                    if (NPC.ai[1] == 120)
                    {
                        BaseAI.FireProjectile(player.position, NPC.position, ModContent.ProjectileType<ForsakenStaff>(), NPC.damage / 2, 14, 10, -1);
                    }
                    if (NPC.ai[1] == 140)
                    {
                        ScepterTeleport();
                    }

                    if (NPC.ai[1] > 160 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<ForsakenStaff>()))
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        Teleport();
                    }

                    break;

                case 3:
                    if (!AliveCheck(player))
                        break;

                    int Max = 3;

                    if (NPC.life < NPC.lifeMax / 2)
                    {
                        Max = 4;
                    }

                    if (NPC.ai[1] > 120 &&  Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float rotation = 2f * (float)Math.PI / Max;
                        Vector2 vel = NPC.velocity;
                        vel.Normalize();
                        vel *= 10f;
                        int type = Mod.Find<ModProjectile>("SunSummon").Type;
                        for (int i = 0; i < Max; i++)
                        {
                            vel = vel.RotatedBy(rotation);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, type, 0, 4, Main.myPlayer, NPC.direction > 0 ? -1f : 1f, 6f);
                        }
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        Teleport();
                        NPC.netUpdate = true;
                    }
                    break;

                case 4:
                    if (!AliveCheck(player))
                        break;

                    if (NPC.ai[1] >= 320)
                    {
                        internalAI[1]++;
                        if (internalAI[3] < TeleportCount && internalAI[1] >= 50)
                        {
                            internalAI[3]++;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        else
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            internalAI[3] = 0;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        return;
                    }

                    int proj1 = ModContent.ProjectileType<AnubisSoul>();

                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, proj1, ref NPC.ai[3], 100, NPC.damage / 2, 10, true);

                    if (NPC.ai[3] == 50)
                    {
                        Teleport();
                    }

                    break;

                case 5:
                    if (!AliveCheck(player))
                        break;
                    if (NPC.life > NPC.lifeMax / 2)
                    {
                        if (NPC.ai[1] > 160 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block>()))
                        {
                            internalAI[1]++;
                            if (internalAI[3] < TeleportCount && internalAI[1] >= 50)
                            {
                                internalAI[3]++;
                                internalAI[1] = 0;
                                Teleport();
                            }
                            else
                            {
                                NPC.ai[0]++;
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                                NPC.ai[3] = 0;
                                internalAI[3] = 0;
                                internalAI[1] = 0;
                                Teleport();
                            }
                            return;
                        }
                        if (NPC.ai[1] == 40)
                        {
                            int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<BlockF>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                            int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<BlockF>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                            Main.projectile[l].ai[1] = r;
                            Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                            Main.projectile[r].ai[1] = l;
                            Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                        }
                        if (NPC.ai[1] == 80)
                        {
                            int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<BlockF1>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                            int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<BlockF1>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                            Main.projectile[u].ai[1] = d;
                            Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                            Main.projectile[d].ai[1] = u;
                            Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                        }
                    }
                    else
                    {
                        if (NPC.ai[1] > 240 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<BlockF>()))
                        {
                            internalAI[1]++;
                            if (internalAI[3] < TeleportCount && internalAI[1] >= 50)
                            {
                                internalAI[3]++;
                                internalAI[1] = 0;
                                Teleport();
                            }
                            else
                            {
                                NPC.ai[0]++;
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                                NPC.ai[3] = 0;
                                internalAI[3] = 0;
                                internalAI[1] = 0;
                                Teleport();
                            }
                            return;
                        }
                        if (NPC.ai[1] % 30 == 0 && NPC.ai[1] <= 240)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<BlockF>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<BlockF>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            else
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<BlockF1>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<BlockF1>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }

                    }
                    break;

                case 6:
                    if (!AliveCheck(player))
                        break;
                    if (NPC.ai[1] > 180)
                    {
                        internalAI[1]++;
                        if (internalAI[3] < TeleportCount && internalAI[1] >= 50)
                        {
                            internalAI[3]++;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        else
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            internalAI[3] = 0;
                            internalAI[1] = 0;
                            Teleport();
                        }
                        return;
                    }
                    if (NPC.ai[1] == 120)
                    {
                        if (NPC.life > NPC.lifeMax / 2)
                        {
                            int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-250, 0), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[l].Center = player.Center + new Vector2(-250, 0);
                            Kaboom(Main.projectile[l]);
                            int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(250, 0), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[r].Center = player.Center + new Vector2(250, 0);
                            Kaboom(Main.projectile[r]);
                            int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -250), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[u].Center = player.Center + new Vector2(0, -250);
                            Kaboom(Main.projectile[u]);
                            int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 250), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[d].Center = player.Center + new Vector2(0, 250);
                            Kaboom(Main.projectile[d]);
                        }
                        else
                        {
                            int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-250, 0), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[a].Center = player.Center + new Vector2(-250, 0);
                            Kaboom(Main.projectile[a]);
                            int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(250, 0), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[b].Center = player.Center + new Vector2(250, 0);
                            Kaboom(Main.projectile[b]);
                            int c = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -250), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[c].Center = player.Center + new Vector2(0, -250);
                            Kaboom(Main.projectile[c]);
                            int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 250), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[d].Center = player.Center + new Vector2(0, 250);
                            Kaboom(Main.projectile[d]);
                            int e = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-200, 200), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[e].Center = player.Center + new Vector2(-200, 200);
                            Kaboom(Main.projectile[e]);
                            int f = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(200, 200), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[f].Center = player.Center + new Vector2(200, 200);
                            Kaboom(Main.projectile[f]);
                            int g = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(200, -200), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[g].Center = player.Center + new Vector2(200, -200);
                            Kaboom(Main.projectile[g]);
                            int h = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-200, -200), Vector2.Zero, ModContent.ProjectileType<AnubisFireball>(), NPC.damage / 2, 7, Main.myPlayer);
                            Main.projectile[h].Center = player.Center + new Vector2(-200, -200);
                            Kaboom(Main.projectile[h]);
                        }
                    }
                    break;

                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void OnKill()
        {
            NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<TownNPCs.Legendscribe>());

            if (!AAWorld.downedAnubisA)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.FAnubisWin"), Color.ForestGreen);
            }

            AAWorld.downedAnubisA = true;

            if (Main.rand.Next(10) == 0)
            {
                NPC.DropLoot(Mod.Find<ModItem>("FAnubisTrophy").Type);
            }

            if (Main.expertMode)
            {
                NPC.DropLoot(ModContent.ItemType<FAnubisBag>());
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    NPC.DropLoot(Mod.Find<ModItem>("FAnubisMask").Type);
                }
                NPC.DropLoot(Mod.Find<ModItem>("SoulFragment").Type, Main.rand.Next(8, 16));
                string[] lootTable = { "Verdict", "Lifeline", "ForsakenStaff", "Soulsplitter", "CursedFury", "HorusCane" };
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
            }
        }

        int deathtimer = 0;

        public bool AliveCheck(Player player)
        {
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f || !player.ZoneDesert)
            {
                NPC.TargetClosest();
                if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f || !player.ZoneDesert)
                {
                    deathtimer++;
                    if (Main.netMode != NetmodeID.MultiplayerClient && deathtimer > 240)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.FAnubis"), Color.ForestGreen);
                        int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TownNPCs.Legendscribe>());
                        Main.npc[a].Center = NPC.Center;
                        NPC.active = false;
                    }
                    return false;
                }
                else
                {
                    deathtimer = 0;
                }
            }
            deathtimer = 0;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 7)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.ai[0] == 2 && NPC.ai[1] >= 120)
                {
                    if (NPC.frame.Y > frameHeight * 11 || NPC.frame.Y < frameHeight * 6 )
                    {
                        NPC.frame.Y = frameHeight * 6;
                    }
                }
                else
                {
                    if (NPC.frame.Y > frameHeight * 5)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
        }

        public void Teleport()
        {
            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }

            Player player = Main.player[NPC.target];
            Vector2 targetPos = player.Center;
            int posX = Main.rand.Next(-400, 400);

            int posY = Main.rand.Next(0, 400);
            if (posX > -150 && posX < 150)
            {
                posY = Main.rand.Next(150, 400);
            }

            NPC.position = new Vector2(targetPos.X + posX, targetPos.Y - posY);
            int pieCut = 20;
            SoundEngine.PlaySound(SoundID.Item14, NPC.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public void Kaboom(Projectile p)
        {
            Vector2 position = p.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public void ScepterTeleport()
        {
            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }

            Vector2 targetPos = Main.player[NPC.target].Center;
            targetPos.X += 300 * (NPC.Center.X < targetPos.X ? 1 : -1);
            targetPos.Y -= 300;
            NPC.position = targetPos;

            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 1f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 1.5f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }
}