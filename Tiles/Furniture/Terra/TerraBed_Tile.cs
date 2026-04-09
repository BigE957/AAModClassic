using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Terra;

public class TerraBed_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.HasOutlines[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Terra Bed");
		AddMapEntry(new Color(65, 205, 12), val);
		base.DustType = DustID.Terra;
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.AdjTiles = new int[1] { 79 };
		TileID.Sets.CanBeSleptIn[Type] = true;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 1;
	}

	public override bool RightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		int num = i - tile.TileFrameX / 18;
		int num2 = j + 2;
		num += ((tile.TileFrameX >= 72) ? 5 : 2);
		if (tile.TileFrameY % 38 != 0)
		{
			num2--;
		}
		localPlayer.FindSpawn();
		if (localPlayer.SpawnX == num && localPlayer.SpawnY == num2)
		{
			localPlayer.RemoveSpawn();
			Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, (byte)240, (byte)20);
		}
		else if (Player.CheckSpawn(num, num2))
		{
			localPlayer.ChangeSpawn(num, num2);
			Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, (byte)240, (byte)20);
		}
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<AAModClassic.Items.Blocks.Terra.TerraBed>();
	}
}
