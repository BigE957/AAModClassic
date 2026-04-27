using AAModClassic.Tiles.Decoration;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class GreedLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Lantern");
		}


		public override void SetDefaults()
		{
            Item.width = 64;
			Item.height = 34;
            Item.value = 150;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
			Item.createTile = ModContent.TileType<GreedLantern_Tile>();
		}
	}
}