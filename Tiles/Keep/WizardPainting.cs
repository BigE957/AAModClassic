using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Blocks.Paintings;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Keep;

public class WizardPainting : ModTile
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
			BaseUtility.Chat("'I don't care what you've become. I still have hope for you.'", Color.Magenta, sync: false);
		}
		return true;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
        Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 48, ModContent.ItemType<AAModClassic.Items.Blocks.Paintings.WizardPainting>(), 1, false, 0, false, false);
	}
}
