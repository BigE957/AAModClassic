using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Delly
{
    public class DellyBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daughter of the Void's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Void Mistress!'");
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
			player.QuickSpawnItem(ModContent.ItemType<DellyWig>());
            player.QuickSpawnItem(ModContent.ItemType<DellyShirt>());
            player.QuickSpawnItem(ModContent.ItemType<DellyBoots>());
        }
    }
}