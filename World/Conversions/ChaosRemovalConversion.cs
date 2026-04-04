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

namespace AAModClassic.World.Conversions
{
    public class ChaosRemovalConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            //Tiles
            TileLoader.RegisterConversion(ModContent.TileType<InfernoGrass>(), Type, TileID.Grass);
            //TileLoader.RegisterConversion(ModContent.TileType<DoomGrass>(), Type, TileID.Grass); //Was a part of order solution, but seems unintentional, especially with the lack of doomstone

            TileLoader.RegisterConversion(ModContent.TileType<Torchstone>(), Type, TileID.Stone);
            TileLoader.RegisterConversion(ModContent.TileType<Depthstone>(), Type, TileID.Stone);

            TileLoader.RegisterConversion(ModContent.TileType<MireGrass>(), Type, TileID.JungleGrass);

            TileLoader.RegisterConversion(ModContent.TileType<TorchAsh>(), Type, TileID.SnowBlock);

            TileLoader.RegisterConversion(ModContent.TileType<Torchsand>(), Type, TileID.Sand);
            TileLoader.RegisterConversion(ModContent.TileType<Depthsand>(), Type, TileID.Sand);

            TileLoader.RegisterConversion(ModContent.TileType<TorchsandHardened>(), Type, TileID.HardenedSand);
            TileLoader.RegisterConversion(ModContent.TileType<DepthsandHardened>(), Type, TileID.HardenedSand);

            TileLoader.RegisterConversion(ModContent.TileType<Torchsandstone>(), Type, TileID.Sandstone);
            TileLoader.RegisterConversion(ModContent.TileType<Depthsandstone>(), Type, TileID.Sandstone);

            TileLoader.RegisterConversion(ModContent.TileType<Torchice>(), Type, TileID.IceBlock);
            TileLoader.RegisterConversion(ModContent.TileType<IndigoIce>(), Type, TileID.IceBlock);

            //Walls
            WallLoader.RegisterConversion(ModContent.WallType<TorchstoneWall>(), Type, WallID.Stone);
            WallLoader.RegisterConversion(ModContent.WallType<DepthstoneWall>(), Type, WallID.Stone);

            WallLoader.RegisterConversion(ModContent.WallType<InfernoGrassWall>(), Type, WallID.GrassUnsafe);
            WallLoader.RegisterConversion(ModContent.WallType<MireJungleWall>(), Type, WallID.JungleUnsafe);

            WallLoader.RegisterConversion(ModContent.WallType<TorchsandHardenedWall>(), Type, WallID.HardenedSand);
            WallLoader.RegisterConversion(ModContent.WallType<DepthsandHardenedWall>(), Type, WallID.HardenedSand);

            WallLoader.RegisterConversion(ModContent.WallType<TorchsandstoneWall>(), Type, WallID.Sandstone);
            WallLoader.RegisterConversion(ModContent.WallType<DepthsandstoneWall>(), Type, WallID.Sandstone);

            WallLoader.RegisterConversion(ModContent.WallType<LivingBogwoodWall>(), Type, WallID.LivingWood);
        }
    }
}
