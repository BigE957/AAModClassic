using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class PitBars_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = false;   
            AddMapEntry(new Color(99, 54, 6));
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}