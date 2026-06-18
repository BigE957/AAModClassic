using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class GroxBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grovite Sea Chest");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Angry Code Pirate!'");
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GroxHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GroxChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GroxLeggings>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GroxWings>());
            }
        }
    }
}