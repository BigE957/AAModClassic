using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodBed : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Bed");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("BogwoodBed").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Bogwood").Type, 15);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

        }

    }
}