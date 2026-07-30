using AAModClassic.Dusts;
using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Tiles.Decoration
{
    public class EventideAbyssiumBrick_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<EventideAbyssiumBrick>());   
            AddMapEntry(AAColor.Yamata);
            DustType = ModContent.DustType<AbyssDust>();
        }
    }
}
