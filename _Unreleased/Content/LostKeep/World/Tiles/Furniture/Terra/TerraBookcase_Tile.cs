using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraBookcase_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpBookcase(ModContent.ItemType<TerraBookcase>());
        DustType = DustID.Terra;
    }

	public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
