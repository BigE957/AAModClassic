using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepWorkbench_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpWorkBench(ModContent.ItemType<KeepWorkbench>());
        DustType = DustID.Stone;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
