using Terraria;

namespace AAMod.Items.Materials
{
    public class M79Parts : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("M79 Parts");
			// Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = 3;
		}
	}
}
