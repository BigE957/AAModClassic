using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class HallowedForge : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed Forge");
            // Tooltip.SetDefault("It's amazing what this thing CAN'T cook");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = 7;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150000;
            Item.createTile = Mod.Find<ModTile>("HallowedForge").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "HallowedOre", 20);
            recipe.AddRecipeGroup("AAMod:HForge");
            recipe.Register();
        }
    }
}
