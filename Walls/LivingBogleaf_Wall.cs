using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class LivingBogleaf_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<BogleafDust>();
			AddMapEntry(new Color(100, 0, 150));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}