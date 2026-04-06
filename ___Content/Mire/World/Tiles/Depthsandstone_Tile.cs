using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Tiles
{
    public class Depthsandstone_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Sandstone[Type] = true;
            Main.tileLighted[Type] = false;
            DustType = ModContent.DustType<Dusts.DeepAbyssiumDust>();
            RegisterItemDrop(ModContent.ItemType<Depthsandstone>());   
            AddMapEntry(new Color(0, 20, 127));
			MinPick = 65;
        }
    }
}