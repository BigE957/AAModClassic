using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodTable_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            this.SetUpTable(ModContent.ItemType<RazewoodTable>());
            DustType = ModContent.DustType<Dusts.RazewoodDust>();
        }

		
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}
