using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodPlatform : ModItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.Furniture.Bogwood";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Bogwood Platform");
            Item.ResearchUnlockCount = 200;
        }

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 10;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<BogwoodPlatform_Tile>();
		}

		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<Bogwood>());
            recipe.Register(); 
        }
	}
}
