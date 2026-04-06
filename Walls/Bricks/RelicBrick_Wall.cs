using AAModClassic.Items.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class RelicBrick_Wall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = DustID.Ice;
			AddMapEntry(new Color(30, 30, 60));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<RelicWall>());
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}