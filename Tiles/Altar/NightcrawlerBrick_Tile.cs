using AAModClassic;
using AAModClassic.Dusts;
using AAModClassic.Items.Blocks.Bricks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Altar
{
    class NightcrawlerBrick_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<DarkmatterBrick>());   
            AddMapEntry(new Color(30, 30, 51));
            DustType = ModContent.DustType<Dusts.DarkmatterDust>();
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (AAWorld.downedEquinox)
            {
                return true;
            }
            return false;
        }
    }
}
