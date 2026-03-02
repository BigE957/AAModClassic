using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Maskano
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
			player.QuickSpawnItem(ModContent.ItemType<Mask>());
            player.QuickSpawnItem(ModContent.ItemType<MaskPlate>());
            player.QuickSpawnItem(ModContent.ItemType<MaskBoots>());
        }
    }
}