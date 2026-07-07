using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class CharlieBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
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
            Item.expert = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieLeggings>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CharlieWings>());
            }
        }
    }
}