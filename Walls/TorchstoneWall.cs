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
            //RegisterItemDrop(ModContent.ItemType<TorchstoneWall>());
            AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }
    }
}