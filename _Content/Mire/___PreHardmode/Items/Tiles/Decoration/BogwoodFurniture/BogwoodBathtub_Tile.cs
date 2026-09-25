using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodBathtub_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpBathtub(ModContent.ItemType<BogwoodBathtub>());
            DustType = ModContent.DustType<BogwoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}