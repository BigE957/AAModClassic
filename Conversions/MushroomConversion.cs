using AAModClassic.Tiles;
using AAModClassic.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public class MushroomConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<Mycelium_Tile>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<RedMushrooomWall_Wall>());
        }
    }
}
