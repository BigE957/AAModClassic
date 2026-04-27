using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Mikpin
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MikpinWig>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MikpinCloak>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MikpinPants>());
        }
    }
}