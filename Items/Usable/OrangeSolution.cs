using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Usable
{
    public class OrangeSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Orange Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Inferno"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = Mod.Find<ModProjectile>("OrangeSolution").Type - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.sellPrice(0, 0, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 999;
			Item.consumable = true;
		}
	}
}
