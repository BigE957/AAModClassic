using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class BogwoodWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = Mod.Find<ModDust>("BogwoodDust").Type;
            AddMapEntry(new Color(25, 12, 10));
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("BogwoodWall").Type;
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}