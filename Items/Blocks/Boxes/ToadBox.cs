using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class ToadBox : BaseAAItem
	{
            
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Truffle Toad Music Box");
            // Tooltip.SetDefault("Plays 'TODESTOOL' by Spectral Aves");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<ToadBox_Tile>();
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
            recipe.AddIngredient(null, "MushiumBar", 10);
            recipe.AddIngredient(null, "GlowingMushiumBar", 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
