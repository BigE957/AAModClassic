using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class RottedDynastyWood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            
            RegisterItemDrop(ModContent.ItemType<RottedDynastyWood>());   
            AddMapEntry(new Color(39, 34, 8));
            MinPick = 0;
        }
    }
}