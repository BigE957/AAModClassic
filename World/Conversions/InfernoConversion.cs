using AAModClassic.Tiles;
using AAModClassic.Walls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.WallLoader;

namespace AAModClassic.World.Conversions
{
    public class InfernoConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<InfernoGrass>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<InfernoGrassWall>());
            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, ModContent.WallType<InfernoGrassWall>());
            
            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<Torchstone>());
            WallLoader.RegisterConversion(WallID.Stone, Type, ModContent.WallType<TorchstoneWall>());
            
            TileLoader.RegisterConversion(TileID.Sand, Type, ModContent.TileType<Torchsand>());
            TileLoader.RegisterConversion(TileID.HardenedSand, Type, ModContent.TileType<TorchsandHardened>());
            WallLoader.RegisterConversion(WallID.HardenedSand, Type, ModContent.WallType<TorchsandHardenedWall>());
            TileLoader.RegisterConversion(TileID.Sandstone, Type, ModContent.TileType<Torchsandstone>());
            WallLoader.RegisterConversion(WallID.Sandstone, Type, ModContent.WallType<TorchsandstoneWall>());
            
            TileLoader.RegisterConversion(TileID.SnowBlock, Type, ModContent.TileType<TorchAsh>());
            TileLoader.RegisterConversion(TileID.IceBlock, Type, ModContent.TileType<Torchice>());
            
            TileLoader.RegisterConversion(TileID.LivingWood, Type, ModContent.TileType<LivingRazewood>());
            TileLoader.RegisterConversion(TileID.LivingMahogany, Type, ModContent.TileType<LivingRazewood>());
            
            TileLoader.RegisterConversion(TileID.LeafBlock, Type, ModContent.TileType<LivingRazeleaves>());
            TileLoader.RegisterConversion(TileID.LivingMahoganyLeaves, Type, ModContent.TileType<LivingRazeleaves>());
        }
    }
}
