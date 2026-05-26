using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class TorchAsh_Tile : ModTile
    {
        public static int _type;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.Snow[Type] = true;
            DustType = ModContent.DustType<Dusts.AshRain>();
            AddMapEntry(new Color(30, 30, 30));
            RegisterItemDrop(ModContent.ItemType<Items.Blocks.TorchAsh>());
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(500))
            {
                PlaceObject(i, j - 1, ModContent.TileType<Hotshroom_Tile>());
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Hotshroom_Tile>(), 0, 0, -1, -1);
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