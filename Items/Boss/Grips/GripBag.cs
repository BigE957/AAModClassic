using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Grips
{
    public class GripBag : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Treasure Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
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
        //public override int BossBagNPC => ModContent.NPCType<GripOfChaosBlue>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GripMaskBlue>());
            }
            else if (Main.rand.Next(7) == 1)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GripMaskRed>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<ClawBaton>());
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Abyssium>(), Main.rand.Next(25, 56));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Incinerite>(), Main.rand.Next(25, 56));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<ClawOfChaos>());
		}
	}
}