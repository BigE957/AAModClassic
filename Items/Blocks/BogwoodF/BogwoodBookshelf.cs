using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodBookshelf : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Bookcase");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("BogwoodBookshelf").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Bogwood").Type, 20);
            recipe.AddIngredient(ItemID.Book, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

        }

    }
}