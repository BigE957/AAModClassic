using AAModClassic.Dusts;
using AAModClassic.Items.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class EventideBrick_Wall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
			AddMapEntry(new Color(33, 37, 96));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<EventideWall>());
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}