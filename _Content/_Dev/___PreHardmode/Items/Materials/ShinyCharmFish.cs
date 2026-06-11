using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Materials
{
    public class ShinyCharmFish : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.width = 34;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Cyan;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shiny Charm Fish");
			// Tooltip.SetDefault("A kind of rare fish");
		}
    }
}
