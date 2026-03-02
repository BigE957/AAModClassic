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

        public override string Texture => "AAMod/NPCs/TownNPCs/GoblinSlayer";

        public override bool IsLoadingEnabled(Mod mod)
		{
			name = "Goblin Slayer";
			return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
		}

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
			return "Goblin Slayer";
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
                shop = true;
            }
		}

		public override void ModifyActiveShop(string shopName, Item[] items)
		{
            if (Goblin)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoblinSlayerHelm>());
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoblinSlayerChest>());
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoblinSlayerGreaves>());
                shop.item[nextSlot].shopCustomPrice = new int?(12);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Melee.GoblinSlayer>());
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GoblinBattleStandard);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Harpoon);
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                nextSlot++;
                if (DownedBools.downedGobSummoner)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ShadowFlameKnife);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ShadowFlameBow);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ShadowFlameHexDoll);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
                    nextSlot++;
                }
            }
            else if (Blood)
            {
                shop.item[nextSlot].SetDefaults(ItemID.TopHat);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.TheBrideHat);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.TheBrideDress);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SharkToothNecklace);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MoneyTrough);
                shop.item[nextSlot].shopCustomPrice = new int?(25);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                nextSlot++;
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.KOCannon);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                    nextSlot++;
                    if (NPC.downedClown)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Bananarang);
                        shop.item[nextSlot].shopCustomPrice = new int?(20);
                        shop.item[nextSlot].shopSpecialCurrency = AAMod.BloodRune;
                        nextSlot++;
                    }
                }
            }
            else if (OOA)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<OldOneCharm>());
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WarTableBanner);
                shop.item[nextSlot].shopCustomPrice = new int?(2);
                shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WarTable);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.DD2PetDragon);
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.DD2PetGato);
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                nextSlot++;
                if (DownedBools.downedOgre == true)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ApprenticeScarf);
                    shop.item[nextSlot].shopCustomPrice = new int?(15);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SquireShield);
                    shop.item[nextSlot].shopCustomPrice = new int?(15);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.HuntressBuckler);
                    shop.item[nextSlot].shopCustomPrice = new int?(15);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.MonkBelt);
                    shop.item[nextSlot].shopCustomPrice = new int?(15);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.DD2PetGhost);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.DD2SquireDemonSword);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.MonkStaffT2);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.MonkStaffT1);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BookStaff);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.DD2PhoenixBow);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                }
                if (DownedBools.downedBetsy == true)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.DD2SquireBetsySword);
                    shop.item[nextSlot].shopCustomPrice = new int?(50);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.MonkStaffT3);
                    shop.item[nextSlot].shopCustomPrice = new int?(50);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.DD2BetsyBow);
                    shop.item[nextSlot].shopCustomPrice = new int?(50);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ApprenticeStaffT3);
                    shop.item[nextSlot].shopCustomPrice = new int?(50);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BetsyWings);
                    shop.item[nextSlot].shopCustomPrice = new int?(50);
                    shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
                    nextSlot++;
                }
            }
            else if (Pirate)
            {
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.PirateMap);
                    shop.item[nextSlot].shopCustomPrice = Item.sellPrice(0, 1, 0, 0);
                    nextSlot++;
                }
                shop.item[nextSlot].SetDefaults(ItemID.EyePatch);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SailorHat);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SailorShirt);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SailorPants);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BuccaneerBandana);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BuccaneerShirt);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BuccaneerPants);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LuckyCoin);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.DiscountCard);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GoldRing);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Cutlass);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.PirateStaff);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CoinGun);
                shop.item[nextSlot].shopCustomPrice = new int?(60);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.PirateBooty;
                nextSlot++;
            }
            else if (Eclipse)
            {
                shop.item[nextSlot].SetDefaults(ItemID.EyeSpring);
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BrokenBatWing);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MoonStone);
                shop.item[nextSlot].shopCustomPrice = new int?(20);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.NeptunesShell);
                shop.item[nextSlot].shopCustomPrice = new int?(20);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.DeathSickle);
                shop.item[nextSlot].shopCustomPrice = new int?(25);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                nextSlot++;
                if (DownedBools.downedMoth)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.BrokenHeroSword);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    if (DownedBools.downedMoth)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MothronWings);
                        shop.item[nextSlot].shopCustomPrice = new int?(40);
                        shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TheEyeOfCthulhu);
                        shop.item[nextSlot].shopCustomPrice = new int?(40);
                        shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                        nextSlot++;
                    }
                    shop.item[nextSlot].SetDefaults(ItemID.NailGun);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Nail);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.PsychoKnife);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.DeadlySphereStaff);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ToxicFlask);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ButchersChainsaw);
                    shop.item[nextSlot].shopCustomPrice = new int?(40);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.MonsterSoul;
                    nextSlot++;
                }
            }
            else if (Pumpkin)
            {
                if (NPC.downedHalloweenTree)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SpookyWood);
                    shop.item[nextSlot].value = 50;
                    nextSlot++;
                }
                shop.item[nextSlot].SetDefaults(ItemID.GoodieBag);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ScarecrowHat);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ScarecrowShirt);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ScarecrowPants);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.JackOLanternMask);
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BloodyMachete);
                shop.item[nextSlot].shopCustomPrice = new int?(20);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BladedGlove);
                shop.item[nextSlot].shopCustomPrice = new int?(20);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
                if (NPC.downedHalloweenTree)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.StakeLauncher);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Stake);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.NecromanticScroll);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SpookyHook);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SpookyTwig);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CursedSapling);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                }
                if (NPC.downedHalloweenKing)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.TheHorsemansBlade);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.JackOLanternLauncher);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.JackOLantern);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CandyCornRifle);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BatScepter);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RavenStaff);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BlackFairyDust);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SpiderEgg);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                    nextSlot++;
                }
                shop.item[nextSlot].SetDefaults(ItemID.MagicalPumpkinSeed);
                shop.item[nextSlot].shopCustomPrice = new int?(60);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.HalloweenTreat;
                nextSlot++;
            }
            else if (Frost)
            {
                shop.item[nextSlot].SetDefaults(ItemID.ElfHat);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ElfShirt);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ElfPants);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SnowGlobe);
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Present);
                shop.item[nextSlot].shopCustomPrice = new int?(15);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GiantBow);
                shop.item[nextSlot].shopCustomPrice = new int?(30);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                nextSlot++;
                if (NPC.downedChristmasTree)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ChristmasTreeSword);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Razorpine);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FestiveWings);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ChristmasHook);
                    shop.item[nextSlot].shopCustomPrice = new int?(20);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
                if (NPC.downedChristmasSantank)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ChainGun);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.EldMelter);
                    shop.item[nextSlot].shopCustomPrice = new int?(25);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
                if (NPC.downedChristmasIceQueen)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.NorthPole);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SnowmanCannon);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BlizzardStaff);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BabyGrinchMischiefWhistle);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ReindeerBells);
                    shop.item[nextSlot].shopCustomPrice = new int?(30);
                    shop.item[nextSlot].shopSpecialCurrency = AAMod.ChristmasCheer;
                    nextSlot++;
                }
            }
            else if (Martian)
            {
                shop.item[nextSlot].SetDefaults(ItemID.MartianConduitPlating);
                shop.item[nextSlot].value = 50;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MartianCostumeMask);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MartianCostumeShirt);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MartianCostumePants);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MartianUniformHelmet);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MartianUniformTorso);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MartianUniformPants);
                shop.item[nextSlot].shopCustomPrice = new int?(5);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BrainScrambler);
                shop.item[nextSlot].shopCustomPrice = new int?(30);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.InfluxWaver);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Xenopopper);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ElectrosphereLauncher);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LaserMachinegun);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ChargedBlasterCannon);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.XenoStaff);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LaserDrill);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.AntiGravityHook);
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CosmicCarKey);
                shop.item[nextSlot].shopCustomPrice = new int?(50);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Energy_Conduit>());
                shop.item[nextSlot].shopCustomPrice = new int?(50);
                shop.item[nextSlot].shopSpecialCurrency = AAMod.MartianCredit;
                nextSlot++;
            }
        }

		public override void OnKill()
		{
			Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Melee.GoblinSlayer>());
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