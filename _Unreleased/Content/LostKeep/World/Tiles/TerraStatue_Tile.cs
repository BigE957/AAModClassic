using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles;

public class TerraStatue_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		DustType = DustID.Terra;
		Main.tileLavaDeath[Type] = false;
        TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.Origin = new Point16(1, 4);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.None;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Lost Hero Statue");
		AddMapEntry(new Color(100, 100, 100), val);
		TileID.Sets.DisableSmartCursor[Type] = true;
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged) => false;

    public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => false;

    public override bool CanExplode(int i, int j) => false;

	public override bool RightClick(int i, int j)
	{
        Main.NewText(Language.GetTextValue("Mods.AAModClassic.Tiles.TerraStatue_Tile.FlavorText"), 200, 0, 0);
        return true;
	}
}
