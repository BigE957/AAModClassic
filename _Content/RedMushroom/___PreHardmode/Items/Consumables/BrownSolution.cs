using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Consumables
{
    public class BrownSolution : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Consumables";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brown Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Red Mushroom biome"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<BrownSolution_Proj>() - ProjectileID.PureSpray;
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
