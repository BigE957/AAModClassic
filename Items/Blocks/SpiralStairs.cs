using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class SpiralStairs : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spiral Stairs");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 100;
            Item.createTile = Mod.Find<ModTile>("SpiralStairs").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddRecipeGroup("AAMod:Iron", 2);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
