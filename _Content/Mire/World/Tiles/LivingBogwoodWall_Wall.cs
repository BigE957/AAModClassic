using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class LivingBogwoodWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.BogwoodDust>();
			AddMapEntry(new Color(100, 0, 30));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}