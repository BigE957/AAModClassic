using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Bricks
{
    public class EquinoxBrick : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("EquinoxBrick").Type;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox Brick");
            // Tooltip.SetDefault("Changes depending on the time of day");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "RadiumBrick", 1);
            recipe.AddIngredient(null, "DarkmatterBrick", 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
