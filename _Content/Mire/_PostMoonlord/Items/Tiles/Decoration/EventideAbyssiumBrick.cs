using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Tiles.Decoration
{
    public class EventideAbyssiumBrick : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EventideAbyssiumBrick_Tile>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eventide Brick");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumOre>(), 1);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
