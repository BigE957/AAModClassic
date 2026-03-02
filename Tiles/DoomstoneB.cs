using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Tiles.Trees;

namespace AAMod.Tiles
{
    public class DoomstoneB : ModTile
    {


        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("Apocalyptite").Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            SetModTree(new OroborosTree())/* tModPorter Note: Removed. Assign GrowsOnTileId to this tile type in ModTree.SetStaticDefaults instead */;
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DoomstoneB").Type;   
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            AddMapEntry(new Color(40, 20, 20));
			MinPick = 60;
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return Mod.Find<ModTile>("OroborosSapling").Type;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}