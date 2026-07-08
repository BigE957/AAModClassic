using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class FulgurGlass_Tile : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            //true for block to emit light
            HitSound = SoundID.Tink; 
            DustType = ModContent.DustType<FulguriteDust>();
            AddMapEntry(new Color(90, 20, 120));
			MinPick = 200;
        }
    }
}