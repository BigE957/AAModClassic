using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodWall : BaseAAItem
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
            Item.createWall = Mod.Find<ModWall>("BogwoodWall").Type; //put your CustomBlock Tile name
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "Bogwood");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = Recipe.Create(null, "Bogwood");
            recipe.AddIngredient(this, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
