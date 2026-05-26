using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public class FungicideConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.MushroomGrass, Type, ModContent.TileType<Mycelium_Tile>());
            WallLoader.RegisterConversion(WallID.Mushroom, Type, WallID.Jungle);
            WallLoader.RegisterConversion(WallID.MushroomUnsafe, Type, WallID.JungleUnsafe);
            TileLoader.RegisterConversion(ModContent.TileType<Mycelium_Tile>(), Type, TileID.Grass);
            WallLoader.RegisterConversion(ModContent.WallType<RedMushrooomWall_Wall>(), Type, WallID.Grass);
        }
    }
}
