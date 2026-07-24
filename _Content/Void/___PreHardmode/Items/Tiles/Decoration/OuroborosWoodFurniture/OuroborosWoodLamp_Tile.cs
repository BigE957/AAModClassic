using AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture
{
    public class OuroborosWoodLamp_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpLamp(ModContent.ItemType<OuroborosWoodLamp>());
            DustType = ModContent.DustType<DoomDust>();
        }

        public override void HitWire(int i, int j)
        {
            FurnitureCommon.LightHitWire(Type, i, j, 1, 3);
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0f;
            b = 0f;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Color color = new Color(255, 50, 50, 0);
            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;
            int width = 20;
            int offsetY = -2;
            int height = 20;
            int offsetX = 2;
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (int k = 0; k < 7; k++)
            {
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}