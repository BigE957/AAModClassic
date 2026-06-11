using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Blazen
{
    public class BlazenBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Thunder Lord's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Thunder Lord!'");
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BlazenHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BlazenPlate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BlazenBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<BlazenBooster>());
            }
        }
    }
}