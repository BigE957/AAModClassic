using AAModClassic.___Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Tiles
{
    public class LivingBogwood_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            DustType = ModContent.DustType<Dusts.BogwoodDust>();
            RegisterItemDrop(ModContent.ItemType<Bogwood>());   
            AddMapEntry(new Color(20, 0, 127));
            MinPick = 0;
        }
    }
}