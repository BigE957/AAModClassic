using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class FulgurusPainting : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Storm's Siege");
		// ((ModItem)this).Tooltip.SetDefault("'I didn't paint this one. It was a gift from the fulgarians...they seem alright.'");
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
        Item.createTile = ModContent.TileType<FulgurusPainting_Tile>();
	}
}
