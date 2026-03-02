using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mikpin
{
    public class MikBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kitsune's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Weeb Fox!'");
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
			player.QuickSpawnItem(ModContent.ItemType<MikpinWig>());
            player.QuickSpawnItem(ModContent.ItemType<MikpinCloak>());
            player.QuickSpawnItem(ModContent.ItemType<MikpinPants>());
        }
    }
}