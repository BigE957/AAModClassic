using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Chaos.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Inferno.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.Mire.__Hardmode.Items.Consumables;
using AAModClassic.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.Chaos.___PreHardmode.NPCs.Friendly
{
    [AutoloadHead]
	public class Samurai : ModNPC, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.TownNPCs";

        //public override string[] AltTextures => new string[] { "AAModClassic/NPCs/TownNPCs/SamuraiParty" };

        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //	name = "Samurai";
        //	return Mod.Properties/* tModPorter Note: _Unreleased. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
        //}

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

            NPC.Happiness
                .SetBiomeAffection<InfernoBiome>(AffectionLevel.Like)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Dislike);
        }

		public override void SetDefaults()
		{
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
            if (!AAConfigClient.Instance.NoAATownNPC)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (player.active)
                    {
                        if (AAWorld.downedGrips == true)
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
            return ["Nobunaga", "Hattori", "Hanzo", "Genji", "Oda", "Hideyoshi"];
		}

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int TravellingMerchant = NPC.FindFirstNPC(NPCID.TravellingMerchant);
			if (TravellingMerchant >= 0 && Main.rand.NextBool(4))
			{
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat1") + Main.npc[TravellingMerchant].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat2"));
            }
            int DD2Bartender = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (DD2Bartender >= 0 && Main.rand.NextBool(4))
            {
                return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat3") + Main.npc[DD2Bartender].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat4");
            }
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat5"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat6"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat7"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat8"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat9"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat10"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat11"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat12"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat13"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat14"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat15"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat16"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat17"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat18"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat19"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat20"));
			chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Samurai.SamuraiChat21"));
            return chat; 
        }
        
		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "shop";
            }
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            int nextSlot = 0;
            items[nextSlot] = new Item(ItemID.DynastyWood);
            nextSlot++;
            if (Main.dayTime)
            {
                items[nextSlot] = new Item(ItemID.RedDynastyShingles);
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<ScorchedSeeds>());
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<Sunpowder>());
                nextSlot++;
                if (NPCExtensions.BeenKilled<Broodmother>() == true)
                {
                    items[nextSlot] = new Item(ModContent.ItemType<AncientBell>());
                    items[nextSlot].value = 100000;
                    nextSlot++;
                }
            }
            if (!Main.dayTime)
            {
                items[nextSlot] = new Item(ItemID.BlueDynastyShingles);
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<DankSeeds>());
                nextSlot++;
                items[nextSlot] = new Item(ModContent.ItemType<Moonpowder>());
                nextSlot++;
                if (NPCExtensions.BeenKilled<HydraBody>() == true)
                {
                    items[nextSlot] = new Item(ModContent.ItemType<HydraChow>());
                    items[nextSlot].value = 100000;
                    nextSlot++;
                }
            }
            items[nextSlot] = new Item(ModContent.ItemType<LuckyCracker>());
            items[nextSlot].value = 2000000;
			nextSlot++;
            items[nextSlot] = new Item(ModContent.ItemType<RoninPotion>());
            items[nextSlot].value = 50000;
			nextSlot++;
			items[nextSlot] = new Item(ItemID.Sake);
			nextSlot++;
			items[nextSlot] = new Item(ItemID.Pho);
			nextSlot++;
            items[nextSlot] = new Item(ItemID.PadThai);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.Gi);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.Kimono);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.FancyDishes);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.Katana);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.Shuriken);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.NinjaHood);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.NinjaShirt);
            nextSlot++;
            items[nextSlot] = new Item(ItemID.NinjaPants);
            nextSlot++;
            if (Main.dayTime)
            {
                if (Main.hardMode == true)
                {
                    items[nextSlot] = new Item(ModContent.ItemType<OrangeSolution>());
                    nextSlot++;
                }
            }
            else
            {
                if (Main.hardMode == true)
                {
                    items[nextSlot] = new Item(ModContent.ItemType<IndigoSolution>());
                    nextSlot++;
                }
            }

            if (Main.hardMode == true)
            {
                items[nextSlot] = new Item(ModContent.ItemType<OrderSolution>());
                nextSlot++;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Katana));
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
            projType = ProjectileID.Shuriken;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}