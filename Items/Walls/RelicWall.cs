using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Snow.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Snow.World.Tiles;

namespace AAModClassic.Items.Walls
{
    public class RelicWall : BaseAAItem
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
            Item.createWall = ModContent.WallType<RelicBrick_Wall>(); //put your CustomBlock Tile name
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Relic Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<RelicBrick>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
