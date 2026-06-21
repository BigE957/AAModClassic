using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class FazerBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wet Furrbag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Funloving Fox!'");
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
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FazerHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FazerChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FazerLeggings>());
        }
    }
}