using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class SharkLauncher : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shark Launcher");
			/* Tooltip.SetDefault("Launches latching deadly shark"
			+"\nPiranha Gun EX"); */
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PiranhaGun);
			Item.damage = 500;
			Item.shoot = ModContent.ProjectileType<SharkLauncher_Shark>();
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PiranhaGun);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}
	}
}
