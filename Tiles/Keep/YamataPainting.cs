using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Blocks.Paintings;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Keep;

public class YamataPainting : ModTile
{
	public override string HighlightTexture => "AAMod/Textures/KeepPainting_Highlight";

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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != NetmodeID.Server)
		{
			BaseUtility.Chat("'Sweet Azathoth, will this oversized lizard zip his lip?!'", AAColor.Yamata, sync: false);
		}
		return true;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
        Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 48, ModContent.ItemType<AAModClassic.Items.Blocks.Paintings.YamataPainting>(), 1, false, 0, false, false);
	}
}
