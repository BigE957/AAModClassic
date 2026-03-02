using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class OroborosWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = Mod.Find<ModDust>("DoomDust").Type;
            AddMapEntry(new Color(8, 8, 8));
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("OroborosWall").Type;
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}