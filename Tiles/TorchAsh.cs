using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles.Trees;

namespace AAMod.Tiles
{
    public class TorchAsh : ModTile
    {
        public static int _type;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new RazewoodTree())/* tModPorter Note: Removed. Assign GrowsOnTileId to this tile type in ModTree.SetStaticDefaults instead */;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.Snow[Type] = true;
            DustType = ModContent.DustType<Dusts.AshRain>();
            AddMapEntry(new Color(30, 30, 30));
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Items.Blocks.TorchAsh>();
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(500) == 0)
            {
                PlaceObject(i, j - 1, Mod.Find<ModTile>("Hotshroom").Type);
                NetMessage.SendObjectPlacement(-1, i, j - 1, Mod.Find<ModTile>("Hotshroom").Type, 0, 0, -1, -1);
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return Mod.Find<ModTile>("RazewoodTree").Type;
        }
    }
}