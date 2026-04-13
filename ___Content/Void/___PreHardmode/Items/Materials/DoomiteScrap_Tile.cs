using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void.___PreHardmode.Items.Materials
{
    public class DoomiteScrap_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = ModContent.DustType<Dusts.DoomDust>();
            RegisterItemDrop(ModContent.ItemType<DoomiteScrap>());
            AddMapEntry(new Color(51, 48, 61));
            MinPick = 0;
        }
    }
}