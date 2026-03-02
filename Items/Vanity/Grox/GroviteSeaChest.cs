using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Grox
{
    public class GroviteSeaChest : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grovite Sea Chest");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Angry Code Pirate!'");
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
			player.QuickSpawnItem(ModContent.ItemType<AngryPirateHood>());
            player.QuickSpawnItem(ModContent.ItemType<AngryPirateCofferplate>());
            player.QuickSpawnItem(ModContent.ItemType<AngryPirateBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<AngryPirateSails>());
            }
        }
    }
}