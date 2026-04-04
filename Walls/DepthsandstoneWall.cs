using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class DepthsandstoneWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<AbyssiumDust>();
			AddMapEntry(new Color(0, 10, 150));
            Terraria.ID.WallID.Sets.Conversion.Sandstone[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
        
    }
}