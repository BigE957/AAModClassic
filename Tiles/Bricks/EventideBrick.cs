using AAModClassic.Dusts;
using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Bricks
{
    class EventideBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(Mod.Find<ModItem>("EventideBrick").Type);   
            AddMapEntry(AAColor.Yamata);
            DustType = ModContent.DustType<AbyssDust>();
        }
    }
}
