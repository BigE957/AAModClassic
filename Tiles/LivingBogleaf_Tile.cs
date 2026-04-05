using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class LivingBogleaf_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            DustType = ModContent.DustType<BogleafDust>();
            //RegisterItemDrop(Mod.Find<ModItem>("").Type);   
            AddMapEntry(new Color(70, 0, 127));
			MinPick = 0;
        }
    }
}