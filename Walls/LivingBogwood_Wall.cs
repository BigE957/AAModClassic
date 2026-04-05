using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class LivingBogwood_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<BogwoodDust>();
			AddMapEntry(new Color(100, 0, 30));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}