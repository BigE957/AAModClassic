using AAModClassic._Content.Hallow.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Crafters
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
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150000;
            Item.createTile = ModContent.TileType<HallowedForge_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallowedOre>(), 20);
            recipe.AddRecipeGroup("AAModClassic:HForge");
            recipe.Register();
        }
    }
}
