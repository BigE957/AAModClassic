using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Tiles.Boxes;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class DjinnBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Desert Djinn Music Box");
            // Tooltip.SetDefault(@"Plays 'The Dust Bowl' by Universe");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<DjinnBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "DesertMana", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
