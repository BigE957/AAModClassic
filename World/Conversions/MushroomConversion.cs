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
    public class MushroomConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<Mycelium_Tile>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<Mushwall>());
        }
    }
}
