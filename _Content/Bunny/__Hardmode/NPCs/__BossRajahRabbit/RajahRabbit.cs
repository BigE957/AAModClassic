using AAModClassic._Content._Misc._PostMoonlord.Items.Consumables;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons;
using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._CrossMod.Thorium.Weapons.Healer;
using AAModClassic.Assets;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit
{
    [AutoloadBossHead]
    public class RajahRabbit : ModNPC
    {
        public ref float TimerJump => ref NPC.ai[0];
        public ref float TimerSwapWeapon => ref NPC.ai[1];
        public ref float TimerPerformAttack => ref NPC.ai[2];
        /// <summary>
        /// 0 is rabbitcopters, 1 is bunny brawlers, 2 is rabid rabbits
        /// </summary>
        public ref float MinionToSummon => ref NPC.ai[3];

        public Player TargetPlayer;

        public bool IsJumping = false;
        public bool IsFlying = false;
        public bool isSupreme = false;

        public int RoarTimer = 0;
        public int RoarTimerMax = 240;
        public bool IsRoaring => RoarTimer > 0;

        public int JumpDelayTimer = -1;

        private Texture2D WeaponTex;
        public int WeaponFrame = 0;
        /// <summary>
        /// the very middle of rajahs weapon hand
        /// </summary>
        public Vector2 WeaponPos;
        public Projectile CurrentlyHeldProj = null;
        public bool DrawCottonCane = false;

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        private bool SayLine = false;
        private bool DefenseLine = false;

        public int damage = 0;

        public RajahAttacks CurrentAttack = RajahAttacks.Nothing;
        public RajahMovements CurrentMovement = RajahMovements.Idle;

        public enum RajahAttacks
        {
            Nothing = -1,
            CottonCane = 0,
            Bunzooka = 1,
            RoyalScepter = 2,
            BaneOfTheBunny = 3,
            Excalihare = 4,
            FluffyFury = 5,
            RabbitsWrath = 6,
            CarrotFarmer = 7,
            ThePunisher = 8
        }

        public enum RajahMovements
        {
            Idle = 0,
            Stomp = 1,
            Fly = 2,
            BeginStomp = 3,
        }

        public static string ATexture = ModContent.GetInstance<RajahRabbitA>().Texture;

        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> SupremeGlowmask;
        public static Asset<Texture2D> SupremeEyes;

        public static Asset<Texture2D> Arms_Bunzooka;
        public static Asset<Texture2D> Arms_RoyalScepter;
        public static Asset<Texture2D> Arms_BaneOfTheBunny;
        public static Asset<Texture2D> Arms_Excalihare;
        public static Asset<Texture2D> Arms_FluffyFury;
        public static Asset<Texture2D> Arms_RabbitsWrath;
        public static Asset<Texture2D> Arms_CottonCane;
        public static Asset<Texture2D> BlankTex;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[NPC.type] = 8;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            SupremeGlowmask = ModContent.Request<Texture2D>(ATexture + "_Glow");
            SupremeEyes = ModContent.Request<Texture2D>(ATexture + "_Eyes");

            Arms_Bunzooka = ModContent.Request<Texture2D>(Texture + "_Arms_Bunzooka");
            Arms_RoyalScepter = ModContent.Request<Texture2D>(Texture + "_Arms_RoyalScepter");
            Arms_BaneOfTheBunny = ModContent.Request<Texture2D>(Texture + "_Arms_BaneOfTheBunny");
            Arms_Excalihare = ModContent.Request<Texture2D>(ATexture + "_Arms_Excalihare");
            Arms_FluffyFury = ModContent.Request<Texture2D>(ATexture + "_Arms_FluffyFury");
            Arms_RabbitsWrath = ModContent.Request<Texture2D>(ATexture + "_Arms_RabbitsWrath");
            Arms_CottonCane = ModContent.Request<Texture2D>(Texture + "_Unofficial_Arms_CottonCane");
            BlankTex = ModContent.Request<Texture2D>(AssetDirectory.General.Nothing);

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new(0, 108),
                PortraitPositionYOverride = 56,
                Scale = 0.75f,
                PortraitScale = 0.6f,
                SpriteDirection = 1
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
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
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/Rajah");
            NPC.value = Item.buyPrice(0, 1, 10, 0);
            NPC.boss = true;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Rajah"];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(IsJumping);
                writer.Write(IsFlying);
                writer.Write(isSupreme);

                writer.Write((byte)CurrentAttack);
                writer.Write((byte)CurrentMovement);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                IsJumping = reader.ReadBoolean();
                IsFlying = reader.ReadBoolean(); 
                isSupreme = reader.ReadBoolean();

                CurrentAttack = (RajahAttacks)reader.ReadByte();
                CurrentMovement = (RajahMovements)reader.ReadByte();
            }
        }

        public void Roar(int timer)
        {
            RoarTimer = timer;
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Rajah"), NPC.Center);
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (isSupreme)
            {
                modifiers.TargetDamageMultiplier *= .7f;
            }
        }

        public float ProjSpeed()
        {
            if (NPC.life < NPC.lifeMax * .85f) //The lower the health, the more damage is done
            {
                return isSupreme ? 12f : 10f;
            }
            if (NPC.life < NPC.lifeMax * .7f)
            {
                return isSupreme ? 13f : 11f;
            }
            if (NPC.life < NPC.lifeMax * .65f)
            {
                return isSupreme ? 14f : 12f;
            }
            if (NPC.life < NPC.lifeMax * .4f)
            {
                return isSupreme ? 15f : 13f;
            }
            if (NPC.life < NPC.lifeMax * .25f)
            {
                return isSupreme ? 16f : 14f;
            }
            if (NPC.life < NPC.lifeMax * .1f)
            {
                return isSupreme ? 16f : 15f;
            }
            return isSupreme ? 11f : 9f;
        }

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
            WeaponPos = new Vector2(NPC.Center.X + (78 * NPC.spriteDirection), NPC.Center.Y - 9);

            if (IsRoaring) 
                RoarTimer--;

            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.type == ModContent.NPCType<RajahRabbitA>() && isSupreme == false)
            {
                isSupreme = true;
                NPC.netUpdate = true;
            }

            if (isSupreme)
            {
                if (CurrentAttack != RajahAttacks.CottonCane && !DefenseLine && !NPCExtensions.BeenKilled<RajahRabbitA>() && Main.netMode != NetmodeID.MultiplayerClient)
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
                        bool evilMaxxing = bunnyKills >= 100 && !NPCExtensions.BeenKilled<RajahRabbitA>();

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
                            else if(!NPCExtensions.BeenKilled<RajahRabbitA>())
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Singleplayer.Normal"), 107, 137, 179);
                            else
                                BaseUtility.Chat(Language.GetOrRegister("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.LastStand.Singleplayer.Repeat").FormatWith(Main.LocalPlayer.name.ToUpper()), 107, 137, 179);
                        }
                    }
                    Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
                }
            }

            if (NPC.target <= 0 || NPC.target == 255 || TargetPlayer.dead)
                NPC.TargetClosest(false);
            TargetPlayer = Main.player[NPC.target];

            // on player death
            if (NPC.target >= 0 && TargetPlayer.dead)
            {
                NPC.TargetClosest(true);
                if (TargetPlayer.dead)
                {
                    if (isSupreme)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Kill"), 107, 137, 179);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahRabbitABookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Kill"), 107, 137, 179);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahRabbitBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    NPC.active = false;
                    NPC.noTileCollide = true;
                    NPC.netUpdate = true;
                    return;
                }
            }

            // on despawn
            if (Math.Abs(NPC.Center.X - TargetPlayer.Center.X) + Math.Abs(NPC.Center.Y - TargetPlayer.Center.Y) > 10000)
            {
                NPC.TargetClosest(true);
                if (Math.Abs(NPC.Center.X - TargetPlayer.Center.X) + Math.Abs(NPC.Center.Y - TargetPlayer.Center.Y) > 10000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Despawn"), 107, 137, 179);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (isSupreme)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahRabbitABookIt>(), damage, 0, Main.myPlayer); //Originally 100 damage
                        }
                        else
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahRabbitBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    NPC.active = false;
                    NPC.noTileCollide = true;
                    NPC.netUpdate = true;
                    return;
                }
            }

            if (TargetPlayer.Center.X < NPC.Center.X)
                NPC.direction = 1;
            else
                NPC.direction = -1;

            if (CurrentMovement == RajahMovements.Idle)
            {
                if(TargetPlayer.Center.Y + TargetPlayer.height / 2 < NPC.Center.Y + NPC.height / 2 - 30f || Math.Abs(NPC.Center.X - TargetPlayer.Center.X) + Math.Abs(NPC.Center.Y - TargetPlayer.Center.Y) > 2000 || isDashing)
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    CurrentMovement = RajahMovements.Fly;
                    IsJumping = false;
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
            else if(CurrentMovement == RajahMovements.Stomp)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                isDashing = false;
                if (TargetPlayer.Center.Y + TargetPlayer.height / 2 <= NPC.Center.Y + NPC.height / 2 + 20f) 
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
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), num622 - 20, NPC.position.Y + NPC.height - 8f, 0, 0, ModContent.ProjectileType<RajahRabbit_Stomp>(), damage, 6, Main.myPlayer, 0, 0);
                            int num625 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(num622 - 20, NPC.position.Y + NPC.height - 8f), default, Main.rand.Next(61, 64), 1f);
                            Main.gore[num625].velocity *= 0.4f;
                        }
                    }
                    NPC.noTileCollide = false;
                    NPC.velocity.X *= .2f;
                    NPC.velocity.Y = -2f;
                    CurrentMovement = RajahMovements.Idle;
                    IsJumping = false;
                    NPC.netUpdate = true;
                    return;
                }
                if(Math.Abs(NPC.Center.Y - TargetPlayer.Center.Y) > 1000)
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    CurrentMovement = RajahMovements.Fly;
                    IsJumping = false;
                }
            }
            else if(CurrentMovement == RajahMovements.Fly)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                FlyAI();
                if(Math.Abs(NPC.Center.X - TargetPlayer.Center.X) < 50f && TargetPlayer.Center.Y > NPC.Center.Y + NPC.height / 2)
                {
                    CurrentMovement = RajahMovements.BeginStomp;
                    NPC.netUpdate = true;
                }
            }
            else if(CurrentMovement == RajahMovements.BeginStomp)
            {
                bool performStomp = false;
                if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    performStomp = true;
                if (JumpDelayTimer >= 8)
                {
                    JumpDelayTimer = -1;
                    NPC.velocity.X = 0f;
                    performStomp = true;
                }

                if (performStomp)
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    isDashing = true;

                    float stompDampen = 0.06f;
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        stompDampen = 0.08f;

                    if (TargetPlayer.velocity.X == 0)
                        NPC.velocity = (TargetPlayer.Center - NPC.Center) * stompDampen;
                    else
                        NPC.velocity = (TargetPlayer.Center + new Vector2(100f * (TargetPlayer.velocity.X > 0 ? 1 : -1), 0) - NPC.Center) * stompDampen;

                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        NPC.velocity.X = 0;

                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 26f;

                    if (NPC.velocity.X > 10f)
                        NPC.velocity.X = 10f;

                    CurrentMovement = RajahMovements.Stomp;
                }
                else
                {
                    if (JumpDelayTimer <= -1)
                    {
                        NPC.velocity.Y = -8f;
                        JumpDelayTimer = 0;
                    }
                    else if (JumpDelayTimer >= 0)
                    {
                        JumpDelayTimer++;
                        NPC.velocity.Y -= 0.06f;
                    }
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                TimerSwapWeapon++;
                TimerPerformAttack++;
            }

            WeaponPos = NPC.Center + NPC.velocity;
            WeaponPos += new Vector2(0, -16);
            WeaponPos += new Vector2(100, 0) * NPC.spriteDirection;
            if (IsFlying == true) // account for anims
                WeaponPos.Y -= 26;
            else
            {
                switch (NPC.frame.Y)
                {
                    case 0:
                        break;
                    case 220:
                        WeaponPos.Y += 2;
                        break;
                    case 220 * 2:
                        WeaponPos.Y += 6;
                        break;
                    case 220 * 3:
                        break;
                    case 220 * 4:
                        WeaponPos.Y -= 20;
                        break;
                    case 220 * 5:
                        WeaponPos.Y -= 26;
                        break;
                    case 220 * 6:
                        WeaponPos.Y -= 22;
                        break;
                    case 220 * 7:
                        WeaponPos.Y += 8;
                        break;
                }
            }

            if (TimerSwapWeapon >= 500)
            {
                TimerPerformAttack = 0;
                TimerSwapWeapon = 0;
                CurrentAttack = RajahAttacks.CottonCane;
                NPC.netUpdate = true;
            }
            else if (CurrentAttack == RajahAttacks.CottonCane && TimerSwapWeapon >= ChangeRate())
            {
                if (Main.rand.NextBool(5))
                {
                    Roar(RoarTimerMax);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    TimerPerformAttack = 0;
                    TimerSwapWeapon = 0;

                    List<RajahAttacks> elegibleAttacks =
                    [
                        RajahAttacks.Bunzooka,
                        RajahAttacks.RoyalScepter,
                        RajahAttacks.BaneOfTheBunny
                    ];

                    if (isSupreme)
                    {
                        elegibleAttacks.Add(RajahAttacks.Excalihare);
                        elegibleAttacks.Add(RajahAttacks.FluffyFury);
                        elegibleAttacks.Add(RajahAttacks.RabbitsWrath);
                    }
                    if (ModLoader.TryGetMod("ThoriumMod", out _))
                        elegibleAttacks.Add(RajahAttacks.CarrotFarmer);
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        elegibleAttacks.Add(RajahAttacks.ThePunisher);

                    CurrentAttack = elegibleAttacks[Main.rand.Next(elegibleAttacks.Count)];

                    if (CurrentlyHeldProj != null)
                        CurrentlyHeldProj.active = false;
                    CurrentlyHeldProj = null;
                }
                NPC.netUpdate = true;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (CurrentAttack == RajahAttacks.CottonCane) 
                {
                    // this kind of sucks but we cant use changeRateMinusOne = ChangeRate() - (ChangeRate() % 80) bcuz then the top instance will be 0 and nothing will be subtracted 
                    int changeRateMinusOne = ChangeRate() - 80;
                    if (isSupreme)
                        changeRateMinusOne = ChangeRate() - 40;

                    if ((IsFlying == true || MinionToSummon == 0) && NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + NPC.CountNPCS(ModContent.NPCType<SupremeRabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>()) >= 5)
                        DrawCottonCane = false;
                    else if (MinionToSummon == 1 && NPC.CountNPCS(ModContent.NPCType<BunnyBrawler>()) + NPC.CountNPCS(ModContent.NPCType<SupremeBunnyBrawler>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_BunnyBrawlerSummon>()) >= 5)
                        DrawCottonCane = false;
                    else if (MinionToSummon == 2 && NPC.CountNPCS(ModContent.NPCType<RabidRabbit>()) + NPC.CountNPCS(ModContent.NPCType<SupremeRabidRabbit>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_BunnyBattlerSummon>()) >= 8)
                        DrawCottonCane = false;
                    else if (TimerSwapWeapon > changeRateMinusOne)
                        DrawCottonCane = false;
                    else
                        DrawCottonCane = true;

                    if (TimerPerformAttack >= 80)
                    {
                        TimerPerformAttack = 0;
                        if (IsFlying == true)
                        {
                            if (NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + NPC.CountNPCS(ModContent.NPCType<SupremeRabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>()) < 5)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 200, (int)NPC.Center.X + 200), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 200, (int)NPC.Center.X + 200), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 200, (int)NPC.Center.X + 200), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                            }
                            NPC.netUpdate = true;
                        }
                        else
                        {
                            if (MinionToSummon == 0)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + NPC.CountNPCS(ModContent.NPCType<SupremeRabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>()) < 5)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_RabbitcopterSoldierSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                }
                            }
                            else if (MinionToSummon == 1)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<BunnyBrawler>()) + NPC.CountNPCS(ModContent.NPCType<SupremeBunnyBrawler>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_BunnyBrawlerSummon>()) < 5)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_BunnyBrawlerSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_BunnyBrawlerSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                }
                            }
                            else if (MinionToSummon == 2)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<RabidRabbit>()) + NPC.CountNPCS(ModContent.NPCType<SupremeRabidRabbit>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<RajahRabbit_BunnyBattlerSummon>()) < 8)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_BunnyBattlerSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_BunnyBattlerSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_BunnyBattlerSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_BunnyBattlerSummon>(), 0, 0, Main.myPlayer, Main.rand.Next((int)NPC.Center.X - 500, (int)NPC.Center.X + 500), Main.rand.Next((int)NPC.Center.Y - 200, (int)NPC.Center.Y - 50));
                                }
                            }

                            MinionToSummon += 1;
                            if (MinionToSummon > 2)
                                MinionToSummon = 0;
                            NPC.netUpdate = true;
                        }
                    }
                }
                else if (CurrentAttack == RajahAttacks.Bunzooka)
                {
                    if (TimerPerformAttack > 40)
                    {
                        TimerPerformAttack = 0;
                        int Rocket = isSupreme ? ModContent.ProjectileType<RajahRabbitA_RajahRocket>() : ModContent.ProjectileType<RajahRabbit_RajahRocket>();
                        Vector2 dir = Vector2.Normalize(TargetPlayer.Center - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, Rocket, damage, 5, Main.myPlayer);
                        NPC.netUpdate = true;
                    }
                }
                else if (CurrentAttack == RajahAttacks.RoyalScepter)
                {
                    int carrots = isSupreme ? 5 : 3;
                    int carrotType = isSupreme ? ModContent.ProjectileType<RajahRabbitA_GoldenCarrot>() : ModContent.ProjectileType<RajahRabbit_Carrot>();
                    float spread = 45f * 0.0174f * .5f;
                    Vector2 dir = Vector2.Normalize(TargetPlayer.Center - WeaponPos);
                    dir *= ProjSpeed() + (isSupreme? 3 : 1);
                    float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / carrots * 2;
                    if (TimerPerformAttack > 40)
                    {
                        TimerPerformAttack = 0;
                        for (int i = 0; i < carrots; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * (i - (int)(carrots * .5f));
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), carrotType, damage, 5, Main.myPlayer, 0);
                        }
                        NPC.netUpdate = true;
                    }
                }
                else if (CurrentAttack == RajahAttacks.BaneOfTheBunny)
                {
                    int Javelin = isSupreme ? ModContent.ProjectileType<RajahRabbitA_BaneOfTheSlaughterer>() : ModContent.ProjectileType<RajahRabbit_BaneOfTheBunny>();
                    if (TimerPerformAttack == (isSupreme ? 40 : 60))
                    {
                        float time = (TargetPlayer.Center - WeaponPos).Length() / ProjSpeed();
                        Vector2 dir = Vector2.Normalize(TargetPlayer.Center + (isSupreme? TargetPlayer.velocity * time : Vector2.Zero) - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, Javelin, damage, 5, Main.myPlayer);
                    }
                    if (TimerPerformAttack > (isSupreme ? 60 : 90))
                    {
                        TimerPerformAttack = 0;
                    }
                    NPC.netUpdate = true;
                }
                else if (CurrentAttack == RajahAttacks.Excalihare)
                {
                    if (TimerPerformAttack > 20)
                    {
                        TimerPerformAttack = 0;
                        Vector2 dir = Vector2.Normalize(TargetPlayer.Center - WeaponPos);
                        dir *= ProjSpeed() + 3f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, ModContent.ProjectileType<RajahRabbitA_Excalihare>(), damage, 5, Main.myPlayer);
                        NPC.netUpdate = true;
                    }
                }
                else if (CurrentAttack == RajahAttacks.FluffyFury)
                {
                    int Arrows = Main.rand.Next(2, 4);
                    float spread = 45f * 0.0174f * .3f;
                    float time = (TargetPlayer.Center - WeaponPos).Length() / ProjSpeed();
                    Vector2 dir = Vector2.Normalize(TargetPlayer.Center + (isSupreme? TargetPlayer.velocity * time : Vector2.Zero) - WeaponPos);
                    dir *= ProjSpeed() + (isSupreme? 3 : 1);
                    float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / (Arrows * 2);
                    float delay = isSupreme? 15 : 50;
                    if (TimerPerformAttack > delay)
                    {
                        TimerPerformAttack = 0;
                        for (int i = 0; i < Arrows; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<RajahRabbitA_Carrow>(), damage, 5, Main.myPlayer);
                        }
                        NPC.netUpdate = true;
                    }
                }
                else if (CurrentAttack == RajahAttacks.RabbitsWrath)
                {
                    if (TimerPerformAttack > 5)
                    {
                        TimerPerformAttack = 0;
                        Vector2 vector12 = new Vector2(TargetPlayer.Center.X, TargetPlayer.Center.Y);
                        float num75 = 14f;
                        for (int num120 = 0; num120 < 3; num120++)
                        {
                            Vector2 vector2 = TargetPlayer.Center + new Vector2(-(float)Main.rand.Next(0, 401) * TargetPlayer.direction, -600f);
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
                            int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector2.X, vector2.Y, speedX5, speedY6, ModContent.ProjectileType<RajahRabbitA_GoldenCarrot>(), damage, 6, Main.myPlayer, 0, 0);
                            Main.projectile[p].tileCollide = false;
                        }
                        NPC.netUpdate = true;
                    }
                }
                else if (CurrentAttack == RajahAttacks.CarrotFarmer) 
                {
                    if (CurrentlyHeldProj == null || CurrentlyHeldProj.active == false || CurrentlyHeldProj.type != ModContent.ProjectileType<RajahRabbit_CarrotFarmer>())
                    {
                        CurrentlyHeldProj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), WeaponPos, Vector2.Zero, ModContent.ProjectileType<RajahRabbit_CarrotFarmer>(), damage, 3f, Main.myPlayer, NPC.whoAmI);
                        NPC.netUpdate = true;
                    }

                    if (CurrentlyHeldProj != null)
                    {
                        CurrentlyHeldProj.Center = WeaponPos;
                    }
                }
                else if (CurrentAttack == RajahAttacks.ThePunisher)
                {
                    if (CurrentlyHeldProj == null || CurrentlyHeldProj.active == false || (CurrentlyHeldProj.type != ModContent.ProjectileType<RajahRabbit_ThePunisher>() && CurrentlyHeldProj.type != ModContent.ProjectileType<RajahRabbitA_TheAvenger>()))
                    {
                        TimerPerformAttack++;

                        float knockBack = isSupreme == true ? 7f : 6.5f;
                        int type = isSupreme == true ? ModContent.ProjectileType<RajahRabbitA_TheAvenger>() : ModContent.ProjectileType<RajahRabbit_ThePunisher>();
                        if (TimerPerformAttack >= 90)
                        {
                            CurrentlyHeldProj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), WeaponPos, WeaponPos.DirectionTo(TargetPlayer.Center) * 15, type, damage, knockBack, Main.myPlayer, 0, 0, NPC.whoAmI);
                            TimerPerformAttack = 0;
                        }
                        NPC.netUpdate = true;
                    }
                }
            }

            if (Main.expertMode)
            {
                if (NPC.life < NPC.lifeMax * .85f) //The lower the health, the more damage is done
                {
                    NPC.damage = (int)(NPC.defDamage * 1.1f);
                }
                if (NPC.life < NPC.lifeMax * .7f)
                {
                    NPC.damage = (int)(NPC.defDamage * 1.3f);
                }
                if (NPC.life < NPC.lifeMax * .65f)
                {
                    NPC.damage = (int)(NPC.defDamage * 1.5f);
                }
                if (NPC.life < NPC.lifeMax * .4f)
                {
                    NPC.damage = (int)(NPC.defDamage * 1.7f);
                }
                if (NPC.life < NPC.lifeMax * .25f)
                {
                    NPC.damage = (int)(NPC.defDamage * 1.9f);
                }
                if (NPC.life < NPC.lifeMax / 7)
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

        public void JumpAI()
        {
            IsFlying = false;
            if (IsJumping == false)
            {
                NPC.noTileCollide = false;
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.8f;
                    TimerJump += 1f;
                    if (TimerJump > 0f)
                    {
                        if (NPC.life < NPC.lifeMax * .85f) //The lower the health, the more frequent the jumps
                        {
                            TimerJump += 2;
                        }
                        if (NPC.life < NPC.lifeMax * .7f)
                        {
                            TimerJump += 2;
                        }
                        if (NPC.life < NPC.lifeMax * .65f)
                        {
                            TimerJump += 2;
                        }
                        if (NPC.life < NPC.lifeMax * .4f)
                        {
                            TimerJump += 2;
                        }
                        if (NPC.life < NPC.lifeMax * .25f)
                        {
                            TimerJump += 2;
                        }
                        if (NPC.life < NPC.lifeMax * .1f)
                        {
                            TimerJump += 2;
                        }
                    }
                    if (Math.Abs(NPC.Center.X - TargetPlayer.Center.X) > 800f)
                    {
                        TimerJump = -1f;
                    }
                    if (TimerJump >= 250f)
                    {
                        TimerJump = -20f;
                    }
                    else if (TimerJump == -1f)
                    {
                        NPC.TargetClosest(true);
                        float longth = Math.Abs(NPC.Center.X - TargetPlayer.Center.X);
                        NPC.velocity.X = (6 + longth * .01f) * NPC.direction;
                        NPC.velocity.Y = -12.1f;
                        if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                            NPC.velocity.Y = -13.67f;
                        IsJumping = true;
                        TimerJump = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (IsJumping == true)
            {
                if (NPC.velocity.Y == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                    IsJumping = false;
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
                    if (NPC.position.X < TargetPlayer.position.X && NPC.position.X + NPC.width > TargetPlayer.position.X + TargetPlayer.width)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                        NPC.velocity.Y = NPC.velocity.Y + 0.4f;
                    }
                    else
                    {
                        
                        float num626 = 3f;
                        float longth = Math.Abs(NPC.Center.X - TargetPlayer.Center.X);
                        num626 = 3f + longth * .056f;
                        
                        if (TargetPlayer.velocity.X != 0)
                        {
                            num626 += Math.Abs(TargetPlayer.velocity.X);
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

                if(Math.Abs(NPC.Center.X - TargetPlayer.Center.X) < 50f && TargetPlayer.Center.Y > NPC.Center.Y + NPC.height / 2)
                {
                    CurrentMovement = RajahMovements.BeginStomp;
                    IsJumping = false;
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
                if (Math.Abs(NPC.Center.X - TargetPlayer.Center.X) + Math.Abs(NPC.Center.Y - TargetPlayer.Center.Y) > 1000)
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
            else if (NPC.life < NPC.lifeMax * .85f) //The lower the health, the more damage is done
            {
                speed = 15f;
            }
            else if (NPC.life < NPC.lifeMax * .7f)
            {
                speed = 16f;
            }
            else if (NPC.life < NPC.lifeMax * .65f)
            {
                speed = 17f;
            }
            else if (NPC.life < NPC.lifeMax * .4f)
            {
                speed = 18f;
            }
            else if (NPC.life < NPC.lifeMax * .25f)
            {
                speed = 19f;
            }
            else if (NPC.life < NPC.lifeMax * .1f)
            {
                speed = 20f;
            }
            AISpaceOctopus(NPC, TargetPlayer.Center, .35f, speed, 300);
            IsFlying = true;
        }

        public void AISpaceOctopus(NPC npc, Vector2 targetCenter = default, float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f)
		{
            float pos = 200f;
            if(TargetPlayer.velocity.X == 0)
            {
                pos = 0;
            }
            else
            {
                pos = (TargetPlayer.velocity.X > 0? 1f: -1f) * 200f;
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
            if (NPC.type == ModContent.NPCType<RajahRabbitA>())
            {
                return 120;
            }
            return 240;
        }

        public override void FindFrame(int frameHeight)
        {
            if (IsFlying == true)
            {
                if (NPC.frameCounter++ > 3)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                    if (NPC.frame.Y > frameHeight * 7)
                    {
                        NPC.frame.Y = 0;
                    }
                }
                WeaponFrame = frameHeight * 5;
            }
            else
            {
                if (IsJumping == false)
                {
                    if (TimerJump < -17f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                    else if (TimerJump < -14f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight;
                    }
                    else if (TimerJump < -11f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 2;
                    }
                    else if (TimerJump < -8f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 3;
                    }
                    else if (TimerJump < -5f)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 4;
                    }
                    else if (TimerJump < -2f)
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
                    WeaponFrame = NPC.frame.Y;
                }
                else if (IsJumping == true)
                {
                    if (NPC.velocity.Y != 0f)
                    {
                        NPC.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        NPC.frameCounter++;
                        if (NPC.frame.Y > 3)
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

            int rajahFrameWidth = TextureAssets.Npc[NPC.type].Value.Width / 4;
            NPC.frame.Width = rajahFrameWidth;
            int currentHorizFrameOffset = 0;
            if (IsRoaring)
                currentHorizFrameOffset = rajahFrameWidth * 2;
            if (IsFlying == true) 
                currentHorizFrameOffset += rajahFrameWidth;
            NPC.frame.X = currentHorizFrameOffset;

            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                NPC.spriteDirection = NPC.direction;
            else
                NPC.spriteDirection = 1;
        }

        public override void OnKill()
        {
            if (isSupreme)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("SupremeRajahHelmet1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("SupremeRajahHelmet2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("SupremeRajahHelmet3").Type, 1f);
                if (!NPCExtensions.BeenKilled<RajahRabbitA>(true))
                {
                    int n = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<RajahRabbitADefeat>());
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
                    int p = Projectile.NewProjectile(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahRabbitALeave>(), 100, 0, Main.myPlayer);
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
                Projectile.NewProjectile(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.ProjectileType<RajahRabbitBookIt>(), 100, 0, Main.myPlayer);
            }
            //NPC.value = 0f;
            //NPC.boss = false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RajahRabbitTreasureBag>()));

            npcLoot.AddLoreItemDrop<RajahRabbit>(ModContent.ItemType<RajahRabbitLore>());

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahRabbitRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahRabbitMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahPelt>(), 1, 10, 26));

            List<int> lootTable = [ ModContent.ItemType<BaneOfTheBunny>(), ModContent.ItemType<Bunzooka>(), ModContent.ItemType<RoyalScepter>(), ModContent.ItemType<ThePunisher>(), ModContent.ItemType<RabbitcopterWings>() ];
            if (ModLoader.TryGetMod("ThoriumMod", out _))
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

        public Asset<Texture2D> WeaponTexture()
        {
            if (CurrentAttack == RajahAttacks.Bunzooka)
            {
                return Arms_Bunzooka;
            }
            else if (CurrentAttack == RajahAttacks.RoyalScepter)
            {
                return Arms_RoyalScepter;
            }
            else if (CurrentAttack == RajahAttacks.BaneOfTheBunny && TimerPerformAttack <= (isSupreme ? 40 : 60))
            {
                return Arms_BaneOfTheBunny;
            }
            else if (CurrentAttack == RajahAttacks.Excalihare)
            {
                return Arms_Excalihare;
            }
            else if (CurrentAttack == RajahAttacks.FluffyFury)
            {
                return Arms_FluffyFury;
            }
            else if (CurrentAttack == RajahAttacks.RabbitsWrath)
            {
                return Arms_RabbitsWrath;
            }
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && CurrentAttack == RajahAttacks.CottonCane && DrawCottonCane)
            {
                return Arms_CottonCane;
            }
            else
            {
                return BlankTex;
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) 
            { 
                auraPercent += 0.1f; 
                auraDirection = auraPercent < 1f; 
            }
            else 
            { 
                auraPercent -= 0.1f; 
                auraDirection = auraPercent <= 0f; 
            }
            bool RageMode = !isSupreme && NPC.life < NPC.lifeMax / 7;
            bool SupremeRageMode = isSupreme && NPC.life < NPC.lifeMax / 7;

            if (isSupreme && (isDashing || (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && CurrentMovement == RajahMovements.Stomp)))
            {
                DrawingUtils.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, NPC, overrideColor: Main.DiscoColor);
            }

            if (RageMode)
            {
                Color RageColor = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Firebrick, drawColor, Color.Firebrick);
                DrawingUtils.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, NPC, auraPercent, overrideColor: RageColor);
            }
            else if (SupremeRageMode)
            {
                DrawingUtils.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, NPC, auraPercent, overrideColor: Main.DiscoColor);
            }

            // draw wep
            WeaponTex = WeaponTexture().Value;
            Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && isSupreme)
                WeaponRectangle.X = WeaponRectangle.Width;
            spriteBatch.Draw(WeaponTex, NPC.Center - screenPos, WeaponRectangle, drawColor, NPC.rotation, WeaponRectangle.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0);

            // draw self
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0);
            
            if (CurrentAttack == RajahAttacks.RabbitsWrath)
            {
                spriteBatch.Draw(WeaponTex, NPC.Center - screenPos, WeaponRectangle, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0);
            }

            if (RageMode)
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (sb) => sb.Draw(Glowmask.Value, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0));
            }

            if (SupremeRageMode)
            {
                spriteBatch.Draw(Glowmask.Value, NPC.Center - screenPos, NPC.frame, Main.DiscoColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0);
                DrawingUtils.DrawAura(spriteBatch, Glowmask.Value, NPC, auraPercent, overrideColor: Main.DiscoColor);
                spriteBatch.Draw(SupremeGlowmask.Value, NPC.Center - screenPos, NPC.frame, Main.DiscoColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0);
                DrawingUtils.DrawAura(spriteBatch, SupremeGlowmask.Value, NPC, auraPercent, overrideColor: Main.DiscoColor);
                return false;
            }
            else if (isSupreme)
            {
                spriteBatch.Draw(SupremeEyes.Value, NPC.Center - screenPos, NPC.frame, Main.DiscoColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0);
            }

            return false;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if(CurrentMovement == RajahMovements.Fly || CurrentMovement == RajahMovements.Stomp)
            {
                target.wingTime = 0;
                target.velocity.Y = 1f;
            }
        }
    }
}
