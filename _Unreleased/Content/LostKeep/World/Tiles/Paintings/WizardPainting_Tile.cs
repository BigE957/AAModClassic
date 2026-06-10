using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;

public class WizardPainting_Tile : ModTile
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
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Painting");
		AddMapEntry(new Color(171, 71, 66), val);
		base.DustType = DustID.WoodFurniture;
		TileID.Sets.DisableSmartCursor[Type] = true;
	}

	public override bool RightClick(int i, int j)
	{
		if (Main.netMode != NetmodeID.Server)
		{
			BaseUtility.Chat("'I don't care what you've become. I still have hope for you.'", Color.Magenta, sync: false);
		}
		return true;
	}
}
