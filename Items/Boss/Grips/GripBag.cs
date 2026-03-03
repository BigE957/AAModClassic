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
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true; Item.expertOnly = true;
        }
        //public override int BossBagNPC => Mod.Find<ModNPC>("GripOfChaosBlue").Type;

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("GripMaskBlue").Type);
            }
            else if (Main.rand.Next(7) == 1)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("GripMaskRed").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("ClawBaton").Type);
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("Abyssium").Type, Main.rand.Next(25, 56));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("Incinerite").Type, Main.rand.Next(25, 56));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("ClawOfChaos").Type);
		}
	}
}