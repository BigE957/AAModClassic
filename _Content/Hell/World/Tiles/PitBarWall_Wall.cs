using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.World.Tiles
{
    public class PitBarWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
            DustType = DustID.Torch;
			AddMapEntry(new Color(50, 34, 0));
            Main.tileBlockLight[Type] = false;
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