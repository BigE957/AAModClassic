using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AbyssWood : ModTile
    {

        public bool glow = true; 
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][Mod.Find<ModTile>("AbyssLeaves").Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("AbyssWoodSolid").Type] = true;
            HitSound = 21;
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