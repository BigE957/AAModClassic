using AAModClassic.Items.Blocks.Keep;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Keep;

public class KeepLamp : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileWaterDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(0, 2);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Keep Lamp");
		AddMapEntry(new Color(30, 150, 12), val);
		base.DustType = DustID.Stone;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
	}

	public override void HitWire(int i, int j)
	{
		int num = i - Main.tile[i, j].TileFrameX / 18 % 1;
		int num2 = j - Main.tile[i, j].TileFrameY / 18 % 3;
		for (int k = num; k < num + 1; k++)
		{
			for (int l = num2; l < num2 + 3; l++)
			{
				if (Main.tile[k, l].TileFrameX >= 18)
				{
					Main.tile[k, l].TileFrameX -= 18;
				}
				else
				{
					Main.tile[k, l].TileFrameX += 18;
				}
			}
		}
		if (Wiring.running)
		{
			Wiring.SkipWire(num, num2);
			Wiring.SkipWire(num, num2 + 1);
			Wiring.SkipWire(num, num2 + 2);
		}
		NetMessage.SendTileSquare(-1, num, num2 + 1, 2);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
		}
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
        Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 48, 32, ModContent.ItemType<AAModClassic.Items.Blocks.Keep.KeepLamp>(), 1, false, 0, false, false);
		Chest.DestroyChest(i, j);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		ulong seed = Main.TileFrameSeed ^ ((ulong)j | (ulong)i);
		Color val = new(100, 100, 100, 0);
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		int num = 20;
		int num2 = 2;
		int num3 = 20;
		int num4 = 2;
		Vector2 zero = new((float)Main.offScreenRange, (float)Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}
		for (int k = 0; k < 7; k++)
		{
			float num5 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.15f;
			float num6 = (float)Utils.RandomInt(ref seed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(Mod.GetTexture("Tiles/Furniture/Terra/TerraLamp_Flame"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X + num4) - ((float)num - 16f) / 2f + num5, (float)(j * 16 - (int)Main.screenPosition.Y + num2) + num6) + zero, (Rectangle?)new Rectangle(frameX, frameY, num, num3), val, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
	}
}
