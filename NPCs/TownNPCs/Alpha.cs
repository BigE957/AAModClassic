using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using AAModClassic.Items.Dev;
using AAModClassic.Items.Vanity.Delly;
using AAModClassic.Items.Vanity.Aves;
using AAModClassic.Items.Vanity.Hallam;
using AAModClassic.Items.Vanity.Fazer;
using AAModClassic.Items.Vanity.Moon;
using AAModClassic;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Vanity.Apawn;
using AAModClassic.Items.Vanity.Shox;
using Terraria.Localization;

namespace AAModClassic.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Alpha : ModNPC
	{
        public override string Texture => "AAModClassic/NPCs/TownNPCs/Alpha";

        //public override bool IsLoadingEnabled(Mod mod)
		//{
		//	name = "Mudfish";
		//	return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
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

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
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

		public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
            return ["Big E"];
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Alpha.AlphaChat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Alpha.AlphaChat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Alpha.AlphaChat3"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Alpha.AlphaChat4"));

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
			button = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Alpha.AlphaButton1");
			button2 = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Alpha.AlphaButton2");
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
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<WetFurrbag>());
				items[nextSlot].shopCustomPrice = new int?(10);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<ShoxBag>());
				items[nextSlot].shopCustomPrice = new int?(10);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Beg.BegBag>());
				items[nextSlot].shopCustomPrice = new int?(10);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.CC.CCBox>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Cerberus.InvokerBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Blazen.BlazenBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<AvesBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<DellyBag>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Tied.OldMagiciansHat>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<MagiciansHat>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Tails.TailsToolbox>());
				items[nextSlot].shopCustomPrice = new int?(15);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;

				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Alphakip.AlphaBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Dallin.FezLordsBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<MoonBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Gibs.GibsBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				items[nextSlot] = new Item(ModContent.ItemType<Items.Vanity.Charlie.CharlieBag>());
				items[nextSlot].shopCustomPrice = new int?(25);
				items[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
			}
			else
			{
				if (Main.hardMode)
				{
					items[nextSlot] = new Item(ModContent.ItemType<PineBreaker>());
					items[nextSlot].shopCustomPrice = new int?(15);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
				}
				if (NPC.downedPlantBoss)
				{
					items[nextSlot] = new Item(ModContent.ItemType<FuryForger>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GameRaider>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Items.Dev.Invoker.InvokerStaff>());
					items[nextSlot].shopCustomPrice = new int?(25);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
				}
				if (NPC.downedMoonlord)
				{
					items[nextSlot] = new Item(ModContent.ItemType<AmphibianLongsword>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<TimeTeller>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<CursedSickle>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Demise>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<DuckstepGun>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<EnderStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Etheral>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<MobianBuster>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GentlemansRapier>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GibsFemur>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Skullshot>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<GrimReaperScythe>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Prismeow>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<MagicAcorn>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<Placeholder>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<PoniumStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<SkrallStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<SockStaff>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<SoulSiphon>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<ThunderLord>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<TitanAxe>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<UmbralReaper>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					items[nextSlot] = new Item(ModContent.ItemType<UmbreonSP>());
					items[nextSlot].shopCustomPrice = new int?(40);
					items[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
				}
			}
		}

		public override void OnKill()
		{
			Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<MudkipBall>());
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
            projType = ModContent.ProjectileType<AAModClassic.Projectiles.AmphibiousProjectile>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}