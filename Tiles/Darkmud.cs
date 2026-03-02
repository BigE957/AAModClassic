using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Darkmud : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("AbyssGrass").Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("AbyssWoodSolid").Type] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            HitSound = 21;
            DustType = Mod.Find<ModDust>("DeepAbyssiumDust").Type;
            AddMapEntry(new Color(0, 0, 100));
			MinPick = 65;
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