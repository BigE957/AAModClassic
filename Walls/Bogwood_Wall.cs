using AAModClassic.Items.Blocks.BogwoodF;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class Bogwood_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.BogwoodDust>();
            AddMapEntry(new Color(25, 12, 10));
            RegisterItemDrop(ModContent.ItemType<BogwoodWall>());
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}