using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomLamp_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpLamp(ModContent.ItemType<DoomLamp>());
            DustType = ModContent.DustType<DoomDust>();
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
            Color color = Color.White;
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
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
