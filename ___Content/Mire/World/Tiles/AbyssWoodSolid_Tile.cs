using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class AbyssWoodSolid_Tile : ModTile
    {

        public bool glow = true; 
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<AbyssLeaves_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AbyssWood_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<Darkmud_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AbyssGrass_Tile>()] = true;
            HitSound = SoundID.Dig;// 21;
            DustType = ModContent.DustType<Dusts.AbyssDust>();
            AddMapEntry(new Color(52, 0, 200));
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