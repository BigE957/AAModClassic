using AAModClassic.Items.Blocks.Terra;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Keep;

public class KeepSink : ModTile
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
		// val.SetDefault("Keep Sink");
		AddMapEntry(new Color(30, 150, 12), val);
		base.DustType = DustID.Stone;
		TileID.Sets.DisableSmartCursor[Type] = true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
