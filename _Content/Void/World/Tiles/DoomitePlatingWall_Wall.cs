using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Content.Void.World.Tiles
{
    public class DoomitePlatingWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
            DustType = ModContent.DustType<Dusts.DoomDust>();
			AddMapEntry(new Color(50, 25, 0));
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<DoomitePlatingWall>());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}