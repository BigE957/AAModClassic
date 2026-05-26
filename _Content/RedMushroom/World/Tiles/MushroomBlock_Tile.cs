using AAModClassic.Items.Blocks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.World.Tiles
{
    public class MushroomBlock_Tile : ModTile
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<MushroomBlock>());
			AddMapEntry(new Color(120, 90, 0));
		}
	}
}