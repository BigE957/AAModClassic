using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class DepthstoneWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = Mod.Find<ModDust>("AbyssiumDust").Type;
            AddMapEntry(new Color(17, 9, 40));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}