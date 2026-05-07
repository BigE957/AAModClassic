using AAModClassic.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public class VoidConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<DoomGrass_Tile>());
            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<DoomstoneB_Tile>());
        }
    }
}
