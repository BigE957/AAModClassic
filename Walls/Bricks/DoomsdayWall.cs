using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class DoomsdayWall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = Mod.Find<ModDust>("DoomDust").Type;
			AddMapEntry(new Color(30, 30, 30));
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DoomsdayWall").Type;
            Main.wallHouse[Type] = true;
            Main.wallLargeFrames[Type] = 2;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            BaseDrawing.DrawWallTexture(spriteBatch, Mod.GetTexture("Glowmasks/DoomsdayWall_Glow"), i, j, false, AAGlobalTile.GetZeroColorDim);
        }
    }
}