using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Altar
{
    class DaybringerBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("RadiumBrick").Type;   
            AddMapEntry(Color.DarkGoldenrod);
            DustType = ModContent.DustType<Dusts.RadiumDust>();
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
