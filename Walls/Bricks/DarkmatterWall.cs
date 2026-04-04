using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Walls.Bricks
{
    public class DarkmatterWall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = ModContent.DustType<DarkmatterDust>();
            AddMapEntry(new Color(30, 30, 60));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<DarkmatterWall>());
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}