using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip.Tiles
{
	public class RottedWall_Wall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
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