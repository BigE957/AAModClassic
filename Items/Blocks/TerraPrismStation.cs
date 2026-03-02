using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class TerraPrismStation : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinity Core");
            /* Tooltip.SetDefault(@"The 'craft-all'.
Combiles all vanilla and Ancients Awakened crafting stations together"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 100000;
            Item.createTile = Mod.Find<ModTile>("TerraPrism").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "FurnitureDynamo", 1);
            recipe.AddIngredient(null, "TerraCore", 1);
            recipe.AddRecipeGroup("AAMod:ACS");
            recipe.Register();
        }
    }
}
