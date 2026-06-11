using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Tiles.Decoration
{
    public class InfernoUndergroundBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Underground Inferno)");
            // Tooltip.SetDefault(@"Plays ‘Inner Mantle’ by ProduceVGM");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<InfernoUndergroundBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<InfernoSurfaceBox>());
            recipe.AddIngredient(ModContent.ItemType<Torchstone>(), 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
