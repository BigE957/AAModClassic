using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class NKPainting : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("The Lost Knight");
		// ((ModItem)this).Tooltip.SetDefault("'Legends say this ruthless dungeon guard was once like us before she died...'");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.rare = ItemRarityID.Blue;
        Item.createTile = ModContent.TileType<NKPainting_Tile>();
	}
}
