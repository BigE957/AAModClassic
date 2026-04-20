using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Grips
{
    public class GripsOfChaosTreasureBag : BaseAAItem
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
			Item.width = 36;
			Item.height = 32;
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<GripOfChaosBlue>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MireGripMask>(), 7).OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<InfernoGripMask>(), 7)));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClawBaton>(), 3));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 25, 56));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 25, 56));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClawOfChaos>()));
        }
	}
}