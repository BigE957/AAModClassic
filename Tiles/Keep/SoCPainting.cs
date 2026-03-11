using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Blocks.Paintings;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Keep;

public class SoCPainting : ModTile
{
	public override string HighlightTexture => "AAMod/Textures/LargeKeepPainting_Highlight";

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
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Painting");
		AddMapEntry(new Color(171, 71, 66), val);
		base.DustType = DustID.WoodFurniture;
		TileID.Sets.HasOutlines[Type] = true;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != NetmodeID.Server)
		{
			BaseUtility.Chat("'An eldritch abomination sealed into the ship it sunk...'", Color.DarkCyan, sync: false);
		}
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
        Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 48, ModContent.ItemType<AAModClassic.Items.Blocks.Paintings.SoCPainting>(), 1, false, 0, false, false);
	}
}
