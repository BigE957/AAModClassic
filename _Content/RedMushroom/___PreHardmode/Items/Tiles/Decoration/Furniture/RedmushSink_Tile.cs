using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushSink_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpSink(ModContent.ItemType<RedmushSink>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}