using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class LivingRazewood_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            DustType = ModContent.DustType<RazewoodDust>();
            RegisterItemDrop(ModContent.ItemType<Razewood>());   
            AddMapEntry(new Color(40, 40, 40));
            MinPick = 0;
        }
    }
}