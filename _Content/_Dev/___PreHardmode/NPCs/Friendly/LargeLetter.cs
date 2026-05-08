using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using AAModClassic.Items.Vanity.Delly;
using AAModClassic.Items.Vanity.Aves;
using AAModClassic.Items.Vanity.Hallam;
using AAModClassic.Items.Vanity.Fazer;
using AAModClassic.Items.Vanity.Moon;
using AAModClassic.Items.Vanity.Apawn;
using AAModClassic.Items.Vanity.Shox;
using Terraria.Localization;
using AAModClassic.Items.Vanity.Tied;
using AAModClassic.Items.Vanity.Tails;
using AAModClassic.Items.Vanity.Alphakip;
using AAModClassic.Items.Vanity.Dallin;
using AAModClassic.Items.Vanity.Gibs;
using AAModClassic.Items.Vanity.Charlie;
using AAModClassic.Items.Vanity.Blazen;
using AAModClassic.Items.Vanity.Cerberus;
using AAModClassic.Items.Vanity.CC;
using AAModClassic.Items.Vanity.Beg;
using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;

namespace AAModClassic._Content._Dev.___PreHardmode.NPCs.Friendly
{
    [AutoloadHead]
	public class LargeLetter : ModNPC
	{
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
				items[nextSlot] = new Item(ModContent.ItemType<ApawnEgg>());
				items[nextSlot].shopCustomPrice = new int?(5);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<WetFurrbag>());
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

				items[nextSlot] = new Item(ModContent.ItemType<CCBox>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<InvokerBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<BlazenBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<AvesBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<DellyBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<OldMagiciansHat>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<MagiciansHat>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<TailsToolbox>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<AlphaBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<FezLordsBag>());
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
					items[nextSlot] = new Item(ModContent.ItemType<InvokerStaff>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
				}
				if (NPC.downedMoonlord)
				{
					items[nextSlot] = new Item(ModContent.ItemType<AmphibianLongsword>());
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
					items[nextSlot] = new Item(ModContent.ItemType<DuckstepGun>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<EnderStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.AncientCoin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Etheral>());
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
					items[nextSlot] = new Item(ModContent.ItemType<GrimReaperScythe>());
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
					items[nextSlot] = new Item(ModContent.ItemType<ThunderLord>());
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
					items[nextSlot] = new Item(ModContent.ItemType<UmbreonSP>());
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
            projType = ModContent.ProjectileType<Projectiles.AmphibiousProjectile>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}