using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
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
			Item.shoot = Mod.Find<ModProjectile>("SharkLauncherP").Type;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PiranhaGun);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}
	}
}
