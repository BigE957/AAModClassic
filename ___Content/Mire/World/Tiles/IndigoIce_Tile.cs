using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Tiles
{
    public class IndigoIce_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            HitSound = SoundID.Item50;
            DustType = ModContent.DustType<Dusts.DeepAbyssiumDust>();
            RegisterItemDrop(ModContent.ItemType<IndigoIce>());   
            AddMapEntry(new Color(0, 60, 127));
            TileID.Sets.Conversion.Ice[Type] = true;
            TileID.Sets.Ices[Type] = true;
        }
    }
}