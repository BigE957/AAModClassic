using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Bricks
{
    public class RadiumBrick : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("RadiumBrick").Type;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Brick");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "RadiumOre", 1);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
