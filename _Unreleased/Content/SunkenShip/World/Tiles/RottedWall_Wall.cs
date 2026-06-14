using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Tiles
{
	public class RottedWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = false;
			AddMapEntry(new Color(31, 26, 0));
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