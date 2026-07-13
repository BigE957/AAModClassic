using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Inferno.World.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class TorchAsh_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.Snow[Type] = true;
            TileID.Sets.Conversion.Snow[Type] = true;
            DustType = ModContent.DustType<Dusts.AshRain>();
            AddMapEntry(new Color(30, 30, 30));
            RegisterItemDrop(ModContent.ItemType<TorchAsh>());
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(500))
            {
                int style = 23; // mushroom
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
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