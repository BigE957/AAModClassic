using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class MushroomBlock : ModTile
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(Mod.Find<ModItem>("MushroomBlock").Type);
			AddMapEntry(new Color(120, 90, 0));
		}
	}
}