using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.World.Conversions
{
    public class JungleRemovalConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Mud, Type, TileID.Dirt);
            TileLoader.RegisterConversion(TileID.JungleGrass, Type, TileID.Grass);

            TileLoader.RegisterConversion(TileID.JunglePlants, Type, TileID.Plants);
            TileLoader.RegisterConversion(TileID.JungleVines, Type, TileID.Vines);
            TileLoader.RegisterConversion(TileID.JunglePlants2, Type, TileID.Plants2);

            WallLoader.RegisterConversion(WallID.MudUnsafe, Type, WallID.DirtUnsafe);
            WallLoader.RegisterConversion(WallID.Jungle, Type, WallID.Grass);
            WallLoader.RegisterConversion(WallID.JungleUnsafe, Type, WallID.GrassUnsafe);
        }
    }
}
