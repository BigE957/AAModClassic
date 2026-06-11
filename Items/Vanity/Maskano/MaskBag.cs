using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Maskano
{
    public class MaskBag : BaseAAItem
    {
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
            Item.expert = true; Item.expertOnly = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<Mask>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MaskPlate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MaskBoots>());
        }
    }
}