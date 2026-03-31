using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Tiles.Fulgurite.Parthenan.Ancient
{
    public class AncientFulgurGlassS : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            //true for block to emit light
            HitSound = SoundID.Tink; 
            //TODOSIEGE
            //ItemDrop/* tModPorter Note: _Unreleased. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("FulguritePlating").Type;   //put your CustomBlock name
            DustType = Mod.Find<ModDust>("FulguriteDust").Type;
            AddMapEntry(new Color(90, 20, 120));
			MinPick = 200;
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