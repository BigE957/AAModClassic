using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomClock_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Example Clock"); // Automatic from .lang files
            AddMapEntry(new Color(200, 0, 0), name);
            DustType = ModContent.DustType<Dusts.DoomDust>();
            AdjTiles = new int[] { TileID.GrandfatherClocks };
        }

        public override bool RightClick(int x, int y)
        {
            if (!AAWorld.downedZero)
            {
                Main.NewText(@"The clock is counting down to something...you aren't sure what though.
The number at the moment is so high you don't even know what the number is called.", 200, 0, 0);
            }
            else
            {
                Main.NewText(@"The Clock reads one digit: 0", 200, 0, 0);
            }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.SceneMetrics.HasClock = true;
            }
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
            int height = 16;
            Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Tiles/Furniture/Doom/DoomClock").Value;
            Texture2D Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            if (AAWorld.downedZero)
            {
                tex = ModContent.Request<Texture2D>("AAModClassic/Tiles/Furniture/Doom/DoomClock0").Value;
                Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            }
            Main.spriteBatch.Draw(tex, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Glow, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}