using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Aves
{
    public class AvesBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("DJ Duck's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Monochrome Mallard!'");
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<DJDuckHead>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<DJDuckShirt>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<DJDuckPants>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<DuckstepWings>());
            }
        }
    }
}