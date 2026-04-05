using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class Depthstone_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<AbyssiumDust>();
            AddMapEntry(new Color(17, 9, 40));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}