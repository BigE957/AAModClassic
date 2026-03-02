using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.RazewoodF
{
    public class RazewoodPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Razewood Platform");
		}

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 10;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("RazewoodPlatform").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(2);
            recipe.AddIngredient(null, "Razewood");
            recipe.Register(); 
            recipe = Recipe.Create(null, "Razewood");
            recipe.AddIngredient(this, 2);
            recipe.Register();
        }
	}
}
