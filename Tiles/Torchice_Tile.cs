using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Torchice_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = false;
            TileID.Sets.Conversion.Ice[Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Item50;
            DustType = ModContent.DustType<RazewoodDust>();
            RegisterItemDrop(ModContent.ItemType<Torchice>());   
            AddMapEntry(new Color(50, 35, 0));
            TileID.Sets.Ices[Type] = true;
        }
    }
}