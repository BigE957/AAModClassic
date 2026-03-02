using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Walls
{
    public class DoomstoneBrickWall : BaseAAItem
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
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createWall = Mod.Find<ModWall>("DoomstoneBrickWall").Type;
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
            recipe = Recipe.Create(null, "Doomstone");
            recipe.AddIngredient(this, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
