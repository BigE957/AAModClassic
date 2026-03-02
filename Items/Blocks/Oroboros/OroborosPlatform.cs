using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Oroboros
{
    public class OroborosPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Oroboros Wood Platform");
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
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("OroborosPlatform").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(2);
            recipe.AddIngredient(null, "OroborosWood");
            recipe.Register(); 
            recipe = Recipe.Create(null, "OroborosWood");
            recipe.AddIngredient(this, 2);
            recipe.Register();
        }
	}
}
