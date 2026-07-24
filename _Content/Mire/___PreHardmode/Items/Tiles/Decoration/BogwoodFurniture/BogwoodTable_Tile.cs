using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodTable_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            this.SetUpTable(ModContent.ItemType<BogwoodTable>());
            DustType = ModContent.DustType<Dusts.BogwoodDust>();
        }

		
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}
