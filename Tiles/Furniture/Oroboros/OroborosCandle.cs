using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Oroboros
{
    public class OroborosCandle : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            Main.tileLighted[Type] = true; 
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
	        AddMapEntry(new Color(70, 0, 10));
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0f;
            b = 0f;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if(Main.tile[i, j].TileFrameX == 0 && Main.tile[i, j].TileFrameY == 0)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, Mod.Find<ModItem>("OroborosCandle").Type);
            }
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
                Main.spriteBatch.Draw(Mod.GetTexture("Tiles/Furniture/Oroboros/OroborosCandle_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}