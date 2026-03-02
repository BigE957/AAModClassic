using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class AcropolisBlock2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<AcropolisBlock>()] = true;
            Main.tileBlockLight[Type] = true;   
            AddMapEntry(new Color(28, 64, 87));
            DustType = DustID.BlueCrystalShard;
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