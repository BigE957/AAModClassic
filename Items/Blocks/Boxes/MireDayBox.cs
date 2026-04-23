using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class MireDayBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mire Day Music Box");
            // Tooltip.SetDefault(@"Plays 'Clouded in Mystery' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<MireDayBox_Tile>();
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
            recipe.AddIngredient(ModContent.ItemType<Bogwood>(), 20);
			recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
