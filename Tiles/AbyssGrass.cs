using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AbyssGrass : ModTile
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
			Main.tileBlendAll[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = Mod.Find<ModDust>("YamataADust").Type;
			AddMapEntry(new Color(100, 0, 30));
		}

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.51f;
            g = 0;
            b = 0f;
        }
	}
}