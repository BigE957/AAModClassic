using Terraria.ID;

namespace AAMod.Items.FishingItem
{
    public class ShinyCharmFish : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.width = 34;
            Item.height = 36;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Cyan;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shiny Charm Fish");
			// Tooltip.SetDefault("A kind of rare fish");
		}
    }
}
