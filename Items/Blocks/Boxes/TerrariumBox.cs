using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class TerrariumBox : BaseAAItem
	{
            
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terrarium Music Box");
            // Tooltip.SetDefault("Plays 'Heart of the World' by Quicksilvur feat Charlie Debnam");

        }

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("TerrariumBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 4;
			Item.value = 10000;
			Item.accessory = true;
            
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBoxTitle);
            recipe.AddIngredient(null, "MonarchBox", 1);
            recipe.AddIngredient(null, "InfernoBox", 1);
            recipe.AddIngredient(null, "MireUBox", 1);
            recipe.AddIngredient(null, "InfernoBox", 1);
            recipe.AddIngredient(null, "MireUBox", 1);
            recipe.AddIngredient(null, "VoidBox", 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
