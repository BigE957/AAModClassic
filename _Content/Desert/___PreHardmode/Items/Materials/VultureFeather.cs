using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Materials
{
    public class VultureFeather : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vulture Feather");
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 34;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 0, 8, 0);
			Item.rare = ItemRarityID.White;
		}
	}
}
