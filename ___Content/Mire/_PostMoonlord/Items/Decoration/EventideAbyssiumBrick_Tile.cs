using AAModClassic.Dusts;
using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items.Building
{
    class EventideBrick_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<EventideBrick>());   
            AddMapEntry(AAColor.Yamata);
            DustType = ModContent.DustType<AbyssDust>();
        }
    }
}
