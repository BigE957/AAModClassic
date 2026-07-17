using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture;
using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration
{
    public class Razewood : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {

            Item.width = 24;
            Item.height = 22;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Razewood_Tile>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood");
            Item.ResearchUnlockCount = 100;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RazewoodWall>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<RazewoodPlatform>(), 2);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();
        }
    }
}
