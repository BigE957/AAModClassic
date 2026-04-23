using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;
using AAModClassic._Content.Mire.World.Tiles;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class MireUndergroundBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mire Underground Music Box");
            // Tooltip.SetDefault(@"Plays 'The Deepest Reaches' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<MireUndergroundBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MireSurfaceBox>());
            recipe.AddIngredient(ModContent.ItemType<Depthstone>(), 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
