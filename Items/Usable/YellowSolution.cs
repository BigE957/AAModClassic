using AAModClassic;
using AAModClassic.Projectiles;
using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Usable
{
    public class YellowSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Yellow Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nClears the Snow biome"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Snowmelt>() - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.sellPrice(0, 0, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 9999;
			Item.consumable = true;
		}
	}
}
