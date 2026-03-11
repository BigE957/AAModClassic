using AAModClassic.Items.Blocks.Terra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Terra;

public class TerraLantern : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
		TileObjectData.newSubTile.LavaDeath = false;
		TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Terra Latern");
		AddMapEntry(new Color(65, 205, 12), val);
		base.DustType = DustID.Terra;
		base.AdjTiles = new int[1] { 42 };
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
	}

	public override void HitWire(int i, int j)
	{
		int num = i - Main.tile[i, j].TileFrameX / 18 % 1;
		int num2 = j - Main.tile[i, j].TileFrameY / 18 % 2;
		for (int k = num; k < num + 1; k++)
		{
			for (int l = num2; l < num2 + 2; l++)
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
			r = 0.9f;
			g = 0.9f;
			b = 0.9f;
		}
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
        Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 48, 32, ModContent.ItemType<AAModClassic.Items.Blocks.Terra.TerraLantern>(), 1, false, 0, false, false);
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
			Main.spriteBatch.Draw(Mod.GetTexture("Tiles/Furniture/Terra/TerraLantern_Flame"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X + num4) - ((float)num - 16f) / 2f + num5, (float)(j * 16 - (int)Main.screenPosition.Y + num2) + num6) + zero, (Rectangle?)new Rectangle(frameX, frameY, num, num3), val, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
	}
}
