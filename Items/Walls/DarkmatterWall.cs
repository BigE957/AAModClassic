using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Walls
{
    public class DarkmatterWall : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = Mod.Find<ModWall>("DarkmatterWall").Type; //put your CustomBlock Tile name
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "DarkmatterBrick");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = Recipe.Create(null, "DarkmatterBrick");
            recipe.AddIngredient(this, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
