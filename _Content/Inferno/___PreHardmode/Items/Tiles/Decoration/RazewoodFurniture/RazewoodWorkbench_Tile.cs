using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodWorkbench_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			this.SetUpWorkBench(ModContent.ItemType<RazewoodWorkbench>());
            DustType = ModContent.DustType<RazewoodDust>();
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
	}
}