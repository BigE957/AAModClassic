using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class StormCloud_Tile : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public Color color;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][TileID.Cloud] = true;
            Main.tileMergeDirt[Type] = false;
            DustType = ModContent.DustType<Dusts.FulguriteDust>();
            AddMapEntry(new Color(60, 20, 90));
			MinPick = 9999;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Tile tile = Main.tile[x, y];
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.White, Color.White, Color.Violet, Color.White, Color.Violet, Color.White);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.White, Color.White, Color.Violet, Color.White, Color.Violet, Color.White);
            Vector2 zero=  new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(x * 16 - (int)Main.screenPosition.X, y * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}