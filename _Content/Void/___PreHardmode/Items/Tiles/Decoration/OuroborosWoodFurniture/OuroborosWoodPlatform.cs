using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture
{
    public class OuroborosWoodPlatform : ModItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.Furniture.OuroborosWood";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Ouroboros Wood Platform");
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
			Item.createTile = ModContent.TileType<OuroborosWoodPlatform_Tile>();
		}

		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<OuroborosWood>());
            recipe.Register(); 
        }
	}
}
