using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepPiano_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpPiano(ModContent.ItemType<KeepPiano>(), true);
        DustType = DustID.Stone;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
