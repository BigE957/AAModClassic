using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.___Content.Void.___PreHardmode.Items.Tiles.Decoration.OroborosWoodFurniture
{
    public class OroborosWoodWall_Wall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = ModContent.DustType<Dusts.DoomDust>();
            AddMapEntry(new Color(8, 8, 8));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<OroborosWoodWall>());
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}