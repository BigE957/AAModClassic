using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
	public class FulguritePlating_Wall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
			//TODOSIEGE
            //ItemDrop/* tModPorter Note: _Unreleased. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Fulgurite Plating Wall>();
			AddMapEntry(new Color(40, 0, 50));
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
            Main.spriteBatch.Draw(Mod.GetTexture("_Removed/Content/Parthenan/Tiles/Ancient/Walls/AncientFulguritePlatingS_Wall_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}