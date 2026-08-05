using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Conversions;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Content.Mire.World.Biomes
{
    public class MireTexGenAssets : ModSystem
    {
        public static TexGenData LakeTileData;
        public static TexGenData LakeWallData;
        public static TexGenData LakeLiquidData;

        public override void OnModLoad()
        {
            LakeTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Mire/World/Biomes/RisingMoonLake");
            LakeWallData = TexGen.GetTextureForGen("AAModClassic/_Content/Mire/World/Biomes/RisingMoonLake_Walls");
            LakeLiquidData = TexGen.GetTextureForGen("AAModClassic/_Content/Mire/World/Biomes/RisingMoonLake_Liquid");
        }
    }

    public class MireGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180; //how deep the biome is (scaled by world size)	

            Dictionary<Color, int> colorToTile = new()
            {
                [new Color(0, 0, 255)] = ModContent.TileType<Depthstone_Tile>(),
                [new Color(255, 128, 0)] = ModContent.TileType<Darkmud_Tile>(),
                [new Color(0, 255, 255)] = ModContent.TileType<DepthMoss_Tile>(),
                [new Color(0, 255, 0)] = ModContent.TileType<AbyssGrass_Tile>(),
                [new Color(255, 0, 0)] = ModContent.TileType<AbyssWood_Tile>(),
                [new Color(128, 0, 0)] = ModContent.TileType<AbyssWoodSolid_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<AbyssVines_Tile>(),
                [new Color(255, 0, 255)] = ModContent.TileType<AbyssLeaves_Tile>(),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            Dictionary<Color, int> colorToWall = new()
            {
                [new Color(0, 0, 255)] = ModContent.WallType<DepthstoneWall_Wall>(),
                [Color.Black] = -1 //don't touch when genning
            };

            TexGen gen = TexGen.GetTexGenerator(MireTexGenAssets.LakeTileData, colorToTile, MireTexGenAssets.LakeWallData, colorToWall, MireTexGenAssets.LakeLiquidData);
            Point newOrigin = new(origin.X, origin.Y - 10); //biomeRadius);

            //convert tiles
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new ConvertTile(ModContent.GetInstance<MireConversion>().Type) //actually place the tile
			]));

            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 30;
            gen.Generate(genX, genY, true, true);


            WorldGen.PlaceObject(genX + 24, genY + 203, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 43, genY + 211, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 59, genY + 221, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 81, genY + 223, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 103, genY + 231, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 124, genY + 222, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 143, genY + 216, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 161, genY + 214, ModContent.TileType<HydraPod_Tile>());
            WorldGen.PlaceObject(genX + 171, genY + 205, ModContent.TileType<HydraPod_Tile>());
            NetMessage.SendObjectPlacement(-1, genX + 25, genY + 204, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 43, genY + 211, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 59, genY + 221, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 81, genY + 223, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 103, genY + 231, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 124, genY + 222, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 143, genY + 216, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 161, genY + 214, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 171, genY + 205, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);

            //WorldGen.PlaceObject(genX + 59, genY + 31, Terraria.ModLoader.ModContent.TileType<DreadAltarS_Tile>());		   

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                        if (Main.rand.NextBool(15))
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<AbyssAltarUnsafe_Tile>());
            }
            return true;
        }
    }

    public class BogwoodCon : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ushort LivingWood = (ushort)ModContent.TileType<LivingBogwood_Tile>(), LivingLeaves = (ushort)ModContent.TileType<LivingBogleaf_Tile>();

            ushort BogwoodWall = (ushort)ModContent.WallType<LivingBogwoodWall_Wall>(), LeafWall = (ushort)ModContent.WallType<LivingBogleafWall_Wall>();

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

            TexGen gen = TexGen.GetTexGenerator(MireTexGenAssets.LakeTileData, colorToTile);
            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 30;
            gen.Generate(genX, genY, true, true);

            return true;
        }
    }
}
