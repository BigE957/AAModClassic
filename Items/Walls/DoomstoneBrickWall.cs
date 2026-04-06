using AAModClassic;
using AAModClassic.Walls.Bricks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Walls
{
    public class DoomstoneBrickWall : BaseAAItem
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
            Item.createWall = ModContent.WallType<DoomstoneBrick_Wall>();
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomstone Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "Doomstone");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
