using AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Armor;
using AAModClassic._Content.MartianMadness.__Hardmode.Items.Accessories;
using AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Content.Terra.__Hardmode.Items.Materials;
using AAModClassic.Globals;
using System.Collections.Generic;
using Terraria;
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
                shopName = "shop";
            }
		}

		public override void ModifyActiveShop(string shopName, Item[] items)
		{
            int nextSlot = 0;
            if (Goblin)
            {
                items[nextSlot] = new Item(ModContent.ItemType<GoblinSlayersHelmet>());
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<GoblinSlayersChestplate>());
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<GoblinSlayersLeggings>());
                items[nextSlot].shopCustomPrice = new int?(12);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<Items.Weapons.GoblinSlayer>());
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.GoblinBattleStandard);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.Harpoon);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                if (DownedBools.downedGobSummoner)
                {
                    items[nextSlot] = new Item(ItemID.ShadowFlameKnife);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ShadowFlameBow);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ShadowFlameHexDoll);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                }
            }
            else if (Blood)
            {
                items[nextSlot] = new Item(ItemID.TopHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.TheBrideHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.TheBrideDress);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.SharkToothNecklace);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MoneyTrough);
                items[nextSlot].shopCustomPrice = new int?(25);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                if (Main.hardMode)
                {
                    items[nextSlot] = new Item(ItemID.KOCannon);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                    nextSlot++;
                    if (NPC.downedClown)
                    {
                        items[nextSlot] = new Item(ItemID.Bananarang);
                        items[nextSlot].shopCustomPrice = new int?(20);
                        items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                        nextSlot++;
                    }
                }
            }
            else if (OOA)
            {
                items[nextSlot] = new Item(ModContent.ItemType<OldOneCharm>());
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.WarTableBanner);
                items[nextSlot].shopCustomPrice = new int?(2);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.WarTable);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.DD2PetDragon);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.DD2PetGato);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                if (DownedBools.downedOgre == true)
                {
                    items[nextSlot] = new Item(ItemID.ApprenticeScarf);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.SquireShield);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.HuntressBuckler);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.MonkBelt);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.DD2PetGhost);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.DD2SquireDemonSword);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.MonkStaffT2);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.MonkStaffT1);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.BookStaff);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.DD2PhoenixBow);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                }
                if (DownedBools.downedBetsy == true)
                {
                    items[nextSlot] = new Item(ItemID.DD2SquireBetsySword);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.MonkStaffT3);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.DD2BetsyBow);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ApprenticeStaffT3);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.BetsyWings);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                }
            }
            else if (Pirate)
            {
                if (Main.hardMode)
                {
                    items[nextSlot] = new Item(ItemID.PirateMap);
                    items[nextSlot].shopCustomPrice = Item.sellPrice(0, 1, 0, 0);
                    nextSlot++;
                }
                items[nextSlot] = new Item(ItemID.EyePatch);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.SailorHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.SailorShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.SailorPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BuccaneerBandana);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BuccaneerShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BuccaneerPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.LuckyCoin);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.DiscountCard);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.GoldRing);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.Cutlass);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.PirateStaff);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.CoinGun);
                items[nextSlot].shopCustomPrice = new int?(60);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
            }
            else if (Eclipse)
            {
                items[nextSlot] = new Item(ItemID.EyeSpring);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BrokenBatWing);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MoonStone);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.NeptunesShell);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.DeathSickle);
                items[nextSlot].shopCustomPrice = new int?(25);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    items[nextSlot] = new Item(ModContent.ItemType<HeroRelics>());
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
                if (DownedBools.downedMoth)
                {
                    items[nextSlot] = new Item(ItemID.BrokenHeroSword);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    if (DownedBools.downedMoth)
                    {
                        items[nextSlot] = new Item(ItemID.MothronWings);
                        items[nextSlot].shopCustomPrice = new int?(40);
                        items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                        nextSlot++;
                        items[nextSlot] = new Item(ItemID.TheEyeOfCthulhu);
                        items[nextSlot].shopCustomPrice = new int?(40);
                        items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                        nextSlot++;
                    }
                    items[nextSlot] = new Item(ItemID.NailGun);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.Nail);
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.PsychoKnife);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.DeadlySphereStaff);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ToxicFlask);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ButchersChainsaw);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
            }
            else if (Pumpkin)
            {
                if (NPC.downedHalloweenTree)
                {
                    items[nextSlot] = new Item(ItemID.SpookyWood);
                    items[nextSlot].value = 50;
                    nextSlot++;
                }
                items[nextSlot] = new Item(ItemID.GoodieBag);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ScarecrowHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ScarecrowShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ScarecrowPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.JackOLanternMask);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BloodyMachete);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BladedGlove);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                if (NPC.downedHalloweenTree)
                {
                    items[nextSlot] = new Item(ItemID.StakeLauncher);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.Stake);
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.NecromanticScroll);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.SpookyHook);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.SpookyTwig);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.CursedSapling);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                }
                if (NPC.downedHalloweenKing)
                {
                    items[nextSlot] = new Item(ItemID.TheHorsemansBlade);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.JackOLanternLauncher);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.JackOLantern);
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.CandyCornRifle);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.BatScepter);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.RavenStaff);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.BlackFairyDust);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.SpiderEgg);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                }
                items[nextSlot] = new Item(ItemID.MagicalPumpkinSeed);
                items[nextSlot].shopCustomPrice = new int?(60);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
            }
            else if (Frost)
            {
                items[nextSlot] = new Item(ItemID.ElfHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ElfShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ElfPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.SnowGlobe);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.Present);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.GiantBow);
                items[nextSlot].shopCustomPrice = new int?(30);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                if (NPC.downedChristmasTree)
                {
                    items[nextSlot] = new Item(ItemID.ChristmasTreeSword);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.Razorpine);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.FestiveWings);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ChristmasHook);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
                if (NPC.downedChristmasSantank)
                {
                    items[nextSlot] = new Item(ItemID.ChainGun);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ElfMelter);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
                if (NPC.downedChristmasIceQueen)
                {
                    items[nextSlot] = new Item(ItemID.NorthPole);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.SnowmanCannon);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.BlizzardStaff);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.BabyGrinchMischiefWhistle);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot] = new Item(ItemID.ReindeerBells);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
            }
            else if (Martian)
            {
                items[nextSlot] = new Item(ItemID.MartianConduitPlating);
                items[nextSlot].value = 50;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MartianCostumeMask);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MartianCostumeShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MartianCostumePants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MartianUniformHelmet);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MartianUniformTorso);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.MartianUniformPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.BrainScrambler);
                items[nextSlot].shopCustomPrice = new int?(30);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.InfluxWaver);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.Xenopopper);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ElectrosphereLauncher);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.LaserMachinegun);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.ChargedBlasterCannon);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.XenoStaff);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.LaserDrill);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.AntiGravityHook);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ItemID.CosmicCarKey);
                items[nextSlot].shopCustomPrice = new int?(50);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<EnergyConduit>());
                items[nextSlot].shopCustomPrice = new int?(50);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
            }
        }

		public override void OnKill()
		{
			Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<Items.Weapons.GoblinSlayer>());
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