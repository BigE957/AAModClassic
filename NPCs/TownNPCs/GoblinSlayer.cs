using AAModClassic.Items.Accessories;
using AAModClassic.Items.Armor.GoblinSlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic.NPCs.TownNPCs
{
    [AutoloadHead]
	public class GoblinSlayer : ModNPC
    {
        public static bool Goblin = false;
        public static bool Blood = false;
        public static bool OOA = false;
        public static bool Pirate = false;
        public static bool Eclipse = false;
        public static bool Pumpkin = false;
        public static bool Frost = false;
        public static bool Martian = false;

        public override string Texture => "AAModClassic/NPCs/TownNPCs/GoblinSlayer";

        //public override bool IsLoadingEnabled(Mod mod)
		//{
		//	name = "Goblin Slayer";
		//	return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
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
			if (Goblin >= 0 && Main.rand.Next(4) == 0)
			{
                chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat1") + Main.npc[Goblin].GivenName + Lang.TownNPCGoblinSlayer("GoblinSlayerChat2"));
            }
            int DD2Bartender = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (DD2Bartender >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[DD2Bartender].GivenName + Lang.TownNPCGoblinSlayer("GoblinSlayerChat3");
            }
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat4"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat5"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat6"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat7"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat8"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat9"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat10"));
            if (NPC.downedPirates || NPC.downedMartians || DownedBools.downedOgre)
            {
                chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat11"));
            }
            return chat; 
        }

        public static int ChatNumber = 0;

        public void ResetBools()
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
			button = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopChangeShopType");

            string GobShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopGoblinLoot");
            string BloodShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopBloodMoonLoot");
            string OOAShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopOldOneArmyLoot");
            string PirateShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopPirateLoot");
            string EclipseShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopEclipseLoot");
            string PumpShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopHalloweenLoot");
            string FrostShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopChristmasLoot");
            string MartianShop = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerShopMartianLoot");

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
                items[nextSlot].SetDefaults(ModContent.ItemType<GoblinSlayerHelm>());
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ModContent.ItemType<GoblinSlayerChest>());
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ModContent.ItemType<GoblinSlayerGreaves>());
                items[nextSlot].shopCustomPrice = new int?(12);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ModContent.ItemType<Items.Melee.GoblinSlayer>());
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.GoblinBattleStandard);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.Harpoon);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                if (DownedBools.downedGobSummoner)
                {
                    items[nextSlot].SetDefaults(ItemID.ShadowFlameKnife);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ShadowFlameBow);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ShadowFlameHexDoll);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                }
            }
            else if (Blood)
            {
                items[nextSlot].SetDefaults(ItemID.TopHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.TheBrideHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.TheBrideDress);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.SharkToothNecklace);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MoneyTrough);
                items[nextSlot].shopCustomPrice = new int?(25);
                items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                if (Main.hardMode)
                {
                    items[nextSlot].SetDefaults(ItemID.KOCannon);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                    nextSlot++;
                    if (NPC.downedClown)
                    {
                        items[nextSlot].SetDefaults(ItemID.Bananarang);
                        items[nextSlot].shopCustomPrice = new int?(20);
                        items[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                        nextSlot++;
                    }
                }
            }
            else if (OOA)
            {
                items[nextSlot].SetDefaults(ModContent.ItemType<OldOneCharm>());
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.WarTableBanner);
                items[nextSlot].shopCustomPrice = new int?(2);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.WarTable);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.DD2PetDragon);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.DD2PetGato);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                if (DownedBools.downedOgre == true)
                {
                    items[nextSlot].SetDefaults(ItemID.ApprenticeScarf);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.SquireShield);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.HuntressBuckler);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.MonkBelt);
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.DD2PetGhost);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.DD2SquireDemonSword);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.MonkStaffT2);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.MonkStaffT1);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.BookStaff);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.DD2PhoenixBow);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                }
                if (DownedBools.downedBetsy == true)
                {
                    items[nextSlot].SetDefaults(ItemID.DD2SquireBetsySword);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.MonkStaffT3);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.DD2BetsyBow);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ApprenticeStaffT3);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.BetsyWings);
                    items[nextSlot].shopCustomPrice = new int?(50);
                    items[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                }
            }
            else if (Pirate)
            {
                if (Main.hardMode)
                {
                    items[nextSlot].SetDefaults(ItemID.PirateMap);
                    items[nextSlot].shopCustomPrice = Item.sellPrice(0, 1, 0, 0);
                    nextSlot++;
                }
                items[nextSlot].SetDefaults(ItemID.EyePatch);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.SailorHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.SailorShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.SailorPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BuccaneerBandana);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BuccaneerShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BuccaneerPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.LuckyCoin);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.DiscountCard);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.GoldRing);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.Cutlass);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.PirateStaff);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.CoinGun);
                items[nextSlot].shopCustomPrice = new int?(60);
                items[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
            }
            else if (Eclipse)
            {
                items[nextSlot].SetDefaults(ItemID.EyeSpring);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BrokenBatWing);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MoonStone);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.NeptunesShell);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.DeathSickle);
                items[nextSlot].shopCustomPrice = new int?(25);
                items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                if (DownedBools.downedMoth)
                {
                    items[nextSlot].SetDefaults(ItemID.BrokenHeroSword);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    if (DownedBools.downedMoth)
                    {
                        items[nextSlot].SetDefaults(ItemID.MothronWings);
                        items[nextSlot].shopCustomPrice = new int?(40);
                        items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                        nextSlot++;
                        items[nextSlot].SetDefaults(ItemID.TheEyeOfCthulhu);
                        items[nextSlot].shopCustomPrice = new int?(40);
                        items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                        nextSlot++;
                    }
                    items[nextSlot].SetDefaults(ItemID.NailGun);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.Nail);
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.PsychoKnife);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.DeadlySphereStaff);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ToxicFlask);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ButchersChainsaw);
                    items[nextSlot].shopCustomPrice = new int?(40);
                    items[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
            }
            else if (Pumpkin)
            {
                if (NPC.downedHalloweenTree)
                {
                    items[nextSlot].SetDefaults(ItemID.SpookyWood);
                    items[nextSlot].value = 50;
                    nextSlot++;
                }
                items[nextSlot].SetDefaults(ItemID.GoodieBag);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ScarecrowHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ScarecrowShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ScarecrowPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.JackOLanternMask);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BloodyMachete);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BladedGlove);
                items[nextSlot].shopCustomPrice = new int?(20);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                if (NPC.downedHalloweenTree)
                {
                    items[nextSlot].SetDefaults(ItemID.StakeLauncher);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.Stake);
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.NecromanticScroll);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.SpookyHook);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.SpookyTwig);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.CursedSapling);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                }
                if (NPC.downedHalloweenKing)
                {
                    items[nextSlot].SetDefaults(ItemID.TheHorsemansBlade);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.JackOLanternLauncher);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.JackOLantern);
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.CandyCornRifle);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.BatScepter);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.RavenStaff);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.BlackFairyDust);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.SpiderEgg);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                }
                items[nextSlot].SetDefaults(ItemID.MagicalPumpkinSeed);
                items[nextSlot].shopCustomPrice = new int?(60);
                items[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
            }
            else if (Frost)
            {
                items[nextSlot].SetDefaults(ItemID.ElfHat);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ElfShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ElfPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.SnowGlobe);
                items[nextSlot].shopCustomPrice = new int?(10);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.Present);
                items[nextSlot].shopCustomPrice = new int?(15);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.GiantBow);
                items[nextSlot].shopCustomPrice = new int?(30);
                items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                if (NPC.downedChristmasTree)
                {
                    items[nextSlot].SetDefaults(ItemID.ChristmasTreeSword);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.Razorpine);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.FestiveWings);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ChristmasHook);
                    items[nextSlot].shopCustomPrice = new int?(20);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
                if (NPC.downedChristmasSantank)
                {
                    items[nextSlot].SetDefaults(ItemID.ChainGun);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ElfMelter);
                    items[nextSlot].shopCustomPrice = new int?(25);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
                if (NPC.downedChristmasIceQueen)
                {
                    items[nextSlot].SetDefaults(ItemID.NorthPole);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.SnowmanCannon);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.BlizzardStaff);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.BabyGrinchMischiefWhistle);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    items[nextSlot].SetDefaults(ItemID.ReindeerBells);
                    items[nextSlot].shopCustomPrice = new int?(30);
                    items[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
            }
            else if (Martian)
            {
                items[nextSlot].SetDefaults(ItemID.MartianConduitPlating);
                items[nextSlot].value = 50;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MartianCostumeMask);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MartianCostumeShirt);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MartianCostumePants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MartianUniformHelmet);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MartianUniformTorso);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.MartianUniformPants);
                items[nextSlot].shopCustomPrice = new int?(5);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.BrainScrambler);
                items[nextSlot].shopCustomPrice = new int?(30);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.InfluxWaver);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.Xenopopper);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ElectrosphereLauncher);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.LaserMachinegun);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.ChargedBlasterCannon);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.XenoStaff);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.LaserDrill);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.AntiGravityHook);
                items[nextSlot].shopCustomPrice = new int?(40);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ItemID.CosmicCarKey);
                items[nextSlot].shopCustomPrice = new int?(50);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                items[nextSlot].SetDefaults(ModContent.ItemType<Energy_Conduit>());
                items[nextSlot].shopCustomPrice = new int?(50);
                items[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
            }
        }

		public override void OnKill()
		{
			Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<Items.Melee.GoblinSlayer>());
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