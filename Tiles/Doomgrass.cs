using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAMod.Tiles.Trees;

namespace AAMod.Tiles
{
    public class Doomgrass : ModTile
    {


        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new OroborosTree())/* tModPorter Note: Removed. Assign GrowsOnTileId to this tile type in ModTree.SetStaticDefaults instead */;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            AddMapEntry(new Color(50, 50, 50));
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ItemID.DirtBlock;
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return Mod.Find<ModTile>("OroborosSapling").Type;
        }
    }
}