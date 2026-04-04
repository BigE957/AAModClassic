using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.RazewoodF
{
    public class RazewoodBookshelf : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Bookcase");
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
            Item.createTile = ModContent.TileType<RazewoodBookshelf>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Razewood>(), 20);
            recipe.AddIngredient(ItemID.Book, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

        }

    }
}