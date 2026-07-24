using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushTub_Tile : ModTile 
    {
        public override void SetStaticDefaults()
        {
            this.SetUpBathtub(ModContent.ItemType<RedmushTub>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}