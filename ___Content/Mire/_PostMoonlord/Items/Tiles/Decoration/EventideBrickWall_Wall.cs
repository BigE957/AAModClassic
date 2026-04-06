using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Decoration
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