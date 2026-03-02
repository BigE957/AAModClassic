using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Beg
{
    public class BegBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Weird Horse Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating that weird horse kid!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
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
			player.QuickSpawnItem(ModContent.ItemType<PonyMask>());
            player.QuickSpawnItem(ModContent.ItemType<PonyBody>());
            player.QuickSpawnItem(ModContent.ItemType<PonyHoofs>());
        }
    }
}