using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Tiles
{
    public class Depthstone_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.AbyssiumDust>();
            AddMapEntry(new Color(17, 9, 40));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}