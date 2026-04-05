using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class DepthsandHardened_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.HardenedSand[Type] = true;
            Main.tileLighted[Type] = false;
            DustType = ModContent.DustType<DeepAbyssiumDust>();
            RegisterItemDrop(ModContent.ItemType<DepthsandHardened>());   
            AddMapEntry(new Color(0, 0, 127));
			MinPick = 65;
        }
    }
}