using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Tails
{
    public class TailsToolbox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tails' Toolbox");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Fox Wonder!'");
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
			player.QuickSpawnItem(ModContent.ItemType<TailsHead>());
            player.QuickSpawnItem(ModContent.ItemType<TailsBody>());
            player.QuickSpawnItem(ModContent.ItemType<TailsLegs>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.Jetpack);
            }
        }
    }
}