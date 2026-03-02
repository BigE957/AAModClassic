using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class Mushwall : ModWall
	{
		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
			ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("Mushroom Wall").Type;
			AddMapEntry(new Color(60, 14, 14));
            Terraria.ID.WallID.Sets.Conversion.Grass[Type] = true;
        }
    }
}