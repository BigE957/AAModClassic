using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAMod.Tiles.Trees;

namespace AAModClassic.Tiles
{
    public class Doomgrass : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            AddMapEntry(new Color(50, 50, 50));
            RegisterItemDrop(ItemID.DirtBlock);
        }
    }
}