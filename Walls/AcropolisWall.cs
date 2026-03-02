using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class AcropolisWall : ModWall
	{
		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
            DustType = DustID.Marble;
			AddMapEntry(new Color(0, 0, 25));
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