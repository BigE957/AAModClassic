using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Charlie
{
    public class CharlieBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaper's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Grim Edgelord!'");
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieCowl>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieCloak>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieWings>());
            }
        }
    }
}