using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Armor;
using AAModClassic._Content.MartianMadness.__Hardmode.Items.Accessories;
using AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Content.Terra.__Hardmode.Items.Materials;
using AAModClassic.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.GoblinArmy.___PreHardmode.NPCs.__Friendly
{
    [AutoloadHead]
	public class GoblinSlayer : ModNPC, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.TownNPCs";

        public static bool Goblin = false;
        public static bool Blood = false;
        public static bool OOA = false;
        public static bool Pirate = false;
        public static bool Eclipse = false;
        public static bool Pumpkin = false;
        public static bool Frost = false;
        public static bool Martian = false;

        //public override bool IsLoadingEnabled(Mod mod)
		//{
		//	name = "Goblin Slayer";
		//	return Mod.Properties/* tModPorter Note: _Unreleased. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
		//}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goblin Slayer");
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 10;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 40;
            NPCID.Sets.AttackAverageChance[NPC.type] = 20;
            NPCID.Sets.HatOffsetY[NPC.type] = 3;

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Like)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Hate);
        }

        public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
            NPC.height = 40;
            NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = NPCAIStyleID.Passive;
			NPC.damage = 80;
			NPC.defense = 98;
			NPC.lifeMax = 600;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.GoblinSlayer")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
		}

		public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            if (!AAConfigClient.Instance.NoAATownNPC)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (player.active)
                    {
                        if (NPC.downedGoblins == true)
                        {
                            return true;
                        }
                    }
                }
			}
			return false;
		}

		public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
			return ["Goblin Slayer"];
		}
        

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int Goblin = NPC.FindFirstNPC(NPCID.GoblinTinkerer);
			if (Goblin >= 0 && Main.rand.NextBool(4))
			{
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat1") + Main.npc[Goblin].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat2"));
            }
            int DD2Bartender = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (DD2Bartender >= 0 && Main.rand.NextBool(4))
            {
                return Main.npc[DD2Bartender].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat3");
            }
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat4"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat5"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat6"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat7"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat8"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat9"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat10"));
            if (NPC.downedPirates || NPC.downedMartians || DownedBools.downedOgre)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.GoblinSlayer.GoblinSlayerChat11"));
            }
            return chat; 
        }

        public static int ChatNumber = 0;

        public static void ResetBools()
        {
            Goblin = false;
            Blood = false;
            OOA = false;
            Pirate = false;
            Pirate = false;
            Eclipse = false;
            Pumpkin = false;
            Frost = false;
            Martian = false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopChangeShopType");

            string GobShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopGoblinLoot");
            string BloodShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopBloodMoonLoot");
            string OOAShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopOldOneArmyLoot");
            string PirateShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopPirateLoot");
            string EclipseShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopEclipseLoot");
            string PumpShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopHalloweenLoot");
            string FrostShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopChristmasLoot");
            string MartianShop = Language.GetTextValue("Mods.AAModClassic.Common.GoblinSlayerShopMartianLoot");

            if (ChatNumber == 0)
            {
                button2 = GobShop;
                Goblin = true;
            }
            else if (ChatNumber == 1)
            {
                button2 = BloodShop;
                Blood = true;
            }
            else if (ChatNumber == 2)
            {
                button2 = OOAShop;
                OOA = true;
            }
            else if (ChatNumber == 3)
            {
                button2 = PirateShop;
                Pirate = true;
            }
            else if (ChatNumber == 4)
            {
                button2 = EclipseShop;
                Eclipse = true;
            }
            else if (ChatNumber == 5)
            {
                button2 = PumpShop;
                Pumpkin = true;
            }
            else if (ChatNumber == 6)
            {
                button2 = FrostShop;
                Frost = true;
            }
            else if (ChatNumber == 7)
            {
                button2 = MartianShop;
                Martian = true;
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                ResetBools();
                ChatNumber += 1;
                if (ChatNumber > 7)
                {
                    ChatNumber = 0;
                }
            }
            else
            {
                if(Goblin)
                    shopName = "GoblinShop";
                else if (Blood)
                    shopName = "BloodMoonShop";
                else if (OOA)
                    shopName = "OOAShop";
                else if (Pirate)
                    shopName = "PirateShop";
                else if (Eclipse)
                    shopName = "EclipseShop";
                else if (Pumpkin)
                    shopName = "PumpkinMoonShop";
                else if (Frost)
                    shopName = "FrostMoonShop";
                else if (Martian)
                    shopName = "MartianShop";
            }
		}

        public override void AddShops()
        {
            NPCShop goblinShop = new(Type, "GoblinShop");
            #region Goblin Shop
            goblinShop.Add(new Item(ModContent.ItemType<GoblinSlayersHelmet>())
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.GoblinSoul
            });
            goblinShop.Add(new Item(ModContent.ItemType<GoblinSlayersChestplate>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.GoblinSoul
            });
            goblinShop.Add(new Item(ModContent.ItemType<GoblinSlayersLeggings>())
            {
                shopCustomPrice = 12,
                shopSpecialCurrency = AAMod.GoblinSoul
            });
            goblinShop.Add(new Item(ModContent.ItemType<Items.Weapons.GoblinSlayer>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.GoblinSoul
            });
            goblinShop.Add(new Item(ItemID.GoblinBattleStandard)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.GoblinSoul
            });
            goblinShop.Add(new Item(ItemID.Harpoon)
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.GoblinSoul
            });

            Condition downedGobSummoner = new(Language.GetTextValue("Mods.AAModClassic.Common.Conditions.DownedGobSummoner"), () => DownedBools.downedGobSummoner);
            goblinShop.Add(new Item(ItemID.ShadowFlameKnife)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.GoblinSoul
            }, downedGobSummoner);
            goblinShop.Add(new Item(ItemID.ShadowFlameBow)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.GoblinSoul
            }, downedGobSummoner);
            goblinShop.Add(new Item(ItemID.ShadowFlameHexDoll)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.GoblinSoul
            }, downedGobSummoner);

            goblinShop.Register();
            #endregion

            NPCShop bloodMoonShop = new(Type, "BloodMoonShop");
            #region Blood Moon Shop
            bloodMoonShop.Add(new Item(ItemID.TopHat)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.BloodRune
            });
            bloodMoonShop.Add(new Item(ItemID.TheBrideHat)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.BloodRune
            });
            bloodMoonShop.Add(new Item(ItemID.TheBrideDress)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.BloodRune
            });
            bloodMoonShop.Add(new Item(ItemID.SharkToothNecklace)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.BloodRune
            });
            bloodMoonShop.Add(new Item(ItemID.MoneyTrough)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.BloodRune
            });

            bloodMoonShop.Add(new Item(ItemID.KOCannon)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.BloodRune
            }, Condition.Hardmode);
            bloodMoonShop.Add(new Item(ItemID.Bananarang)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.BloodRune
            }, Condition.Hardmode);

            bloodMoonShop.Register();
            #endregion

            NPCShop ooaShop = new(Type, "OOAShop");
            #region OOA Shop
            ooaShop.Add(new Item(ModContent.ItemType<OldOneCharm>())
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            });
            ooaShop.Add(new Item(ItemID.WarTableBanner)
            {
                shopCustomPrice = 2,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            });
            ooaShop.Add(new Item(ItemID.WarTable)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            });
            ooaShop.Add(new Item(ItemID.DD2PetDragon)
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            });
            ooaShop.Add(new Item(ItemID.DD2PetGato)
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            });

            ooaShop.Add(new Item(ItemID.ApprenticeScarf)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.SquireShield)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.HuntressBuckler)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.MonkBelt)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.DD2PetGhost)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.DD2SquireDemonSword)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.MonkStaffT2)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.MonkStaffT1)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.BookStaff)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);
            ooaShop.Add(new Item(ItemID.DD2PhoenixBow)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT2);

            ooaShop.Add(new Item(ItemID.DD2SquireBetsySword)
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT3);
            ooaShop.Add(new Item(ItemID.MonkStaffT3)
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT3);
            ooaShop.Add(new Item(ItemID.DD2BetsyBow)
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT3);
            ooaShop.Add(new Item(ItemID.ApprenticeStaffT3)
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT3);
            ooaShop.Add(new Item(ItemID.BetsyWings)
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            }, Condition.DownedOldOnesArmyT3);

            ooaShop.Register();
            #endregion

            NPCShop pirateShop = new(Type, "PirateShop");
            #region Pirate Shop
            pirateShop.Add(new Item(ItemID.PirateMap)
            {
                shopCustomPrice = Item.sellPrice(0, 1, 0, 0)
            }, Condition.Hardmode);

            pirateShop.Add(new Item(ItemID.EyePatch)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.SailorHat)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.SailorShirt)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.SailorPants)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.BuccaneerBandana)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.BuccaneerShirt)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.BuccaneerPants)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.LuckyCoin)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.DiscountCard)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.GoldRing)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.Cutlass)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.PirateStaff)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.PirateBooty
            });
            pirateShop.Add(new Item(ItemID.CoinGun)
            {
                shopCustomPrice = 60,
                shopSpecialCurrency = AAMod.PirateBooty
            });

            pirateShop.Register();
            #endregion

            NPCShop eclipseShop = new(Type, "EclipseShop");
            #region Eclipse Shop
            eclipseShop.Add(new Item(ItemID.EyeSpring)
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.MonsterSoul
            });
            eclipseShop.Add(new Item(ItemID.BrokenBatWing)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.MonsterSoul
            });
            eclipseShop.Add(new Item(ItemID.MoonStone)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.MonsterSoul
            });
            eclipseShop.Add(new Item(ItemID.NeptunesShell)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.MonsterSoul
            });
            eclipseShop.Add(new Item(ItemID.DeathSickle)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.MonsterSoul
            });

            eclipseShop.Add(new Item(ModContent.ItemType<HeroRelics>())
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedMechBossAll);

            Condition downedMoth = new(Language.GetTextValue("Mods.AAModClassic.Common.Conditions.DownedMoth"), () => DownedBools.downedMoth);
            eclipseShop.Add(new Item(ItemID.BrokenHeroSword)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, downedMoth);

            eclipseShop.Add(new Item(ItemID.MothronWings)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera, downedMoth);
            eclipseShop.Add(new Item(ItemID.TheEyeOfCthulhu)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera, downedMoth);

            eclipseShop.Add(new Item(ItemID.NailGun)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera);
            eclipseShop.Add(new Item(ItemID.Nail), Condition.DownedPlantera);
            eclipseShop.Add(new Item(ItemID.PsychoKnife)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera);
            eclipseShop.Add(new Item(ItemID.DeadlySphereStaff)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera);
            eclipseShop.Add(new Item(ItemID.ToxicFlask)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera);
            eclipseShop.Add(new Item(ItemID.ButchersChainsaw)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MonsterSoul
            }, Condition.DownedPlantera);

            eclipseShop.Register();
            #endregion

            NPCShop pumpkinShop = new(Type, "PumpkinMoonShop");
            #region Pumpkin Moon Shop
            pumpkinShop.Add(new Item(ItemID.SpookyWood)
            {
                shopCustomPrice = 50
            }, Condition.DownedMourningWood);

            pumpkinShop.Add(new Item(ItemID.GoodieBag)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });
            pumpkinShop.Add(new Item(ItemID.ScarecrowHat)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });
            pumpkinShop.Add(new Item(ItemID.ScarecrowShirt)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });
            pumpkinShop.Add(new Item(ItemID.ScarecrowPants)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });
            pumpkinShop.Add(new Item(ItemID.JackOLanternMask)
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });
            pumpkinShop.Add(new Item(ItemID.BloodyMachete)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });
            pumpkinShop.Add(new Item(ItemID.BladedGlove)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });

            pumpkinShop.Add(new Item(ItemID.StakeLauncher)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedMourningWood);
            pumpkinShop.Add(new Item(ItemID.Stake), Condition.DownedMourningWood);
            pumpkinShop.Add(new Item(ItemID.NecromanticScroll)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedMourningWood);
            pumpkinShop.Add(new Item(ItemID.SpookyHook)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedMourningWood);
            pumpkinShop.Add(new Item(ItemID.SpookyTwig)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedMourningWood);
            pumpkinShop.Add(new Item(ItemID.CursedSapling)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedMourningWood);

            pumpkinShop.Add(new Item(ItemID.TheHorsemansBlade)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.JackOLanternLauncher)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.JackOLantern), Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.CandyCornRifle)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.BatScepter)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.RavenStaff)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.BlackFairyDust)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);
            pumpkinShop.Add(new Item(ItemID.SpiderEgg)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.HalloweenTreat
            }, Condition.DownedPumpking);

            pumpkinShop.Add(new Item(ItemID.MagicalPumpkinSeed)
            {
                shopCustomPrice = 60,
                shopSpecialCurrency = AAMod.HalloweenTreat
            });

            pumpkinShop.Register();
            #endregion

            NPCShop frostShop = new(Type, "FrostMoonShop");
            #region Frost Moon Shop
            frostShop.Add(new Item(ItemID.ElfHat)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.ChristmasCheer
            });
            frostShop.Add(new Item(ItemID.ElfShirt)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.ChristmasCheer
            });
            frostShop.Add(new Item(ItemID.ElfPants)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.ChristmasCheer
            });
            frostShop.Add(new Item(ItemID.SnowGlobe)
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.ChristmasCheer
            });
            frostShop.Add(new Item(ItemID.Present)
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.ChristmasCheer
            });
            frostShop.Add(new Item(ItemID.GiantBow)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.ChristmasCheer
            });

            frostShop.Add(new Item(ItemID.ChristmasTreeSword)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedEverscream);
            frostShop.Add(new Item(ItemID.Razorpine)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedEverscream);
            frostShop.Add(new Item(ItemID.FestiveWings)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedEverscream);
            frostShop.Add(new Item(ItemID.ChristmasHook)
            {
                shopCustomPrice = 20,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedEverscream);

            frostShop.Add(new Item(ItemID.ChainGun)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedSantaNK1);
            frostShop.Add(new Item(ItemID.ElfMelter)
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedSantaNK1);

            frostShop.Add(new Item(ItemID.NorthPole)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedIceQueen);
            frostShop.Add(new Item(ItemID.SnowmanCannon)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedIceQueen);
            frostShop.Add(new Item(ItemID.BlizzardStaff)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedIceQueen);
            frostShop.Add(new Item(ItemID.BabyGrinchMischiefWhistle)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedIceQueen);
            frostShop.Add(new Item(ItemID.ReindeerBells)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.ChristmasCheer
            }, Condition.DownedIceQueen);

            frostShop.Register();
            #endregion

            NPCShop martianShop = new(Type, "MartianShop");
            #region Martian Shop
            martianShop.Add(new Item(ItemID.MartianConduitPlating)
            {
                shopCustomPrice = 50
            });
            martianShop.Add(new Item(ItemID.MartianCostumeMask)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.MartianCostumeShirt)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.MartianCostumePants)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.MartianUniformHelmet)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.MartianUniformTorso)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.MartianUniformPants)
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.BrainScrambler)
            {
                shopCustomPrice = 30,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.InfluxWaver)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.Xenopopper)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.ElectrosphereLauncher)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.LaserMachinegun)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.ChargedBlasterCannon)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.XenoStaff)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.LaserDrill)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.AntiGravityHook)
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ItemID.CosmicCarKey)
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = AAMod.MartianCredit
            });
            martianShop.Add(new Item(ModContent.ItemType<EnergyConduit>())
            {
                shopCustomPrice = 50,
                shopSpecialCurrency = AAMod.MartianCredit
            });

            martianShop.Register();
            #endregion
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.GoblinSlayer>()));
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 80;
			knockback = 3f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.ThrowingKnife;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)

        {

            multiplier = 9f;

            randomOffset = 1f;

        }
    }
}