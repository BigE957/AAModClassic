using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class TerraPainting_Tile : ModTile
{
	public override string HighlightTexture => "AAModClassic/_Unreleased/Content/LostKeep/World/Tiles/Paintings/KeepPainting_Highlight";

	public override void SetStaticDefaults()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = false;
		TileID.Sets.HasOutlines[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.addTile((int)Type);
        AddMapEntry(new Color(171, 71, 66), Language.GetText("MapObject.Painting"));
        base.DustType = DustID.WoodFurniture;
		TileID.Sets.DisableSmartCursor[Type] = true;
	}

	public override bool RightClick(int i, int j)
	{
		if (Main.netMode != NetmodeID.Server)
		{
			BaseUtility.Chat(Language.GetTextValue($"Mods.AAModClassic.Items.Placeables.{Name.Replace("_Tile", "")}.Tooltip"), Color.LimeGreen, sync: false);
		}
		return true;
	}
}
