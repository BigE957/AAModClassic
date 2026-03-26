using AAModClassic.Items.Blocks.Keep;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Keep;

public class KeepBath : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Keep Bathtub");
		AddMapEntry(new Color(30, 150, 12), val);
		base.DustType = DustID.Stone;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 1;
	}
}
