using Terraria.ModLoader;
using Terraria;

namespace AAModClassic.Items.Vanity.Shox
{
    public class ShoxBag : BaseAAItem
    {
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
            Item.expert = true; Item.expertOnly = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShoxVisor>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShoxPlate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShoxPants>());
        }
    }
}