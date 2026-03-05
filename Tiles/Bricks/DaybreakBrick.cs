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
            RegisterItemDrop(Mod.Find<ModItem>("DaybreakBrick").Type);   
            AddMapEntry(AAColor.Akuma);
            DustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
        }
    }
}
