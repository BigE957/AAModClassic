using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Decoration
{
    public class RadiumBrickWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(60, 60, 30));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<RadiumBrickWall>());
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}