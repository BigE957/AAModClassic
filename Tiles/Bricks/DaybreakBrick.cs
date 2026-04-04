using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Bricks
{
    class DaybreakBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<DaybreakBrick>());   
            AddMapEntry(AAColor.Akuma);
            DustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
        }
    }
}
