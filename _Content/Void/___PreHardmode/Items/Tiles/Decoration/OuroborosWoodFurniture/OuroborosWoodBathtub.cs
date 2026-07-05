using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture
{
    public class OuroborosWoodBathtub : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Furniture.OuroborosWood";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ouroboros Wood Bathtub");
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
            Item.createTile = ModContent.TileType<OuroborosWoodBathtub_Tile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OuroborosWood>(), 14);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
            
        }

    }
}