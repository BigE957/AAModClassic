using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Items.Walls;
using AAModClassic.Dusts;

namespace AAModClassic.Walls.Bricks
{
    public class DoomstoneBrick_Wall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = ModContent.DustType<DoomDust>();
			AddMapEntry(new Color(10, 10, 10));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<DoomstoneBrickWall>());
            Main.wallHouse[Type] = true;
            Main.wallLargeFrames[Type] = 2;
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
            BaseDrawing.DrawWallTexture(spriteBatch, Mod.GetTexture("Glowmasks/DoomstoneBrickWall_Glow"), i, j, false, AAGlobalTile.GetBlankColorDim);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}