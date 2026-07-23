using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodBookshelf_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpBookcase(ModContent.ItemType<RazewoodBookshelf>());
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}
