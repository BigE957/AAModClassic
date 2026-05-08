using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

using System.IO;
using Terraria.Graphics.Shaders;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.CrossMod;
using Terraria.Localization;
using AAModClassic.UI.Titles;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Terraria.GameContent.ItemDropRules;
using System.Collections.Generic;
using AAModClassic.Items.Thorium.Healer;
using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.BossStandard;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs._BossRajahA;
using AAModClassic._Content._Misc._PostMoonlord.Items.Consumables;

namespace AAModClassic._Content.Bunny.__Hardmode.NPCs._BossRajah
{
    [AutoloadBossHead]
    public class Rajah : ModNPC
    {
        public override string Texture => "AAModClassic/_Content/Bunny/__Hardmode/NPCs/_BossRajah/Rajah";
        public int damage = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.width = 130;
            NPC.height = 220;
            NPC.aiStyle = -1;
            NPC.damage = 130;
            NPC.defense = 90;
            NPC.lifeMax = 65000;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 1000f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("Sounds/Rajah");
            NPC.value = Item.sellPrice(0, 1, 10, 0);
            NPC.boss = true;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Rajah"];
        }

        public bool isSupreme = false;
        public float[] internalAI = new float[6];
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
                writer.Write(isSupreme);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle(); //SpaceOctopus AI stuff
                internalAI[1] = reader.ReadSingle(); //Is Flying
                internalAI[2] = reader.ReadSingle(); //Is Jumping
                internalAI[3] = reader.ReadSingle(); //Minion/Rocket Timer
                internalAI[4] = reader.ReadSingle(); //JumpFlyControl and Vertical dash
                isSupreme = reader.ReadBoolean();
            }
        }

        private Texture2D RajahTex;
        private Texture2D Glow;
        private Texture2D SupremeGlow;
        private Texture2D SupremeEyes;
        private Texture2D ArmTex;
        public int WeaponFrame = 0;
        public Vector2 MovePoint;
        public bool SelectPoint = false;

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Ground Minion Alternation
         * npc.ai[2] = Weapon Change timer
         * npc.ai[3] = Weapon type
         */

        public int roarTimer = 0;
        public int roarTimerMax = 240;
        public bool Roaring => roarTimer > 0;

        public void Roar(int timer)
        {
            roarTimer = timer;
            SoundEngine.PlaySound(new SoundStyle("Sounds/Rajah"), NPC.Center);
        }

        public Vector2 WeaponPos;
        public Vector2 StaffPos;

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (isSupreme)
            {
                modifiers.TargetDamageMultiplier *= .7f;
            }
        }

        public float ProjSpeed()
        {
            if (NPC.life < (NPC.lifeMax * .85f)) //The lower the health, the more damage is done
            {
                return isSupreme ? 12f : 10f;
            }
            if (NPC.life < (NPC.lifeMax * .7f))
            {
                return isSupreme ? 13f : 11f;
            }
            if (NPC.life < (NPC.lifeMax * .65f))
            {
                return isSupreme ? 14f : 12f;
            }
            if (NPC.life < (NPC.lifeMax * .4f))
            {
                return isSupreme ? 15f : 13f;
            }
            if (NPC.life < (NPC.lifeMax * .25f))
            {
                return isSupreme ? 16f : 14f;
            }
            if (NPC.life < (NPC.lifeMax * .1f))
            {
                return isSupreme ? 16f : 15f;
            }
            return isSupreme ? 11f : 9f;
        }

        private bool SayLine = false;
        private bool DefenseLine = false;

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }
            AAModGlobalNPC.Rajah = NPC.whoAmI;
            WeaponPos = new Vector2(NPC.Center.X + (NPC.direction == 1 ? -78 : 78), NPC.Center.Y - 9);
            StaffPos = new Vector2(NPC.Center.X + (NPC.direction == 1 ? 78 : -78), NPC.Center.Y - 9);
            if (Roaring) roarTimer--;

            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.type == ModContent.NPCType<SupremeRajah>() && isSupreme == false)
            {
                isSupreme = true;
                NPC.netUpdate = true;
            }

            if (isSupreme)
            {
                if (NPC.ai[3] != 0 && !DefenseLine && !NPCExtensions.BeenKilled<SupremeRajah>() && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    DefenseLine = true;
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.PowerUp"), Color.MediumPurple);

                }
                if (NPC.life <= NPC.lifeMax / 7 && !SayLine && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SayLine = true;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                        bool evilMaxxing = bunnyKills >= 100 && !NPCExtensions.BeenKilled<SupremeRajah>();

                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            if(evilMaxxing)
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Multiplayer.Murderer"), 107, 137, 179);
                            else
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Multiplayer.Normal"), 107, 137, 179);
                        }
                        else
                        {
                            if (evilMaxxing)
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Singleplayer.Murderer"), 107, 137, 179);
                            else if(!NPCExtensions.BeenKilled<SupremeRajah>())
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Singleplayer.Normal"), 107, 137, 179);
                            else
                                BaseUtility.Chat(Language.GetOrRegister("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Singleplayer.Repeat").Format(Main.LocalPlayer.name.ToUpper()), 107, 137, 179);
                        }
                    }
                    Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
                }
            }

            Player player = Main.player[NPC.target];
            if (NPC.target >= 0 && Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    if (isSupreme)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Kill"), 107, 137, 179);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<SupremeRajahBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Kill"), 107, 137, 179);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    NPC.active = false;
                    NPC.noTileCollide = true;
                    NPC.netUpdate = true;
                    return;
                }
            }

            if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > 10000)
            {
                NPC.TargetClosest(true);
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > 10000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Despawn"), 107, 137, 179);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (isSupreme)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<SupremeRajahBookIt>(), damage, 0, Main.myPlayer); //Originally 100 damage
                        }
                        else
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    NPC.active = false;
                    NPC.noTileCollide = true;
                    NPC.netUpdate = true;
                    return;
                }
            }


            if (player.Center.X < NPC.Center.X)
            {
                NPC.direction = 1;
            }
            else
            {
                NPC.direction = -1;
            }

            if (internalAI[4] == 0)
            {
                if(player.Center.Y + player.height / 2 < NPC.Center.Y + NPC.height / 2 - 30f || Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > 2000 || isDashing)
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    internalAI[4] = 2f;
                    NPC.ai[0] = 0;
                    return;
                }
                else
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = false;
                    isDashing = false;
                    JumpAI();
                }
            }
            else if(internalAI[4] == 1f)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                isDashing = false;
                if (player.Center.Y + player.height / 2 <= NPC.Center.Y + NPC.height / 2 + 20f) 
                {
                    if(NPC.collideY && NPC.velocity.Y > 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                        for (int num622 = (int)NPC.position.X - 20; num622 < (int)NPC.position.X + NPC.width + 40; num622 += 20)
                        {
                            for (int num623 = 0; num623 < 4; num623++)
                            {
                                int num624 = Dust.NewDust(new Vector2(NPC.position.X - 20f, NPC.position.Y + NPC.height), NPC.width + 20, 4, DustID.Smoke, 0f, 0f, 100);
                                Main.dust[num624].velocity *= 0.2f;
                            }
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), num622 - 20, NPC.position.Y + NPC.height - 8f, 0, 0, ModContent.ProjectileType<RajahStomp>(), damage, 6, Main.myPlayer, 0, 0);
                            int num625 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(num622 - 20, NPC.position.Y + NPC.height - 8f), default, Main.rand.Next(61, 64), 1f);
                            Main.gore[num625].velocity *= 0.4f;
                        }
                    }
                    NPC.noTileCollide = false;
                    NPC.velocity.X *= .2f;
                    NPC.velocity.Y = -2f;
                    internalAI[4] = 0f;
                    NPC.ai[0] = 0;
                    NPC.netUpdate = true;
                    return;
                }
                if(Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > 1000)
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    internalAI[4] = 2f;
                    NPC.ai[0] = 0;
                }
            }
            else if(internalAI[4] == 2f)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                FlyAI();
                if(Math.Abs(NPC.Center.X - player.Center.X) < 50f && player.position.Y > NPC.Center.Y + NPC.height / 2)
                {
                    internalAI[4] = 3f;
                    NPC.netUpdate = true;
                }
            }
            else if(internalAI[4] == 3f)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                isDashing = true;
                if(player.velocity.X == 0)
                {
                    NPC.velocity = (player.Center - NPC.Center) * .06f;
                }
                else
                {
                    NPC.velocity = (player.Center + new Vector2(100f * (player.velocity.X > 0? 1 : -1), 0) - NPC.Center) * .06f;
                }
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 26f;
                if(NPC.velocity.X > 10f) NPC.velocity.X = 10f;
                internalAI[0] = 0f;
                internalAI[4] = 1f;
            }
            else if(internalAI[4] == 4f)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = false;
                isDashing = false;
                if (player.Center.Y + player.height / 2 <= NPC.Center.Y + NPC.height / 2 + 20f) 
                {
                    internalAI[0] = 0f;
                    internalAI[4] = 1f;
                }
            }

            if (NPC.target <= 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[2]++;
                internalAI[3]++;
            }
            if (NPC.ai[2] >= 500)
            {
                internalAI[3] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.netUpdate = true;
            }
            else if (NPC.ai[3] == 0 && NPC.ai[2] >= ChangeRate())
            {
                if (Main.rand.NextBool(5))
                {
                    Roar(roarTimerMax);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[3] = 0;
                    NPC.ai[2] = 0;
                    if (ModSupport.GetMod("ThoriumMod") != null && Main.rand.NextBool(7))
                    {
                        NPC.ai[3] = 7;
                    }
                    else
                    {
                        if (isSupreme)
                        {
                            NPC.ai[3] = Main.rand.Next(7);
                        }
                        else
                        {
                            NPC.ai[3] = Main.rand.Next(4);
                        }
                    }
                }
                NPC.netUpdate = true;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[3] == 0) //Minion Phase
                {
                    if (internalAI[3] >= 80)
                    {
                        internalAI[3] = 0;
                        if (internalAI[1] == 0)
                        {
                            if (NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon1>()) < 5)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 200, (int)NPC.Center.X + 200), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 200, (int)NPC.Center.X + 200), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 200, (int)NPC.Center.X + 200), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                            }
                            NPC.netUpdate = true;
                        }
                        else
                        {
                            if (NPC.ai[1] > 2)
                            {
                                NPC.ai[1] = 0;
                            }
                            if (NPC.ai[1] == 0)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon1>()) < 5)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                }
                            }
                            else if (NPC.ai[1] == 1)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<BunnyBrawler>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon2>()) < 5)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon2>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon2>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                }
                            }
                            else if (NPC.ai[1] == 2)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<BunnyBattler>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon3>()) < 8)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));

                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));

                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));

                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                }
                            }
                            NPC.ai[1] += 1;
                            NPC.netUpdate = true;
                        }
                    }
                }
                else if (NPC.ai[3] == 1) //Bunzooka
                {
                    if (internalAI[3] > 40)
                    {
                        internalAI[3] = 0;
                        int Rocket = isSupreme ? ModContent.ProjectileType<RajahRocketEXR>() : ModContent.ProjectileType<RajahRocket>();
                        Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, Rocket, damage, 5, Main.myPlayer);
                        NPC.netUpdate = true;
                    }
                }
                else if (NPC.ai[3] == 2) //Royal Scepter
                {
                    int carrots = isSupreme ? 5 : 3;
                    int carrotType = isSupreme ? ModContent.ProjectileType<CarrotEXR>() : ModContent.ProjectileType<CarrotHostile>();
                    float spread = 45f * 0.0174f * .5f;
                    Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                    dir *= ProjSpeed() + (isSupreme? 3 : 1);
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / carrots * 2;
                    if (internalAI[3] > 40)
                    {
                        internalAI[3] = 0;
                        for (int i = 0; i < carrots; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * (i - (int)(carrots * .5f));
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), carrotType, damage, 5, Main.myPlayer, 0);
                        }
                        NPC.netUpdate = true;
                    }
                }
                else if (NPC.ai[3] == 3) //Javelin
                {
                    int Javelin = isSupreme ? ModContent.ProjectileType<BaneTEXR>() : ModContent.ProjectileType<BaneR>();
                    if (internalAI[3] == (isSupreme ? 40 : 60))
                    {
                        float time = (player.Center - WeaponPos).Length() / ProjSpeed();
                        Vector2 dir = Vector2.Normalize(player.Center + (isSupreme? player.velocity * time : Vector2.Zero) - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, Javelin, damage, 5, Main.myPlayer);
                    }
                    if (internalAI[3] > (isSupreme ? 60 : 90))
                    {
                        internalAI[3] = 0;
                    }
                    NPC.netUpdate = true;
                }
                else if (NPC.ai[3] == 4) //Excalihare
                {
                    if (internalAI[3] > 20)
                    {
                        internalAI[3] = 0;
                        Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                        dir *= ProjSpeed() + 3f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, ModContent.ProjectileType<ExcalihareR>(), damage, 5, Main.myPlayer);
                        NPC.netUpdate = true;
                    }
                }
                else if (NPC.ai[3] == 5) //Fluffy Fury
                {
                    int Arrows = Main.rand.Next(2, 4);
                    float spread = 45f * 0.0174f * .3f;
                    float time = (player.Center - WeaponPos).Length() / ProjSpeed();
                    Vector2 dir = Vector2.Normalize(player.Center + (isSupreme? player.velocity * time : Vector2.Zero) - WeaponPos);
                    dir *= ProjSpeed() + (isSupreme? 3 : 1);
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / (Arrows * 2);
                    float delay = isSupreme? 15 : 50;
                    if (internalAI[3] > delay)
                    {
                        internalAI[3] = 0;
                        for (int i = 0; i < Arrows; i++)
                        {
                            double offsetAngle = startAngle + (deltaAngle * i);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<CarrowR>(), damage, 5, Main.myPlayer);
                        }
                        NPC.netUpdate = true;
                    }
                }
                else if (NPC.ai[3] == 6) //Rabbits Wrath
                {
                    if (internalAI[3] > 5)
                    {
                        internalAI[3] = 0;
                        Vector2 vector12 = new Vector2(player.Center.X, player.Center.Y);
                        float num75 = 14f;
                        for (int num120 = 0; num120 < 3; num120++)
                        {
                            Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                            vector2.Y -= 120 * num120;
                            Vector2 vector13 = vector12 - vector2;
                            if (vector13.Y < 0f)
                            {
                                vector13.Y *= -1f;
                            }
                            if (vector13.Y < 20f)
                            {
                                vector13.Y = 20f;
                            }
                            vector13.Normalize();
                            vector13 *= num75;
                            float num82 = vector13.X;
                            float num83 = vector13.Y;
                            float speedX5 = num82;
                            float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                            int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector2.X, vector2.Y, speedX5, speedY6, ModContent.ProjectileType<CarrotEXR>(), damage, 6, Main.myPlayer, 0, 0);
                            Main.projectile[p].tileCollide = false;
                        }
                        NPC.netUpdate = true;
                    }
                }
                else if (NPC.ai[3] == 7) //Carrot Farmer
                {
                    if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<CarrotFarmerR>()))
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<CarrotFarmerR>(), damage, 3f, Main.myPlayer, NPC.whoAmI);
                        NPC.netUpdate = true;
                    }
                }
            }

            if (Main.expertMode)
            {
                if (NPC.life < (NPC.lifeMax * .85f)) //The lower the health, the more damage is done
                {
                    NPC.damage = (int)(NPC.defDamage * 1.1f);
                }
                if (NPC.life < (NPC.lifeMax * .7f))
                {
                    NPC.damage = (int)(NPC.defDamage * 1.3f);
                }
                if (NPC.life < (NPC.lifeMax * .65f))
                {
                    NPC.damage = (int)(NPC.defDamage * 1.5f);
                }
                if (NPC.life < (NPC.lifeMax * .4f))
                {
                    NPC.damage = (int)(NPC.defDamage * 1.7f);
                }
                if (NPC.life < (NPC.lifeMax * .25f))
                {
                    NPC.damage = (int)(NPC.defDamage * 1.9f);
                }
                if (NPC.life < (NPC.lifeMax / 7))
                {
                    NPC.damage = (int)(NPC.defDamage * 2.2f);
                }
            }
            else
            {
                if (NPC.life == NPC.lifeMax / 7)
                {
                    NPC.damage = (int)(NPC.defDamage * 1.5f);
                }
            }

            NPC.rotation = 0;
        }

        public bool TileBelowEmpty()
        {
            int tileX = (int)(NPC.Center.X / 16f) + NPC.direction * 2;
            int tileY = (int)((NPC.position.Y + NPC.height) / 16f);

            for (int tY = tileY; tY < tileY + 17; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                    continue;
                if ((Main.tile[tileX, tY].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX, tY].TileType] && !TileID.Sets.Platforms[Main.tile[tileX, tY].TileType]) || Main.tile[tileX, tY].LiquidAmount > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public string WeaponTexture()
        {
            if (NPC.ai[3] == 1) //Bunzooka
            {
                return "_Content/Bunny/__Hardmode/NPCs/_BossRajah/RajahArmsB";
            }
            else if (NPC.ai[3] == 2) //Scepter
            {
                return "_Content/Bunny/__Hardmode/NPCs/_BossRajah/RajahArmsR";
            }
            else if (NPC.ai[3] == 3 && internalAI[3] <= (isSupreme ? 40 : 60)) //Javelin
            {
                return "_Content/Bunny/__Hardmode/NPCs/_BossRajah/RajahArmsS";
            }
            else if (NPC.ai[3] == 4) //Excalihare
            {
                return "_Content/Bunny/_PostMoonlord/NPCs/_BossRajahA/Excalihare";
            }
            else if (NPC.ai[3] == 5) //Fluffy Fury
            {
                return "_Content/Bunny/_PostMoonlord/NPCs/_BossRajahA/FluffyFury";
            }
            else if (NPC.ai[3] == 6) //Rabbits Wrath
            {
                return "_Content/Bunny/_PostMoonlord/NPCs/_BossRajahA/RabbitsWrath";
            }
            else
            {
                return "BlankTex";
            }
        }

        public void JumpAI()
        {
            internalAI[1] = 1;
            if (NPC.ai[0] == 0f)
            {
                NPC.noTileCollide = false;
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.8f;
                    internalAI[2] += 1f;
                    if (internalAI[2] > 0f)
                    {
                        if (NPC.life < (NPC.lifeMax * .85f)) //The lower the health, the more frequent the jumps
                        {
                            internalAI[2] += 2;
                        }
                        if (NPC.life < (NPC.lifeMax * .7f))
                        {
                            internalAI[2] += 2;
                        }
                        if (NPC.life < (NPC.lifeMax * .65f))
                        {
                            internalAI[2] += 2;
                        }
                        if (NPC.life < (NPC.lifeMax * .4f))
                        {
                            internalAI[2] += 2;
                        }
                        if (NPC.life < (NPC.lifeMax * .25f))
                        {
                            internalAI[2] += 2;
                        }
                        if (NPC.life < (NPC.lifeMax * .1f))
                        {
                            internalAI[2] += 2;
                        }
                    }
                    if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) > 800f)
                    {
                        internalAI[2] = -1f;
                    }
                    if (internalAI[2] >= 250f)
                    {
                        internalAI[2] = -20f;
                    }
                    else if (internalAI[2] == -1f)
                    {
                        NPC.TargetClosest(true);
                        float longth = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X);
                        NPC.velocity.X = (6 + longth * .01f) * NPC.direction;
                        NPC.velocity.Y = -12.1f;
                        NPC.ai[0] = 1f;
                        internalAI[2] = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                if (NPC.velocity.Y == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                    NPC.ai[0] = 0f;
                    for (int num622 = (int)NPC.position.X - 20; num622 < (int)NPC.position.X + NPC.width + 40; num622 += 20)
                    {
                        for (int num623 = 0; num623 < 4; num623++)
                        {
                            int num624 = Dust.NewDust(new Vector2(NPC.position.X - 20f, NPC.position.Y + NPC.height), NPC.width + 20, 4, DustID.Smoke, 0f, 0f, 100);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(NPC.GetSource_Death(), new Vector2(num622 - 20, NPC.position.Y + NPC.height - 8f), default, Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
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
                        
                        float num626 = 3f;
                        float longth = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X);
                        num626 = 3f + longth * .056f;
                        
                        if (Main.player[NPC.target].velocity.X != 0)
                        {
                            num626 += Math.Abs(Main.player[NPC.target].velocity.X);
                        }

                        if (NPC.direction < 0)
                        {
                            NPC.velocity.X = NPC.velocity.X - 0.2f;
                        }
                        else if (NPC.direction > 0)
                        {
                            NPC.velocity.X = NPC.velocity.X + 0.2f;
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

                Player player = Main.player[NPC.target];
                if(player.Center.Y + player.height / 2 <= NPC.Center.Y + NPC.height / 2 + 20f && NPC.velocity.Y > 0)
                {
                    internalAI[4] = 4f;
                    NPC.ai[0] = 0;
                    NPC.netUpdate = true;
                    return;
                }
                else if(Math.Abs(NPC.Center.X - player.Center.X) < 50f && player.position.Y > NPC.Center.Y + NPC.height / 2)
                {
                    internalAI[4] = 3f;
                    NPC.ai[0] = 0;
                    NPC.netUpdate = true;
                    return;
                }
            }
        }

        bool isDashing = false;
        public void FlyAI()
        {
            float speed = 14f;
            if (isSupreme)
            {
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > 1000)
                {
                    speed = 50f;
                    isDashing = true;
                }
                else
                {
                    speed = 20f;
                    isDashing = false;
                }
            }
            else if (NPC.life < (NPC.lifeMax * .85f)) //The lower the health, the more damage is done
            {
                speed = 15f;
            }
            else if (NPC.life < (NPC.lifeMax * .7f))
            {
                speed = 16f;
            }
            else if (NPC.life < (NPC.lifeMax * .65f))
            {
                speed = 17f;
            }
            else if (NPC.life < (NPC.lifeMax * .4f))
            {
                speed = 18f;
            }
            else if (NPC.life < (NPC.lifeMax * .25f))
            {
                speed = 19f;
            }
            else if (NPC.life < (NPC.lifeMax * .1f))
            {
                speed = 20f;
            }
            AISpaceOctopus(NPC, Main.player[NPC.target].Center, .35f, speed, 300);
            internalAI[1] = 0;
        }

        public static void AISpaceOctopus(NPC npc, Vector2 targetCenter = default(Vector2), float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f)
		{
            float pos = 200f;
            if(Main.player[npc.target].velocity.X == 0)
            {
                pos = 0;
            }
            else
            {
                pos = (Main.player[npc.target].velocity.X > 0? 1f: -1f) * 200f;
            }
			Vector2 wantedVelocity = targetCenter - npc.Center + new Vector2(pos, -hoverDistance);
			float dist = (float)Math.Sqrt(wantedVelocity.X * wantedVelocity.X + wantedVelocity.Y * wantedVelocity.Y);
			if (dist < 20f)
			{
				wantedVelocity = npc.velocity;
			}
			else if (dist < 40f)
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax * 0.35f;
			}
			else if (dist < 80f)
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax * 0.65f;
			}
			else
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax;
			}
			if (npc.velocity.X < wantedVelocity.X)
			{
				npc.velocity.X = npc.velocity.X + moveSpeed;
				if (npc.velocity.X < 0f && wantedVelocity.X > 0f)
				{
					npc.velocity.X = npc.velocity.X + moveSpeed;
				}
			}
			else if (npc.velocity.X > wantedVelocity.X)
			{
				npc.velocity.X = npc.velocity.X - moveSpeed;
				if (npc.velocity.X > 0f && wantedVelocity.X < 0f)
				{
					npc.velocity.X = npc.velocity.X - moveSpeed;
				}
			}
			if (npc.velocity.Y < wantedVelocity.Y)
			{
				npc.velocity.Y = npc.velocity.Y + moveSpeed;
				if (npc.velocity.Y < 0f && wantedVelocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + moveSpeed;
				}
			}
			else if (npc.velocity.Y > wantedVelocity.Y)
			{
				npc.velocity.Y = npc.velocity.Y - moveSpeed;
				if (npc.velocity.Y > 0f && wantedVelocity.Y < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - moveSpeed;
				}
			}
        }

        public int ChangeRate()
        {
            if (NPC.type == ModContent.NPCType<SupremeRajah>())
            {
                return 120;
            }
            return 240;
        }

        public override void FindFrame(int frameHeight)
        {
            if (internalAI[1] == 0)
            {
                WeaponFrame = frameHeight * 5;
                if (NPC.frameCounter++ > 3)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                    if (NPC.frame.Y > frameHeight * 7)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                WeaponFrame = NPC.frame.Y;
                if (NPC.ai[0] == 0f)
                {
                    if (internalAI[2] < -17f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                    else if (internalAI[2] < -14f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight;
                    }
                    else if (internalAI[2] < -11f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 2;
                    }
                    else if (internalAI[2] < -8f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 3;
                    }
                    else if (internalAI[2] < -5f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 4;
                    }
                    else if (internalAI[2] < -2f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        if (NPC.frameCounter++ > 7.5f)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += frameHeight;
                            if (NPC.frame.Y > frameHeight * 2)
                            {
                                NPC.frame.Y = 0;
                            }
                        }
                    }
                }
                else if (NPC.ai[0] == 1f)
                {
                    if (NPC.velocity.Y != 0f)
                    {
                        NPC.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        NPC.frameCounter++;
                        if  (NPC.frame.Y > 3)
                        {
                            if (NPC.frameCounter > 0)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y = frameHeight * 6;
                            }
                            else if (NPC.frameCounter > 4)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y = frameHeight * 7;
                            }
                            else if (NPC.frameCounter > 8)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y = 0;
                            }
                        }
                        else
                        {
                            if (NPC.frameCounter > 7.5f)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y += frameHeight;
                                if (NPC.frame.Y > frameHeight * 2)
                                {
                                    NPC.frame.Y = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void OnKill()
        {
            if (isSupreme)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("SupremeRajahHelmet1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("SupremeRajahHelmet2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("SupremeRajahHelmet3").Type, 1f);
                if (!NPCExtensions.BeenKilled<SupremeRajah>(true))
                {
                    int n = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SupremeRajahDefeat>());
                    Main.npc[n].Center = NPC.Center;
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (Main.netMode != NetmodeID.SinglePlayer)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.Repeat.Multiplayer"), 107, 137, 179, true);
                        else
                            BaseUtility.Chat(Language.GetOrRegister("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.Repeat.Singleplayer").FormatWith(Main.LocalPlayer.name), 107, 137, 179, true);
                    }
                    int p = Projectile.NewProjectile(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.ProjectileType<SupremeRajahLeave>(), 100, 0, Main.myPlayer);
                    Main.projectile[p].Center = NPC.Center;
                }
            }
            else
            {
                int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                if (bunnyKills >= 100)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Defeat.Murderer"), 107, 137, 179, true);
                }
                Projectile.NewProjectile(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
            }
            //NPC.value = 0f;
            //NPC.boss = false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RajahRabbitTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahRabbitMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahPelt>(), 1, 10, 26));

            List<int> lootTable = [ ModContent.ItemType<BaneOfTheBunny>(), ModContent.ItemType<Bunzooka>(), ModContent.ItemType<RoyalScepter>(), ModContent.ItemType<ThePunisher>(), ModContent.ItemType<RabbitcopterEars>() ];
            if (ModSupport.GetMod("ThoriumMod") != null)
                lootTable.Add(ModContent.ItemType<CarrotFarmer>());

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable.ToArray()));

            npcLoot.Add(notExpertRule);
        }

        public override void BossLoot(ref int potionType)
        {
            if (isSupreme)
            {
                potionType = ModContent.ItemType<TheBigOne>();
                return;
            }
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * .6f);
        }

        public void RajahTexture()
        {
            string IsRoaring = Roaring ? "Roar" : "";
            string path = isSupreme ? "/_PostMoonlord/NPCs/_BossRajahA/Supreme" : "/__Hardmode/NPCs/_BossRajah/";
            if (internalAI[1] == 0)
            {
                RajahTex = ModContent.Request<Texture2D>("_Content/Bunny" + path + "Rajah" + IsRoaring + "_Fly").Value;
                Glow = ModContent.Request<Texture2D>("Glowmasks/Rajah" + IsRoaring + "_Fly_Glow").Value;
                SupremeGlow = ModContent.Request<Texture2D>("Glowmasks/SupremeRajah" + IsRoaring + "_Fly_Glow").Value;
                SupremeEyes = ModContent.Request<Texture2D>("Glowmasks/SupremeRajah" + IsRoaring + "_Fly_Eyes").Value;
            }
            else
            {
                RajahTex = ModContent.Request<Texture2D>("_Content/Bunny" + path + "Rajah" + IsRoaring).Value;
                Glow = ModContent.Request<Texture2D>("Glowmasks/Rajah" + IsRoaring + "_Glow").Value;
                SupremeGlow = ModContent.Request<Texture2D>("Glowmasks/SupremeRajah" + IsRoaring + "_Glow").Value;
                SupremeEyes = ModContent.Request<Texture2D>("Glowmasks/SupremeRajah" + IsRoaring + "_Eyes").Value;
            }
        }
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            bool RageMode = !isSupreme && NPC.life < NPC.lifeMax / 7;
            bool SupremeRageMode = isSupreme && NPC.life < NPC.lifeMax / 7;
            RajahTexture();
            if (isSupreme && isDashing)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, RajahTex, 0, NPC, 1f, 1f, 10, false, 0f, 0f, Main.DiscoColor);
            }
            if (RageMode)
            {
                Color RageColor = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Firebrick, drawColor, Color.Firebrick);
                BaseDrawing.DrawAura(spriteBatch, RajahTex, 0, NPC.position, NPC.width, NPC.height, auraPercent, 1f, 1f, 0f, NPC.direction, 8, NPC.frame, 0f, -5f, RageColor);
            }
            else if (SupremeRageMode)
            {
                BaseDrawing.DrawAura(spriteBatch, RajahTex, 0, NPC.position, NPC.width, NPC.height, auraPercent, 1f, 1f, 0f, NPC.direction, 8, NPC.frame, 0f, -5f, Main.DiscoColor);
            }
            if (NPC.ai[3] != 0 && NPC.ai[3] < 6) //If holding a weapon
            {
                ArmTex = ModContent.Request<Texture2D>(WeaponTexture()).Value;
                Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
                BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, WeaponRectangle, drawColor, true);
            }
            BaseDrawing.DrawTexture(spriteBatch, RajahTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, drawColor, true);
            if (NPC.ai[3] == 6) //If Rabbits Wrath
            {
                ArmTex = ModContent.Request<Texture2D>("_Content/Bunny/_PostMoonlord/NPCs/_BossRajahA/RabbitsWrath").Value;
                Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
                BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, WeaponRectangle, drawColor, true);
            }
            if (RageMode)
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                BaseDrawing.DrawTexture(spriteBatch, Glow, shader, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, Color.White, true);
            }
            if (SupremeRageMode)
            {
                BaseDrawing.DrawTexture(spriteBatch, Glow, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, Main.DiscoColor, true);
                BaseDrawing.DrawAura(spriteBatch, Glow, 0, NPC.position, NPC.width, NPC.height, auraPercent, 1f, 1f, 0f, NPC.direction, 8, NPC.frame, 0f, -5f, Main.DiscoColor);
                BaseDrawing.DrawTexture(spriteBatch, SupremeGlow, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, Main.DiscoColor, true);
                BaseDrawing.DrawAura(spriteBatch, SupremeGlow, 0, NPC.position, NPC.width, NPC.height, auraPercent, 1f, 1f, 0f, NPC.direction, 8, NPC.frame, 0f, -5f, Main.DiscoColor);
                return false;
            }
            else if (isSupreme)
            {
                BaseDrawing.DrawTexture(spriteBatch, SupremeEyes, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, Main.DiscoColor, true);
            }
            return false;
        }

        public override string BossHeadTexture => "AAModClassic/_Content/Bunny/__Hardmode/NPCs/_BossRajah/Rajah_Head_Boss";

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 30f;
            if (moveSpeed == 0f || NPC.Center == point) return;
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if(internalAI[4] == 4f || internalAI[4] == 2f || internalAI[4] == 1f)
            {
                target.wingTime = 0;
                target.velocity.Y = 1f;
            }
        }
    }
}
