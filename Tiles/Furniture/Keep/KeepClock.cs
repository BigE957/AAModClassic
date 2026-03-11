using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Blocks.Keep;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Keep;

public class KeepClock : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		AddMapEntry(new Color(30, 150, 12), val);
		base.DustType = DustID.Stone;
		base.AdjTiles = new int[1] { 104 };
	}

	public override bool RightClick(int i, int j)
	{
		string text = "AM";
		double num = Main.time;
		if (!Main.dayTime)
		{
			num += 54000.0;
		}
		num = num / 86400.0 * 24.0;
		num = num - 7.5 - 12.0;
		if (num < 0.0)
		{
			num += 24.0;
		}
		if (num >= 12.0)
		{
			text = "PM";
		}
		int num2 = (int)num;
		double num3 = (int)((num - (double)num2) * 60.0);
		string text2 = string.Concat(num3);
		if (num3 < 10.0)
		{
			text2 = "0" + text2;
		}
		if (num2 > 12)
		{
			num2 -= 12;
		}
		if (num2 == 0)
		{
			num2 = 12;
		}
		string s = string.Concat(Language.GetTextValue("CLI.Time_Command") + ": ", num2, ":", text2, " ", text);
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			BaseUtility.Chat(s, byte.MaxValue, 240, 20);
		}
		return true;
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			Main.SceneMetrics.HasClock = true;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
        Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 48, 32, ModContent.ItemType<AAModClassic.Items.Blocks.Keep.KeepClock>(), 1, false, 0, false, false);
	}
}
