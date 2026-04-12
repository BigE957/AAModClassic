using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Greed.WKG
{
    public class GreedABag : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<GreedA>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WKGreedMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesireTalisman>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StoneShell>(), 1, 25, 30));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CovetiteOre>(), 1, 30, 50));

            int[] lootTable = { ModContent.ItemType<OreCannon>(), ModContent.ItemType<Unearther>(), ModContent.ItemType<Earthbreaker>(), ModContent.ItemType<OreStaff>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
	}
}