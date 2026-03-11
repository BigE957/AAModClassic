using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using System;
using Terraria.Localization;
using log4net;
using Terraria.ModLoader.Utilities;
using AAModClassic.Dusts;
using AAModClassic.NPCs.Enemies.Terrarium.PreHM;
using AAModClassic.NPCs.Enemies.Terrarium.PostPlant;
using AAModClassic.NPCs.Enemies.Snow;
using AAModClassic.NPCs.Bosses.Serpent;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Currency;
using AAModClassic.NPCs.Enemies.Terrarium.Hardmode;
using AAModClassic.Items.Materials;
using AAModClassic.NPCs.Bosses.Shen;
using AAModClassic.NPCs.Enemies.Cavern;
using AAModClassic.NPCs.Bosses.Rajah;
using AAModClassic.NPCs.Enemies.Sky;
using AAModClassic.UI.WorldGen;

namespace AAModClassic.Globals
{
    public class AAModGlobalNPC : GlobalNPC
    {
        //debuffs
        public bool CursedHellfire = false;
        public bool TimeFrozen = false;
        public bool infinityOverload = false;
        public bool terraBlaze = false;
        public bool Hydratoxin = false;
        public bool Moonraze = false;
        public bool Electrified = false;
        public bool InfinityScorch = false;
        public bool irradiated = false;
        public bool DiscordInferno = false;
        public bool riftBent = false;
        public bool BrokenArmor = false;
        public bool DynaEnergy1 = false;
        public bool DynaEnergy2 = false;
        public bool Spear = false;
        public bool AssassinHurt = false;
        public bool FFlames = false;

        public static int Toad = -1;
        public static int Rose = -1;
        public static int Brain = -1;
        public static int Rajah = -1;

        public override bool InstancePerEntity => true;

        public static bool IsBunny(NPC npc)
        {
            return npc.type == NPCID.Bunny || npc.type == NPCID.GoldBunny || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.PartyBunny;
        }

        public override void ResetEffects(NPC npc)
        {
            CursedHellfire = false;
            infinityOverload = false;
            terraBlaze = false;
            TimeFrozen = false;
            Hydratoxin = false;
            Moonraze = false;
            Electrified = false;
            InfinityScorch = false;
            DiscordInferno = false;
            irradiated = false;
            riftBent = false;
            BrokenArmor = false;
            DynaEnergy1 = false;
            DynaEnergy2 = false;
            Spear = false;
            AssassinHurt = false;
            FFlames = false;
        }

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc); 

            if (NPCID.Sets.TownCritter[npc.type])
            {
                npc.dontTakeDamageFromHostiles = true;
            }

            if (IsBunny(npc) && AAWorld.downedRajahsRevenge)
            {
                npc.dontTakeDamage = true;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.type == NPCID.KingSlime || npc.type == NPCID.Plantera || 
                npc.type == ModContent.NPCType<SerpentBody>() || npc.type == ModContent.NPCType<SerpentHead>() || npc.type == ModContent.NPCType<SerpentTail>() ||
                npc.type == ModContent.NPCType<SnakeHead>() || npc.type == ModContent.NPCType<SnakeBody>() || npc.type == ModContent.NPCType<SnakeBody2>() || npc.type == ModContent.NPCType<SnakeTail>())
            {
                ApplyDPSDebuff(npc.onFire, 20, ref npc.lifeRegen);
            }

            if (DiscordInferno)
            {
                npc.damage -= 10;

                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= (int)npc.velocity.X * (npc.velocity.X > 0?  1 : -1)  + 52;
            }

            if (BrokenArmor)
            {
                npc.defense *= (int).8f;
            }

            if(AssassinHurt)
			{
				npc.defense -= 20;
			}

            bool shen = npc.type == ModContent.NPCType<Shen>() || npc.type == ModContent.NPCType<ShenA>();

            ApplyDPSDebuff(terraBlaze, shen ? 46 : 26, shen ? 30 : 10, ref npc.lifeRegen, ref damage);
            ApplyDPSDebuff(infinityOverload, 60, 40, ref npc.lifeRegen, ref damage);
            ApplyDPSDebuff(Spear, 5, 5, ref npc.lifeRegen, ref damage);
            ApplyDPSDebuff(InfinityScorch, 80, 40, ref npc.lifeRegen, ref damage);

            ApplyDPSDebuff(CursedHellfire, 30, ref npc.lifeRegen);
            ApplyDPSDebuff(Moonraze, 100, ref npc.lifeRegen);
            ApplyDPSDebuff(Hydratoxin, (int)npc.velocity.X * (npc.velocity.X > 0?  1 : -1) / 2, ref npc.lifeRegen);
            ApplyDPSDebuff(Electrified, 40, ref npc.lifeRegen);
            if(npc.lifeMax > 0) ApplyDPSDebuff(FFlames, 40 * (npc.life / npc.lifeMax), ref npc.lifeRegen);
        }

        public static void ApplyDPSDebuff(bool debuff, int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
        {
            if (debuff)
            {
                if (lifeRegen > 0)
                {
                    lifeRegen = 0;
                }

                lifeRegen -= lifeRegenValue;

                if (damage < damageValue)
                {
                    damage = damageValue;
                }
            }
        }

        public static void ApplyDPSDebuff(bool debuff, int lifeRegenValue, ref int lifeRegen)
        {
            if (debuff)
            {
                if (lifeRegen > 0)
                {
                    lifeRegen = 0;
                }

                lifeRegen -= lifeRegenValue;
            }
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (npc.type >= NPCID.Golem && npc.type <= NPCID.GolemFistRight)
            {
                if (item.pick > 0)
                {
                    modifiers.ScalingBonusDamage += item.pick;
                }
                else
                {
                    npc.defense = 20;
                }
            }
        }

        public override bool PreAI(NPC npc)
        {
            if(npc.type != NPCID.MartianSaucerCore && (npc.boss || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail))
            {
                Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().bossactive = true;
            }
            return base.PreAI(npc);
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
			if(AssassinHurt)
			{
                modifiers.TargetDamageMultiplier *= 1.1f;
			}
		}

        internal ILog Logging = LogManager.GetLogger("AAMod");

        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.GoblinSummoner)
            {
                DownedBools.downedGobSummoner = true;
            }

            if (npc.type == NPCID.DD2OgreT2)
            {
                DownedBools.downedOgre = true;
            }

            if (npc.type == NPCID.DD2Betsy)
            {
                DownedBools.downedBetsy = true;
            }

            if (npc.type == NPCID.Mothron)
            {
                DownedBools.downedMoth = true;
            }

            if (npc.type == NPCID.FireImp)
            {
                npc.DropLoot(Mod.Find<ModItem>("DevilSilk").Type, Main.rand.Next(2, 3));
            }

            if (npc.type == NPCID.RedDevil && Main.rand.Next(3) == 0)
            {
                npc.DropLoot(Mod.Find<ModItem>("PureEvil").Type);
            }

            if (npc.type == NPCID.Demon)
            {
                npc.DropLoot(Mod.Find<ModItem>("DevilSilk").Type, Main.rand.Next(4, 5));
            }

            if (npc.type == NPCID.VoodooDemon)
            {
                npc.DropLoot(Mod.Find<ModItem>("DevilSilk").Type, Main.rand.Next(5, 6));
            }

            if (npc.type == NPCID.Plantera)
            {
                npc.DropLoot(Mod.Find<ModItem>("PlanteraPetal").Type, Main.rand.Next(30, 40));
            }

            if (npc.type == NPCID.GreekSkeleton)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("GladiatorsGlory").Type);
                }
            }

            if (DynaEnergy1)
            {
                Projectile.NewProjectile(npc.GetSource_Death(), npc.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.DynaEnergy>(), 60, 1, Main.myPlayer);
            }

            if (DynaEnergy2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.DynaEnergy>(), 60, 1, Main.myPlayer);
                }
            }

            if (npc.type == NPCID.DukeFishron)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Seashroom").Type);
                }
            }

            if (npc.type == NPCID.EnchantedSword)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    npc.DropLoot(ItemID.Excalibur);
                }
            }

            if (npc.type == NPCID.CrimsonAxe)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    npc.DropLoot(ItemID.BloodLustCluster);
                }
            }

            if (npc.type == NPCID.CursedHammer)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Shadowban").Type);
                }
            }

            if (Main.rand.NextBool(8192))
            {
                npc.DropLoot(Mod.Find<ModItem>("ShinyCharm").Type);
            }

            if (npc.type == NPCID.LostGirl || npc.type == NPCID.Nymph || npc.type == NPCID.DoctorBones)
            {
                if (Main.rand.NextBool(20) && Main.expertMode)
                {
                    npc.DropLoot(Mod.Find<ModItem>("AncientGoldLeg").Type);
                }
            }

            if (npc.type == NPCID.Tim || npc.type == NPCID.RuneWizard)
            {
                if (Main.rand.NextBool(20) && Main.expertMode)
                {
                    npc.DropLoot(Mod.Find<ModItem>("AncientGoldBody").Type);
                }
            }

            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (Main.rand.NextBool(4))
                {
                    npc.DropLoot(Mod.Find<ModItem>("CthulhusBlade").Type);
                }
            }

            if (npc.type == NPCID.GiantFlyingFox)
            {
                if (Main.rand.NextBool(4))
                {
                    npc.DropLoot(Mod.Find<ModItem>("TheFox").Type);
                }
            }

            if (npc.type == NPCID.Necromancer)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Exorcist").Type);
                }
            }

            if (npc.type == NPCID.AngryBones || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigHelmet || npc.type == NPCID.AngryBonesBigMuscle)
            {
                if (Main.rand.NextFloat() < 0.01f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("AncientPoker").Type);
                }
            }

            if (npc.type == NPCID.Probe)
            {
                npc.DropLoot(Mod.Find<ModItem>("Energy_Cell").Type, Main.rand.Next(3, 12));
            }

            if (npc.type == NPCID.TheDestroyer)
            {
                npc.DropLoot(Mod.Find<ModItem>("Energy_Cell").Type, Main.rand.Next(8, 16));

                if (Main.rand.NextFloat() < .34f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Laser_Rifle").Type);
                }
            }

            if (npc.type == NPCID.SkeletronPrime)
            {
                npc.DropLoot(Mod.Find<ModItem>("Energy_Cell").Type, Main.rand.Next(8, 16));

                if (Main.rand.NextFloat() < .34f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Laser_Rifle").Type);
                }
            }

            if (npc.type == NPCID.WallofFlesh)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("HK_MP5").Type);
                }
            }

            if (npc.type == NPCID.MartianSaucerCore)
            {
                if (Main.rand.NextFloat() < .12f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Alien_Rifle").Type);
                }

                if (Main.rand.NextFloat() < .03f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Energy_Conduit").Type);
                }
            }

            if (npc.type == NPCID.CursedSkull)
            {
                if (Main.rand.NextFloat() < .12f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("SkullStaff").Type);
                }
            }

            if (npc.type == NPCID.Vulture)
            {
                npc.DropLoot(Mod.Find<ModItem>("vulture_feather").Type, Main.rand.Next(1, 3));
            }

            if (npc.type == NPCID.Drippler)
            {
                if (Main.rand.NextFloat() < .005f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("Bloody_Mary").Type);
                }
            }

            if (npc.type == NPCID.TacticalSkeleton || npc.type == NPCID.SkeletonSniper || npc.type == NPCID.SkeletonCommando)
            {
                if (Main.rand.Next(50) == 0)
                {
                    npc.DropLoot(Mod.Find<ModItem>("M79Parts").Type);
                }
            }

            if (npc.type == NPCID.QueenBee)
            {
                if (Main.rand.NextFloat() < .01f)
                {
                    npc.DropLoot(Mod.Find<ModItem>("BugSwatter").Type);
                }

                npc.DropLoot(ItemID.Stinger, Main.rand.Next(14, 20));
            }

            if (npc.type == NPCID.Plantera)
            {
                npc.DropLoot(ItemID.ChlorophyteOre, Main.rand.Next(50, 80));
            }

            if (npc.type == NPCID.SkeletronHand)
            {
                npc.DropLoot(ItemID.Bone, Main.rand.Next(4, 8));
            }

            if (npc.type == NPCID.SkeletronHead)
            {
                npc.DropLoot(ItemID.Bone, Main.rand.Next(30, 45));
            }

            if ((npc.type == NPCID.ArmoredViking || npc.type == NPCID.UndeadViking) && NPC.downedBoss3 && Main.rand.Next(3) == 0)
            {
                npc.DropLoot(ModContent.ItemType<VikingRelic>(), Main.rand.Next(1, 3));
            }

            if (AASets.Goblins[npc.type] && NPC.downedGoblins)
            {
                if (Main.rand.NextBool(20))
                {
                    npc.DropLoot(Mod.Find<ModItem>("GoblinSoul").Type);
                }
            }

            if (npc.type == NPCID.GoldBunny && NPC.downedGolemBoss)
            {
                npc.DropLoot(Mod.Find<ModItem>("GoldenCarrot").Type);
            }

            if (IsBunny(npc) && NPC.downedGolemBoss)
            {
                if (Main.rand.NextBool(80))
                {
                    npc.DropLoot(Mod.Find<ModItem>("GoldenCarrot").Type);
                }
            }

            if (Main.hardMode)
            {
                Player player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
                if (player.GetModPlayer<AAPlayer>().ZoneMire && player.position.Y > (Main.worldSurface * 16.0))
                {
                    if (Main.rand.NextBool(5))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("SoulOfSpite").Type);
                    }
                }

                if (player.GetModPlayer<AAPlayer>().ZoneInferno && player.position.Y > (Main.worldSurface * 16.0))
                {
                    if (Main.rand.NextBool(5))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("SoulOfSmite").Type);
                    }
                }
                if (player.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    if (Main.rand.NextBool(2500))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("MireKey").Type);
                    }
                }
                if (player.GetModPlayer<AAPlayer>().ZoneInferno)
                {
                    if (Main.rand.NextBool(2500))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("InfernoKey").Type);
                    }
                }
                if (player.GetModPlayer<AAPlayer>().ZoneVoid)
                {
                    if (Main.rand.NextBool(1250))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("DoomstopperKey").Type);
                    }
                }
                if (player.GetModPlayer<AAPlayer>().Terrarium && NPC.downedPlantBoss)
                {
                    if (Main.rand.NextBool(100))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("TerraCrystal").Type);
                    }
                }

                if ((player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneMire) && NPC.downedPlantBoss)
                {
                    if (Main.rand.NextBool(100))
                    {
                        npc.DropLoot(Mod.Find<ModItem>("ChaosCrystal").Type);
                    }
                }
            }


            if (WorldTypeSystem.WorldType != AAWorldType.Beta && Main.hardMode && IsBunny(npc) && Rajah != -1)
            {
                Player player = Main.player[Player.FindClosest(npc.Center, npc.width, npc.height)];

                int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                if (bunnyKills % 100 == 0 && bunnyKills < 1000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RajahGlobalInfo1"), 107, 137, 179);
                    }

                    SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Sounds/Rajah"), npc.Center);
                    SpawnRajah(player, true, new Vector2(npc.Center.X, npc.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));

                }

                if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RajahGlobalInfo2") + player.name.ToUpper() + "!", 107, 137, 179);
                    }

                    SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Sounds/Rajah"), npc.Center);
                    SpawnRajah(player, true, new Vector2(npc.Center.X, npc.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));
                }

                if (bunnyKills % 50 == 0 && bunnyKills % 100 != 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RajahGlobalInfo3"), 107, 137, 179);
                    }
                }
            }

            if (Main.bloodMoon)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<BloodRune>());
            }

            if ((npc.type >= NPCID.PirateDeckhand && npc.type <= NPCID.PirateCaptain) || npc.type == NPCID.Parrot || npc.type == NPCID.PirateShip)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<PirateBooty>());
            }

            if (npc.type == NPCID.Frankenstein || npc.type == NPCID.Vampire || npc.type == NPCID.VampireBat || npc.type == NPCID.SwampThing ||
                npc.type == NPCID.CreatureFromTheDeep || npc.type == NPCID.Fritz || npc.type == NPCID.Reaper || npc.type == NPCID.ThePossessed ||
                npc.type == NPCID.Mothron || npc.type == NPCID.Butcher || npc.type == NPCID.DeadlySphere || npc.type == NPCID.DrManFly ||
                npc.type == NPCID.Nailhead || npc.type == NPCID.Psycho || npc.type == NPCID.Eyezor)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<MonsterSoul>());
            }

            if ((npc.type >= NPCID.PirateDeckhand && npc.type <= NPCID.PirateCaptain) || npc.type == NPCID.Parrot || npc.type == NPCID.PirateShip)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<PirateBooty>());
            }

            if ((npc.type >= NPCID.Scarecrow1 && npc.type <= NPCID.HeadlessHorseman) || (npc.type >= NPCID.MourningWood && npc.type <= NPCID.Poltergeist && npc.type != NPCID.PumpkingBlade))
            {
                if (Main.rand.Next(8)== 0) npc.DropLoot(ModContent.ItemType<HalloweenTreat>());
            }

            if (npc.type >= NPCID.ZombieElf && npc.type <= NPCID.Krampus)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<ChristmasCheer>());
            }

            if (npc.type >= NPCID.BrainScrambler && npc.type <= NPCID.MartianSaucer && npc.type != NPCID.ForceBubble)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<MartianCredit>());
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            Rectangle hitbox = npc.Hitbox;
            if (FFlames)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("ForsakenDust").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (CursedHellfire)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.CursedTorch, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (Electrified)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (infinityOverload)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("InfinityOverloadB").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.3f, 0.7f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("InfinityOverloadR").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.2f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("InfinityOverloadG").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.7f, 0.1f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("InfinityOverloadY").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.5f, 0.5f, 0.1f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("InfinityOverloadP").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.6f, 0.1f, 0.6f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("InfinityOverloadO").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.8f, 0.5f, 0.1f);
            }

            if (Moonraze)
            {
                int dustCount = Math.Max(1, Math.Min(5, Math.Max(npc.width, npc.height) / 10));
                for (int i = 0; i < dustCount; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, ModContent.DustType<Moonraze>(), 0f, 1f, 0);
                    if (Main.dust[num4].velocity.Y > 0) Main.dust[num4].velocity.Y *= -1;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (DiscordInferno)
            {
                for (int i = 0; i < 8; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, ModContent.DustType<Discord>(), 0f, -2.5f, 0);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (Hydratoxin)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Dusts.HydratoxinDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.3f, 0.7f);
            }

            

            if (terraBlaze)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Terra, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.7f, 0.2f);
            }

            if (InfinityScorch)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Terra, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, ModContent.DustType<VoidDust>(), default, 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.1f);
            }

        }

        public static void ClearPoolWithExceptions(IDictionary<int, float> pool)
        {
            try
            {
                Dictionary<int, float> keepPool = new Dictionary<int, float>();
                foreach (var kvp in pool)
                {
                    int npcID = kvp.Key;
                    ModNPC mnpc = NPCLoader.GetNPC(npcID);
                    if (mnpc != null && mnpc.Mod != null) //splitting so you can add other exceptions if need be
                    {
                        if (mnpc.Mod.Name.Equals("GRealm")) //do not remove GRealm spawns!
                        {
                            keepPool.Add(npcID, kvp.Value);
                        }
                    }
                }
                pool.Clear();

                foreach (var newkvp in keepPool)
                {
                    pool.Add(newkvp.Key, newkvp.Value);
                }

                keepPool.Clear();
            }
            catch (Exception e)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(e.StackTrace);
                }
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneTowerNebula || spawnInfo.Player.ZoneTowerSolar || spawnInfo.Player.ZoneTowerStardust || spawnInfo.Player.ZoneTowerVortex || 
                Main.eclipse || 
                Main.invasionType == InvasionID.MartianMadness ||
                Main.invasionType == InvasionID.CachedPumpkinMoon ||
                Main.invasionType == InvasionID.CachedFrostMoon)
            {
                return;
            }
            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneStars)
            {
                pool.Add(Main.dayTime ? Mod.Find<ModNPC>("Sunwatcher").Type : Mod.Find<ModNPC>("Nightguard").Type, .2f);
            }

            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                ClearPoolWithExceptions(pool);
                if ((spawnInfo.Player.position.Y < (Main.worldSurface * 16.0)) && (Main.dayTime || AAWorld.downedAkuma))
                {
                    pool.Add(Mod.Find<ModNPC>("Wyrmling").Type, .25f);
                    pool.Add(Mod.Find<ModNPC>("InfernalSlime").Type, .05f);
                    pool.Add(Mod.Find<ModNPC>("Flamebrute").Type, .25f);
                    pool.Add(Mod.Find<ModNPC>("InfernoSalamander").Type, .5f);
                    pool.Add(Mod.Find<ModNPC>("DragonClaw").Type, .05f);

                    if (Main.hardMode)
                    {
                        pool.Add(Mod.Find<ModNPC>("MagmaSwimmer").Type, SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(Mod.Find<ModNPC>("BlazePhoenix").Type, .1f);
                    }

                    if (AAWorld.downedSisters)
                    {
                        pool.Add(Mod.Find<ModNPC>("BlazeClaw").Type, .05f);
                    }
                }
                else if (spawnInfo.Player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(Mod.Find<ModNPC>("Wyrmling").Type, .25f);
                    pool.Add(Mod.Find<ModNPC>("Flamebrute").Type, .25f);
                    pool.Add(Mod.Find<ModNPC>("InfernoSalamander").Type, .5f);
                    pool.Add(Mod.Find<ModNPC>("DragonClaw").Type, .05f);

                    if (Main.hardMode)
                    {
                        pool.Add(Mod.Find<ModNPC>("MagmaSwimmer").Type, SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(Mod.Find<ModNPC>("Wyrm").Type, .008f);
                        pool.Add(Mod.Find<ModNPC>("ChaoticDawn").Type, .01f);

                        if (spawnInfo.Player.ZoneSnow)
                        {
                            pool.Add(Mod.Find<ModNPC>("Dragron").Type, .01f);
                        }

                        if (spawnInfo.Player.ZoneUndergroundDesert)
                        {
                            pool.Add(Mod.Find<ModNPC>("InfernoGhoul").Type, .1f);
                        }
                    }
                }

                if (NPC.downedMoonlord)
                {
                    pool.Add(Mod.Find<ModNPC>("Lung").Type, .01f);
                }
            }

            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                ClearPoolWithExceptions(pool);
                if ((spawnInfo.Player.position.Y < (Main.worldSurface * 16.0)) && (!Main.dayTime || AAWorld.downedYamata))
                {
                    pool.Add(Mod.Find<ModNPC>("Mosster").Type, .025f);
                    pool.Add(Mod.Find<ModNPC>("Newt").Type, .05f);
                    pool.Add(Mod.Find<ModNPC>("HydraClaw").Type, .025f);
                    pool.Add(Mod.Find<ModNPC>("MireSkulker").Type, .02f);
                    pool.Add(Mod.Find<ModNPC>("MireSlime").Type, .025f);

                    if (Main.hardMode)
                    {
                        pool.Add(Mod.Find<ModNPC>("FogAngler").Type, SpawnCondition.WaterCritter.Chance * 0.05f);
                        pool.Add(Mod.Find<ModNPC>("Toxitoad").Type, .005f);
                        pool.Add(Mod.Find<ModNPC>("Kappa").Type, .025f);
                    }

                    if (AAWorld.downedSisters)
                    {
                        pool.Add(Mod.Find<ModNPC>("AbyssClaw").Type, .01f);
                    }
                }
                else if (spawnInfo.Player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(Mod.Find<ModNPC>("Mosster").Type, .025f);
                    pool.Add(Mod.Find<ModNPC>("Newt").Type, .05f);
                    pool.Add(Mod.Find<ModNPC>("HydraClaw").Type, .025f);
                    pool.Add(Mod.Find<ModNPC>("MireSkulker").Type, .02f);

                    if (Main.hardMode)
                    {
                        pool.Add(Mod.Find<ModNPC>("FogAngler").Type, SpawnCondition.WaterCritter.Chance * 0.1f);
                        pool.Add(Mod.Find<ModNPC>("Miresquito").Type, .025f);
                        pool.Add(Mod.Find<ModNPC>("ChaoticTwilight").Type, .005f);

                        if (spawnInfo.Player.ZoneSnow)
                        {
                            pool.Add(Mod.Find<ModNPC>("Miregron").Type, .005f);
                        }

                        if (spawnInfo.Player.ZoneUndergroundDesert)
                        {
                            pool.Add(Mod.Find<ModNPC>("MireGhoul").Type, .025f);
                        }
                    }
                }

                if (NPC.downedMoonlord)
                {
                    pool.Add(Mod.Find<ModNPC>("Soulsucker").Type, .01f);
                }

                if (!AAWorld.downedSisters && AAWorld.downedHydra && !NPC.AnyNPCs(Mod.Find<ModNPC>("HarukaShadow").Type))
                pool.Add(Mod.Find<ModNPC>("HarukaShadow").Type, .00005f);
            }

            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                ClearPoolWithExceptions(pool);

                if (AAWorld.downedSag)
                {
                    pool.Add(Mod.Find<ModNPC>("SagittariusMini").Type, .005f);
                }

                if (NPC.downedPlantBoss)
                {
                    pool.Add(Mod.Find<ModNPC>("Vortex").Type, 0.002f);
                    pool.Add(Mod.Find<ModNPC>("Scout").Type, .005f);
                }

                if (NPC.downedMoonlord)
                {
                    pool.Add(Mod.Find<ModNPC>("Searcher").Type, .005f);

                    if (AAWorld.downedZero)
                    {
                        pool.Add(Mod.Find<ModNPC>("Null").Type, .005f);
                    }
                }
                else
                {
                    pool.Add(Mod.Find<ModNPC>("Searcher1").Type, .005f);
                }
            }

            if (spawnInfo.Player.GetModPlayer<AAPlayer>().Terrarium)
            {
                ClearPoolWithExceptions(pool);

                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Bladon>(), .05f);
                    pool.Add(ModContent.NPCType<TerraDeadshot>(), .05f);
                    pool.Add(ModContent.NPCType<TerraWizard>(), .05f);
                    pool.Add(ModContent.NPCType<TerraWarlock>(), .05f);
                    pool.Add(ModContent.NPCType<PurityWeaver>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySphere>(), .03f);
                    pool.Add(ModContent.NPCType<PurityCrawler>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySquid>(), .03f);
                    return;
                }
                else if (Main.hardMode)
                {
                    pool.Add(ModContent.NPCType<TerraProbe>(), .07f);
                    pool.Add(ModContent.NPCType<TerraWatcher>(), .07f);
                    pool.Add(ModContent.NPCType<TerraSquire>(), .07f);
                    pool.Add(ModContent.NPCType<PurityWeaver>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySphere>(), .03f);
                    pool.Add(ModContent.NPCType<PurityCrawler>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySquid>(), .03f);
                }
                else if (AAWorld.Terra1)
                {
                    pool.Add(ModContent.NPCType<PurityWeaver>(), .05f);
                    pool.Add(ModContent.NPCType<PuritySphere>(), .05f);
                    pool.Add(ModContent.NPCType<PurityCrawler>(), .05f);
                    pool.Add(ModContent.NPCType<PuritySquid>(), .05f);
                }
            }

            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneAcropolis)
            {
                ClearPoolWithExceptions(pool);
                pool.Add(NPCID.Harpy, .06f);
                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Seraph>(), .03f);
                }
            }

            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneHoard)
            {
                ClearPoolWithExceptions(pool);

                pool.Add(NPCID.GiantWormHead, .005f);
                pool.Add(NPCID.GoldWorm, .001f);
                pool.Add(NPCID.Worm, .005f);

                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Scavenger>(), .03f);
                }
            }
            if(spawnInfo.Player.GetModPlayer<AAPlayer>().StripeManSpawn)
            {
                if(NPC.goldCritterChance >= 30) NPC.goldCritterChance = 30;
                if(!spawnInfo.Player.calmed && !spawnInfo.Player.GetModPlayer<AAPlayer>().luckycalm)
                {
                    foreach (int npctype in Config.ListRareNpc)
                    {
                        if (pool.Keys.Contains(npctype) && pool[npctype] <= 0.05f)
                        {
                            pool[npctype] = 0.05f;
                        }
                    }
                }
            }
            else if(spawnInfo.Player.GetModPlayer<AAPlayer>().AncientGoldLeg)
            {
                if(NPC.goldCritterChance >= 40) NPC.goldCritterChance = 40;
            }
            else
            {
                NPC.goldCritterChance = 150;
            }
        }

        public static void VanillaNPCSpawn(Player player)
        {
            int spawnRangeXMin = (int)(player.position.X / 16f) - (int)(NPC.sWidth / 16 * 0.7);
            int spawnRangeXMax = (int)(player.position.X / 16f) + (int)(NPC.sWidth / 16 * 0.7);
            int spawnRangeYMin = (int)(player.position.Y / 16f) - (int)(NPC.sHeight / 16 * 0.7);
            int spawnRangeYMax = (int)(player.position.Y / 16f) + (int)(NPC.sHeight / 16 * 0.7);

            int x = Main.rand.Next(spawnRangeXMin, spawnRangeXMax);
			int y = Main.rand.Next(spawnRangeYMin, spawnRangeYMax);

            int npcid = 0;

            if (!Main.tile[x, y].HasTile)
            {
                if (Sandstorm.Happening && player.ZoneSandstorm && TileID.Sets.Conversion.Sand[Main.tile[x, y].TileType] && NPC.Spawning_SandstoneCheck(x, y))
                {
                    if (Main.hardMode && Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.SandElemental, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (player.ZoneDungeon && NPC.downedPlantBoss)
                {
                    if (Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BoneLee, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        int Skeletontype = 0;
                        switch (Main.rand.Next(3))
                        {
                            case 0: Skeletontype = 291; break;
                            case 1: Skeletontype = 292; break;
                            case 3: Skeletontype = 293; break;
                        }
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, Skeletontype, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Paladin, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.worldSurface && Main.dayTime && Main.eclipse)
                {
                    bool flag = false;
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        flag = true;
                    }
                    if (flag && Main.rand.Next(40) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Mothron, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.worldSurface)
                {
                    if (player.ZoneSnow && Main.hardMode && Main.cloudAlpha > 0f && Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.IceGolem, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (player.ZoneHallow && Main.hardMode && Main.rand.Next(30) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.RainbowSlime, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (y <= Main.worldSurface/2 && NPC.AnyDanger() && Main.hardMode && NPC.downedGolemBoss && Main.rand.Next(100) == 0 && !NPC.AnyNPCs(399))
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.MartianProbe, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.hardMode && Main.rand.Next(25) == 0 && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Clown, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(100) == 0 && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.TheGroom, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(100) == 0 && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.TheBride, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    
                    if (Main.dayTime)
                    {
                        if (Main.rand.Next(50) == 0)
                        {
                            npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BlueSlime, 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[npcid].SetDefaults(-4);
                        }
                    }
                }
                else if (Main.hardMode && y > Main.worldSurface && Main.rand.Next(40) == 0)
                {
                    if (Main.rand.Next(2) == 0 && player.ZoneCorrupt)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BigMimicCorruption, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (Main.rand.Next(2) == 0 && player.ZoneCrimson)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BigMimicCrimson, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (Main.rand.Next(2) == 0 && player.ZoneHallow)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BigMimicHallow, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Mimic, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (Main.hardMode && Main.tile[x, y - 1].WallType == WallID.DirtUnsafe && Main.rand.Next(10) == 0)
                {
                    npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Mimic, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (Main.tile[x, y].TileType == TileID.JungleGrass && Main.rand.Next(100) == 0 && !Main.dayTime)
                {
                    npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.DoctorBones, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (Main.tile[x, y].TileType == TileID.JungleGrass && Main.hardMode && Main.rand.Next(45) == 0 && !Main.dayTime)
                {
                    npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Moth, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (y > Main.maxTilesY - 190)
                {
                    if (Main.hardMode && !NPC.savedTaxCollector && Main.rand.Next(10) == 0 && !NPC.AnyNPCs(534))
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.DemonTaxCollector, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.maxTilesY - 190 && y > Main.rockLayer)
                {
                    if (Main.rand.Next(50) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.LostGirl, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (y > (Main.rockLayer + Main.maxTilesY) / 2.0 && Main.rand.Next(50) == 0)
                    {
                        npcid = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Tim, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
            }

            if (Main.netMode == NetmodeID.Server && npcid < 200)
            {
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid, 0f, 0f, 0f, 0, 0, 0);
                return;
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
            if (player.GetModPlayer<AAPlayer>().luckycalm)
			{
				spawnRate = (int)((double)spawnRate * 30f);
				maxSpawns = (int)(maxSpawns * 0.009f);
            }

            if(!player.GetModPlayer<AAPlayer>().luckycalm && player.GetModPlayer<AAPlayer>().StripeManSpawn && !player.calmed && player.active && !player.dead && player.nearbyActiveNPCs < maxSpawns && Main.rand.NextDouble()*(spawnRate/1.333f) < 1 )
            {
                VanillaNPCSpawn(player);
            }
        }

        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            int type = npc.type;
            int nextSlot = 0;
            if (type == NPCID.Demolitionist && !Main.dayTime)
            {
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("M79Round").Type);
                nextSlot++;
            }

            if (type == NPCID.WitchDoctor && Main.hardMode)
            {
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("Mortar").Type);
                nextSlot++;
            }

            if (type == NPCID.Dryad)
            {
                if (Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMush)
                {
                    items[nextSlot].SetDefaults(Mod.Find<ModItem>("MyceliumSeeds").Type);
                    nextSlot++;
                }
                if (AAWorld.downedRajah)
                {
                    items[nextSlot].SetDefaults(Mod.Find<ModItem>("GoldenCarrot").Type);
                    items[nextSlot].shopCustomPrice = Item.sellPrice(0, 30, 0, 0);
                    nextSlot++;
                }
            }

            if (type == NPCID.Truffle && NPC.downedPlantBoss)
            {
                items[nextSlot].SetDefaults(ItemID.TruffleWorm);
                items[nextSlot].shopCustomPrice = Item.sellPrice(3, 0, 0, 0);
                nextSlot++;
            }
            if (type == NPCID.Steampunker)
            {
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("DeepGreenSolution").Type);
                nextSlot++;
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("LimeSolution").Type);
                nextSlot++;
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("FungicideSolution").Type);
                nextSlot++;
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("WhiteSolution").Type);
                nextSlot++;
                items[nextSlot].SetDefaults(Mod.Find<ModItem>("YellowSolution").Type);
                nextSlot++;
            }
        }

        // SpawnBoss(player, "MyBoss", true, 0, 0, "DerpyBoi", false);
        public static void SpawnBoss(Player player, string type, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            Mod mod = AAMod.instance;
            SpawnBoss(player, mod.Find<ModNPC>(type).Type, spawnMessage, overrideDirection, overrideDirectionY, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, mod.NPCType("MyBoss"), true, 0, 0, "DerpyBoi 2", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            if (overrideDirection == 0)
            {
                overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            }

            if (overrideDirectionY == 0)
            {
                overrideDirectionY = -1;
            }

            Vector2 npcCenter = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
            SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, "MyBoss", true, player.Center + new Vector2(0, -800f), "DerpFromAbove", false);
        public static void SpawnBoss(Player player, string type, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            Mod mod = AAMod.instance;
            SpawnBoss(player, mod.Find<ModNPC>(type).Type, spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, mod.NPCType("MyBoss"), true, player.Center + new Vector2(0, 800f), "DerpFromBelow", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
            {
                npcCenter = player.Center;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType))
                    return;

                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)npcCenter.X, (int)npcCenter.Y, bossType);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;

                if (spawnMessage)
                {
                    string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName;
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].ModNPC != null)
                    {
                        npcName = Main.npc[npcID].ModNPC.DisplayName.ToString();
                    }

                    if (namePlural)
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                BaseUtility.Chat(npcName + " " + Language.GetTextValue("Mods.AAModClassic.Common.NPCarrive"), 175, 75, 255, false);
                            }
                        }
                        else if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(npcName + Language.GetTextValue("Mods.AAModClassic.Common.NPCarrive")), new Color(175, 75, 255));
                        }
                    }
                    else
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false);
                            }
                        }
                        else if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(
                                NetworkText.FromKey("Announcement.HasAwoken", new object[] { NetworkText.FromLiteral(npcName) }), 
                                new Color(175, 75, 255)
                            );
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

        public static void SpawnRajah(Player player, bool spawnMessage = false, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
            {
                npcCenter = player.Center;
            }

            int RajahType = ModContent.NPCType<Rajah>();
            if (NPC.killCount[NPCID.Bunny] >= 1000)
            {
                RajahType = ModContent.NPCType<SupremeRajah>();
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(RajahType))
                {
                    return;
                }

                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)npcCenter.X, (int)npcCenter.Y, RajahType, 0, 0, 0, player.whoAmI);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate = true;
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)RajahType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }
    }
}
