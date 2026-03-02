using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class DoomiteWall : ModWall
	{
		public override void SetStaticDefaults()
		{
            DustType = Mod.Find<ModDust>("DoomsdayDust").Type;
			AddMapEntry(new Color(50, 25, 0));
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DoomiteWall").Type;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}