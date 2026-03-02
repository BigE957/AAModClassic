using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class WhiteSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("White Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the snow biome"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.SnowSolution>() - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.sellPrice(0, 0, 25, 0);
			Item.rare = 3;
			Item.maxStack = 999;
			Item.consumable = true;
		}
	}
}
