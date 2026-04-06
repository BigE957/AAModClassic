using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Blocks.Boxes
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
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<TerrariumBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
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
