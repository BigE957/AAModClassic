using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Tiles
{
    public class MireGrassWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.AbyssiumDust>();
			AddMapEntry(new Color(0, 0, 120));
            Terraria.ID.WallID.Sets.Conversion.Grass[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}