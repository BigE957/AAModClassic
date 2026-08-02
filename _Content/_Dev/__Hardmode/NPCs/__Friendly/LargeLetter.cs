using AAModClassic._Content._Dev.__Hardmode.Items.Accessories.Vanity;
using AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity;
using AAModClassic._Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Chaos.___PreHardmode.NPCs.Friendly;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Tools;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.GoblinArmy.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.Friendly;
using AAModClassic._Content.SunkenShip.__PreHardmode.NPCs.__Friendly;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Unofficial.Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Unofficial.Desert;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.LargeLetter")
            ]);
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

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
                shopName = "VanityShop";
			else
                shopName = "WeaponShop";
		}

        public override void AddShops()
        {
			NPCShop vanityShop = new(Type, "VanityShop");
            #region Vanity Shop
            vanityShop.Add(new Item(ModContent.ItemType<ApawnBag>()) 
			{
                shopCustomPrice = 5,
                shopSpecialCurrency = AAMod.AncientCoin
            });

            vanityShop.Add(new Item(ModContent.ItemType<FazerBag>())
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<ShoxBag>())
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<BegBag>())
            {
                shopCustomPrice = 10,
                shopSpecialCurrency = AAMod.AncientCoin
            });

            vanityShop.Add(new Item(ModContent.ItemType<CCBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<CerberusBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<BlazenBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<AvesBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<DellyBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<TiedBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<HallamBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<TailsBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<PlanterrorBag>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            }, new Condition(Language.GetTextValue("Mods.AAModClassic.Common.Conditions.Unofficial"), () => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial)));

            vanityShop.Add(new Item(ModContent.ItemType<BigEBag>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<DallinBag>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<MoonBag>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<GibsBag>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            vanityShop.Add(new Item(ModContent.ItemType<CharlieBag>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            });
            #endregion

            NPCShop weaponShop = new(Type, "WeaponShop");
            #region Weapon Shop
            weaponShop.Add(new Item(ModContent.ItemType<PineBreaker>())
            {
                shopCustomPrice = 15,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.Hardmode);

            weaponShop.Add(new Item(ModContent.ItemType<FuryForger>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedPlantera);
            weaponShop.Add(new Item(ModContent.ItemType<GameRaider>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedPlantera);
            weaponShop.Add(new Item(ModContent.ItemType<AleisterStaff>())
            {
                shopCustomPrice = 25,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedPlantera);

            weaponShop.Add(new Item(ModContent.ItemType<ExtravagantLongsword>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<TimeTeller>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<CursedSickle>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<Demise>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<DuckstepLauncher>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<ConflagrateStaff>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<Ethereal>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<MobianBuster>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<GentlemansRapier>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<GibsFemur>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<Skullshot>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<ScytheOfTheGrimReaper>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<Prismeow>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<MagicAcorn>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<Placeholder>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<PoniumStaff>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<SkrallStaff>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<SockStaff>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<SoulSiphon>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<StormRifle>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<TitanAxe>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<UmbralReaper>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            weaponShop.Add(new Item(ModContent.ItemType<BladeOfNight>())
            {
                shopCustomPrice = 40,
                shopSpecialCurrency = AAMod.AncientCoin
            }, Condition.DownedMoonLord);
            #endregion
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LittleE>()));
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