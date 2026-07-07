using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class ShenPainting : ModItem, ILocalizedModType
{
    public new string LocalizationCategory => "Items.Placeables";
    public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Discordian Doomsayer");
		// ((ModItem)this).Tooltip.SetDefault("'I sealed him away, but the thing about containing chaos forever...is that you can't.'");
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
        Item.createTile = ModContent.TileType<ShenPainting_Tile>();
	}
}
