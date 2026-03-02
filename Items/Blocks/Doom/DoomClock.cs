using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomClock : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Countdown Clock");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("DoomClock").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 6);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(Mod.Find<ModItem>("ApcalyptitePlate").Type, 10);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }

    }
}