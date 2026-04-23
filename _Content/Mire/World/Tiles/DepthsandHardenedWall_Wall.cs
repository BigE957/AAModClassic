using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class DepthsandHardenedWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.AbyssiumDust>();
			AddMapEntry(new Color(0, 10, 150));
            Terraria.ID.WallID.Sets.Conversion.HardenedSand[Type] = true;

        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
        
    }
}