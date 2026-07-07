using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Materials
{
    public class M79Parts : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Materials";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("M79 Parts");
			// Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
