using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Purity.__Hardmode.Items.Consumables
{
    public class DeepGreenSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Deep Green Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nConverts the jungle into forest"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<DeepGreenSolution_Proj>() - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.sellPrice(0, 0, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
		}
	}
}
