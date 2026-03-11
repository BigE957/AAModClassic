using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Boss;

public class Core : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = false;
		TileObjectData.newTile.Width = 10;
		TileObjectData.newTile.Height = 8;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.AnchorWall = true;
		TileObjectData.newTile.AnchorBottom = default(AnchorData);
		TileObjectData.newTile.CoordinateHeights = new int[8] { 16, 16, 16, 16, 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.None;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Biomite Cell");
		AddMapEntry(new Color(20, 60, 20), val);
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.DustType = DustID.Terra;
	}
}
