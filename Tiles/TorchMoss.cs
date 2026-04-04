using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Tiles.Trees;
using AAModClassic.Items.Blocks;

namespace AAModClassic.Tiles
{
    public class TorchMoss : ModTile
    {
        public static int _type;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = ModContent.DustType<RazeleafDust>();
            AddMapEntry(new Color(255, 153, 51));
            RegisterItemDrop(ModContent.ItemType<AAModClassic.Items.Blocks.Torchstone>());
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(500) == 0)
            {
                PlaceObject(i, j - 1, ModContent.TileType<Hotshroom>());
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Hotshroom>(), 0, 0, -1, -1);

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
    }
}