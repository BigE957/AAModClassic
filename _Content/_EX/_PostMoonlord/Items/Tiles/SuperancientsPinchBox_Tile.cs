using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Tiles
{
    public class SuperancientsPinchBox_Tile : ModTile
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
            TileID.Sets.DisableSmartCursor[Type]/* tModPorter Note: _Unreleased. Use TileID.Sets.DisableSmartCursor instead */ = true;
            DustType = DustID.BlueCrystalShard;
            AddMapEntry(new Color(200, 200, 200), Language.GetText("ItemName." + ItemID.Search.GetName(ItemID.MusicBox)));
            RegisterItemDrop(ModContent.ItemType<SuperancientsPinchBox>());

            if (!Main.dedServ)
                glowTex = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            r = Main.DiscoR / 255f; 
            g = Main.DiscoG / 255f; 
            b = Main.DiscoB / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile.TileFrameX < 36)
                return;

            int frameY = (int)(Main.GlobalTimeWrappedHourly * 12f) % 7;
            Rectangle frame = new(tile.TileFrameX - 36, tile.TileFrameY + (frameY * 36), 16, 16);
            Vector2 drawOffset = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
            Vector2 drawPos = BaseDrawing.GetTileDrawPosition(x, y, 16, 16, drawOffset);
            sb.Draw(glowTex.Value, drawPos, frame, Main.DiscoColor);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<SuperancientsPinchBox>();
        }
    }
}
