using AAModClassic.Items.Blocks.RazewoodF;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class Razewood_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.RazewoodDust>();
            AddMapEntry(new Color(25, 12, 10));
            RegisterItemDrop(ModContent.ItemType<RazewoodWall>());
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}