using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items.Tiles.Decoration
{
    public class EventideBrickWall_Wall : ModWall
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