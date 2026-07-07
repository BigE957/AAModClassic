using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodBathtub : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Furniture.Bogwood";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Bathtub");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<BogwoodBathtub_Tile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bogwood>(), 14);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
            
        }

    }
}