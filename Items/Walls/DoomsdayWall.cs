using AAModClassic.___Content.Void._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Walls.Bricks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Walls
{
    public class DoomsdayWall : BaseAAItem
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
            Item.createWall = ModContent.WallType<DoomsdayPlating_Wall>(); //put your CustomBlock Tile name
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Circuit Wall");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<DoomsdayCircuitPlating>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
