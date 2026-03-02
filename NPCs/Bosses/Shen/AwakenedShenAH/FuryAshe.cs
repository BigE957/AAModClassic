using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Terraria.Graphics.Shaders;
using AAMod.NPCs.Bosses.AH.Ashe;

namespace AAMod.NPCs.Bosses.Shen.AwakenedShenAH
{
    [AutoloadBossHead]
    public class FuryAshe : ModNPC
    {
        public int OrbiterCount = Main.expertMode ? 16 : 10;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fury Ashe");
            Main.npcFrameCount[NPC.type] = 24;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 80;
            NPC.damage = 150;
            NPC.defense = 150;
            NPC.lifeMax = 100000;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.knockBackResist = 0f;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
        }
        
        public int[] Vortexes = null;

        public bool RuneCrash = false;
        public bool Health = false;

        public override bool CheckActive()
        {
            return !NPC.AnyNPCs(ModContent.NPCType<ShenA>());
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);

            NPC.direction = NPC.spriteDirection = NPC.position.X < player.position.X ? 1 : -1;
            RingEffects();

            NPC.damage = NPC.defDamage * VortexDamage();

            Vector2 targetPos;

            switch (NPC.ai[0])
            {
                case 0:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 1:
                    if (!AliveCheck(player))
                        break;

                    MoveToPoint(wantedVelocity);

                    BaseAI.ShootPeriodic(NPC, player.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), player.width, player.height, Mod.Find<ModProjectile>("DiscordianInferno").Type, ref NPC.ai[2], 22, NPC.damage / 4, 9, false);
                    if (NPC.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        AIChange();
                    }
                    break;
                case 2:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 3:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 4:
                    if (!AliveCheck(player))
                        break;

                    int firepos = 0;
                    if (player.Center.X > NPC.Center.X) //If NPC's X position is less than the player's
                    {
                        firepos = 500;
                    }
                    else
                    {
                        firepos = -500;
                    }

                    wantedVelocity = player.Center - new Vector2(firepos, 0);

                    MoveToPoint(wantedVelocity);

                    BaseAI.ShootPeriodic(NPC, player.Center, player.width, player.height, Mod.Find<ModProjectile>("ShenABreath").Type, ref NPC.ai[2], 5, NPC.damage / 4, 16, false);
                    if (NPC.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        NPC.ai[1] = 0;
                        AIChange();
                    }
                    break;
                case 5: //draw dash frame
                    if (!AliveCheck(player))
                        break;
                    MoveToPoint(wantedVelocity);
                    if (++NPC.ai[1] > 30)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[0]++;
                    }
                    break;
                case 6: //prepare for fishron dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(NPC.Center) * 600;
                    Movement(targetPos, 0.8f);
                    if (++NPC.ai[1] > 20)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.velocity = NPC.DirectionTo(player.Center) * (NPC.life < NPC.lifeMax/3 ? 50:40);
                        if(NPC.velocity.Length() < 40f)
                        {
                            NPC.velocity = Vector2.Normalize(NPC.DirectionTo(targetPos)) * (NPC.life < NPC.lifeMax/3 ? 50:40);
                        }
                    }
                    break;

                case 7: //dashing
                    if (++NPC.ai[2] > 3)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != 1)
                        {
                            const float ai0 = 0.01f;
                            Projectile.NewProjectile(NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2), Mod.Find<ModProjectile>("ShenFireballAccel").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                            Projectile.NewProjectile(NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2), Mod.Find<ModProjectile>("ShenFireballAccel").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                        }
                    }
                    if (++NPC.ai[1] > 40)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= (NPC.life < NPC.lifeMax/3 ? 4:3)) //dash three/Four times
                        {
                            if(NPC.life < NPC.lifeMax / 3)
                            {
                                NPC.ai[0] = Main.rand.Next(4) == 0? 4:9;
                            }
                            else
                            {
                                NPC.ai[0]++;
                                NPC.netUpdate = true;
                            }
                            NPC.ai[3] = 0;
                        }
                        else
                        {
                            NPC.ai[0]--;
                        }
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.velocity.X < 0)
                        NPC.rotation += (float)Math.PI;
                    break;
                case 8:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 9:
                    if (!AliveCheck(player))
                        break;
                    
                    if (NPC.ai[1] == 100)
                    {
                        pos = - pos;//ChangePos
                    }
                    if (NPC.ai[1] > 100)
                    {
                        MoveToPoint(player.Center + new Vector2((player.velocity.X > 0? 1 : -1) * 600, -400));
                    }
                    else
                    {
                        MoveToPoint(wantedVelocity);
                    }
                    if (NPC.life > NPC.lifeMax / 3 || NPC.ai[1] < 100)
                    {
                        BaseAI.ShootPeriodic(NPC, player.Center, player.width, player.height, ModContent.ProjectileType<FuryAsheFire>(), ref NPC.ai[2], NPC.life < NPC.lifeMax * 0.666f ? 30 : 60, NPC.damage / 4, 8, false);
                    }
                    if (NPC.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        if (NPC.life < NPC.lifeMax / 3)
                        {
                            for(int i = 0; i < 8; i++)
                            {
                                Vector2 shoot = new Vector2((float)Math.Sin(i * 0.25f * 3.1415926f), (float)Math.Cos(i * 0.25f * 3.1415926f));
                                shoot *= 8f;
                                Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, shoot.X, shoot.Y, ModContent.ProjectileType<FuryAsheFire>(), NPC.damage / 4, 5, Main.myPlayer);
                            }
                            if(Main.rand.Next(3) == 0) 
                            {
                                NPC.netUpdate = true;
                                goto case 5;
                            }
                        }
                        AIChange();
                    }
                    break;
                case 10:
                    if (NPC.CountNPCS(ModContent.NPCType<Shenling>()) >= 2)
                    {
                        NPC.ai[0] = 12;
                        NPC.netUpdate = true;
                        goto case 12;
                    }
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 11:
                    if (!AliveCheck(player))
                        break;
                    MoveToPoint(wantedVelocity);
                    if (NPC.ai[1]++ > 200)
                    {
                        if(Main.netMode != 1)
                        {
                            NPC.NewNPC((int)NPC.position.X, (int)NPC.position.Y - 100, ModContent.NPCType<Shenling>());
                            NPC.NewNPC((int)NPC.position.X, (int)NPC.position.Y + 100, ModContent.NPCType<Shenling>());
                        }
                        NPC.netUpdate = true;
                        AIChange();
                    }
                    break;
                case 12:
                    if (!AliveCheck(player))
                        break;
                    FireMagic(NPC);
                    IdlePhase();
                    break;
                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }

            if (NPC.ai[0] != 6 && NPC.ai[0] != 7)
            {
                NPC.rotation = 0;
            }

            if (NPC.ai[0] == 2 || NPC.ai[0] == 3 || NPC.ai[0] == 8)
            {
                if((NPC.ai[1] == 0 && (Main.rand.Next(6) == 0 || NPC.life < NPC.lifeMax * 0.66f && Main.rand.Next(3) == 0)) || NPC.life < NPC.lifeMax * 0.33f) RuneCrash = true;
            }
            else
            {
                RuneCrash = false;
            }

            if (RuneCrash)
            {
                if(NPC.ai[2]++ > 5)
                {
                    if(Main.netMode != 1)
                    {
                        Vector2 Runeposition = new Vector2(0,0);
                        Runeposition = player.Center + new Vector2((250f + 4f * Main.rand.Next(-7, 7)) * (float)Math.Sin(5.18f * Main.rand.Next(30) * 3.1415926f), (250f + 4f * Main.rand.Next(-7, 7)) * (float)Math.Cos(5.18f * Main.rand.Next(30) * 3.1415926f));
                        
                        float RunepositionX = Runeposition.X;
                        float RunepositionY = Runeposition.Y;
                        int id = NPC.NewNPC((int)RunepositionX, (int)RunepositionY, ModContent.NPCType<AsheRune>(), 0, RunepositionX, RunepositionY, NPC.damage / 4, NPC.whoAmI, player.whoAmI);
                        if (Main.netMode == 2 && id < 200) NetMessage.SendData(23, -1, -1, null, id);
                    }
                    NPC.ai[2] = 0;
                }
            }
        }

        public void IdlePhase()
        {
            Player player = Main.player[NPC.target];
            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
            MoveToPoint(wantedVelocity);

            if (NPC.ai[1]++ > (Main.expertMode ? 180 : 280))
            {
                AIChange();
            }
        }

        public int Frame = 0;

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] == 1 || NPC.ai[0] == 4 || NPC.ai[0] == 9)
            {
                if (NPC.frameCounter++ >= 10)
                {
                    NPC.frameCounter = 0;
                    Frame++;
                }
            }
            else if (NPC.ai[0] == 5)
            {
                if (NPC.frameCounter++ >= 10)
                {
                    NPC.frameCounter = 0;
                    Frame++;
                }
            }
            else
            {
                if (NPC.frameCounter++ >= 7)
                {
                    NPC.frameCounter = 0;
                    Frame++;
                }
            }
            
            if (NPC.ai[0] == 5)
            {
                if (Frame < 8 || Frame > 10)
                {
                    Frame = 8;
                }
            }
            else if (NPC.ai[0] == 6 || NPC.ai[0] == 7)
            {
                if (Frame >= 14 || Frame < 11)
                {
                    Frame = 11;
                }
            }
            else if (NPC.ai[0] == 1 || NPC.ai[0] == 4)
            {
                if (Frame < 15 || Frame > 18)
                {
                    Frame = 15;
                }
            }
            else if (NPC.ai[0] == 9)
            {
                if (NPC.life < NPC.lifeMax / 3 && NPC.ai[1] >= (Main.expertMode ? 140 : 240))
                {
                    if (Frame > 23 || Frame < 20)
                    {
                        Frame = 20;
                    }
                }
                else
                {
                    if (Frame < 15 || Frame > 18)
                    {
                        Frame = 15;
                    }
                }
            }
            else if (RuneCrash)
            {
                if (Frame < 19 || Frame > 20)
                {
                    Frame = 19;
                }
            }
            else if (!FlyingBack)
            {
                if (Frame > 3)
                {
                    Frame = 0;
                }
            }
            else
            {
                if (Frame >= 8 || Frame < 4)
                {
                    Frame = 4;
                }
            }

            if (Frame > 23)
            {
                Frame = 0;
            }

            NPC.frame.Y = Frame * frameHeight;
        }

        private void Movement(Vector2 targetPos, float speedModifier)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (NPC.velocity.X > 30 || NPC.velocity.X < -30)
                NPC.velocity.X = 30 * Math.Sign(NPC.velocity.X);
            if (NPC.velocity.Y > 30 || NPC.velocity.Y < -30)
                NPC.velocity.Y = 30 * Math.Sign(NPC.velocity.Y);
        }

        private bool AliveCheck(Player player)
        {
            if (player.dead || !player.active || (NPC.position.X - Main.player[NPC.target].position.X) > 6000f || (NPC.position.X - Main.player[NPC.target].position.X) < -6000f || (NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f || (NPC.position.Y - Main.player[NPC.target].position.Y) < -6000f)
            {
                NPC.TargetClosest(true);
                if (Main.netMode != 1)
                {
                    int DeathAnim = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<FuryAsheVanish>(), 0);
                    Main.npc[DeathAnim].velocity = NPC.velocity;
                    Main.npc[DeathAnim].netUpdate = true;
                }
                NPC.active = false;
                return false;
            }
            if (NPC.timeLeft < 600)
                NPC.timeLeft = 600;
            return true;
        }

        private void AIChange()
        {
            NPC.ai[0]++;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            NPC.ai[3] = 0;
        }

        public static int VortexDamage()
        {
            return  1 + (NPC.CountNPCS(ModContent.NPCType<FuryAsheOrbiter>()) / 15);
        }

        public void FireMagic(NPC npc)
        {
            if (Main.netMode != 1)
            {
                if (Health)
                {
                    for(int i = 0; i < 200; i++)
                    {
                        if(Main.npc[i].type == Mod.Find<ModNPC>("FuryAsheOrbiter").Type)
                        {
                            Main.npc[i].life = 0;
                            Main.npc[i].active = false;
                        } 
                    }
                }
                const float distance = 125f;
                float rotation = 2f * (float)Math.PI / OrbiterCount;
                if (Health && npc.life >= npc.lifeMax * .66f)
                {
                    Health = false;
                    rotation = 2f * (float)Math.PI / 4;
                    for (int m = 0; m < 4; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("FuryAsheOrbiter").Type, 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                }
                if (Health && npc.life < npc.lifeMax * .66f && npc.life >= npc.lifeMax * .33f)
                {
                    Health = false;
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("FuryAsheOrbiter").Type, 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                }
                if (Health && npc.life < npc.lifeMax * .33f)
                {
                    OrbiterCount += 2;
                    Health = false;
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("FuryAsheOrbiter").Type, 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                    OrbiterCount -= 2;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        private readonly bool DontSayDeathLine = false;

        public override void OnKill()
        {
            if (DontSayDeathLine)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("FuryAshe1") + Main.LocalPlayer.name + "!", new Color(102, 20, 48));
            }
            else
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("FuryAshe2"), new Color(102, 20, 48));
            }
            int DeathAnim = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<FuryAsheVanish>(), 0);
            Main.npc[DeathAnim].velocity = NPC.velocity;
            NPC.value = 0f;
            NPC.boss = false;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossLifeScale);  
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        #region movement stuff

        public bool FlyingBack = false;
        public bool FlyingPositive = false;
        public bool FlyingNegative = false;
        public float pos = 350f;
        public override void PostAI()
        {
            Player player = Main.player[NPC.target];

            if (NPC.velocity.X > 0) //Flying in the positive X direction
            {
                FlyingPositive = true;
                FlyingNegative = false;
            }
            else //Flying in the nagative X direction
            {
                FlyingPositive = false;
                FlyingNegative = true;
            }
            if (NPC.ai[0] == 6 || NPC.ai[0] == 7)
            {
                NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
            }
            else
            {
                if (player.Center.X > NPC.Center.X) //If NPC's X position is less than the player's
                {
                    if (pos == -600)
                    {
                        pos = 600;
                    }

                    NPC.direction = 1;

                    if (FlyingPositive)
                    {
                        FlyingBack = true;
                    }
                    else
                    {
                        FlyingBack = false;
                    }
                }
                else //If NPC's X position is higher than the player's
                {
                    if (pos == 600)
                    {
                        pos = -600;
                    }

                    NPC.direction = -1;

                    if (FlyingNegative)
                    {
                        FlyingBack = true;
                    }
                    else
                    {
                        FlyingBack = false;
                    }
                }
                NPC.direction = player.position.X > NPC.position.X ? 1 : -1;
                NPC.netUpdate = true;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 25f;
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 10f)
            {
                moveSpeed *= 0.01f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        #endregion

        #region draw stuff

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D Tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D Glow = Mod.GetTexture("Glowmasks/FuryAshe_Glow");

            Texture2D RingTex = Mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing1");
            Texture2D RingTex1 = Mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing2");
            Texture2D RitualTex = Mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRitual");
            Texture2D ShieldTex = Mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheShield");

            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);

            if (scale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, NPC.position, NPC.width, NPC.height, scale, RingRotation, 0, 1, new Rectangle(0, 0, RitualTex.Width, RitualTex.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, NPC.position, NPC.width, NPC.height, scale, -RingRotation, 0, 1, new Rectangle(0, 0, RingTex.Width, RingTex.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, NPC.position, NPC.width, NPC.height, scale, -RingRotation, 0, 1, new Rectangle(0, 0, RingTex1.Width, RingTex1.Height), dColor, true);
            }
            if (scale2 > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, ShieldTex, red, NPC.position, NPC.width, NPC.height, scale2, RingRotation2, 0, 1, new Rectangle(0, 0, ShieldTex.Width, ShieldTex.Height), dColor, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Tex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, Main.npcFrameCount[NPC.type], NPC.frame, dColor, true);

            BaseDrawing.DrawTexture(spritebatch, Glow, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, Main.npcFrameCount[NPC.type], NPC.frame, Color.White, true);

            return false;
        }

        public float scale = 0;
        public float RingRotation = 0;

        public float scale2 = 0;
        public float RingRotation2 = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(pos);
                writer.Write(Health);
                writer.Write(RuneCrash);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                pos = reader.ReadFloat();
                Health = reader.ReadBool();
                RuneCrash = reader.ReadBool();
            }
        }

        private void RingEffects()
        {
            RingRotation += 0.02f;
            if (NPC.ai[0] == 12 || NPC.AnyNPCs(ModContent.NPCType<FuryAsheOrbiter>()))
            {
                if (scale >= 1f)
                {
                    scale = 1f;

                    if(NPC.CountNPCS(ModContent.NPCType<FuryAsheOrbiter>()) < OrbiterCount)
                    {
                        Health = true;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    scale += .02f;
                }
            }
            else
            {
                RingRotation -= 0.02f;
                if (scale > .1f)
                {
                    scale -= .02f;
                }
                else
                {
                    scale = 0;
                }
            }

            if (NPC.ai[0] == 1 || NPC.ai[0] == 6 || NPC.ai[0] == 11 || RuneCrash)
            {
                if (scale2 >= 1f)
                {
                    scale2 = 1f;
                }
                else
                {
                    scale2 += .02f;
                }
            }
            else
            {
                if (scale2 > .1f)
                {
                    scale2 -= .02f;
                }
                else
                {
                    scale2 = 0;
                }
            }

            if(scale >= 1f || scale2 >= 1f)
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        #endregion
    }
}


