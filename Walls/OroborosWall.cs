using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Walls
{
    public class OroborosWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<DoomDust>();
            AddMapEntry(new Color(8, 8, 8));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<OroborosWall>());
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}