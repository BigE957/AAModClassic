using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Oroboros
{
    public class OroborosWorkbench : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Workbench");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 18;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("OroborosWorkbench").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("OroborosWood").Type, 10);
            recipe.Register();

        }

    }
}