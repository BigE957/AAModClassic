using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class TorchstoneWall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("TorchstoneWall").Type;
            AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }
    }
}