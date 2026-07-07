using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraSink_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Terra Sink");
		AddMapEntry(new Color(65, 205, 12), val);
		base.DustType = DustID.Terra;
		TileID.Sets.DisableSmartCursor[Type] = true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
