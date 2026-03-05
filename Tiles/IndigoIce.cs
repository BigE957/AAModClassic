using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class IndigoIce : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            HitSound = SoundID.Item50;
            DustType = Mod.Find<ModDust>("DeepAbyssiumDust").Type;
            RegisterItemDrop(Mod.Find<ModItem>("IndigoIce").Type);   
            AddMapEntry(new Color(0, 60, 127));
            TileID.Sets.Conversion.Ice[Type] = true;
            TileID.Sets.Ices[Type] = true;
        }
    }
}