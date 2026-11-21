using AAModClassic.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Tiles
{
    public class EquinoxAltarBox_Tile : ModTile
    {
        private static Asset<Texture2D> glowTex;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = DustID.BlueCrystalShard;
            AddMapEntry(new Color(200, 200, 200), Language.GetText("ItemName." + ItemID.Search.GetName(ItemID.MusicBox)));
            RegisterItemDrop(ModContent.ItemType<EquinoxAltarBox>());

            if (!Main.dedServ)
                glowTex = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile.TileFrameX < 36)
                return;

            int frameY = (int)(Main.GlobalTimeWrappedHourly * 5f) % 4;
            Rectangle frame = new(tile.TileFrameX - 36, tile.TileFrameY + (frameY * 36), 16, 16);
            Vector2 drawOffset = Main.drawToScreen ? default : new Vector2(Main.offScreenRange);
            Vector2 drawPos = BaseDrawing.GetTileDrawPosition(x, y, 16, 16, drawOffset);
            sb.Draw(glowTex.Value, drawPos, frame, Color.White);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<EquinoxAltarBox>();
        }
    }
}