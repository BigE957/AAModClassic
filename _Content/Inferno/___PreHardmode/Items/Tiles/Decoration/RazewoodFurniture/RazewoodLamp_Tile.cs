using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodLamp_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpLamp(ModContent.ItemType<RazewoodLamp>());
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void HitWire(int i, int j)
        {
            FurnitureCommon.LightHitWire(Type, i, j, 1, 3);
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX < 18)
            {
                r = 0.9f;
                g = 0.9f;
                b = 0.9f;
            }
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            ulong randSeed = Main.TileFrameSeed ^ (ulong)(j<< 32 | (long)(ulong)i);
            Color color = new Color(100, 100, 100, 0);
            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;
            int width = 20;
            int offsetY = 2;
            int height = 20;
            int offsetX = 2;
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (int k = 0; k < 7; k++)
            {
                float x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
                float y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + offsetY + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
