using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Tiles.Decoration
{
    public class DaybreakBrick_Tile : ModTile
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
