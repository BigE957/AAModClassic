using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class TorchsandstoneWall : ModWall
	{
		public override void SetStaticDefaults()
		{
            DustType = ModContent.DustType<IncineriteDust>();
			AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Sandstone[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}