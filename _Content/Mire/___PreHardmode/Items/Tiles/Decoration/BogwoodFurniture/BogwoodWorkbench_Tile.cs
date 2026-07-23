using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodWorkbench_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpWorkBench(ModContent.ItemType<BogwoodWorkbench>());
            DustType = ModContent.DustType<BogwoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}