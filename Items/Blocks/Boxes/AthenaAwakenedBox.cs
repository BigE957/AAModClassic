using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Tiles.Boxes;
using AAModClassic.Items.Boss.Athena.Olympian;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class AthenaAwakenedBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Olympian Athena Music Box");
            // Tooltip.SetDefault(@"Plays 'Goddess of Those Winged' by Turquoise");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AthenaAwakenedBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
			recipe.AddIngredient(ModContent.ItemType<StarChart>(), 1);
			recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
