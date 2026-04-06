using AAModClassic.Items.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class CovetiteBrick_Wall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = DustID.Gold;
            AddMapEntry(new Color(60, 60, 0));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<CovetiteBrickWall>());
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}