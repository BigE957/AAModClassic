using AAModClassic.Tiles;
using AAModClassic.Walls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.World.Convertions
{
    public class MireConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<MireGrass>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<MireJungleWall>());
            
            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe1, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe2, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe3, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe4, Type, ModContent.WallType<MireJungleWall>());
            
            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<Depthstone>());
            WallLoader.RegisterConversion(WallID.Stone, Type, ModContent.WallType<DepthstoneWall>());
            
            TileLoader.RegisterConversion(TileID.Sand, Type, ModContent.TileType<Depthsand>());
            TileLoader.RegisterConversion(TileID.HardenedSand, Type, ModContent.TileType<DepthsandHardened>());
            WallLoader.RegisterConversion(WallID.HardenedSand, Type, ModContent.WallType<DepthsandHardenedWall>());
            TileLoader.RegisterConversion(TileID.Sandstone, Type, ModContent.TileType<Depthsandstone>());
            WallLoader.RegisterConversion(WallID.Sandstone, Type, ModContent.WallType<DepthsandstoneWall>());
            
            TileLoader.RegisterConversion(TileID.IceBlock, Type, ModContent.TileType<IndigoIce>());
            
            TileLoader.RegisterConversion(TileID.LivingWood, Type, ModContent.TileType<LivingBogwood>());
            WallLoader.RegisterConversion(WallID.LivingWood, Type, ModContent.WallType<LivingBogwoodWall>());
            WallLoader.RegisterConversion(WallID.LivingWoodUnsafe, Type, ModContent.WallType<LivingBogwoodWall>());
            TileLoader.RegisterConversion(TileID.LeafBlock, Type, ModContent.TileType<LivingBogleaves>());
            WallLoader.RegisterConversion(WallID.LivingLeaf, Type, ModContent.WallType<LivingBogleafWall>());

            TileLoader.RegisterConversion(TileID.LivingMahogany, Type, ModContent.TileType<LivingBogwood>());
            TileLoader.RegisterConversion(TileID.LivingMahoganyLeaves, Type, ModContent.TileType<LivingBogleaves>());
        }
    }
}
