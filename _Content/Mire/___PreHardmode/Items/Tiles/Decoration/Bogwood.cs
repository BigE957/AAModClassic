using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration
{
    class Bogwood : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Bogwood_Tile>(); //put your CustomBlock Tile name
            Item.ammo = Item.type;
            Item.notAmmo = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood");
            // Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BogwoodWall>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
