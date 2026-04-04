using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Bricks
{
    class RadiumBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<RadiumBrick>());   
            AddMapEntry(Color.DarkGoldenrod);
            DustType = ModContent.DustType<Dusts.RadiumDust>();
        }
    }
}
