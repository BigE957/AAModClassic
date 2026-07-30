using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard
{
    public class ZeroATrophy_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            DustType = DustID.WoodFurniture;
			TileID.Sets.DisableSmartCursor[Type] = true;
			AddMapEntry(new Color(120, 85, 60));
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            /*
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 9) frame = 0;
            }
            */
        }

        /*
        public static Color Glow(Color color)
        {
            return ColorUtils.COLOR_GLOWPULSE;
        }
        */

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            /*
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D Sphere = ModContent.Request<Texture2D>(Texture + "_Glow1").Value;
            int frameY = tile != null && tile.HasTile ? tile.TileFrameY + Main.tileFrame[Type] * 54 : 0;

            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.TileFrameX, frameY, false, false, false, null, Glow);
            */
        }
	}
}