using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;

namespace AAModClassic.Items.Blocks.Boxes
{
	public class SagBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sagittarius Music Box");

            // Tooltip.SetDefault(@"Plays 'Event Horizon' by SpectralAves");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SagBox_Tile>();
            Item.width = 72;
			Item.height = 36;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
