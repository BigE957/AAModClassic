using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Tiles
{
	public class RottedFence : ModWall
	{
		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = false;
			//drop = mod.ItemType("Rotted Fence");
			AddMapEntry(new Color(39, 34, 8));
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