using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class StormChest : ModItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Storm Chest");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 500;
			Item.createTile = ModContent.TileType<StormChest_Tile>();
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:IronBar", 2);
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 12);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}