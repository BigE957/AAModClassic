using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class IncineriteWall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("IncineriteWall").Type;
            AddMapEntry(new Color(40, 30, 10));
            DustType = Mod.Find<ModDust>("IncineriteDust").Type;
        }
    }
}