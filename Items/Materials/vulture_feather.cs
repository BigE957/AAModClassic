using Terraria;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class vulture_feather : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vulture Feather");
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 34;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 0, 8, 0);
			Item.rare = ItemRarityID.White;
		}
	}
}
