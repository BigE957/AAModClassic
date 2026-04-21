using AAModClassic.___Content.Acropolis._PostMoonlord.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Acropolis._PostMoonlord.Items.Tiles.Decoration
{
    public class SkymarbleBrick : BaseAAItem
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
            Item.createTile = ModContent.TileType<SkymarbleBrick_Tile>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skymarble Brick");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ModContent.ItemType<SkyCrystal>(), 1);
            recipe.AddIngredient(ItemID.Marble, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
