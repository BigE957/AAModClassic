using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraPiano_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpPiano(ModContent.ItemType<TerraPiano>(), true);
        DustType = DustID.Terra;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
