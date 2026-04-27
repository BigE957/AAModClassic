using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodWall : BaseAAItem
    {
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
            Item.createWall = ModContent.WallType<RazewoodWall_Wall>();
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<Razewood>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
