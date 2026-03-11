using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Keep;

public class KeepPlatform : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileSolidTop[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.Platforms[Type] = true;
		TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 27;
		TileObjectData.newTile.StyleWrapLimit = 27;
		TileObjectData.newTile.UsesCustomCanPlace = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.addTile((int)Type);
		AddMapEntry(new Color(100, 142, 111), (LocalizedText)null);
		DustType = DustID.Stone;
		TileID.Sets.DisableSmartCursor[Type] = true;
		AdjTiles = new int[1] { 19 };
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
