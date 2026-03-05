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
            DustType = Mod.Find<ModDust>("DarkmatterDust").Type;
            AddMapEntry(new Color(30, 30, 60));
            HitSound = SoundID.Tink;
            RegisterItemDrop(Mod.Find<ModItem>("DarkmatterWall").Type);
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}