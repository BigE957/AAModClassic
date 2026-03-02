using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchice : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = false;
            TileID.Sets.Conversion.Ice[Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = 21;
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("Torchice").Type;   
            AddMapEntry(new Color(50, 35, 0));
            TileID.Sets.Ices[Type] = true;
        }
    }
}