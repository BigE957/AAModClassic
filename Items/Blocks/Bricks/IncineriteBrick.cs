using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;
using AAModClassic.Tiles.Bricks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Bricks
{
    public class IncineriteBrick : BaseAAItem
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
            Item.createTile = ModContent.TileType<IncineriteBrick_Tile>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Incinerite Brick");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteOre>(), 1);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
