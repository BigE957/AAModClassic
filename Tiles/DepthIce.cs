using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class DepthIce : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.SnowBlock][Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = 21;
            DustType = Mod.Find<ModDust>("DeepAbyssiumDust").Type;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DepthIce").Type;   
            AddMapEntry(new Color(0, 60, 127));
            TileID.Sets.Conversion.Ice[Type] = true;
            TileID.Sets.Ices[Type] = true;
        }
    }
}