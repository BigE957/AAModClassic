using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class LivingBogleafWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = Mod.Find<ModDust>("BogleafDust").Type;
			AddMapEntry(new Color(100, 0, 150));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}