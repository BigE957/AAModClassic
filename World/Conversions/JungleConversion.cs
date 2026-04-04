using AAModClassic.Tiles;
using AAModClassic.Walls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.World.Conversions
{
    public class JungleConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Dirt, Type, TileID.Mud);
            TileLoader.RegisterConversion(TileID.Grass, Type, TileID.JungleGrass);

            TileLoader.RegisterConversion(TileID.Plants, Type, TileID.JunglePlants);
            TileLoader.RegisterConversion(TileID.Vines, Type, TileID.JungleVines);
            TileLoader.RegisterConversion(TileID.Plants2, Type, TileID.JunglePlants2);

            WallLoader.RegisterConversion(WallID.DirtUnsafe, Type, WallID.MudUnsafe);
            WallLoader.RegisterConversion(WallID.Grass, Type, WallID.Jungle);
            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, WallID.JungleUnsafe);

            TileLoader.RegisterConversion(TileID.Stone, Type, TileID.Stone);
            WallLoader.RegisterConversion(WallID.Stone, Type, WallID.Stone);

            TileLoader.RegisterConversion(TileID.Sand, Type, TileID.Sand);
            TileLoader.RegisterConversion(TileID.HardenedSand, Type, TileID.HardenedSand);
            WallLoader.RegisterConversion(WallID.HardenedSand, Type, WallID.HardenedSand);
            TileLoader.RegisterConversion(TileID.Sandstone, Type, TileID.Sandstone);
            WallLoader.RegisterConversion(WallID.Sandstone, Type, WallID.Sandstone);
        }
    }
}
