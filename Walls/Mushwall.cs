using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls
{
    public class Mushwall : ModWall
	{
		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
			RegisterItemDrop(Mod.Find<ModItem>("Mushroom Wall").Type);
			AddMapEntry(new Color(60, 14, 14));
            Terraria.ID.WallID.Sets.Conversion.Grass[Type] = true;
        }
    }
}