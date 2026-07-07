using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class ZeroPainting : ModItem, ILocalizedModType
{
    public new string LocalizationCategory => "Items.Placeables";
    public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("0");
		// ((ModItem)this).Tooltip.SetDefault("'That thing...I don't know what it is, but it just...gives me the chills.'");
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
        Item.createTile = ModContent.TileType<ZeroPainting_Tile>();
	}
}
