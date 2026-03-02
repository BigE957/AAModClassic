using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Walls
{
    public class EquinoxWall : BaseAAItem
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
            Item.createWall = Mod.Find<ModWall>("EquinoxWall").Type; //put your CustomBlock Tile name
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "EquinoxBrick");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = Recipe.Create(null, "EquinoxBrick");
            recipe.AddIngredient(this, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
