using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodCouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Couch");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("BogwoodCouch").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Bogwood").Type, 5);
            recipe.AddIngredient(ItemID.Silk, 2);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}