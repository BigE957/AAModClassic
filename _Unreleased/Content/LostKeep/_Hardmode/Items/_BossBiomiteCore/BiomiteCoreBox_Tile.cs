using AAModClassic.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.Items._BossBiomiteCore
{
    public class BiomiteCoreBox_Tile : ModTile
    {
        private static Asset<Texture2D> glowTex;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = DustID.BlueCrystalShard;
            AddMapEntry(new Color(200, 200, 200), Language.GetText("ItemName." + ItemID.Search.GetName(ItemID.MusicBox)));
            RegisterItemDrop(ModContent.ItemType<BiomiteCoreBox>());

            if (!Main.dedServ)
                glowTex = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[x, y];
            if (tile.TileFrameX < 36)
                return;

            r = Main.DiscoR / 255f * 0.15f; 
            g = Main.DiscoG / 255f * 0.15f; 
            b = Main.DiscoB / 255f * 0.15f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile.TileFrameX < 36)
                return;

            int frameY = (int)(Main.GlobalTimeWrappedHourly * 15f) % 6;
            Rectangle frame = new(tile.TileFrameX - 36, tile.TileFrameY + (frameY * 36), 16, 16);
            Vector2 drawOffset = Main.drawToScreen ? default : new Vector2(Main.offScreenRange);
            Vector2 drawPos = BaseDrawing.GetTileDrawPosition(x, y, 16, 16, drawOffset);
            sb.Draw(glowTex.Value, drawPos, frame, Main.DiscoColor);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<BiomiteCoreBox>();
        }
    }
}
