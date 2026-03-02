using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class PitStoneWall : ModWall
	{
		public override void SetStaticDefaults()
		{
            DustType = DustID.Torch;
			AddMapEntry(new Color(10, 10, 10));
		}

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

        public override void KillWall(int i, int j, ref bool fail)
        {
            fail = true;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}