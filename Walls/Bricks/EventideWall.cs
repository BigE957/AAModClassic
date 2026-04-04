using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class EventideWall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            DustType = ModContent.DustType<AbyssiumDust>();
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