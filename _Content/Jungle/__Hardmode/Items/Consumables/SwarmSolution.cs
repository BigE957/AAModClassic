using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Jungle.__Hardmode.Items.Consumables
{
    public class SwarmSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Swarm Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nCleanses the mushroom biomes"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<SwarmSolution_Proj>() - ProjectileID.PureSpray;
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
