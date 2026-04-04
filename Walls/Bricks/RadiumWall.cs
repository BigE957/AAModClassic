using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class RadiumWall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = ModContent.DustType<RadiumDust>();
            AddMapEntry(new Color(60, 60, 30));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<RadiumWall>());
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}