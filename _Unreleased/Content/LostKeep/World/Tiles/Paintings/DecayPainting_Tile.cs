using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class DecayPainting_Tile : ModTile
{
	public override string HighlightTexture => "AAModClassic/_Unreleased/Content/LostKeep/World/Tiles/Paintings/LargeKeepPainting2_Highlight";

	public override void SetStaticDefaults()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Width = 6;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.AnchorBottom = default(AnchorData);
		TileObjectData.newTile.AnchorTop = default(AnchorData);
		TileObjectData.newTile.AnchorWall = true;
		TileObjectData.addTile((int)Type);
		AddMapEntry(new Color(171, 71, 66), Language.GetText("MapObject.Painting"));
		base.DustType = DustID.WoodFurniture;
		TileID.Sets.HasOutlines[Type] = true;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != NetmodeID.Server)
		{
			BaseUtility.Chat(Language.GetTextValue($"Mods.AAModClassic.Items.Placeables.{Name.Replace("_Tile", "")}.Tooltip"), Color.DarkSlateBlue, sync: false);
		}
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
