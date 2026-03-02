using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class FishnadoStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fishnado Staff");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(2621);
			Item.damage = 150;
			Item.rare = 11;
			Item.shoot = Mod.Find<ModProjectile>("Fishnado").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TempestStaff);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}