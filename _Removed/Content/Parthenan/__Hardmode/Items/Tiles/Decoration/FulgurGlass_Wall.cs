using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
	public class FulgurGlass_Wall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
			AddMapEntry(new Color(40, 0, 50));
            
		}
    }
}