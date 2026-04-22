using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Walls.Bricks;
using AAModClassic.___Content.Stars._PostMoonlord.Items.Tiles.Decoration;

namespace AAModClassic.Items.Walls
{
    public class EquinoxWall : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<EquinoxBrick_Wall>(); //put your CustomBlock Tile name
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<EquinoxBrick>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
