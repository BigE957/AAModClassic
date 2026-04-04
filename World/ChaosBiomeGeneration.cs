using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Ranged;
using AAModClassic.Tiles;
using AAModClassic.Tiles.Boss;
using AAModClassic.Tiles.Chests;
using AAModClassic.Tiles.Crafters;
using AAModClassic.UI.WorldGen;
using AAModClassic.Walls;
using AAModClassic.World.Convertions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace AAModClassic.World
{
    public class TexGenAssets : ModSystem
    {
        public static TexGenData LakeTileData;
        public static TexGenData LakeWallData;
        public static TexGenData LakeLiquidData;

        public static TexGenData VolcanoTileData;
        public static TexGenData VolcanoWallData;
        public static TexGenData VolcanoLiquidData;

        public static TexGenData TerrariumSmallDeletionData;
        public static TexGenData TerrariumMediumDeletionData;

        public static TexGenData TerrariumSmallTileData;
        public static TexGenData TerrariumMediumTileData;

        public static TexGenData TerrariumSmallWallData;
        public static TexGenData TerrariumMediumWallData;

        public static TexGenData HoardDeletionData;

        public static TexGenData HoardTileData;
        public static TexGenData HoardWallData;

        public static TexGenData AcropolisTileData;
        public static TexGenData AcropolisWallData;
        public static TexGenData AcropolisRoofData;

        public static TexGenData EquinoxTileData;
        public static TexGenData EquinoxSlopeData;

        public static TexGenData PitContructionTileData;
        public static TexGenData PitTileData;
        public static TexGenData PitWallData;
        public static TexGenData PitLiquidData;
        public static TexGenData PitSlopeData;

        public static TexGenData EnderCrystalTileData;
        public static TexGenData EnderCrystalWallData;
        public static TexGenData EnderCrystalSlopeData;

        public static TexGenData KeepTileData;
        public static TexGenData KeepWallData;
        public static TexGenData KeepSlopeData;
        public static TexGenData KeepPlatformData;
        public static TexGenData KeepObjectData;

        public override void OnModLoad()
        {
            LakeTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/Lake", AssetRequestMode.ImmediateLoad).Value);
            LakeWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LakeWalls", AssetRequestMode.ImmediateLoad).Value);
            LakeLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LakeWater", AssetRequestMode.ImmediateLoad).Value);

            VolcanoTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/Volcano", AssetRequestMode.ImmediateLoad).Value);
            VolcanoWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/VolcanoWalls", AssetRequestMode.ImmediateLoad).Value);
            VolcanoLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/VolcanoLava", AssetRequestMode.ImmediateLoad).Value);

            TerrariumSmallDeletionData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/TerrariumDelete", AssetRequestMode.ImmediateLoad).Value);
            TerrariumMediumDeletionData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/TerrariumMedDelete", AssetRequestMode.ImmediateLoad).Value);

            TerrariumSmallTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/Terrarium", AssetRequestMode.ImmediateLoad).Value);
            TerrariumMediumTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/TerrariumMed", AssetRequestMode.ImmediateLoad).Value);

            TerrariumSmallWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/TerrariumWalls", AssetRequestMode.ImmediateLoad).Value);
            TerrariumMediumWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/TerrariumMedWalls", AssetRequestMode.ImmediateLoad).Value);

            HoardDeletionData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/GreedNestClear", AssetRequestMode.ImmediateLoad).Value);

            HoardTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/GreedNest", AssetRequestMode.ImmediateLoad).Value);
            HoardWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/GreedNestWalls", AssetRequestMode.ImmediateLoad).Value);

            AcropolisTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/Acropolis", AssetRequestMode.ImmediateLoad).Value);
            AcropolisWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/AcropolisWalls", AssetRequestMode.ImmediateLoad).Value);
            AcropolisRoofData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/AcropolisRoof", AssetRequestMode.ImmediateLoad).Value);

            EquinoxTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/EquinoxAltar", AssetRequestMode.ImmediateLoad).Value);
            EquinoxSlopeData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/EquinoxAltarSlope", AssetRequestMode.ImmediateLoad).Value);

            PitContructionTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/PitConstruction", AssetRequestMode.ImmediateLoad).Value);
            PitTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/Pit", AssetRequestMode.ImmediateLoad).Value);
            PitWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/PitWall", AssetRequestMode.ImmediateLoad).Value);
            PitLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/PitLava", AssetRequestMode.ImmediateLoad).Value);
            PitSlopeData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/PitSlope", AssetRequestMode.ImmediateLoad).Value);

            EnderCrystalTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/EnderCrystal", AssetRequestMode.ImmediateLoad).Value);
            EnderCrystalWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/EnderCrystalWall", AssetRequestMode.ImmediateLoad).Value);
            EnderCrystalSlopeData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/EnderCrystalSlope", AssetRequestMode.ImmediateLoad).Value);

            KeepTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LostKeep", AssetRequestMode.ImmediateLoad).Value);
            KeepWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LostKeepWall", AssetRequestMode.ImmediateLoad).Value);
            KeepSlopeData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LostKeepSlope", AssetRequestMode.ImmediateLoad).Value);
            KeepPlatformData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LostKeepPlatforms", AssetRequestMode.ImmediateLoad).Value);
            KeepObjectData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/World/LostKeepObjects", AssetRequestMode.ImmediateLoad).Value);
        }
    }

    public class MireBiome : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod mod = AAMod.instance;
            //ushort tileGrass = (ushort)mod.Find<ModTile>("MireGrass").Type, tileDirt = TileID.Mud, tileStone = (ushort)mod.Find<ModTile>("Depthstone").Type, tileIce = (ushort)mod.Find<ModTile>("IndigoIce").Type,
            //tileSand = (ushort)mod.Find<ModTile>("Depthsand").Type, tileSandHardened = (ushort)mod.Find<ModTile>("DepthsandHardened").Type, tileSandstone = (ushort)mod.Find<ModTile>("Depthsandstone").Type,
            //LivingWood = (ushort)ModContent.TileType<LivingBogwood>(), LivingLeaves = (ushort)ModContent.TileType<LivingBogleaves>();

            ushort StoneWall = (ushort)ModContent.WallType<DepthstoneWall>(),
            SandstoneWall = (ushort)ModContent.WallType<DepthsandstoneWall>(),
            HardenedSandWall = (ushort)ModContent.WallType<DepthsandHardenedWall>(),
            GrassWall = (ushort)ModContent.WallType<LivingBogleafWall>(),
            JungleWall = (ushort)ModContent.WallType<MireJungleWall>();

			int worldSize = GetWorldSize();
			int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180; //how deep the biome is (scaled by world size)	

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = ModContent.TileType<Depthstone>(),
                [new Color(255, 128, 0)] = ModContent.TileType<Darkmud>(),
                [new Color(0, 255, 0)] = ModContent.TileType<AbyssGrass>(),
                [new Color(255, 0, 0)] = ModContent.TileType<AbyssWood>(),
                [new Color(128, 0, 0)] = ModContent.TileType<AbyssWoodSolid>(),
                [new Color(255, 255, 0)] = ModContent.TileType<AbyssVines>(),
                [new Color(0, 255, 255)] = ModContent.TileType<DepthMoss>(),
                [new Color(255, 0, 255)] = ModContent.TileType<AbyssLeaves>(),
                [new Color(128, 0, 0)] = ModContent.TileType<AbyssWoodSolid>(),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = ModContent.WallType<DepthstoneWall>(),
                [Color.Black] = -1 //don't touch when genning
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.LakeTileData, colorToTile, TexGenAssets.LakeWallData, colorToWall, TexGenAssets.LakeLiquidData);
			Point newOrigin = new Point(origin.X, origin.Y - 10); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //convert tiles
			{
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new ConvertTile(ModContent.GetInstance<MireConversion>().Type) //actually place the tile
			}));

            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 30;
            gen.Generate(genX, genY, true, true);


            WorldGen.PlaceObject(genX + 24, genY + 203, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 43, genY + 211, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 59, genY + 221, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 81, genY + 223, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 103, genY + 231, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 124, genY + 222, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 143, genY + 216, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 161, genY + 214, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 171, genY + 205, ModContent.TileType<HydraPod>());
            NetMessage.SendObjectPlacement(-1, genX + 25, genY + 204, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 43, genY + 211, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 59, genY + 221, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 81, genY + 223, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 103, genY + 231, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 124, genY + 222, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 143, genY + 216, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 161, genY + 214, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 171, genY + 205, ModContent.TileType<HydraPod>(), 0, 0, -1, -1);

            //WorldGen.PlaceObject(genX + 59, genY + 31, Terraria.ModLoader.ModContent.TileType<DreadAltarS>());		   

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        if (Main.rand.Next(15) == 0)
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<ChaosAltar1>());
                        }
                    }
                }
            }
            return true;
		}
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class BogwoodCon : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;
            ushort LivingWood = (ushort)ModContent.TileType<LivingBogwood>(), LivingLeaves = (ushort)ModContent.TileType<LivingBogleaves>();

            ushort BogwoodWall = (ushort)ModContent.WallType<LivingBogwoodWall>(), LeafWall = (ushort)ModContent.WallType<LivingBogleafWall>();

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;
            Point newOrigin = new Point(origin.X, origin.Y - 10);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Living Wood.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahogany, TileID.LivingWood}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingWood, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and Living Leaves.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahoganyLeaves, TileID.LeafBlock}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingLeaves, true, true)
            }));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[]
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new ushort[]{ WallID.LivingWood }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(BogwoodWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new ushort[]{ WallID.LivingLeaf }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(LeafWall, true)
            }));

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class MireDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = -2,
                [new Color(255, 128, 0)] = -2,
                [new Color(0, 255, 0)] = -2,
                [new Color(255, 0, 0)] = -2,
                [new Color(128, 0, 0)] = -2,
                [new Color(255, 255, 0)] = -2,
                [new Color(255, 0, 255)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.LakeTileData, colorToTile);
			int genX = origin.X - (gen.width / 2);
			int genY = origin.Y - 30;			
            gen.Generate(genX, genY, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class InfernoBiome : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            //--- Initial variable creation
            //ushort tileGrass = (ushort)mod.Find<ModTile>("InfernoGrass").Type, tileStone = (ushort)mod.Find<ModTile>("Torchstone").Type, tileSnow = (ushort)mod.Find<ModTile>("TorchAsh").Type,
            //tileIce = (ushort)mod.Find<ModTile>("Torchice").Type, tileSand = (ushort)mod.Find<ModTile>("Torchsand").Type, tileSandHardened = (ushort)mod.Find<ModTile>("TorchsandHardened").Type, tileSandstone = (ushort)mod.Find<ModTile>("Torchsandstone").Type,
            //LivingWood = (ushort)ModContent.TileType<LivingRazewood>(), LivingLeaves = (ushort)ModContent.TileType<LivingRazeleaves>();

            ushort StoneWall = (ushort)ModContent.WallType<TorchstoneWall>(), 
            SandstoneWall = (ushort)ModContent.WallType<TorchsandstoneWall>(),
            HardenedSandWall = (ushort)ModContent.WallType<TorchsandHardenedWall>(),
            GrassWall = (ushort)ModContent.WallType<InfernoGrassWall>();


            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<Torchstone>(),
                [new Color(0, 0, 255)] = ModContent.TileType<Torchstone>(),
                [new Color(0, 255, 0)] = ModContent.TileType<ScorchedDynastyWoodS>(),
                [new Color(255, 255, 0)] = ModContent.TileType<ScorchedShinglesS>(),
                [new Color(255, 0, 255)] = ModContent.TileType<ScorchedPlatform>(),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.WallType<TorchstoneWall>(),
                [new Color(0, 0, 255)] = ModContent.WallType<BurnedDynastyWall>(),
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.VolcanoTileData, colorToTile, TexGenAssets.VolcanoWallData, colorToWall, TexGenAssets.VolcanoLiquidData);
            Point newOrigin = new Point(origin.X, origin.Y - 30); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
				new InWorld(),				
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),			
                new Actions.SetLiquid(1, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
			{
				new InWorld(),
                new Actions.SetLiquid(0, 0)
            }));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //convert tiles
			{
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new ConvertTile(ModContent.GetInstance<InfernoConversion>().Type) //actually place the tile
			}));

            /*
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.CrimsonGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
            {
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[] { TileID.SnowBlock }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSnow, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //and stone.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone, TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileStone, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //ice...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileIce, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //sand...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sand, TileID.Ebonsand, TileID.Crimsand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSand, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //hardened sand...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSandHardened, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSandstone, true, true)
            }));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LeafBlock }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingLeaves, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and Living Wood.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahogany, TileID.LivingWood}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingWood, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new ushort[]{ WallID.Stone, WallID.EbonstoneUnsafe, WallID.CrimstoneUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(StoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new ushort[]{ WallID.Sandstone, WallID.CorruptSandstone, WallID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(SandstoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new ushort[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new ushort[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new ushort[]{ WallID.GrassUnsafe, WallID.CorruptGrassUnsafe, WallID.CrimsonGrassUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(GrassWall, true)
            }));
            */

            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 80;
            gen.Generate(genX, genY, true, true);

            //WorldGen.PlaceObject(genX + 65, genY + 4, Terraria.ModLoader.ModContent.TileType<DracoAltarS>());
            WorldGen.PlaceObject(genX + 24, genY + 307, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 33, genY + 313, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 46, genY + 314, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 57, genY + 316, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 67, genY + 316, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 78, genY + 317, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 87, genY + 315, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 96, genY + 312, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 103, genY + 307, ModContent.TileType<DragonEgg>());
            NetMessage.SendObjectPlacement(-1, genX + 24, genY + 307, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 33, genY + 313, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 46, genY + 314, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 57, genY + 316, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 67, genY + 316, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 78, genY + 317, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 87, genY + 315, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 96, genY + 312, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 103, genY + 307, (ushort)ModContent.TileType<DragonEgg>(), 0, 0, -1, -1);

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        if (Main.rand.Next(15) == 0)
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<ChaosAltar2>());
                        }
                    }
                }
            }

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class InfernoDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [new Color(0, 0, 255)] = -2,
                [new Color(0, 255, 0)] = -2,
                [new Color(255, 255, 0)] = -2,
                [new Color(255, 0, 255)] = -2,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.VolcanoTileData, colorToTile);
            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 80;		
            gen.Generate(genX, genY, true, true);						

            return true;
        }
    }

    public class SurfaceMushroom : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            ushort tileGrass = (ushort)ModContent.TileType<Mycelium>(); //change to types in your mod

            int worldSize = GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150, biomeWidthHalf = biomeWidth / 2; //how wide the biome is (scaled by world size)
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;

            //ok time to check to see if this spot is actually a good place to gen
            Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
            Point newOrigin = new Point(origin.X - biomeWidthHalf, origin.Y - 10);
            WorldUtils.Gen(newOrigin, new Shapes.Rectangle(biomeWidth, biomeHeight), new Actions.TileScanner(new ushort[]
            {
                TileID.Grass,
                TileID.Dirt,
                TileID.Stone,
                TileID.Sand,
                TileID.SnowBlock,
                TileID.IceBlock,
                TileID.BlueDungeonBrick,
                TileID.PinkDungeonBrick,
                TileID.GreenDungeonBrick
            }).Output(dictionary));

            int normalBiomeCount = dictionary[TileID.Grass] + dictionary[TileID.Dirt] + dictionary[TileID.Stone];
            int IceBlockBiomeCount = dictionary[TileID.SnowBlock] + dictionary[TileID.IceBlock];
            int sandBiomeCount = dictionary[TileID.Sand];
            int dungeonCount = dictionary[TileID.BlueDungeonBrick] + dictionary[TileID.PinkDungeonBrick] + dictionary[TileID.GreenDungeonBrick];

            if (dungeonCount > 0 || IceBlockBiomeCount >= normalBiomeCount || sandBiomeCount >= normalBiomeCount) //don't gen if you're in the Dungeon at all or if the Ice count (Snow) or the Sand count (desert) is too high
            {
                return false;
            }
            WorldUtils.Gen(newOrigin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[] //gen grass...
            {
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass }), //ensure we only replace the intended tile (in this case, grass)
                new RadialDitherTopMiddle(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10), //this provides the 'blending' on the edges (except the top)
                new SetModTile(tileGrass, true, true) //actually place the tile
            }));
            return true;
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6300) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class TerrariumDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            if (WorldTypeSystem.WorldType == AAWorldType.Beta)
                return true;
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning		
            };


            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning	

            TexGenData Terrasphere = null;

            if (Terrasphere == null)
            {
                if (worldSize == 1)
                {
                    Terrasphere = TexGenAssets.TerrariumSmallDeletionData;
                }
                else
                {
                    Terrasphere = TexGenAssets.TerrariumMediumDeletionData;
                }
            }

            TexGen gen = TexGen.GetTexGenerator(Terrasphere, colorToTile, Terrasphere, colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
				new InWorld(),				
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
				new InWorld(),				
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }
    
    public class TerrariumSphere : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = ModContent.TileType<TerraCrystal>(),
                [new Color(255, 0, 255)] = ModContent.TileType<TerraWood>(),
                [new Color(255, 255, 0)] = ModContent.TileType<TerraLeaves>(),
                [new Color(0, 0, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGenData Terrasphere = null;

            TexGenData TerraWalls = null;

            if (Terrasphere == null)
            {
                if (worldSize == 1)
                {
                    Terrasphere = TexGenAssets.TerrariumSmallTileData;

                    TerraWalls = TexGenAssets.TerrariumSmallWallData;
                }
                else
                {
                    Terrasphere = TexGenAssets.TerrariumMediumTileData;

                    TerraWalls = TexGenAssets.TerrariumMediumWallData;
                }
            }

            TexGen gen = TexGen.GetTexGenerator(Terrasphere, colorToTile, TerraWalls, colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
				new InWorld(),				
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
				new InWorld(),				
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX <= 4200) { return 1; }
            else { return 2; }
        }
    }

    public class HoardClear : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.HoardDeletionData, colorToTile);
            gen.Generate(origin.X, origin.Y, true, true);

            return true;
        }
    }

    public class Hoard : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<GreedStone>(),
                [new Color(0, 0, 255)] = ModContent.TileType<GreedBrick>(),
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.HoardTileData, colorToTile, TexGenAssets.HoardWallData, colorToWall);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldUtils.Gen(new Point(origin.X, origin.Y), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[]
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0)
            }));

            HoardChest(origin.X + 19, origin.Y + 55);
            HoardChest(origin.X + 38, origin.Y + 67, 1);
            HoardChest(origin.X + 25, origin.Y + 34);
            HoardChest(origin.X + 41, origin.Y + 27);
            HoardChest(origin.X + 53, origin.Y + 38);
            HoardChest(origin.X + 49, origin.Y + 54);
            HoardChest(origin.X + 67, origin.Y + 70, 2);
            HoardChest(origin.X + 79, origin.Y + 61);
            HoardChest(origin.X + 72, origin.Y + 41);
            HoardChest(origin.X + 95, origin.Y + 45);
            HoardChest(origin.X + 107, origin.Y + 57);
            HoardChest(origin.X + 121, origin.Y + 33);
            HoardChest(origin.X + 131, origin.Y + 48, 3);
            HoardChest(origin.X + 130, origin.Y + 69);

            WorldGen.PlaceObject(origin.X + 80, origin.Y + 88, ModContent.TileType<GreedAltar>());
            NetMessage.SendObjectPlacement(-1, origin.X + 80, origin.Y + 88, ModContent.TileType<GreedAltar>(), 0, 0, -1, -1);

            return true;
        }

        public static void HoardChest(int x, int y, int specialItem = 0)
        {
            int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<GreedChest>(), false, 1);

            int[] GreedChestLoot = new int[] {

                ItemID.GoldenChair,
                ItemID.GoldenToilet,
                ItemID.GoldenDoor,
                ItemID.GoldenTable,
                ItemID.GoldenBed,
                ItemID.GoldenPiano,
                ItemID.GoldenDresser,
                ItemID.GoldenSofa,
                ItemID.GoldenSink,
                ItemID.GoldenBathtub,
                ItemID.GoldenClock,
                ItemID.GoldenLamp,
                ItemID.GoldenBookcase,
                ItemID.GoldenChandelier,
                ItemID.GoldenLantern,
                ItemID.GoldenCandelabra,
                ItemID.GoldenCandle,
                ItemID.GoldenChest,
                ItemID.GoldenWorkbench,
                ItemID.GoldWatch,
                ItemID.GoldDust,
                ItemID.AncientGoldHelmet,
                ItemID.GoldBunny,
                ItemID.GoldButterfly,
                ItemID.GoldFrog,
                ItemID.GoldGrasshopper,
                ItemID.SquirrelGold,
                ItemID.GoldBird,
                ItemID.GoldMouse,
                ItemID.GoldWorm,
                ItemID.GoldCrown,
                ItemID.GoldenKey,
                ItemID.Goldfish,
                ItemID.ReflectiveGoldDye,
                ItemID.GoldGreaves,
                ItemID.GoldHelmet,
                ItemID.FindingGold,
                ItemID.GoldChainmail,
                ItemID.GoldShortsword,
                ItemID.GoldBroadsword,
                ItemID.GoldBow,
                ItemID.GoldHammer,
                ItemID.GoldPickaxe,
                ItemID.GoldenCrate
            };

            int[] Loot = new int[]
            {
                ItemID.CoinGun,
                ItemID.Cutlass,
                ItemID.DiscountCard,
                ItemID.GoldRing,
                ItemID.LuckyCoin,
            };

            int[] Loot2 = new int[]
            {
                ModContent.ItemType<Items.Armor.AncientGold.AncientGoldBody>(),
                ModContent.ItemType<Items.Armor.AncientGold.AncientGoldLeg>(),
            };

            if (PlacementSuccess >= 0)
            {
                Chest chest = Main.chest[PlacementSuccess];

                Item item0 = chest.item[0];
                UnifiedRandom genRand0 = WorldGen.genRand;
                int type;
                if (specialItem == 1)
                {
                    type = ModContent.ItemType<OdinsBlade>();
                }
                else if (specialItem == 2)
                {
                    type = ModContent.ItemType<Items.Melee.RomulusTazesaber>();
                }
                else if (specialItem == 3)
                {
                    type = ModContent.ItemType<Items.Misc.AnubisBook>();
                }
                else if (genRand0.Next(100) < 2f)
                {
                    type = Utils.Next(genRand0, Loot2);
                }
                else
                {
                    type = Utils.Next(genRand0, Loot);
                }

                item0.SetDefaults(type, false);

                chest.item[1].SetDefaults(ItemID.GoldBar);
                chest.item[1].stack = WorldGen.genRand.Next(70, 90);

                Item item = chest.item[2];
                item.SetDefaults(ItemID.FlaskofGold);
                chest.item[2].stack = WorldGen.genRand.Next(1, 4);

                chest.item[3].SetDefaults(ItemID.GoldCoin, false);
                chest.item[3].stack = WorldGen.genRand.Next(70, 90);

                for (int i = 0; i < 20; i++)
                {
                    chest.item[i + 4].SetDefaults(Utils.Next(WorldGen.genRand, GreedChestLoot));
                    if (chest.item[i + 4].maxStack > 1)
                    {
                        chest.item[i + 4].stack = WorldGen.genRand.Next(1, 3);
                    }
                }
            }

            NetMessage.SendObjectPlacement(-1, x, y, (ushort)ModContent.TileType<GreedChest>(), 1, 0, -1, -1);
        }
    }

    public class Acropolis : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<AcropolisBlock>(),
                [new Color(128, 128, 128)] = ModContent.TileType<AcropolisBlock2>(),
                [new Color(255, 255, 0)] = ModContent.TileType<SkyShard>(),
                [new Color(0, 255, 255)] = TileID.Grass,
                [new Color(0, 255, 0)] = TileID.Dirt,
                [new Color(0, 0, 255)] = TileID.Cloud,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.WallType<AcropolisBrickWall>(),
                [new Color(0, 255, 255)] = ModContent.WallType<AcropolisWall>(),
                [new Color(0, 255, 0)] = WallID.Dirt,
                [new Color(0, 0, 255)] = WallID.Cloud,
                [new Color(255, 255, 255)] = -2, 
                [Color.Black] = -1			
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.AcropolisTileData, colorToTile, TexGenAssets.AcropolisWallData, colorToWall, null, TexGenAssets.AcropolisRoofData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 79, origin.Y + 86, (ushort)ModContent.TileType<AcropolisAltar>());
            NetMessage.SendObjectPlacement(-1, origin.X + 79, origin.Y + 87, (ushort)ModContent.TileType<AcropolisAltar>(), 0, 0, -1, -1);

            return true;
        }
    }

    public class Equinox : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<GreedBrick>(),
                [new Color(0, 255, 255)] = ModContent.TileType<DayCrystal>(),
                [new Color(0, 255, 0)] = ModContent.TileType<NightCrystal>(),
                [new Color(255, 255, 0)] = ModContent.TileType<DaybringerBrick>(),
                [new Color(0, 0, 255)] = ModContent.TileType<NightcrawlerBrick>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.EquinoxTileData, colorToTile, null, null, null, TexGenAssets.EquinoxSlopeData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 36, origin.Y + 39, ModContent.TileType<WormAltar>());
            NetMessage.SendObjectPlacement(-1, origin.X + 36, origin.Y + 39, ModContent.TileType<WormAltar>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 30, origin.Y + 42, ModContent.TileType<StarAltar>());
            NetMessage.SendObjectPlacement(-1, origin.X + 30, origin.Y + 42, ModContent.TileType<StarAltar>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 45, origin.Y + 42, ModContent.TileType<GravAltar>());
            NetMessage.SendObjectPlacement(-1, origin.X + 80, origin.Y + 88, ModContent.TileType<GravAltar>(), 0, 0, -1, -1);

            return true;
        }
    }

    public class Pit : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = ModContent.TileType<PitStone>(),
                [new Color(0, 0, 255)] = ModContent.TileType<PitBars>(),
                [new Color(0, 255, 0)] = ModContent.TileType<PitBridge>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = ModContent.WallType<PitBarWall>(),
                [new Color(255, 0, 0)] = ModContent.WallType<PitStoneWall>(),
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(336, 145), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0),
                new Actions.SetSlope(0)
            }));

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.PitTileData, colorToTile, TexGenAssets.PitWallData, colorToWall, TexGenAssets.PitLiquidData, TexGenAssets.PitSlopeData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 281, origin.Y + 52, ModContent.TileType<Throne>());
            NetMessage.SendObjectPlacement(-1, origin.X + 281, origin.Y + 52, ModContent.TileType<Throne>(), 0, 0, -1, -1);

            return true;
        }
    }
    
    public class PitTeaser : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = ModContent.TileType<PitStone>(),
                [new Color(0, 0, 255)] = ModContent.TileType<PitBars>(),
                [new Color(0, 255, 0)] = ModContent.TileType<PitBridge>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(90, 103), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetSlope(0)
            }));

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.PitContructionTileData, colorToTile);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 35, origin.Y + 20, ModContent.TileType<Throne>());
            NetMessage.SendObjectPlacement(-1, origin.X + 30, origin.Y + 20, ModContent.TileType<Throne>(), 0, 0, -1, -1);

            return true;
        }
    }

    //Unused... For now...
    /*
    public class Parthenan : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;


            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = ModContent.TileType<FulguritePlatingS>(),
                [new Color(255, 0, 0)] = ModContent.TileType<FulguriteBrickS>(),
                [new Color(0, 0, 255)] = ModContent.TileType<StormCloud>(),
                [new Color(255, 0, 255)] = ModContent.TileType<FulgurGlassS>(),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = ModContent.WallType<FulguritePlatingWallS>(),
                [new Color(255, 0, 255)] = ModContent.TileType<FulgurGlassWall>(),
                [Color.Black] = -1 //don't touch when genning				
            };

            //TexGen gen = TexGen.GetTexGenerator(ModContent.Request<Texture2D>("AAModClassic/World/Parthenan").Value, colorToTile, ModContent.Request<Texture2D>("AAModClassic/World/ParthenanWalls").Value, colorToWall);
            
            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject(origin.X + 34, origin.Y + 47, (ushort)ModContent.TileType<DataBank>());
            WorldGen.PlaceChest(origin.X + 32, origin.Y + 47, (ushort)ModContent.TileType<StormChest>(), true);
            WorldGen.PlaceChest(origin.X + 41, origin.Y + 47, (ushort)ModContent.TileType<StormChest>(), true);
            return true;
        }
    }

    public class BOTE : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<RottedDynastyWoodS>(),
                [new Color(0, 255, 0)] = ModContent.TileType<RottedPlatform>(),
                //colorToTile[new Color(0, 0, 255)] = TileID.Rope;
                [new Color(0, 255, 255)] = ModContent.TileType<CthulhuPortal>(),
                [new Color(255, 255, 0)] = TileID.Sand,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.WallType<RottedFence>(),
                [new Color(255, 255, 0)] = ModContent.WallType<RottedWall>(),
                [new Color(255, 255, 255)] = ModContent.WallType<RottedWall>(),
                [new Color(0, 255, 255)] = ModContent.WallType<RottedWall>(),
                [new Color(255, 0, 255)] = ModContent.WallType<RottedWall>(),
                [new Color(0, 255, 0)] = ModContent.WallType<RottedWall>(),
                [new Color(0, 0, 255)] = WallID.Sail,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGen gen = TexGen.GetTexGenerator(ModContent.Request<Texture2D>("AAModClassic/World/Ship").Value, colorToTile, ModContent.Request<Texture2D>("AAModClassic/World/ShipWalls").Value, colorToWall, ModContent.Request<Texture2D>("AAModClassic/World/ShipWater").Value);

			int newOriginX = origin.X - (gen.width / 2);
			int newOriginY = origin.Y - (gen.height / 2) + 10;
            gen.Generate(newOriginX, newOriginY, true, true);
            
            //WorldGen.PlaceChest(newOriginX + 130, newOriginY + 102, (ushort)mod.TileType("SunkenChest"), true);
            return true;
        }
    }
    */

    public class Crystal : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = TileID.CrystalBlock,
                [new Color(0, 0, 255)] = TileID.GraniteBlock,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = WallID.Crystal,
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets.EnderCrystalTileData, colorToTile, TexGenAssets.EnderCrystalWallData, colorToWall, null, TexGenAssets.EnderCrystalSlopeData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 27, origin.Y + 26, (ushort)ModContent.TileType<EnderMemory>());
            NetMessage.SendObjectPlacement(-1, origin.X + 27, origin.Y + 26, (ushort)ModContent.TileType<EnderMemory>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 16, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier>());
            NetMessage.SendObjectPlacement(-1, origin.X + 16, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 41, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier>());
            NetMessage.SendObjectPlacement(-1, origin.X + 41, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier>(), 0, 0, -1, -1);

            return true;
        }
    }

    public class RadialDitherTopMiddle2 : GenAction
	{
        private readonly int _width;
        private readonly float _innerRadius, _outerRadius;

		public RadialDitherTopMiddle2(int width, float innerRadius, float outerRadius)
		{
			_width = width;
			_innerRadius = innerRadius;
			_outerRadius = outerRadius;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Vector2 value = new Vector2((float)origin.X + (_width / 2), origin.Y);
			Vector2 value2 = new Vector2(x, y);
			float num = Vector2.Distance(value2, value);
			float num2 = Math.Max(0f, Math.Min(1f, (num - _innerRadius) / (_outerRadius - _innerRadius)));
			if (_random.NextDouble() > num2)
			{
				return UnitApply(origin, x, y, args);
			}
			return Fail();
		}
	}	

	public class InWorld : GenAction
	{
		public InWorld()
		{
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if(x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
				return Fail();
			return UnitApply(origin, x, y, args);
		}
	}	

    public class ConvertTile : GenAction
    {
        int conversionType = -1;
        public ConvertTile(int type)
        {
            conversionType = type;
        }

        public override bool Apply(Point origin, int x, int y, params object[] args)
        {
            if(conversionType == -1)
                return Fail();

            if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                return Fail();

            WorldGen.Convert(x, y, conversionType, 1, true, true);

            return UnitApply(origin, x, y, args);
        }
    }
}