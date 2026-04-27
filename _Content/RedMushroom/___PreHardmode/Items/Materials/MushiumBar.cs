using AAModClassic.Tiles.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials
{
    public class MushiumBar : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Blue;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<MushiumBar_Tile>();
            Item.value = Item.sellPrice(0, 0, 9, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushium Bar");
            // Tooltip.SetDefault("Mushy");
        }

		public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Mushium>(), 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
