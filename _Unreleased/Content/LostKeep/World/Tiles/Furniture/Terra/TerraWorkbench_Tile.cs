using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraWorkbench_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpWorkBench(ModContent.ItemType<TerraWorkbench>());
        DustType = DustID.Terra;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
