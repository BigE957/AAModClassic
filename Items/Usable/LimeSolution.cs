using AAModClassic.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Usable
{
    public class LimeSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lime Solution");
			/* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nConverts the forest into the jungle"); */
		}

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<JungleSolution>() - ProjectileID.PureSpray;
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
