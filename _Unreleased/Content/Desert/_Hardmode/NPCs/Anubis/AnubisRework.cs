using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Desert._Hardmode.NPCs.Anubis.Runes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.NPCs.Bosses.Anubis;
using AAModClassic.NPCs.Bosses.Anubis.Forsaken;
using AAModClassic.NPCs.TownNPCs;
using AAModClassic.UI.Titles;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Desert._Hardmode.NPCs.Anubis
{
    public class AnubisRework : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Legendscribe");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.damage = 35;
            NPC.defense = 40;
            NPC.lifeMax = 30000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Anubis"];
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<AnubisBag>();
            NPC.value = Item.sellPrice(0, 1, 0, 0);
            NPC.noTileCollide = false;
        }

        public float[] internalAI = new float[4];
        
        Vector2 movePoint = Vector2.Zero;
        Vector2 DashPoint = Vector2.Zero;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(movePoint.X);
                writer.Write(movePoint.Y);
                writer.Write(DashPoint.X);
                writer.Write(DashPoint.Y);
            }
        }

        /* [0] = Attack Type
         * [1] = Attack Timer
         * [2] = Shooting Timer
         * [3] = Upcoming Attack
         */

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
                movePoint.X = reader.ReadSingle();
                movePoint.Y = reader.ReadSingle();
                DashPoint.X = reader.ReadSingle();
                DashPoint.Y = reader.ReadSingle();
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
            NPC.damage = (int)(NPC.damage * 0.85f);
        }

        public int LocustCount = Main.expertMode ? 6 : 4;

        public override void AI()
        {
            NPC.velocity.X *= 0;

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

            if (internalAI[0] != 1)
            {
                NPC.noGravity = false;
                Preamble();
                return;
            }
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = false;
            NPC.noGravity = true;

            if (NPC.ai[0] == 6)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.damage != 50)
                {
                    NPC.velocity.Y *= 0f;
                    NPC.damage = 50;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.damage != 35)
                {
                    NPC.damage = 35;
                    NPC.netUpdate = true;
                }
                if (internalAI[3] == 0)
                {
                    NPC.velocity.Y += 0.002f;
                    if (NPC.velocity.Y > .1f)
                    {
                        internalAI[3] = 1f;
                        NPC.netUpdate = true;
                    }
                }
                else if (internalAI[3] == 1)
                {
                    NPC.velocity.Y -= 0.002f;
                    if (NPC.velocity.Y < -.1f)
                    {
                        internalAI[3] = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }

            if (NPC.life < NPC.lifeMax / 3 && internalAI[2] == 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int m = 0; m < LocustCount; m++)
                    {
                        int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Locust>(), 0);
                        Main.npc[npcID].Center = NPC.Center;
                        Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                        Main.npc[npcID].velocity *= 8f;
                        Main.npc[npcID].ai[0] = m;
                        Main.npc[npcID].netUpdate2 = true;
                        if (!Main.expertMode)
                        {
                            Main.npc[npcID].ai[2] = 0;
                            if (m == 0 || m == 2)
                            {
                                Main.npc[npcID].ai[2] = 40;
                            }
                        }
                        else
                        {
                            Main.npc[npcID].ai[2] = 0;
                            if (m == 0 || m == 3)
                            {
                                Main.npc[npcID].ai[2] = 40;
                            }
                            else if (m == 2 || m == 4)
                            {
                                Main.npc[npcID].ai[2] = 80;
                            }
                        }
                        int dustType = ModContent.DustType<Dusts.JudgementDust>();
                        int pieCut = 20;
                        for (int i = 0; i < pieCut; i++)
                        {
                            int dustID = Dust.NewDust(Main.npc[npcID].position, Main.npc[npcID].width, Main.npc[npcID].height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                            Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), i / (float)pieCut * 6.28f);
                            Main.dust[dustID].noLight = false;
                            Main.dust[dustID].noGravity = true;
                        }
                        for (int i = 0; i < pieCut; i++)
                        {
                            int dustID = Dust.NewDust(Main.npc[npcID].position, Main.npc[npcID].width, Main.npc[npcID].height, dustType, 0f, 0f, 100, Color.White, 2f);
                            Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), i / (float)pieCut * 6.28f);
                            Main.dust[dustID].noLight = false;
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
                internalAI[2] = 1;
            }

            Vector2 targetPos = player.Center;
            NPC.ai[1]++;
            switch (NPC.ai[0])
            {
                case 0:
                    if (NPC.ai[3] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[3] = Main.rand.Next(5) + 1;

                        if (NPC.ai[3] == 5)
                        {
                            int posX;
                            if (player.position.X > NPC.position.X)
                            {
                                posX = -400;
                            }
                            else
                            {
                                posX = 400;
                            }
                            movePoint = new Vector2(targetPos.X + posX, targetPos.Y);
                        }
                        else
                        {
                            int posX = Main.rand.Next(-400, 400);

                            int posY = Main.rand.Next(0, 400);
                            if (posX > -150 && posX < 150)
                            {
                                posY = Main.rand.Next(150, 400);
                            }
                            movePoint = new Vector2(targetPos.X + posX, targetPos.Y - posY);
                        }

                        if (NPC.life < (int)(NPC.lifeMax * .66) && Main.rand.Next(4 - Repeat()) == 0)
                        {
                            if (Main.rand.Next(2) == 0 && NPC.life < NPC.lifeMax / 3)
                            {
                                int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X + 100, NPC.Center.Y), Vector2.Zero, ModContent.ProjectileType<EyeSummon>(), 0, 0, Main.myPlayer, NPC.Center.X - 200, NPC.Center.Y);
                                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X - 100, NPC.Center.Y), Vector2.Zero, ModContent.ProjectileType<EyeSummon>(), 0, 0, Main.myPlayer, NPC.Center.X + 200, NPC.Center.Y);
                            }
                            else
                            {
                                int m = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 100, (int)NPC.position.Y, ModContent.NPCType<MinionCircle>());
                                Main.npc[m].Center = new Vector2(NPC.Center.X + 100, NPC.Center.Y);

                                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 100, (int)NPC.position.Y, ModContent.NPCType<MinionCircle>());
                                Main.npc[n].Center = new Vector2(NPC.Center.X - 100, NPC.Center.Y);

                                if (NPC.life < NPC.lifeMax / 2)
                                {
                                    int o = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 100, ModContent.NPCType<MinionCircle>());
                                    Main.npc[o].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 100);

                                    int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y - 100, ModContent.NPCType<MinionCircle>());
                                    Main.npc[p].Center = new Vector2(NPC.Center.X, NPC.Center.Y - 100);
                                }
                            }
                        }

                        NPC.netUpdate = true;
                    }

                    if (NPC.ai[1] == 40 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AnubisCircle>(), 0, movePoint.X, movePoint.Y, NPC.ai[3] - 1, NPC.whoAmI);
                        NPC.netUpdate = true;
                    }

                    if (NPC.ai[1] >= 80 - (10 * Repeat()))
                    {
                        MoveToPoint(movePoint);

                        if (Vector2.Distance(NPC.Center, movePoint) <= 10)
                        {
                            NPC.velocity *= 0;
                            NPC.ai[0] = NPC.ai[3];
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                        }
                    }

                    break;

                #region Shoot Stuff
                case 1:

                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<Runeblast>(), ref NPC.ai[2], 80, NPC.damage / 2, 10, true);

                    if (NPC.ai[2] == 79)
                    {
                        int posX = Main.rand.Next(-400, 400);

                        int posY = Main.rand.Next(0, 400);
                        if (posX > -150 && posX < 150)
                        {
                            posY = Main.rand.Next(150, 400);
                        }

                        movePoint = new Vector2(targetPos.X + posX, targetPos.Y - posY);
                    }

                    MoveToPoint(movePoint);

                    if (NPC.ai[1] == 241 + (80 * Repeat()))
                    {
                        ResetAI();
                    }
                    break;
                #endregion

                #region Scepter Throw
                case 2:
                    if (NPC.ai[1] == 120)
                    {
                        BaseAI.FireProjectile(player.position, NPC.position, ModContent.ProjectileType<Scepter>(), NPC.damage / 2, 14, 10, -1);
                    }
                    if (NPC.ai[1] == 160)
                    {
                        targetPos.X += 300 * (NPC.Center.X < targetPos.X ? 1 : -1);
                        targetPos.Y -= 300;
                        movePoint = targetPos;
                    }
                    if (NPC.ai[1] >= 160)
                    {
                        MoveToPoint(movePoint);
                    }

                    if (NPC.ai[1] > 160 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Scepter>()))
                    {
                        ResetAI();
                    }
                    break;
                #endregion 

                #region Block Crush
                case 3:
                    if (NPC.life > (int)(NPC.lifeMax * .66f))
                    {
                        if (NPC.ai[1] == 60)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Block>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Block>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            else
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Block1>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Block1>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }

                        if (NPC.ai[1] > 120 && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block>()) && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block1>()))
                        {
                            ResetAI();
                        }
                    }
                    else if (NPC.life < (int)(NPC.lifeMax * .66f))
                    {
                        if (NPC.ai[1] == 50)
                        {
                            int l = Projectile.NewProjectile(NPC.GetSource_FromThis(),  player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Block>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                            int r = Projectile.NewProjectile(   NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Block>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                            Main.projectile[l].ai[1] = r;
                            Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                            Main.projectile[r].ai[1] = l;
                            Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                        }
                        if (NPC.ai[1] == 100)
                        {
                            int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Block1>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                            int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Block1>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                            Main.projectile[u].ai[1] = d;
                            Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                            Main.projectile[d].ai[1] = u;
                            Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                        }
                        if (NPC.ai[1] > 180 && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block>()) && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block1>()))
                        {
                            ResetAI();
                        }
                    }
                    else if (NPC.life < NPC.lifeMax / 3)
                    {
                        if (NPC.ai[1] % 40 == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Block>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Block>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            else
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Block1>(), NPC.damage / 2, 7, Main.myPlayer, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Block1>(), NPC.damage / 2, 7, Main.myPlayer, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }

                        if (NPC.ai[1] > 270 && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block>()) && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Block1>()))
                        {
                            ResetAI();
                        }
                    }
                    break;
                #endregion

                #region Axe
                case 4:
                    if (NPC.ai[1] == 80)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                            {
                                Vector2 goal = player.Center - NPC.Center;

                                float vyi = 10;
                                float vyisq = 100;
                                float g = 0.2f;

                                float vyf = -MathF.Sqrt(vyisq + 2 * g * goal.Y);
                                float t = Math.Abs((vyf - vyi) / g);

                                float vxi = goal.X / t;

                                Vector2 velocity = new Vector2(float.IsNaN(vxi) ? (goal.X > 0 ? 10 : -10) : vxi, -vyi);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<Axe>(), NPC.damage / 2, 0.5f, -1);
                            }
                            else
                                BaseAI.FireProjectile(player.position, NPC.position, ModContent.ProjectileType<Axe>(), NPC.damage / 2, 14, 10, -1);
                        }
                    }
                    if (NPC.ai[1] == 86)
                    {
                        ResetAI();
                    }

                    break;
                #endregion

                #region Claws Prep
                case 5:
                    if (NPC.ai[1] == 30 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int posX;
                        if (player.position.X > NPC.position.X)
                        {
                            posX = 400;
                        }
                        else
                        {
                            posX = -400;
                        }
                        DashPoint = new Vector2(targetPos.X + posX, targetPos.Y);

                        NPC.ai[0] = 6;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                #endregion

                #region Claws
                case 6:
                    if (NPC.ai[1] < 36)
                    {
                        MoveToPoint(DashPoint);
                    }
                    else
                    {
                        NPC.velocity.X *= .95f;
                    }

                    if (NPC.ai[1] >= 54)
                    {
                        ResetAI();
                    }

                    break;
                #endregion

                default:
                    NPC.ai[0] = 1;
                    goto case 1;

            }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;
        }

        public void ResetAI()
        {
            NPC.velocity *= 0;
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            NPC.ai[3] = 0;
        }

        public int Repeat()
        {
            if (NPC.life < (int)(NPC.lifeMax * .66))
            {
                return 2;
            }
            if (NPC.life < (int)(NPC.lifeMax * .66))
            {
                return 1;
            }
            return 0;
        }

        public void MoveToPoint(Vector2 point)
        {
            float Speed = 13;

            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < Speed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / Speed);
            }
            if (length < 200f)
            {
                Speed *= 0.5f;
            }
            if (length < 100f)
            {
                Speed *= 0.5f;
            }
            if (length < 50f)
            {
                Speed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= Speed;
            NPC.velocity *= velMultiplier;
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
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisFalse"), Color.Gold);
                        int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Legendscribe>());
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

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool PreKill()
        {
            if (NPC.downedMoonlord && NPCExtensions.BeenKilled<AAModClassic.NPCs.Bosses.Anubis.Anubis>())
            {
                if (!AAWorld.AnubisAwakened)
                    AAWorld.AnubisAwakened = true;

                NPC.boss = false;
            }
            return true;
        }

        public override void OnKill()
        {
            Main.BestiaryTracker.Kills.RegisterKill(ContentSamples.NpcsByNetId[ModContent.NPCType<AAModClassic.NPCs.Bosses.Anubis.Anubis>()]);
            if (NPC.downedMoonlord && NPCExtensions.BeenKilled<AAModClassic.NPCs.Bosses.Anubis.Anubis>(true))
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<FATransition>());
            else
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Legendscribe>());
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AnubisTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AnubisTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AnubisMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ForsakenFragment>(), 1, 8, 16));

            int[] lootTable = { ModContent.ItemType<Judgment>(), ModContent.ItemType<NeithsString>(), ModContent.ItemType<DesertStaff>(), ModContent.ItemType<JackalsWrath>(), ModContent.ItemType<Sandthrower>(), ModContent.ItemType<SentryOfTheEye>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            npcLoot.Add(notExpertRule);
        }

        int PreludeFrame = 0;
        int AxeFrame = 0;
        int ClawFrame1 = 0;
        int ClawFrame2 = 0;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 6)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                PreludeFrame++; 
                AxeFrame++;
            }
            //Prelude
            if (internalAI[0] == 0)
            {
                if (internalAI[1] >= 240 && internalAI[1] < 320)
                {
                    if (PreludeFrame < 9)
                    {
                        PreludeFrame = 9;
                    }
                    if (PreludeFrame >= 10)
                    {
                        PreludeFrame = 10;
                    }
                }
                else
                {
                    if (NPC.velocity.Y == 0)
                    {
                        if (PreludeFrame < 4 || PreludeFrame > 8)
                        {
                            PreludeFrame = 4;
                        }
                    }
                    else
                    {
                        if (NPC.frame.Y > 3)
                        {
                            PreludeFrame = 0;
                        }
                    }
                }
            }
            else
            {
                //Axe
                if (NPC.ai[0] == 4)
                {
                    if (NPC.ai[1] < 80 && AxeFrame > 3)
                    {
                        AxeFrame = 3;
                    }
                    if (NPC.ai[0] == 80)
                    {
                        AxeFrame = 4;
                    }
                }
                //Claws prep
                else if (NPC.ai[0] == 5)
                {
                    if (NPC.ai[1] % 6 == 0)
                    {
                        ClawFrame1++;
                        if (ClawFrame1 > 3)
                        {
                            ClawFrame1 = 3;
                        }
                    }
                }
                //Claws
                else if (NPC.ai[0] == 6)
                {
                    if (NPC.ai[1] % 6 == 0)
                    {
                        ClawFrame2++;
                        if (ClawFrame2 > 7)
                        {
                            ClawFrame2 = 7;
                        }
                    }
                }
                //Idle
                else
                {
                    AxeFrame = 0;
                    ClawFrame1 = 0;
                    ClawFrame2 = 0;
                    if (NPC.frame.Y > frameHeight * 3)
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
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
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
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
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
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
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
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
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

        public void Preamble()
        {
            NPC.dontTakeDamage = true;

            NPC.ai[3] = 39;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/silence");
                if (NPC.velocity.Y == 0)
                {
                    if (internalAI[1]++ < 420)
                    {
                        if (!NPCExtensions.BeenKilled<AAModClassic.NPCs.Bosses.Anubis.Anubis>())
                        {
                            if (internalAI[1] == 60)
                            {
                                int activePlayers = 0;
                                foreach (Player p in Main.ActivePlayers)
                                    activePlayers++;
                                string s = activePlayers > 1 ? "Multiplayer" : "Singleplayer";
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.1." + s), Color.Gold);
                            }

                            if (internalAI[1] == 150)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.2"), Color.Gold);
                            }

                            if (internalAI[1] == 240)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.3"), Color.Gold);
                            }

                            if (internalAI[1] == 320)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.4"), Color.Gold);
                            }

                            if (internalAI[1] >= 410)
                            {
                                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Anubis");
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.5"), Color.Gold);
                                internalAI[0] = 1;
                                NPC.ai[3] = 0;
                                NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                                Teleport();
                                NPC.netUpdate = true;
                            }
                        }
                        else
                        {
                            Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Anubis");
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.Rematch"), Color.Gold);
                            internalAI[0] = 1;
                            NPC.ai[3] = 0;
                            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                            Teleport();
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(150, 255, 150) * (Main.mouseTextColor / 255f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            string path = "_Unreleased/Content/Desert/_Hardmode/NPCs/Anubis/";
            Texture2D PreludeTex = Mod.GetTexture(path + "AnubisPrelude");
            Texture2D AxeTex = Mod.GetTexture(path + "AnubisAxe");
            Texture2D ClawTex1 = Mod.GetTexture(path + "AnubisClaws1");
            Texture2D ClawTex2 = Mod.GetTexture(path + "AnubisClaws2");
            Texture2D Glow = Mod.GetTexture(path + "Glow/Anubis_Glow");
            Texture2D PreludeGlow = Mod.GetTexture(path + "Glow/AnubisPrelude_Glow");
            Texture2D AxeGlow = Mod.GetTexture(path + "Glow/AnubisAxe_Glow");
            Texture2D ClawGlow1 = Mod.GetTexture(path + "Glow/AnubisClaws1_Glow");
            Texture2D ClawGlow2 = Mod.GetTexture(path + "Glow/AnubisClaws2_Glow");
            if (internalAI[0] != 1)
            {
                Rectangle frame = BaseDrawing.GetFrame(PreludeFrame, PreludeTex.Width, PreludeTex.Height / 11, 0, 0);
                BaseDrawing.DrawTexture(spriteBatch, PreludeTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 11, frame, drawColor, true);
                BaseDrawing.DrawTexture(spriteBatch, PreludeGlow, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 11, frame, AAColor.COLOR_WHITEFADE1, true);
            }
            else
            {
                if (NPC.ai[0] == 4)
                {
                    Rectangle frame = BaseDrawing.GetFrame(AxeFrame, AxeTex.Width, AxeTex.Height / 5, 0, 0);
                    BaseDrawing.DrawTexture(spriteBatch, AxeTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 5, frame, drawColor, true);
                    BaseDrawing.DrawTexture(spriteBatch, AxeGlow, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 5, frame, AAColor.COLOR_WHITEFADE1, true);
                }
                else if (NPC.ai[0] == 5)
                {
                    Rectangle frame = BaseDrawing.GetFrame(ClawFrame1, ClawTex1.Width, ClawTex1.Height / 4, 0, 0);
                    BaseDrawing.DrawTexture(spriteBatch, ClawTex1, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 4, frame, drawColor, true);
                    BaseDrawing.DrawTexture(spriteBatch, ClawGlow1, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 4, frame, AAColor.COLOR_WHITEFADE1, true);
                }
                else if (NPC.ai[0] == 6)
                {
                    Rectangle frame = BaseDrawing.GetFrame(ClawFrame2, ClawTex2.Width, ClawTex2.Height / 8, 0, 0);
                    BaseDrawing.DrawTexture(spriteBatch, ClawTex2, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, frame, drawColor, true);
                    BaseDrawing.DrawTexture(spriteBatch, ClawGlow2, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, frame, AAColor.COLOR_WHITEFADE1, true);
                }
                else
                {
                    if (NPC.velocity.X != 0)
                    {
                        BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1, 1, 8, true, 0, 0, GetAlpha(Color.White), NPC.frame, 4);
                    }
                    BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 4, NPC.frame, drawColor, true);
                    BaseDrawing.DrawTexture(spriteBatch, Glow, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 4, NPC.frame, AAColor.COLOR_WHITEFADE1, true);
                }
            }
            return false;
        }
    }
}