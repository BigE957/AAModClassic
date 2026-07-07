using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomWorkbench : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Furniture.Doom";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Workbench");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 18;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<DoomWorkbench_Tile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomsdayCircuitPlating>(), 10);
            recipe.Register();

        }

    }
}