using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraCandle_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpCandle(ModContent.ItemType<TerraCandle>(), true);
        DustType = DustID.Terra;
	}

    public override bool RightClick(int i, int j)
    {
        FurnitureUtils.RightClickBreak(i, j);
        return true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void HitWire(int i, int j) => FurnitureUtils.LightHitWire(Type, i, j, 1, 1);

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<TerraCandle>();
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

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		ulong seed = Main.TileFrameSeed ^ ((ulong)j | (ulong)i);
		Color val = new(100, 100, 100, 0);
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		int num = 20;
		int num2 = -2;
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
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + num4) - ((float)num - 16f) / 2f + num5, (float)(j * 16 - (int)Main.screenPosition.Y + num2) + num6) + zero, (Rectangle?)new Rectangle(frameX, frameY, num, num3), val, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
	}
}
