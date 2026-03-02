using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using AAMod.Items.Dev;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Alpha : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/Alpha";

        public override bool IsLoadingEnabled(Mod mod)
		{
			name = "Mudfish";
			return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
		}

		public override void SetStaticDefaults()
		{
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
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
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
            return "Alphakip";
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(Lang.TownNPCAlpha("AlphaChat1"));
            chat.Add(Lang.TownNPCAlpha("AlphaChat2"));
            chat.Add(Lang.TownNPCAlpha("AlphaChat3"));
            chat.Add(Lang.TownNPCAlpha("AlphaChat4"));

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
			button = Lang.TownNPCAlpha("AlphaButton1");
			button2 = Lang.TownNPCAlpha("AlphaButton2");
		}

		public static bool VanityShop = true;

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
			{
				VanityShop = true;
				shop = true;
			}
			else
			{
				VanityShop = false;
				shop = true;
			}
		}

		public override void ModifyActiveShop(string shopName, Item[] items)
		{
			if (VanityShop)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Apawn.ApawnEgg>());
				shop.item[nextSlot].shopCustomPrice = new int?(5);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Fazer.WetFurrbag>());
				shop.item[nextSlot].shopCustomPrice = new int?(10);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Shox.ShoxBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(10);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Beg.BegBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(10);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.CC.CCBox>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Cerberus.InvokerBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Blazen.BlazenBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Aves.AvesBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Delly.DellyBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Tied.OldMagiciansHat>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Hallam.MagiciansHat>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Tails.TailsToolbox>());
				shop.item[nextSlot].shopCustomPrice = new int?(15);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Alphakip.AlphaBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(25);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Dallin.FezLordsBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(25);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Eliza.LizBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(25);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Moon.MoonBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(25);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Gibs.GibsBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(25);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Charlie.CharlieBag>());
				shop.item[nextSlot].shopCustomPrice = new int?(25);
				shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
				nextSlot++;
			}
			else
			{
				if (Main.hardMode)
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<PineBreaker>());
					shop.item[nextSlot].shopCustomPrice = new int?(15);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
				}
				if (NPC.downedPlantBoss)
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<FuryForger>());
					shop.item[nextSlot].shopCustomPrice = new int?(25);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<GameRaider>());
					shop.item[nextSlot].shopCustomPrice = new int?(25);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Dev.Invoker.InvokerStaff>());
					shop.item[nextSlot].shopCustomPrice = new int?(25);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
				}
				if (NPC.downedMoonlord)
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<AmphibianLongsword>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<CatsEyeRifle>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<TimeTeller>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<CursedSickle>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Demise>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<DuckstepGun>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<EnderStaff>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Etheral>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<MobianBuster>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<GentlemansRapier>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<GibsFemur>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Skullshot>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<GrimReaperScythe>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Prismeow>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<MagicAcorn>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Placeholder>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<PoniumStaff>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<SkrallStaff>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<SockStaff>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulSiphon>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<ThunderLord>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<TitanAxe>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<UmbralReaper>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<UmbreonSP>());
					shop.item[nextSlot].shopCustomPrice = new int?(40);
					shop.item[nextSlot].shopSpecialCurrency = AAMod.Coin;
					nextSlot++;
				}
			}
		}

		public override void OnKill()
		{
			Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Pets.MudkipBall>());
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