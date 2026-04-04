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
            HitSound = SoundID.Item50;
            DustType = ModContent.DustType<DeepAbyssiumDust>();
            //RegisterItemDrop(ModContent.ItemType<DepthIce>());   
            AddMapEntry(new Color(0, 60, 127));
            TileID.Sets.Conversion.Ice[Type] = true;
            TileID.Sets.Ices[Type] = true;
        }
    }
}