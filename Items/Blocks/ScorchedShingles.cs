using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    class ScorchedShingles : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ScorchedShingles>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scorched Dynasty Shingles");
            // Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RedDynastyShingles, 1);
            recipe.AddCondition(Condition.NearLava);
            recipe.Register();
        }
    }
}
