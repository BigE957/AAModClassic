using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodPiano_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpPiano(ModContent.ItemType<RazewoodPiano>(), true);
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}
