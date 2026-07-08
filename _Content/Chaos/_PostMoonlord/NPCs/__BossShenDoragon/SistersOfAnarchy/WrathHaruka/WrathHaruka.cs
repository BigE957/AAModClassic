using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Haruka;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.WrathHaruka
{
    [AutoloadBossHead]
    public class WrathHaruka : ModNPC
    {
        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> Slash => Haruka.Slash;
        public static Asset<Texture2D> Danger => Haruka.Danger;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wrath Haruka");
            Main.npcFrameCount[NPC.type] = 28;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            internalAI[0] = 3;
            NPC.width = 50;
            NPC.height = 60;
            NPC.friendly = false;
            NPC.damage = 120;
            NPC.defense = 180;
            NPC.lifeMax = 130000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0f;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Shen_Awakened"];
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
        }


        public int ProjectileShoot = -1;
        public int repeat = 10;
        public bool isSlashing = false;

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public bool Invisible = false;
        public int Frame = 0;

        public int damage = 0;

        public int[] internalAI = new int[8];

        public int[] ShadowNPC = new int[3];
        
        public Vector2 ShadowkingPosition = Vector2.Zero;

        public bool SpawnClone = false;

        public bool Shadowkill = false;

        public bool SHADOWDOG = false;

        public int SHADOWCONTER = 0;

        public int strikebackproj = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]); //Used as the AI selector
                writer.Write(internalAI[1]); //Used as the Frame Counter
                writer.Write(internalAI[2]); //Used for current frame
                writer.Write(internalAI[3]); //Used to count down to AI change
                writer.Write(internalAI[4]); //Used as an AI Timer
                writer.Write(internalAI[5]);
                writer.Write(internalAI[6]); //Haruka skill counter
                writer.Write(internalAI[7]); //Haruka Shadow Dash Counter
                writer.Write(ShadowNPC[0]);
                writer.Write(ShadowNPC[1]);
                writer.Write(ShadowNPC[2]);
                writer.Write(SHADOWCONTER);
                writer.Write(strikebackproj);
                writer.Write(ProjectileShoot);
                writer.Write(repeat);
                writer.Write(SelectPoint);
                writer.Write(isSlashing);
                writer.Write(Invisible);
                writer.Write(SpawnClone);
                writer.Write(SHADOWDOG);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadInt32();
                internalAI[1] = reader.ReadInt32();
                internalAI[2] = reader.ReadInt32();
                internalAI[3] = reader.ReadInt32();
                internalAI[4] = reader.ReadInt32();
                internalAI[5] = reader.ReadInt32();
                internalAI[6] = reader.ReadInt32();
                internalAI[7] = reader.ReadInt32();
                ShadowNPC[0] = reader.ReadInt32();
                ShadowNPC[1] = reader.ReadInt32();
                ShadowNPC[2] = reader.ReadInt32();
                SHADOWCONTER = reader.ReadInt32();
                strikebackproj = reader.ReadInt32();
                ProjectileShoot = reader.ReadInt32();
                repeat = reader.ReadInt32();
                SelectPoint = reader.ReadBoolean();
                isSlashing = reader.ReadBoolean();
                Invisible = reader.ReadBoolean();
                SpawnClone = reader.ReadBoolean();
                SHADOWDOG = reader.ReadBoolean();
            }
        }

        public float XPos = 20f;

        public float pos = 250f;

        public override void HitEffect(NPC.HitInfo hit)
        {
            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<Dusts.AcidDust>(), NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            if(Invisible || internalAI[0] == AISTATE_Shadowkilling) return false;
            return null;
        }

        public override void OnKill()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.Flee.Haruka"), new Color(72, 78, 117));
            NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<WrathHarukaVanish>());
            NPC.value = 0f;
            NPC.boss = false;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * .9f);
        }

        public override bool CheckActive()
        {
            return !NPC.AnyNPCs(ModContent.NPCType<ShenDoragonA>());
        }

        public bool SetMovePos = false;

        public int Shadowdashcounter = 0;

        public static int AISTATE_PROJ = 0, AISTATE_SLASH = 1, AISTATE_SPIN = 2, AISTATE_IDLE = 3, AISTATE_Shadowkilling = 4; 


        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 0);
            NPC.direction = NPC.spriteDirection = NPC.position.X < player.position.X ? 1 : -1;

            if(SHADOWCONTER <= 0)
            {
                NPC.dontTakeDamage = false;
                SHADOWCONTER = 0;
                Shadowdashcounter = 0;
            }

            if (Invisible)
            {
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 5;
                }
                else
                {
                    NPC.chaseable = false;
                    NPC.alpha = 255;
                }
            }
            else
            {
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 8;
                }
                else
                {
                    NPC.chaseable = true;
                    NPC.alpha = 0;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                internalAI[1]++;
                internalAI[5]++;
                internalAI[6]++;
                internalAI[7]++;
            }

            int InvisTimer1 = 1000;

            int InvisTimer2 = 1300;

            if (NPC.life < NPC.lifeMax * .66f)
            {
                InvisTimer1 = 800;

                InvisTimer2 = 1100;

                if(Main.expertMode) internalAI[6]++;
            }
            if (NPC.life < NPC.lifeMax * .33f)
            {
                InvisTimer1 = 600;

                InvisTimer2 = 900;

                if(Main.expertMode) internalAI[6]+=2;
            }
            if (internalAI[5] > InvisTimer1)
            {
                if (!Invisible)
                {
                    Invisible = true;
                    NPC.netUpdate = true;
                }
            }
            if (internalAI[5] > InvisTimer2 && internalAI[0] != AISTATE_Shadowkilling)
            {
                Invisible = false;
                Backstab();
                Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                Vector2 fireTarget = NPC.Center;
                int projType = ModContent.ProjectileType<WrathHaruka_WrathNightSlash>();
                BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage*1, 0f, 14f);
                internalAI[0] = Main.rand.Next(2);
                internalAI[5] = 0;
            }

            if (internalAI[6] >= 6000)
            {
                internalAI[6] = 6000;
            }

            if (internalAI[7] >= 3000)
            {
                internalAI[7] = 3000;
            }
            
            if (Invisible)
            {
                NPC.dontTakeDamage = true;
                internalAI[0] = 3;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

            if (NPC.alpha >= 200)
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

            if(Shadowkill && NPC.alpha > 250)
            {
                internalAI[0] = AISTATE_Shadowkilling;
                internalAI[1] = 0;
                internalAI[2] = 0;
                internalAI[3] = 0;
                internalAI[5] = 0;
                Invisible = false;
                NPC.netUpdate = true;
            }
            
            if (ProjectileShoot == 0 || internalAI[0] == AISTATE_SLASH)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (internalAI[1] > 4)
                    {
                        internalAI[1] = 0;
                        internalAI[2]++;
                    }
                }
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (internalAI[1] > 8)
                    {
                        internalAI[1] = 0;
                        internalAI[2]++;
                    }
                }
            }

            if(Main.expertMode && internalAI[0] != AISTATE_Shadowkilling && internalAI[0] != AISTATE_SPIN && SHADOWCONTER <= 0 && !Invisible)
            {
                foreach(Projectile p in Main.ActiveProjectiles)
                {
                    if(p.friendly && p.damage > 0 && NPC.Hitbox.Intersects(p.Hitbox))
                    {
                        if(internalAI[6] >= 2000)
                        {
                            p.Kill();
                            internalAI[6] -= 2000;
                            strikebackproj ++;
                            internalAI[0] = AISTATE_SPIN;
                        }
                        else if(internalAI[7] >= 3000)
                        {
                            internalAI[7] = 0;
                            SHADOWDOG = true;
                        }
                        NPC.netUpdate = true;
                        break;
                    }
                }
            }

            if(SHADOWDOG)
            {
                SHADOWCONTER ++;
                NPC.dontTakeDamage = true;
                internalAI[0] = AISTATE_IDLE;
                if(InvisTimer1 - internalAI[5] <= 360)
                {
                    internalAI[5] -= 360;
                }
                if(internalAI[5] < 0)
                {
                    internalAI[5] = 0;
                }
                if(SHADOWCONTER >= 180)
                {
                    SHADOWDOG = false;
                    internalAI[0] = AISTATE_SLASH;
                    if(internalAI[6] >= 800)
                    {
                        internalAI[0] = AISTATE_SPIN;
                        internalAI[6] -= 800;
                    }
                    Shadowdashcounter = 0;
                }
            }

            if (internalAI[0] == AISTATE_IDLE)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) 
                {
                    internalAI[3]++;

                    if (internalAI[3] >= 90 && !Invisible && !SHADOWDOG)
                    {
                        internalAI[3] = 0;
                        internalAI[0] = Main.rand.Next(2);
                        if(internalAI[6] >= 1500 && Main.expertMode)
                        {
                            internalAI[6] -= 1500;
                            Shadowkill = true;
                            Invisible = true;
                        }
                        else if(Main.rand.NextBool(4) && internalAI[6] >= 500)
                        {
                            internalAI[0] = AISTATE_SPIN;
                            internalAI[6] -= 500;
                        }
                        NPC.ai = new float[4];
                        NPC.netUpdate = true;
                    }
                    else if(internalAI[3] >= 95)
                    {
                        internalAI[3] = 0;
                    }
                }

                if (internalAI[2] > 3 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                }
            }
            else if (internalAI[0] == AISTATE_PROJ)
            {
                if (ProjectileShoot == -1 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    ProjectileShoot = Main.rand.Next(2);
                    NPC.netUpdate = true;
                }
                if (ProjectileShoot == 0)
                {
                    if (internalAI[2] == 5 && internalAI[1] == 3 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        repeat -= 1;
                        int projType = ModContent.ProjectileType<Haruka_Kunai>();
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                        dir *= 14f;
                        float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        for (int i = 0; i < 3; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, damage*1, 5, Main.myPlayer);
                        }
                    }
                    if ((internalAI[2] < 4 || internalAI[2] > 6) && Main.netMode != NetmodeID.MultiplayerClient) 
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 4;
                    }
                    if (repeat <= 0)
                    {
                        NPC.frameCounter = 0;
                        Frame = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            internalAI[0] = 3;
                            internalAI[1] = 0;
                            internalAI[2] = 0;
                            internalAI[3] = 0;
                            internalAI[4] = 0;
                            ProjectileShoot -= 1;
                            repeat = 12;
                            NPC.ai = new float[4];
                            NPC.netUpdate = true;
                        }
                    }
                }
                else if (ProjectileShoot == 1)
                {
                    internalAI[3]++;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (internalAI[3] == 100 || internalAI[3] == 200 || internalAI[3] == 299)
                        {
                            isSlashing = true;
                        }
                        if (isSlashing)
                        {
                            if (internalAI[2] < 7 || internalAI[2] > 9)
                            {
                                internalAI[1] = 0;
                                internalAI[2] = 7;
                                NPC.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (internalAI[2] > 3)
                            {
                                internalAI[1] = 0;
                                internalAI[2] = 0;
                                NPC.netUpdate = true;
                            }
                        }
                    }

                    if (internalAI[2] == 8 && internalAI[1] == 4 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                        Vector2 fireTarget = NPC.Center;
                        int projType = ModContent.ProjectileType<WrathHaruka_WrathNightSlash>();
                        BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage*1, 0f, 14f);
                    }
                    if (isSlashing && internalAI[2] > 9)
                    {
                        isSlashing = false;
                    }
                    if (internalAI[3] > 300)
                    {
                        NPC.frameCounter = 0;
                        Frame = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            internalAI[0] = 3;
                            internalAI[1] = 0;
                            internalAI[2] = 0;
                            internalAI[3] = 0;
                            internalAI[4] = 0;
                            ProjectileShoot -= 1;
                            NPC.ai = new float[4];
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SLASH)
            {
                internalAI[3]++;

                if(SHADOWCONTER > 0)
                {
                    SHADOWCONTER--;
                }
                else
                {
                    if (internalAI[2] < 17)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 17;
                    }
                    if (internalAI[2] > 26)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 17;
                        internalAI[4] += 1;
                    }
                    if (internalAI[4] > 5)
                    {
                        NPC.frameCounter = 0;
                        Frame = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            internalAI[0] = 3;
                            internalAI[1] = 0;
                            internalAI[2] = 0;
                            internalAI[3] = 0;
                            internalAI[4] = 0;
                            if(internalAI[6] >= 500 && Main.rand.NextBool(2))
                            {
                                internalAI[0] = AISTATE_SPIN;
                                internalAI[6] -= 500;
                                NPC.netUpdate = true;
                            }
                            else
                            {
                                internalAI[0] = 3;
                                NPC.netUpdate = true;
                            }
                            NPC.ai = new float[4];
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                internalAI[4]++;
                
                if(SHADOWCONTER > 0)
                {
                    SHADOWCONTER--;
                }
                else
                {
                    if (internalAI[2] < 10)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 10;
                    }
                    if (internalAI[2] > 16)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 13;
                    }

                    if(internalAI[4] < 100)
                    {
                        SelectPoint = true;
                    }

                    if(InvisTimer1 - internalAI[5] <= 120)
                    {
                        internalAI[5] -= 120;
                    }

                    if (SelectPoint)
                    {
                        MovePoint = player.Center;
                        SelectPoint = false;
                    }

                    if (strikebackproj != 0)
                    {
                        foreach(Projectile p in Main.ActiveProjectiles)
                        {
                            if(p.friendly && p.damage > 0 && NPC.Hitbox.Intersects(p.Hitbox))
                            {
                                strikebackproj ++;
                                break;
                            }
                        }
                    }
                    

                    if (internalAI[4] > 200)
                    {
                        NPC.frameCounter = 0;
                        Frame = 0;

                        if(internalAI[6] >= 2000 && Main.expertMode)
                        {
                            internalAI[6] -= 2000;
                            Shadowkill = true;
                            Invisible = true;
                            NPC.netUpdate = true;
                        }
                        else if(Main.rand.NextBool(2))
                        {
                            internalAI[0] = AISTATE_PROJ;
                            NPC.netUpdate = true;
                        }
                        else
                        {
                            internalAI[0] = 3;
                            NPC.netUpdate = true;
                        }

                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        pos *= -1f;
                        NPC.ai = new float[4];
                        
                        int projType = ModContent.ProjectileType<WrathHaruka_WrathNightSlash>();
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                        dir *= 14f;
                        float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                double offsetAngle = startAngle + deltaAngle * i;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, damage, 0, Main.myPlayer);
                            }
                            if(strikebackproj != 0)
                            {
                                startAngle -= .1d * (strikebackproj / 2);
                                deltaAngle = spread / 3f;
                                for (int i = 0; i < strikebackproj; i++)
                                {
                                    double offsetAngle = startAngle + deltaAngle * i;
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<Haruka_Arrow>(), damage, 0, Main.myPlayer);
                                }
                            }
                        }
                        strikebackproj = 0;
                    }
                    else if(internalAI[4] > 100)
                    {
                        MovePoint = player.Center - new Vector2(pos * 1.5f, 0);
                    }
                }
            }
            else if (internalAI[0] == AISTATE_Shadowkilling)
            {
                ShadowNPC[0] = NPC.whoAmI;
                internalAI[4]++;
                if(NPC.alpha >= 255)
                {
                    SpawnClone = true;
                }
                Shadowkilling();
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[0] = 3;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    NPC.ai = new float[4];
                }
            }

            if (internalAI[0] == AISTATE_SLASH || internalAI[0] == AISTATE_SPIN) //Melee Damage/Speed boost
            {
                NPC.damage = 180;
                NPC.defense = 300;
            }
            else if (internalAI[0] == AISTATE_Shadowkilling) //Melee Damage/Speed boost
            {
                NPC.damage = 150;
                NPC.defense = 9999;
            }
            else //Reset Stats
            {
                NPC.defense = NPC.defDefense;
                NPC.damage = 80;
            }


            if (internalAI[0] == AISTATE_IDLE || internalAI[0] == AISTATE_PROJ || Invisible) //When charging the player
            {
                MoveToPoint(wantedVelocity);
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                if(SHADOWCONTER > 0)
                {
                    LOOPPOINT(player.Center + new Vector2(500f, 0), player.Center - new Vector2(500f, 0));
                }
                else
                {
                    MoveToPoint(MovePoint);
                }
            }
            else if (internalAI[0] == AISTATE_SLASH) //When charging the player
            {
                if(SHADOWCONTER > 0)
                {
                    LOOPPOINT(player.Center + new Vector2(500f, 0), player.Center - new Vector2(500f, 0));
                }
                else
                {
                    MoveToPoint(player.Center);
                }
            }
            NPC.rotation = 0;

            NPC.noTileCollide = true;
        }

        public override void PostAI()
        {
            Player player = Main.player[NPC.target];
            if (internalAI[0] != AISTATE_SPIN && internalAI[0] != AISTATE_Shadowkilling)
            {
                if (player.Center.X > NPC.Center.X) //If NPC's X position is higher than the player's
                {
                    if (pos == -250)
                    {
                        pos = 250;
                    }
                    NPC.direction = 1;
                }
                else //If NPC's X position is lower than the player's
                {
                    if (pos == 250)
                    {
                        pos = -250;
                    }
                    NPC.direction = -1;
                }
            }
            else
            {
                NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (ProjectileShoot == 0 || internalAI[0] == AISTATE_SLASH || internalAI[0] == AISTATE_SPIN)
            {
                if (NPC.frameCounter > 4)
                {
                    NPC.frameCounter = 0;
                    Frame++;
                }
            }
            else
            {
                if (NPC.frameCounter > 8)
                {
                    NPC.frameCounter = 0;
                    Frame++;
                }
            }
            if (internalAI[0] == AISTATE_IDLE)
            {
                if (Frame > 3)
                {
                    NPC.frameCounter = 0;
                    Frame = 0;
                }
            }
            else if (internalAI[0] == AISTATE_PROJ)
            {
                if (ProjectileShoot == 0)
                {
                    if (Frame < 4 || Frame > 6)
                    {
                        NPC.frameCounter = 0;
                        Frame = 4;
                    }
                }
                else if (ProjectileShoot == 1)
                {
                    internalAI[3]++;
                    if (isSlashing)
                    {
                        if (Frame < 7 || Frame > 9) //Sets to frame 16
                        {
                            NPC.frameCounter = 0;
                            Frame = 7;
                        }
                    }
                    else
                    {
                        if (Frame > 3)
                        {
                            NPC.frameCounter = 0;
                            Frame = 0;
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SLASH)
            {
                if (Frame < 17)
                {
                    NPC.frameCounter = 0;
                    Frame = 17;
                }
                if (Frame > 26)
                {
                    NPC.frameCounter = 0;
                    Frame = 17;
                }
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                if (Frame < 10)
                {
                    NPC.frameCounter = 0;
                    Frame = 10;
                }
                if (Frame > 16)
                {
                    NPC.frameCounter = 0;
                    Frame = 13;
                }
            }
            else if (internalAI[0] == AISTATE_Shadowkilling)
            {
                if (Frame != 27)
                {
                    NPC.frameCounter = 0;
                    Frame = 27;
                }
            }
            NPC.frame.Y = Frame * frameHeight;
        }

        public void Shadowkilling()
        {
            double Pi = Math.PI;
            Vector2 playerLocation = Main.player[NPC.target].position + new Vector2(Main.player[NPC.target].width * 0.5f, Main.player[NPC.target].height * 0.5f);
            
            Vector2[] spawnpoint = new Vector2[3];
            spawnpoint[0] = playerLocation + 275f * new Vector2((float)Math.Sin(0.5f * Pi), (float)Math.Cos(0.5f * Pi));
            spawnpoint[1] = playerLocation + 275f * new Vector2((float)Math.Sin(1.16f * Pi), (float)Math.Cos(1.13f * Pi));
            spawnpoint[2] = playerLocation + 275f * new Vector2((float)Math.Sin(1.83f * Pi), (float)Math.Cos(1.83f * Pi));

            if(!SpawnClone && (ShadowNPC[0] == -1 || ShadowNPC[1] == -1 || ShadowNPC[2] == -1))
            {
                return;
            }

            if(NPC.alpha >= 255 && SpawnClone)
            {
                int k = Main.rand.Next(3);
                NPC.position = spawnpoint[k];
                int k1 = k + 1;
                if (k1 >= 3)
                {
                    k1 = 0;
                }
                int k2 = k1 + 1;
                if (k2 >= 3)
                {
                    k2 = 0;
                }
                if(Main.netMode != NetmodeID.MultiplayerClient)
                {
                    ShadowNPC[1] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnpoint[k1].X, (int)spawnpoint[k1].Y, ModContent.NPCType<WrathHarukaClone>(), 0, NPC.whoAmI);
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, ShadowNPC[1], 0f, 0f, 0f, 0, 0, 0);
                    ShadowNPC[2] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnpoint[k2].X, (int)spawnpoint[k2].Y, ModContent.NPCType<WrathHarukaClone>(), 0, NPC.whoAmI);
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, ShadowNPC[2], 0f, 0f, 0f, 0, 0, 0);
                    NPC.alpha = 250;
                }
                NPC.netUpdate = true;
                if(ShadowNPC[1] != -1 && ShadowNPC[2] != -1)
                {
                    Main.npc[ShadowNPC[1]].alpha = NPC.alpha;
                    Main.npc[ShadowNPC[2]].alpha = NPC.alpha;
                    Main.npc[ShadowNPC[1]].boss = true;
                    Main.npc[ShadowNPC[2]].boss = true;
                    Main.npc[ShadowNPC[1]].life = Main.npc[ShadowNPC[0]].life;
                    Main.npc[ShadowNPC[2]].life = Main.npc[ShadowNPC[0]].life;
                }
                SpawnClone = false;
            }
            else if (NPC.alpha > 0)
            {
                NPC.velocity = Main.player[NPC.target].velocity;
                Main.npc[ShadowNPC[1]].velocity = Main.player[NPC.target].velocity;
                Main.npc[ShadowNPC[2]].velocity = Main.player[NPC.target].velocity;

                

                NPC.alpha -= 8;
                Main.npc[ShadowNPC[1]].alpha = NPC.alpha;
                Main.npc[ShadowNPC[2]].alpha = NPC.alpha;
            }
            else if(!NPC.active || NPC.life <= 0)
            {
                NPC.velocity = Main.player[NPC.target].velocity;
                Main.npc[ShadowNPC[1]].velocity = Main.player[NPC.target].velocity;
                Main.npc[ShadowNPC[2]].velocity = Main.player[NPC.target].velocity;

                Main.npc[ShadowNPC[1]].active = false;
                Main.npc[ShadowNPC[2]].active = false;
            }
            else
            {
                if(internalAI[4] < 90)
                {
                    NPC.velocity = Main.player[NPC.target].velocity;
                    Main.npc[ShadowNPC[1]].velocity = Main.player[NPC.target].velocity;
                    Main.npc[ShadowNPC[2]].velocity = Main.player[NPC.target].velocity;
                    
                    ShadowkingPosition = playerLocation;
                }
                else
                {
                    NPC.alpha = 0;
                    Main.npc[ShadowNPC[1]].alpha = NPC.alpha;
                    Main.npc[ShadowNPC[2]].alpha = NPC.alpha;
                    Vector2[] dist = new Vector2[3];
                    int k = 0;
                    while(k < 3)
                    {
                        dist[k] = ShadowkingPosition - Main.npc[ShadowNPC[k]].Center;
                        if(k == 0) NPC.velocity = Vector2.Normalize(dist[k]) * 15f;
                        else Main.npc[ShadowNPC[k]].velocity = Vector2.Normalize(dist[k]) * 15f;
                        if (ShadowkingPosition.X > Main.npc[ShadowNPC[k]].Center.X)
                        {
                            Main.npc[ShadowNPC[k]].direction = 1;
                        }
                        else
                        {
                            Main.npc[ShadowNPC[k]].direction = -1;
                        }
                        k++;
                    }
                }
            }

            if(internalAI[4] >= 160 || Main.npc[ShadowNPC[1]].Hitbox.Intersects(Main.npc[ShadowNPC[0]].Hitbox) || Main.npc[ShadowNPC[1]].Hitbox.Intersects(Main.npc[ShadowNPC[2]].Hitbox) || Main.npc[ShadowNPC[2]].Hitbox.Intersects(Main.npc[ShadowNPC[0]].Hitbox))
            {
                if(Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), ShadowkingPosition.X, ShadowkingPosition.Y, 0, 0, ModContent.ProjectileType<Haruka_DepthKillingBlast>(), damage*1, 5, Main.myPlayer);

                    Vector2 shoot = Vector2.Zero;
                    int projType = ModContent.ProjectileType<WrathHaruka_WrathNightSlash>();
                    for(int i = 0; i < 16; i++)
                    {
                        shoot = new Vector2((float)Math.Sin(i * 0.125f * Pi), (float)Math.Cos(i * 0.125f * Pi));
                        shoot *= 14f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), ShadowkingPosition.X, ShadowkingPosition.Y, shoot.X, shoot.Y, projType, damage*1, 5, Main.myPlayer);
                    }
                }

                Main.npc[ShadowNPC[1]].boss = false;
                Main.npc[ShadowNPC[2]].boss = false;
                Main.npc[ShadowNPC[1]].active = false;
                Main.npc[ShadowNPC[2]].active = false;
                
                ShadowNPC[0] = -1;
                ShadowNPC[1] = -1;
                ShadowNPC[2] = -1;
                internalAI[4] = 0;
                ShadowkingPosition = Vector2.Zero;
                internalAI[0] = 3;
                NPC.netUpdate = true;
            }
        }

        public void Backstab()
        {
            Vector2 playerLocation = Main.player[NPC.target].Center;
            Vector2 playerVelocity = Main.player[NPC.target].velocity;
            if(playerVelocity.X < 0)
            {
                NPC.position.X = playerLocation.X - 250f;
                NPC.position.Y = playerLocation.Y;
            }
            else if(playerVelocity.X > 0)
            {
                NPC.position.X = playerLocation.X + 250f;
                NPC.position.Y = playerLocation.Y;
            }
            else
            {
                NPC.position.X = playerLocation.X - Main.player[NPC.target].direction * 250f;
                NPC.position.Y = playerLocation.Y;
            }
            NPC.netUpdate = true;
        }

        public void LOOPPOINT(Vector2 point1, Vector2 point2)
        {
            Shadowdashcounter ++;

            if(Shadowdashcounter < 20)
            {
                MoveToPoint(point2);
            }
            else if(Shadowdashcounter < 40)
            {
                MoveToPoint(point1);
            }
            else if((NPC.Center - point1).Length() < 40f)
            {
                MoveToPoint(point2);
                Shadowdashcounter = 0;
            }
            else if((NPC.Center - point2).Length() < 40f)
            {
                MoveToPoint(point1);
                Shadowdashcounter = 21;
            }
            else
            {
                Shadowdashcounter = 0;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
            if (internalAI[0] == AISTATE_SPIN)
            {
                moveSpeed = 20f;
                if(SHADOWCONTER > 0)
                {
                    moveSpeed = 55f;
                }
            }
            if (Vector2.Distance(NPC.Center, point) > 500)
            {
                moveSpeed = 25f;
                if(SHADOWCONTER > 0)
                {
                    moveSpeed = 55f;
                }
            }
            if (internalAI[0] == AISTATE_SLASH)
            {
                moveSpeed = 25f;
                if(SHADOWCONTER > 0)
                {
                    moveSpeed = 55f;
                }
            }
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

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if(strikebackproj > 0 && internalAI[0] == AISTATE_SPIN)
            {
                modifiers.TargetDamageMultiplier *= 0f;
            }
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask.Value;

            Texture2D slash = Slash.Value;
            if (internalAI[0] == AISTATE_SPIN)
            {
                if(strikebackproj > 0)
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Red);
                else
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Navy);
            }
            else if(internalAI[0] == AISTATE_Shadowkilling)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Navy);
            
                if(internalAI[4] < 90)
                {
                    Vector2 playerLocation = Main.player[NPC.target].Center;
                    Texture2D texture = Danger.Value;
                    float scaleFactor = 1f + internalAI[4] / 30f;
                    float scaleFactor2 = (float)Math.Cos(6.2831855f * (internalAI[4] / 60f));
                    if(scaleFactor < 2.2f)
                    {
                        Color Alpha = drawColor;
                        Alpha.R = (byte)(float)(255 - internalAI[4] * 3);
                        Alpha.G = (byte)(float)(255 - internalAI[4] * 3);
                        Alpha.B = (byte)(float)(255 - internalAI[4] * 3);
                        Alpha.A = (byte)(float)(255 - internalAI[4] * 3);
                        spriteBatch.Draw(texture, playerLocation - new Vector2(texture.Width/2 * .6f * scaleFactor, texture.Height/2 * .6f * scaleFactor + 95f) + Vector2.UnitY * Main.player[NPC.target].gfxOffY - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), NPC.GetAlpha(Alpha), 0f, default, 0.6f * scaleFactor, SpriteEffects.None, 0f);
                    }
                    spriteBatch.Draw(texture, playerLocation - new Vector2(texture.Width/2 * .6f, texture.Height/2 * .6f + 95f) + Vector2.UnitY * Main.player[NPC.target].gfxOffY - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), NPC.GetAlpha(drawColor) * (0.6f + 0.4f * scaleFactor2), 0f, default, 0.6f, SpriteEffects.None, 0f);
                }
            }
            else if(SHADOWDOG && !Invisible)
            {
                Vector2 Position = NPC.position;
                Position.X = NPC.position.X + (SHADOWCONTER > 60? 60:SHADOWCONTER) * 1f;
                Color Alpha = drawColor;
                Alpha.R = (byte)(float)(255 - (SHADOWCONTER > 60? 60:SHADOWCONTER) * 3);
                Alpha.G = (byte)(float)(255 - (SHADOWCONTER > 60? 60:SHADOWCONTER) * 3);
                Alpha.B = (byte)(float)(255 - (SHADOWCONTER > 60? 60:SHADOWCONTER) * 3);
                Alpha.A = (byte)(float)(255 - (SHADOWCONTER > 60? 60:SHADOWCONTER) * 3);
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, Position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 28, NPC.frame, NPC.GetAlpha(Alpha), false);
                Position.X = NPC.position.X - (SHADOWCONTER > 60? 60:SHADOWCONTER) * 1f;
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, Position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 28, NPC.frame, NPC.GetAlpha(Alpha), false);
            }
            if(internalAI[0] == AISTATE_SLASH && SHADOWCONTER > 0)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Navy);
            }
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            if (Invisible) 
                return false;
            
            spriteBatch.Draw(slash, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);

            BaseDrawing.DrawAfterimage(spriteBatch, glowTex, 0, NPC, 1f, 1f, 7, true, 0f, 0f, AAColor.YamataA);
            return false;
        }
    }
}