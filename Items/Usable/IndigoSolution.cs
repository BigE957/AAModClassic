using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class IndigoSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Indigo Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Mire"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = Mod.Find<ModProjectile>("IndigoSolution").Type - ProjectileID.PureSpray;
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
