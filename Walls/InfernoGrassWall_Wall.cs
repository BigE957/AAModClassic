using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class InfernoGrassWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<RazeleafDust>();
			AddMapEntry(new Color(200, 150, 0));
            Terraria.ID.WallID.Sets.Conversion.Grass[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}