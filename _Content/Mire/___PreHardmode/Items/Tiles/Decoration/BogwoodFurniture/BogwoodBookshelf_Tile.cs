using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodBookshelf_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpBookcase(ModContent.ItemType<BogwoodBookshelf>());
            DustType = ModContent.DustType<BogwoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}
