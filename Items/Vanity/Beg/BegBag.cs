using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Beg
{
    public class BegBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Weird Horse Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating that weird horse kid!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BegHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BegChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BegLeggings>());
        }
    }
}