using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories;
using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.BossStandard
{
    public class GreedTreasureBag : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Greed)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<Greed>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<StoneShell>(), Main.rand.Next(25, 30));
            if (Main.rand.NextBool(7))
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GreedMask>());
            }
            if (Main.rand.NextBool(10))
            {
                ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
                modPlayer.PPDevArmor();
            }
            string[] lootTable = { "GildedGlock", "Miner", "StoneSlammer", "GoldDigger"};
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            //player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<CovetiteCoin>(), Main.rand.Next(60, 150));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<CharmOfDesire>());
        }
	}
}