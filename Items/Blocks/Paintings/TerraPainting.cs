using AAModClassic.Tiles.Keep;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Paintings;

public class TerraPainting : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("The Hero");
		// ((ModItem)this).Tooltip.SetDefault("'They hail me as a Hero, yet I feel like I haven't done enough.'");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 9999;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.rare = ItemRarityID.Blue;
        Item.createTile = ModContent.TileType<AAModClassic.Tiles.Keep.TerraPainting_Tile>();
	}
}
