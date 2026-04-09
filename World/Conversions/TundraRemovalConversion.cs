using AAModClassic.___Content.Mire.World.Tiles;
using AAModClassic.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.World.Conversions
{
    public class TundraRemovalConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.SnowBlock, Type, ConvertSnow);
            TileLoader.RegisterConversion(TileID.IceBlock, Type, TileID.Stone);
            TileLoader.RegisterConversion(TileID.CorruptIce, Type, TileID.Ebonstone);
            TileLoader.RegisterConversion(TileID.FleshIce, Type, TileID.Crimstone);
            TileLoader.RegisterConversion(TileID.HallowedIce, Type, TileID.Pearlstone);
            TileLoader.RegisterConversion(ModContent.TileType<Torchice_Tile>(), Type, ModContent.TileType<Torchstone_Tile>());
            TileLoader.RegisterConversion(ModContent.TileType<IndigoIce_Tile>(), Type, ModContent.TileType<Depthstone_Tile>());

            WallLoader.RegisterConversion(WallID.IceUnsafe, Type, WallID.Stone);
            WallLoader.RegisterConversion(WallID.SnowWallUnsafe, Type, WallID.GrassUnsafe);
        }

        private static bool ConvertSnow(int k, int l, int type, int conversionType)
        {
            int newType;

            if ((WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1].TileType == TileID.Trees) || (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1].TileType == TileID.Trees) ||
                                    (WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1] == null) ||
                                    (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null))
            {
                newType = TileID.Grass;
            }
            else
            {
                newType = TileID.Dirt;
            }

            WorldGen.ConvertTile(k, l, newType);

            return true;
        }
    }
}
