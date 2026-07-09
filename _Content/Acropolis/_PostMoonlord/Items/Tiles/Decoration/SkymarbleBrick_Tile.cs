using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items.Tiles.Decoration
{
    public class SkymarbleBrick_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<SkycrystalBrick_Tile>()] = true;
            RegisterItemDrop(ModContent.ItemType<SkymarbleBrick>());   
            AddMapEntry(new Color(130, 130, 150));
            DustType = DustID.Gold;
        }
    }
}
