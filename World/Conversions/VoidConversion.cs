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
    public class VoidConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<DoomGrass>());
            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<DoomstoneB>());
        }
    }
}
