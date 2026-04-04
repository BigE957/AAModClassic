using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.Tiles.Ancient.Walls
{
	public class AncientFulgurGlassWallS : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
			//TODOSIEGE
            //ItemDrop/* tModPorter Note: _Unreleased. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Fulgurite Glass Wall>();
			AddMapEntry(new Color(40, 0, 50));
            
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