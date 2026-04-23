using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.World.Conversions
{
    public class TundraConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, TileID.SnowBlock);
            TileLoader.RegisterConversion(TileID.Dirt, Type, TileID.SnowBlock);
            TileLoader.RegisterConversion(TileID.Stone, Type, TileID.IceBlock);
            TileLoader.RegisterConversion(TileID.Ebonstone, Type, TileID.CorruptIce);
            TileLoader.RegisterConversion(TileID.Crimstone, Type, TileID.FleshIce);
            TileLoader.RegisterConversion(TileID.Pearlstone, Type, TileID.HallowedIce);
            TileLoader.RegisterConversion(ModContent.TileType<Torchstone_Tile>(), Type, ModContent.TileType<Torchice_Tile>());
            TileLoader.RegisterConversion(ModContent.TileType<Depthstone_Tile>(), Type, ModContent.TileType<IndigoIce_Tile>());

            WallLoader.RegisterConversion(WallID.Stone, Type, WallID.IceUnsafe);
            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, WallID.SnowWallUnsafe);
            WallLoader.RegisterConversion(WallID.DirtUnsafe, Type, WallID.SnowWallUnsafe);
            WallLoader.RegisterConversion(WallID.EbonstoneUnsafe, Type, WallID.IceUnsafe);
            WallLoader.RegisterConversion(WallID.CrimstoneUnsafe, Type, WallID.IceUnsafe);
            WallLoader.RegisterConversion(WallID.PearlstoneBrickUnsafe, Type, WallID.IceUnsafe);
        }
    }
}
