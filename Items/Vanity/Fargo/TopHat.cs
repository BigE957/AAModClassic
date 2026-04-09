using Terraria.ModLoader;
using Terraria;

namespace AAModClassic.Items.Vanity.Fargo
{
    public class TopHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Squirrelly Top Hat");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Meme Squirrel!'");
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
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FargoHat>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FargoSuit>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FargoPants>());
        }
    }
}