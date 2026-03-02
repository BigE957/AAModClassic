
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Trophy
{
    public class ZeroATrophy : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            DustType = 7;
			disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
			AddMapEntry(new Color(120, 85, 60));
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 9) frame = 0;
            }
        }

        public Color Glow(Color color)
        {
            return ColorUtils.COLOR_GLOWPULSE;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = Mod.GetTexture("Glowmasks/ZeroATrophy_Glow");
            Texture2D Sphere = Mod.GetTexture("Glowmasks/ZeroATrophy_Glow1");
            int frameY = tile != null && tile.HasTile ? tile.TileFrameY + (Main.tileFrame[Type] * 54) : 0;

            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.TileFrameX, frameY, false, false, false, null, Glow);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
            Item.NewItem(i * 16, j * 16, 48, 48, Mod.Find<ModItem>("ZeroATrophy").Type);
        }
	}
}