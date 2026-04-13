using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.___PreHardmode.Items.Tiles.Decoration.Bogwood
{
    class Bogwood_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<Bogwood>());   
            AddMapEntry(new Color(0, 0, 51));
            DustType = ModContent.DustType<Dusts.BogwoodDust>();
        }
    }
}
