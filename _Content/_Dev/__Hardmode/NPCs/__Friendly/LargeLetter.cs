using AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity;
using AAModClassic._Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Chaos.___PreHardmode.NPCs.Friendly;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.GoblinArmy.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.Friendly;
using AAModClassic._Content.SunkenShip.__PreHardmode.NPCs.__Friendly;
using AAModClassic._Unofficial.Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Unofficial.Desert;
using AAModClassic.UI.World;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content._Dev.__Hardmode.NPCs.__Friendly
{
    [AutoloadHead]
	public class LargeLetter : ModNPC, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.TownNPCs";

        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //	name = "Large Letter";
        //	return Mod.Properties/* tModPorter Note: _Unreleased. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
        //}

        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 10;
			NPCID.Sets.AttackFrameCount[NPC.type] = 5;
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 40;
			NPCID.Sets.AttackAverageChance[NPC.type] = 20;
			NPCID.Sets.HatOffsetY[NPC.type] = 3;

			NPC.Happiness
				.SetNPCAffection(ModContent.NPCType<Legendscribe>(), AffectionLevel.Like)
				.SetNPCAffection(ModContent.NPCType<LegendscribeUnofficial>(), AffectionLevel.Like)
				.SetNPCAffection(ModContent.NPCType<Lovecraftian>(), AffectionLevel.Like)
				.SetNPCAffection(ModContent.NPCType<Samurai>(), AffectionLevel.Like)
				.SetNPCAffection(ModContent.NPCType<Mushman>(), AffectionLevel.Like)
				.SetNPCAffection(ModContent.NPCType<GoblinSlayer>(), AffectionLevel.Hate);
        }

		public override void SetDefaults()
		{
            Main.npcFrameCount[NPC.type] = 25;
            NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = NPCAIStyleID.Passive;
			NPC.damage = 40;
			NPC.defense = 38;
			NPC.lifeMax = 600;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{

		}

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
			for (int k = 0; k < 255; k++)
			{
				Player player = Main.player[k];
				if (player.active && Main.expertMode)
				{
					if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !Main.hardMode)
						return false;
					return true;
				}
			}
			return false;
		}

		public override List<string> SetNPCNameList()
		{
            return ["Big E"];
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.LargeLetter.Chat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.LargeLetter.Chat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.LargeLetter.Chat3"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.LargeLetter.Chat4"));

			return chat;
		}

		public override void PostAI()
		{
			if (!Main.expertMode)
			{
				NPC.life = 0;
				NPC.active = false;
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.LargeLetter.Button1");
			button2 = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.LargeLetter.Button2");
		}

		public static bool VanityShop = true;

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
			{
				VanityShop = true;
                shopName = "shop";
			}
			else
			{
				VanityShop = false;
                shopName = "shop";
            }
		}

		public override void ModifyActiveShop(string shopName, Item[] items)
		{
			int nextSlot = 0;
			if (VanityShop)
			{
				items[nextSlot] = new Item(ModContent.ItemType<ApawnBag>());
				items[nextSlot].shopCustomPrice = new int?(5);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<FazerBag>());
				items[nextSlot].shopCustomPrice = new int?(10);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<ShoxBag>());
				items[nextSlot].shopCustomPrice = new int?(10);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<BegBag>());
				items[nextSlot].shopCustomPrice = new int?(10);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<CCBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<CerberusBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<BlazenBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                {
                    // thrown randomly in middle cuz i feel like it
					items[nextSlot] = new Item(ModContent.ItemType<PlanterrorBag>());
                    items[nextSlot].shopCustomPrice = new int?(15);
                    items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
                    nextSlot++;
                }
                items[nextSlot] = new Item(ModContent.ItemType<AvesBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<DellyBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<TiedBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<HallamBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<TailsBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<BigEBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<DallinBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<MoonBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<GibsBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<CharlieBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
			}
			else
			{
				if (Main.hardMode)
				{
					items[nextSlot] = new Item(ModContent.ItemType<PineBreaker>());
					items[nextSlot].shopCustomPrice = new int?(15);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
				}
				if (NPC.downedPlantBoss)
				{
					items[nextSlot] = new Item(ModContent.ItemType<FuryForger>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GameRaider>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<AleisterStaff>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
				}
				if (NPC.downedMoonlord)
				{
					items[nextSlot] = new Item(ModContent.ItemType<ExtravagantLongsword>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<TimeTeller>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<CursedSickle>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Demise>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<DuckstepLauncher>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<ConflagrateStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Ethereal>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<MobianBuster>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GentlemansRapier>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GibsFemur>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Skullshot>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<ScytheOfTheGrimReaper>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Prismeow>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<MagicAcorn>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Placeholder>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<PoniumStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<SkrallStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<SockStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<SoulSiphon>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<StormRifle>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<TitanAxe>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<UmbralReaper>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<BladeOfNight>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
				}
			}
		}

		public override void OnKill()
		{
			Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<MudFishBall>());
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 30;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<ExtravagantLongsword_BigE>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}