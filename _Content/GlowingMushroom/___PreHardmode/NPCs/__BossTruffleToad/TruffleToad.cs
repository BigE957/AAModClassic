using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Weapons;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos.GripOfChaosAbstract;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad
{
    [AutoloadBossHead]
    public class TruffleToad : ModNPC
    {
        public float bossLife;
        public int damage = 0;

        public int TeleCooldown = 300;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);

                writer.Write(Minion[0]);
                writer.Write(Minion[1]);
                writer.Write(Minion[2]);

                writer.Write(TeleCooldown);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
                internalAI[4] = reader.ReadSingle();

                Minion[0] = reader.ReadBoolean();
                Minion[1] = reader.ReadBoolean();
                Minion[2] = reader.ReadBoolean();

                TeleCooldown = reader.ReadInt32();
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Truffle Toad");
            Main.npcFrameCount[NPC.type] = 12;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 2000;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 1, 0, 0);
            NPC.aiStyle = -1;
            NPC.width = 98;
            NPC.height = 72;
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            Music = MusicManagementSystem.MusicSlots["TruffleToad"];
            NPC.netAlways = true;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Zombie29;
            if (Main.expertMode)
            {
                NPC.defense = 20;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
            ]);
        }

        public const int AISTATE_JUMP = 0, AISTATE_BARF = 1, AISTATE_JUMPALOT = 2, AISTATE_BUBBLES = 3, AISTATE_SEED = 4, AISTATE_STOMP = 5, AISTATE_TOADS = 6, AISTATE_BUBBLES2 = 7;
        public float[] internalAI = new float[5];
        public bool[] Minion = new bool[3];
        public bool tonguespawned = false;
        public bool TongueAttack = false;
        public float AIChangeRate = 180;
        public float JumpX = 6f, JumpY = -8f, JumpX2 = 6f, JumpY2 = -10f;

        public override void AI()
        {
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }
            NPC.TargetClosest();
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting
            AAModGlobalNPC.Toad = NPC.whoAmI;

            Vector2 tile = new Vector2(NPC.Center.X,NPC.Center.Y + NPC.height / 2);
            bool tileCheck = Main.tile[(int)(tile.X / 16), (int)(tile.Y / 16)].HasTile && (TileID.Sets.Platforms[Main.tile[(int)(tile.X / 16), (int)(tile.Y / 16)].TileType] || Main.tileSolid[Main.tile[(int)(tile.X / 16), (int)(tile.Y / 16)].TileType]);
            if (player.Center.Y + player.height / 2 >= NPC.Center.Y + NPC.height / 2 + 20f && tileCheck) 
            {
                NPC.noTileCollide = true;
                internalAI[4] = 1f;
            }
            if (internalAI[4] == 1f)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = false;
                if (player.Center.Y + player.height / 2 <= NPC.Center.Y + NPC.height / 2) 
                {
                    NPC.noTileCollide = false;
                    if(tileCheck)
                    {
                        NPC.velocity.X *= .2f;
                        NPC.velocity.Y = 0f;
                    }
                    internalAI[4] = 2f;
                }
            }
            else if (internalAI[4] == 2f)
            {
                NPC.noTileCollide = false;
                if(tileCheck)
                {
                    NPC.velocity.X *= .2f;
                    NPC.velocity.Y = 0f;
                    internalAI[4] = 0;
                }
            }

            if (player.dead || !player.active || !player.ZoneGlowshroom)
            {
                NPC.TargetClosest();
                if (player.dead || !player.active || !player.ZoneGlowshroom)
                {
                    NPC.alpha += 5;
                    if (NPC.alpha >= 255)
                    {
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }

            if (player != null)
            {
                if(TeleCooldown > 0)
                {
                    TeleCooldown --;
                }
                float dist = NPC.Distance(player.Center);
                Vector2 tileabove = new Vector2(NPC.Center.X,NPC.Center.Y - NPC.height / 2);
                Vector2 tileleft = new Vector2(NPC.Center.X - NPC.width / 2,NPC.Center.Y);
                Vector2 tileright = new Vector2(NPC.Center.X + NPC.width / 2,NPC.Center.Y);
                Vector2 tile1 = new Vector2(NPC.Center.X - NPC.width / 2,NPC.Center.Y - NPC.height / 2);
                Vector2 tile2 = new Vector2(NPC.Center.X + NPC.width / 2,NPC.Center.Y - NPC.height / 2);
                bool tileCheckabove = Main.tile[(int)(tileabove.X / 16), (int)(tileabove.Y / 16)].HasTile && Main.tileSolid[Main.tile[(int)(tileabove.X / 16), (int)(tileabove.Y / 16)].TileType];
                bool tileCheckleft = Main.tile[(int)(tileleft.X / 16), (int)(tileleft.Y / 16)].HasTile && Main.tileSolid[Main.tile[(int)(tileleft.X / 16), (int)(tileleft.Y / 16)].TileType];
                bool tileCheckright = Main.tile[(int)(tileright.X / 16), (int)(tileright.Y / 16)].HasTile && Main.tileSolid[Main.tile[(int)(tileright.X / 16), (int)(tileright.Y / 16)].TileType];
                bool tileCheck1 = Main.tile[(int)(tile1.X / 16), (int)(tile1.Y / 16)].HasTile && Main.tileSolid[Main.tile[(int)(tile1.X / 16), (int)(tile1.Y / 16)].TileType];
                bool tileCheck2 = Main.tile[(int)(tile2.X / 16), (int)(tile2.Y / 16)].HasTile && Main.tileSolid[Main.tile[(int)(tile2.X / 16), (int)(tile2.Y / 16)].TileType];
                bool tiletele = TeleCooldown == 0 && !NPC.noTileCollide && (tileCheckabove && NPC.collideY || tileCheckleft && NPC.collideX || tileCheckright && NPC.collideX || (tileCheck1 || tileCheck2) && (NPC.collideX || NPC.collideY));
                if (dist > 400 || tiletele)
                {
                    NPC.alpha += 3;
                    if (NPC.alpha >= 255)
                    {
                        Vector2 tele = new Vector2(player.Center.X, player.Center.Y - 350);
                        NPC.Center = tele;
                        if(tiletele) TeleCooldown = 300;
                        for (int m = 0; m < 6; m++)
                        {
                            Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, NPC.velocity.RotatedBy(Main.rand.NextFloat() * 3.1415926f).X * 0.2f, NPC.velocity.RotatedBy(Main.rand.NextFloat() * 3.1415926f).Y * 0.2f, ModContent.DustType<Dusts.ShroomDust>(), default, 1.5f);
                        }
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    NPC.alpha -= 5;
                    if (NPC.alpha <= 0)
                    {
                        NPC.alpha = 0;
                    }
                }
            }

            int[] Shrooms = BaseAI.GetNPCs(NPC.Center, ModContent.NPCType<LuminousAccordyceps>(), 1000);
            if (Shrooms != null && Shrooms.Length > 0)
            {
                float ShroomCount = 1 + Shrooms.Length / 10;
                NPC.damage = (int)(NPC.defDamage * ShroomCount);
                NPC.defense = (int)(NPC.defDefense * ShroomCount);
                if(internalAI[3] ++ > 20)
                {
                    if(NPC.life < NPC.lifeMax) NPC.life += Shrooms.Length;
                    internalAI[3] = 0;
                }
                AIChangeRate = 120;
                JumpX = 8f; JumpY = -10f; JumpX2 = 10f; JumpY2 = -14f;
                if (Main.netMode != NetmodeID.Server && Main.LocalPlayer.miscCounter % 2 == 0)
                {
                    for (int m = 0; m < Shrooms.Length; m++)
                    {
                        NPC npc2 = Main.npc[Shrooms[m]];
                        if (npc2 != null && npc2.active)
                        {
                            int dustID = Dust.NewDust(npc2.position, npc2.width, npc2.height, ModContent.DustType<Dusts.ShroomDust>());
                            Main.dust[dustID].position += NPC.position - NPC.oldPosition;
                            Main.dust[dustID].velocity = (NPC.Center - npc2.Center) * 0.10f;
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
            }
            else
            {
                NPC.damage = NPC.defDamage;
                NPC.defense = NPC.defDefense;
                AIChangeRate = 180;
                JumpX = 6f; JumpY = -8f; JumpX2 = 6f; JumpY2 = -10f;
            }

            if (NPC.velocity.Y != 0)
            {
                if (NPC.velocity.X < 0)
                {
                    NPC.spriteDirection = 1;
                }
                else if (NPC.velocity.X > 0)
                {
                    NPC.spriteDirection = -1;
                }
            }
            else
            {
                if (player.position.X < NPC.position.X)
                {
                    NPC.spriteDirection = 1;
                }
                else if (player.position.X > NPC.position.X)
                {
                    NPC.spriteDirection = -1;
                }
            }

            if (internalAI[0] == AISTATE_JUMP)
            {
                NPC.wet = false;
                AITortoise();
                //BaseAI.AISlime(npc, ref npc.ai, false, 20, JumpX, JumpY, JumpX2, JumpY2);
                internalAI[1]++;
                if (internalAI[1] == 179)
                {
                    SoundEngine.PlaySound(SoundID.Zombie13, NPC.position);
                }
                if (internalAI[1] >= AIChangeRate && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.velocity.X = 0f;
                    internalAI[1] = 0;
                    internalAI[0] = Main.rand.Next(Main.expertMode ? 8 : 7);
                    internalAI[2] = 0;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_BARF)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                NPC.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (NPC.velocity.Y == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 5)
                    {
                        internalAI[2] = 0;
                        if (NPC.direction == -1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_Puffball>(), damage, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_Puffball>(), damage, 3);
                        }
                        NPC.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_JUMPALOT)
            {
                internalAI[1]++;// if (npc.ai[0] < -10) npc.ai[0] = -10; //force rapid jumping
                AITortoise();
                NPC.wet = false;
                //BaseAI.AISlime(npc, ref npc.ai, false, -10, JumpX, JumpY, JumpX2, JumpY2);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[1]++;
                }
                if (internalAI[1] >= 300)
                {
                    NPC.velocity.X = 0f;
                    internalAI[1] = 0;
                    internalAI[0] = 0;
                    internalAI[2] = 0;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_BUBBLES)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                NPC.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (NPC.velocity.Y == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 8)
                    {
                        internalAI[2] = 0;
                        if (NPC.direction == -1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_FungusBubble>(), damage, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_FungusBubble>(), damage, 3); //Originally 35 damage
                        }
                        NPC.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_SEED)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                NPC.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (NPC.velocity.Y == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 25)
                    {
                        internalAI[2] = 0;
                        if (NPC.direction == -1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_WaterleafSeed>(), 0, 0);
                        }
                        else
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_WaterleafSeed>(), 0, 0);
                        }
                        NPC.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_STOMP)
            {
                if (internalAI[2] == 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0)
                    {
                        NPC.TargetClosest(true);
                        NPC.velocity.X = 6 * NPC.direction;
                        NPC.velocity.Y = -10f;
                        internalAI[2] = 1f;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (NPC.velocity.Y == 0f)
                    {
                        SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                        NPC.ai[0] = 0f;
                        for (int num622 = (int)NPC.position.X - 20; num622 < (int)NPC.position.X + NPC.width + 40; num622 += 20)
                        {
                            for (int num623 = 0; num623 < 4; num623++)
                            {
                                int num624 = Dust.NewDust(new Vector2(NPC.position.X - 20f, NPC.position.Y + NPC.height), NPC.width + 20, 4, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                                Main.dust[num624].velocity *= 0.2f;
                            }
                            int num625 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(num622 - 20, NPC.position.Y + NPC.height - 8f), default, Main.rand.Next(61, 64), 1f);
                            Main.gore[num625].velocity *= 0.4f;
                        }
                        for (int a = 0; a < 4; a++)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + Main.rand.Next(40)), (int)(NPC.position.Y + NPC.height), ModContent.NPCType<GregariousGlowshrooms>());
                        }
                        internalAI[0] = AISTATE_JUMP;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        NPC.ai = new float[4];
                        NPC.netUpdate = true;
                    }
                    else
                    {
                        NPC.TargetClosest(true);
                        if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + NPC.width > Main.player[NPC.target].position.X + Main.player[NPC.target].width)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.9f;
                            NPC.velocity.Y = NPC.velocity.Y + 0.4f;
                        }
                        else
                        {
                            if (NPC.direction < 0)
                            {
                                NPC.velocity.X = NPC.velocity.X - 0.2f;
                            }
                            else if (NPC.direction > 0)
                            {
                                NPC.velocity.X = NPC.velocity.X + 0.2f;
                            }
                            float num626 = 3f;
                            if (NPC.life < NPC.lifeMax)
                            {
                                num626 += 1f;
                            }
                            if (NPC.life < NPC.lifeMax / 2)
                            {
                                num626 += 1f;
                            }
                            if (NPC.life < NPC.lifeMax / 4)
                            {
                                num626 += 1f;
                            }
                            if (NPC.velocity.X < -num626)
                            {
                                NPC.velocity.X = -num626;
                            }
                            if (NPC.velocity.X > num626)
                            {
                                NPC.velocity.X = num626;
                            }
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_BUBBLES2)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                NPC.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (NPC.velocity.Y == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 20)
                    {
                        internalAI[2] = 0;
                        if (NPC.direction == -1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_LargeFungusBubble>(), damage, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), ModContent.ProjectileType<TruffleToad_LargeFungusBubble>(), damage, 3);
                        }
                        NPC.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_TOADS)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                NPC.velocity.X *= .98f;
                if (internalAI[1] == 35)
                {
                    NPC toadNPC = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)(NPC.Center.X - 30f), (int)(NPC.Center.Y - 16), ModContent.NPCType<TinyToad>());
                    TinyToad toad = toadNPC.ModNPC as TinyToad;
                    toad.WasSpawnedByTruffleToad = true;
                    toadNPC = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)(NPC.Center.Y - 16), ModContent.NPCType<TinyToad>());
                    toad = toadNPC.ModNPC as TinyToad;
                    toad.WasSpawnedByTruffleToad = true;
                    toadNPC = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)(NPC.Center.X + 30f), (int)(NPC.Center.Y - 16), ModContent.NPCType<TinyToad>());
                    toad = toadNPC.ModNPC as TinyToad;
                    toad.WasSpawnedByTruffleToad = true;
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    NPC.netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.velocity.Y == 0 && internalAI[0] != AISTATE_JUMP)
            {
                if (internalAI[0] == AISTATE_BARF || internalAI[0] == AISTATE_BUBBLES || internalAI[0] == AISTATE_BUBBLES2)
                {
                    if (NPC.frame.Y < frameHeight * 6)
                    {
                        NPC.frame.Y = frameHeight * 6;
                    }
                    if (NPC.frameCounter >= 10)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                        if (NPC.frame.Y > frameHeight * 11)
                        {
                            NPC.frame.Y = frameHeight * 11;
                        }
                    }
                }
                else
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > frameHeight * 4)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 4;
                    }
                }
            }
        }
        public override void BossLoot(ref int potionType)
        {
            
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<TruffleToadTreasureBag>()));

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TruffleToadRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TruffleToadTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TruffleToadMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<MushrockStaff>(), ModContent.ItemType<ToadTongue>(), ModContent.ItemType<FrogLob>()));

            npcLoot.Add(notExpertRule);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * .8f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(GlowTex, NPC.Center - screenPos, NPC.frame, ColorUtils.COLOR_GLOWPULSE, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }

        private void AITortoise()
        {
            NPC.TargetClosest(true);
            bool flag31 = true;
            int num513 = 0;
            if (NPC.velocity.X < 0f)
            {
                num513 = -1;
            }
            if (NPC.velocity.X > 0f)
            {
                num513 = 1;
            }
            Vector2 position = NPC.position;
            position.X += NPC.velocity.X;
            int num514 = (int)((position.X + NPC.width / 2 + (NPC.width / 2 + 1) * num513) / 16f);
            int num515 = (int)((position.Y + NPC.height - 1f) / 16f);
            if (num514 * 16 < position.X + NPC.width && num514 * 16 + 16 > position.X && (Main.tile[num514, num515].HasUnactuatedTile && !Main.tile[num514, num515].TopSlope && !Main.tile[num514, num515 - 1].TopSlope && (Main.tileSolid[Main.tile[num514, num515].TileType] && !Main.tileSolidTop[Main.tile[num514, num515].TileType] || flag31 && Main.tileSolidTop[Main.tile[num514, num515].TileType] && (!Main.tileSolid[Main.tile[num514, num515 - 1].TileType] || !Main.tile[num514, num515 - 1].HasUnactuatedTile) && Main.tile[num514, num515].TileType != TileID.Anvils && Main.tile[num514, num515].TileType != TileID.WorkBenches && Main.tile[num514, num515].TileType != TileID.MythrilAnvil) || Main.tile[num514, num515 - 1].IsHalfBlock && Main.tile[num514, num515 - 1].HasUnactuatedTile) && (!Main.tile[num514, num515 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num514, num515 - 1].TileType] || Main.tileSolidTop[Main.tile[num514, num515 - 1].TileType] || Main.tile[num514, num515 - 1].IsHalfBlock && (!Main.tile[num514, num515 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num514, num515 - 4].TileType] || Main.tileSolidTop[Main.tile[num514, num515 - 4].TileType])) && (!Main.tile[num514, num515 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num514, num515 - 2].TileType] || Main.tileSolidTop[Main.tile[num514, num515 - 2].TileType]) && (!Main.tile[num514, num515 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num514, num515 - 3].TileType] || Main.tileSolidTop[Main.tile[num514, num515 - 3].TileType]) && (!Main.tile[num514 - num513, num515 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num514 - num513, num515 - 3].TileType] || Main.tileSolidTop[Main.tile[num514 - num513, num515 - 3].TileType]))
            {
                float num516 = num515 * 16;
                if (Main.tile[num514, num515].IsHalfBlock)
                {
                    num516 += 8f;
                }
                if (Main.tile[num514, num515 - 1].IsHalfBlock)
                {
                    num516 -= 8f;
                }
                if (num516 < position.Y + NPC.height)
                {
                    float num517 = position.Y + NPC.height - num516;
                    if (num517 <= 16.1)
                    {
                        NPC.gfxOffY += NPC.position.Y + NPC.height - num516;
                        NPC.position.Y = num516 - NPC.height;
                        if (num517 < 9f)
                        {
                            NPC.stepSpeed = 0.75f;
                        }
                        else
                        {
                            NPC.stepSpeed = 1.5f;
                        }
                    }
                }
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.velocity.X = NPC.velocity.X * 0.5f;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 30f)
                {
                    NPC.netUpdate = true;
                    NPC.TargetClosest(true);
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[0] = 2f;
                }
            }
            else
            {
                if (NPC.ai[0] == 2f)
                {
                    if (Main.expertMode)
                    {
                        NPC.damage = (int)(NPC.defDamage * 2 * 0.9);
                    }
                    else
                    {
                        NPC.damage = NPC.defDamage * 2;
                    }
                    NPC.defense = NPC.defDefense * 2;
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] == 1f)
                    {
                        NPC.netUpdate = true;
                        NPC.TargetClosest(true);
                        NPC.ai[2] += 0.3f;
                        NPC.ai[1] += 1f;
                        bool flag34 = Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height);
                        float num531 = 10f;
                        if (!flag34)
                        {
                            num531 = 6f;
                        }
                        Vector2 vector67 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                        float num532 = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f - vector67.X;
                        float num533 = Math.Abs(num532) * 0.2f;
                        if (NPC.directionY > 0)
                        {
                            num533 = 0f;
                        }
                        float num534 = Main.player[NPC.target].position.Y - vector67.Y - num533;
                        float num535 = (float)Math.Sqrt(num532 * num532 + num534 * num534);
                        NPC.netUpdate = true;
                        num535 = num531 / num535;
                        num532 *= num535;
                        num534 *= num535;
                        if (!flag34)
                        {
                            num534 = -10f;
                        }
                        NPC.velocity.X = num532;
                        NPC.velocity.Y = num534;
                        NPC.ai[3] = NPC.velocity.X;
                    }
                    else
                    {
                        if (NPC.position.X + NPC.width > Main.player[NPC.target].position.X && NPC.position.X < Main.player[NPC.target].position.X + Main.player[NPC.target].width && NPC.position.Y < Main.player[NPC.target].position.Y + Main.player[NPC.target].height)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.8f;
                            NPC.ai[3] = 0f;
                            if (NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + 0.2f;
                            }
                        }
                        if (NPC.ai[3] != 0f)
                        {
                            NPC.velocity.X = NPC.ai[3];
                            NPC.velocity.Y = NPC.velocity.Y - 0.22f;
                        }
                        if (NPC.ai[1] >= 90f)
                        {
                            NPC.noGravity = false;
                            NPC.ai[1] = 0f;
                            NPC.ai[0] = 3f;
                        }
                    }
                    if (NPC.wet && NPC.directionY < 0)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.3f;
                    }
                    return;
                }
                if (NPC.ai[0] == 3f)
                {
                    if (NPC.wet && NPC.directionY < 0)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.3f;
                    }
                    NPC.velocity.X = NPC.velocity.X * 0.96f;
                    if (NPC.ai[2] > 0f)
                    {
                        NPC.ai[2] -= 0.01f;
                    }
                    if (NPC.ai[2] <= 0f && (NPC.velocity.Y == 0f || NPC.wet))
                    {
                        NPC.netUpdate = true;
                        NPC.ai[2] = 0f;
                        NPC.ai[1] = 0f;
                        NPC.ai[0] = 4f;
                        return;
                    }
                }
                else
                {
                    if (NPC.ai[0] == 5f)
                    {
                        NPC.damage = (int)(NPC.defDamage * (Main.expertMode ? 1.4f : 1.8f));
                        NPC.defense = NPC.defDefense * 2;
                        NPC.knockBackResist = 0f;
                        if (Main.rand.Next(3) < 2)
                        {
                            int num536 = Dust.NewDust(NPC.Center - new Vector2(30f), 60, 60, DustID.Torch, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default, 1.5f);
                            Main.dust[num536].noGravity = true;
                            Dust dust3 = Main.dust[num536];
                            dust3.velocity *= 0.2f;
                            Main.dust[num536].fadeIn = 1f;
                        }
                        NPC.ai[1] += 1f;
                        if (NPC.ai[3] > 0f)
                        {
                            int num;
                            if (NPC.ai[3] == 1f)
                            {
                                Vector2 vector68 = NPC.Center - new Vector2(50f);
                                for (int num537 = 0; num537 < 32; num537 = num + 1)
                                {
                                    int num538 = Dust.NewDust(vector68, 100, 100, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                                    Main.dust[num538].noGravity = true;
                                    Dust dust3 = Main.dust[num538];
                                    dust3.velocity *= 3f;
                                    num538 = Dust.NewDust(vector68, 100, 100, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                                    dust3 = Main.dust[num538];
                                    dust3.velocity *= 2f;
                                    Main.dust[num538].noGravity = true;
                                    num = num537;
                                }
                                for (int num539 = 0; num539 < 4; num539 = num + 1)
                                {
                                    int num540 = Gore.NewGore(NPC.GetSource_FromThis(), vector68 + new Vector2(50 * Main.rand.Next(100) / 100f, 50 * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default, Main.rand.Next(61, 64), 1f);
                                    Gore gore = Main.gore[num540];
                                    gore.velocity *= 0.3f;
                                    Gore gore2 = Main.gore[num540];
                                    gore2.velocity.X = gore2.velocity.X + Main.rand.Next(-10, 11) * 0.05f;
                                    Gore gore3 = Main.gore[num540];
                                    gore3.velocity.Y = gore3.velocity.Y + Main.rand.Next(-10, 11) * 0.05f;
                                    num = num539;
                                }
                            }
                            for (int num541 = 0; num541 < 5; num541 = num + 1)
                            {
                                int num542 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                                Main.dust[num542].velocity = Main.dust[num542].velocity * Main.rand.NextFloat();
                                num = num541;
                            }
                            NPC.ai[3] += 1f;
                            if (NPC.ai[3] >= 10f)
                            {
                                NPC.ai[3] = 0f;
                            }
                        }
                        if (NPC.ai[1] == 1f)
                        {
                            NPC.netUpdate = true;
                            NPC.TargetClosest(true);
                            bool flag35 = Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height);
                            float num543 = 16f;
                            if (!flag35)
                            {
                                num543 = 10f;
                            }
                            Vector2 vector69 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                            float num544 = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f - vector69.X;
                            float num545 = Math.Abs(num544) * 0.2f;
                            if (NPC.directionY > 0)
                            {
                                num545 = 0f;
                            }
                            float num546 = Main.player[NPC.target].position.Y - vector69.Y - num545;
                            float num547 = (float)Math.Sqrt(num544 * num544 + num546 * num546);
                            NPC.netUpdate = true;
                            num547 = num543 / num547;
                            num544 *= num547;
                            num546 *= num547;
                            if (!flag35)
                            {
                                num546 = -12f;
                            }
                            NPC.velocity.X = num544;
                            NPC.velocity.Y = num546;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.9f;
                            if (NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + 0.2f;
                            }
                            if (NPC.ai[2] == 0f || NPC.ai[1] >= 1200f)
                            {
                                NPC.ai[1] = 0f;
                                NPC.ai[0] = 4f;
                            }
                        }
                        if (NPC.wet && NPC.directionY < 0)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - 0.3f;
                        }
                        return;
                    }
                    if (NPC.ai[0] == 4f)
                    {
                        NPC.velocity.X = 0f;
                        NPC.ai[1] += 1f;
                        if (NPC.ai[1] >= 30f)
                        {
                            NPC.TargetClosest(true);
                            NPC.netUpdate = true;
                            NPC.ai[1] = 0f;
                            NPC.ai[0] = 0f;
                        }
                        if (NPC.wet)
                        {
                            NPC.ai[0] = 2f;
                            NPC.ai[1] = 0f;
                            return;
                        }
                    }
                }
            }
        }
    }
}


