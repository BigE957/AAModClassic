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
            TileLoader.RegisterConversion(ModContent.TileType<InfernoGrass_Tile>(), Type, TileID.Grass);
            //TileLoader.RegisterConversion(ModContent.TileType<DoomGrass_Tile>(), Type, TileID.Grass); //Was a part of order solution, but seems unintentional, especially with the lack of doomstone

            TileLoader.RegisterConversion(ModContent.TileType<Torchstone_Tile>(), Type, TileID.Stone);
            TileLoader.RegisterConversion(ModContent.TileType<Depthstone_Tile>(), Type, TileID.Stone);

            TileLoader.RegisterConversion(ModContent.TileType<MireGrass_Tile>(), Type, TileID.JungleGrass);

            TileLoader.RegisterConversion(ModContent.TileType<TorchAsh_Tile>(), Type, TileID.SnowBlock);

            TileLoader.RegisterConversion(ModContent.TileType<Torchsand_Tile>(), Type, TileID.Sand);
            TileLoader.RegisterConversion(ModContent.TileType<Depthsand_Tile>(), Type, TileID.Sand);

            TileLoader.RegisterConversion(ModContent.TileType<TorchsandHardened_Tile>(), Type, TileID.HardenedSand);
            TileLoader.RegisterConversion(ModContent.TileType<DepthsandHardened_Tile>(), Type, TileID.HardenedSand);

            TileLoader.RegisterConversion(ModContent.TileType<Torchsandstone_Tile>(), Type, TileID.Sandstone);
            TileLoader.RegisterConversion(ModContent.TileType<Depthsandstone_Tile>(), Type, TileID.Sandstone);

            TileLoader.RegisterConversion(ModContent.TileType<Torchice_Tile>(), Type, TileID.IceBlock);
            TileLoader.RegisterConversion(ModContent.TileType<IndigoIce_Tile>(), Type, TileID.IceBlock);

            //Walls
            WallLoader.RegisterConversion(ModContent.WallType<Torchstone_Wall>(), Type, WallID.Stone);
            WallLoader.RegisterConversion(ModContent.WallType<Depthstone_Wall>(), Type, WallID.Stone);

            WallLoader.RegisterConversion(ModContent.WallType<InfernoGrassWall_Wall>(), Type, WallID.GrassUnsafe);
            //TODO: Fake wall
            //WallLoader.RegisterConversion(ModContent.WallType<MireJungle_Wall>(), Type, WallID.JungleUnsafe);

            WallLoader.RegisterConversion(ModContent.WallType<TorchsandHardened_Wall>(), Type, WallID.HardenedSand);
            WallLoader.RegisterConversion(ModContent.WallType<DepthsandHardened_Wall>(), Type, WallID.HardenedSand);

            WallLoader.RegisterConversion(ModContent.WallType<Torchsandstone_Wall>(), Type, WallID.Sandstone);
            WallLoader.RegisterConversion(ModContent.WallType<Depthsandstone_Wall>(), Type, WallID.Sandstone);

            WallLoader.RegisterConversion(ModContent.WallType<LivingBogwood_Wall>(), Type, WallID.LivingWood);
        }
    }
}
