using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class MaskanoBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mask Bag");
            /* Tooltip.SetDefault(@"<right> to open
'All the essentials for impersonating the Mask Lord.'"); */
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MaskanoHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MaskanoChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MaskanoLeggings>());
        }
    }
}