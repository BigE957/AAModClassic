using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unofficial.Bunny.Items
{
    public class RabbitTombstone_Tile : ModTile
    {
        private static int RandomStyleRange => 1;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.TileInteractRead[Type] = true;
            Main.tileSign[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;

            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.DrawYOffset = 2;
            if (RandomStyleRange > 1)
                TileObjectData.newTile.RandomStyleRange = RandomStyleRange;

            TileObjectData.newTile.StyleHorizontal = true;

            TileObjectData.addTile(Type);

            DustType = DustID.Stone;
            AddMapEntry(Color.Gray, Language.GetText("ItemName.Tombstone"));
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Sign.KillSign(i, j);
    }
}
