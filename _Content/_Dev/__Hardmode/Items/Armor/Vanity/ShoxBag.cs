using Terraria.ModLoader;
using Terraria;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class ShoxBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charged Shock Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Shock Lord!'");
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
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShoxHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShoxChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShoxLeggings>());
        }
    }
}