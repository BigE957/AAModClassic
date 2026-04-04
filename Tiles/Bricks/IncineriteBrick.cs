using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Bricks
{
    class IncineriteBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<IncineriteBrick>());   
            AddMapEntry(new Color(80, 60, 20));
            DustType = ModContent.DustType<Dusts.IncineriteDust>();
        }
    }
}
