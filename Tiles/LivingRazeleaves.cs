using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class LivingRazeleaves : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            DustType = Mod.Find<ModDust>("RazeleafDust").Type;
            //RegisterItemDrop(Mod.Find<ModItem>("").Type);   
            AddMapEntry(new Color(127, 57, 0));
            MinPick = 0;
        }
    }
}