using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Crafters
{
    public class HallowedAnvil : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed Anvil");
            // Tooltip.SetDefault("A Holy Anvil");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 100000;
            Item.createTile = ModContent.TileType<HallowedAnvil_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddRecipeGroup("AAModClassic:HAnvil");
            recipe.AddIngredient(ItemID.PearlwoodWorkBench, 1);
            recipe.AddIngredient(ItemID.CrystalBall, 1);
            recipe.AddIngredient(ItemID.Autohammer, 1);
            recipe.Register();
        }
    }
}
