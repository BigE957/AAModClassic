using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepCouch_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook((Func<int, int, int, int, int, int, int>)Chest.FindEmptyChest, -1, 0, true);
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Keep Couch");
		AddMapEntry(new Color(30, 150, 12), val);
		base.DustType = DustID.Stone;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
