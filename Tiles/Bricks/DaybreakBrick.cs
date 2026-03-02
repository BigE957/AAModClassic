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
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DaybreakBrick").Type;   
            AddMapEntry(AAColor.Akuma);
            DustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
        }
    }
}
