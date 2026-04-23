using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class AbyssWood_Tile : ModTile
    {

        public bool glow = true; 
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][ModContent.TileType<AbyssLeaves_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AbyssWoodSolid_Tile>()] = true;
            HitSound = SoundID.Dig;//21;
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