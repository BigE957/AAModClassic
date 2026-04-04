using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.RazewoodF
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
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<RazewoodPlatform>();
		}

		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(2);
            recipe.AddIngredient(null, "Razewood");
            recipe.Register(); 
        }
	}
}
