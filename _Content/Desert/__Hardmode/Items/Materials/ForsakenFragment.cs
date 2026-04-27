using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Desert.__Hardmode.Items.Materials
{
    public class ForsakenFragment : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forsaken Fragment");
		}

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 20000;
			Item.rare = ItemRarityID.Pink;
		}
	}
}