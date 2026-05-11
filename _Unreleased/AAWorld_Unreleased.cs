using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Removed;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration;
using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip.Tiles;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.CrossMod;
using AAModClassic.Tiles;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased
{
    public class AAWorld_Unreleased : ModSystem
    {
        private Vector2 shipPos = new Vector2(0, 0);
        private int shipSide = 0;

        public static bool downedSoC;
        public static bool downedIZ;
        public static bool Compass;

        public static int StormTiles = 0;
        public static int ShipTiles = 0;

        #region stupid bullshit
        public override void PreWorldGen()
        {
            downedSoC = false;
            downedIZ = false;
            Compass = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downedUnreleased = new List<string>();
            if (downedSoC) downedUnreleased.Add("SoC");
            if (downedIZ) downedUnreleased.Add("IZ");
            if (Compass) downedUnreleased.Add("Compass");

            tag.Add("downedUnreleased", downedUnreleased);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downedUnreleased = tag.GetList<string>("downedUnreleased");
            downedSoC = downedUnreleased.Contains("SoC");
            downedIZ = downedUnreleased.Contains("IZ");
            Compass = downedUnreleased.Contains("Compass");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedSoC;
            flags[1] = downedIZ;
            //flags[2] = downedIZ;
            //flags[3] = downedIZ;
            //flags[4] = downedIZ;
            //flags[5] = downedIZ;
            //flags[6] = downedIZ;
            //flags[7] = downedIZ;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedSoC = flags[0];
            downedIZ = flags[1];
            //downedIZ = flags[2];
            //downedIZ = flags[3];
            //downedIZ = flags[4];
            //downedIZ = flags[5];
            //downedIZ = flags[6];
            //downedIZ = flags[7];
        }
        #endregion

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
            {
                int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
                int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
                int chaosBiomeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));

                tasks.Insert(shiniesIndex2, new PassLegacy("Parthenan", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    ParthenanIsland(progress);
                }));

                if(!ContentReplacementSystem.NeedToReplaceContent)
                    tasks.Insert(shiniesIndex2, new PassLegacy("Mush", delegate (GenerationProgress progress, GameConfiguration config)
                    {
                        Mush_Refactored(progress);
                    }));

                tasks.Insert(shiniesIndex2, new PassLegacy("Ship", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    Ship(progress);
                }));
            }
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            StormTiles = tileCounts[ModContent.TileType<StormCloud_Tile>()] + tileCounts[ModContent.TileType<FulguritePlating_Tile>()] + tileCounts[ModContent.TileType<FulguriteBrick_Tile>()] + tileCounts[ModContent.TileType<FulgurGlass_Tile>()];
            ShipTiles = tileCounts[ModContent.TileType<RottedDynastyWoodS_Tile>()] + tileCounts[ModContent.TileType<RottedPlatform_Tile>()];
        }

        private static void Mush_Refactored(GenerationProgress progress)
        {
            progress.Message = "Growing Shrooms";

            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            int center = Main.maxTilesX / 2;

            int worldSize = WorldGenUtils.GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;
            int biomeWidthHalf = biomeWidth / 2;
            int biomeHeightHalf = biomeHeight / 2;

            int attempts = 5000;
            while (attempts > 0)
            {
                Point origin = new Point();
                Point biomeCenter = new Point();

                bool isInBounds = false;
                bool isInCenter = false;
                while (attempts > 0 && (!isInBounds || isInCenter || origin == new Point() || biomeCenter == new Point()))
                {
                    origin = new Point(WorldGen.genRand.Next(0, x), (int)GenVars.worldSurfaceLow);
                    origin.Y = WorldGenUtils.GetFirstTileFloor(origin.X, origin.Y, true);

                    // do some stuff to make it so the old origin is the position
                    biomeCenter = new Point(origin.X, origin.Y);
                    origin.X -= biomeHeightHalf;
                    origin.Y -= biomeHeightHalf;

                    if (origin.X > (biomeWidth * 1.25) && origin.X < x - (biomeWidth * 1.25))
                        isInBounds = true;
                    else
                        isInBounds = false;

                    if (origin.X < center - (biomeWidth * 2) && origin.X > center + (biomeWidth * 2))
                        isInCenter = true;
                    else
                        isInCenter = false;

                    attempts--;
                }

                Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
                WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), new Actions.TileScanner(new ushort[]
                {
                    TileID.Grass,
                    TileID.Dirt,
                    TileID.Stone,
                    TileID.ClayBlock
                }).Output(dictionary));

                // we do this manually bcuz im stupid as fuuuuuuuuuuuck
                int grassCount = 0;
                for (int i = origin.X; i < origin.X + biomeWidth; i++)
                {
                    for (int j = origin.Y; j < origin.Y + biomeHeight; j++)
                    {
                        if (Main.tile[i, j].TileType == TileID.Grass)
                        {
                            // if touching air, aka if this should actually qualify as grass
                            if (Main.tile[i + 1, j].HasTile == false || Main.tile[i - 1, j].HasTile == false || Main.tile[i, j + 1].HasTile == false || Main.tile[i, j - 1].HasTile == false)
                            {
                                grassCount++;
                            }
                        }
                    }
                }

                int grassCountThreshold = worldSize == 3 ? 100 : worldSize == 2 ? 75 : 50;
                int regularBlockCount = dictionary[TileID.Dirt] + dictionary[TileID.Stone] + dictionary[TileID.ClayBlock];

                Ref<int> solidCount = new Ref<int>(0);
                WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[]
                {
                    new Actions.ContinueWrapper(Actions.Chain(new GenAction[]
                    {
                        new Modifiers.IsSolid(),
                        new Actions.Scanner(solidCount)
                    })),
                }));

                if (grassCount > grassCountThreshold && regularBlockCount > (solidCount.Value * 0.9))
                {
                    //TODO: this worldgen has to truth nuke the stupid fairy logs
                    attempts = 0;
                    SurfaceMushroomGen_Refactored biome = new SurfaceMushroomGen_Refactored();
                    biome.Place(origin, GenVars.structures);
                    //Main.spawnTileX = biomeCenter.X;
                    //Main.spawnTileY = biomeCenter.Y;
                }
                attempts--;
            }
        }

        // kept for posterity
        private static void Mush(GenerationProgress progress)
        {
            progress.Message = "Growing Shrooms";

            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int biomes = 0; biomes < 0; biomes++)
            {
                Point origin = new Point(WorldGen.genRand.Next(0, x), (int)GenVars.worldSurfaceLow);
                origin.Y = WorldGenUtils.GetFirstTileFloor(origin.X, origin.Y, true);
                RedMushroomGeneration biome = new();
                biome.Place(origin, GenVars.structures);
            }
        }

        private static void ParthenanIsland(GenerationProgress progress)
        {
            progress.Message = "Storming the Parthenan";

            int ParthenanHeight = 0;
            ParthenanHeight = 120;
            Point center = new Point((Main.maxTilesX / 15), center.Y = ParthenanHeight);
            ParthenanGen biome = new ParthenanGen();
            biome.Place(center, GenVars.structures);
        }

        private void Ship(GenerationProgress progress)
        {
            bool small = WorldGenUtils.GetWorldSize() == 1;
            shipSide = (Main.dungeonX > Main.maxTilesX / 2 ? -1 : 1);
            int dist = small ? 90 : 140;
            shipPos.X = (shipSide == 1 ? Main.maxTilesX - dist : dist);
            shipPos.Y = WorldGenUtils.GetFirstTileFloor((int)shipPos.X, 10, true);
            if (!small)
                shipPos.Y += 36;
            progress.Message = "Sinking the ship";

            Point origin = new Point((int)shipPos.X, (int)shipPos.Y);
            origin.Y = WorldGenUtils.GetFirstTileFloor(origin.X, origin.Y, true);
            new SunkenShipGen().Place(origin, GenVars.structures);
        }

        public override void PostWorldGen()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
            {
                int[] itemsToPlaceInSunkenChest = new int[] { ModContent.ItemType<CursedCompass>() };
                int itemsToPlaceInSunkenChestsChoice = 0;
                for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
                {
                    Chest chest = Main.chest[chestIndex];
                    if (chest != null && Main.tile[chest.x, chest.y].TileType == ModContent.TileType<SunkenChest_Tile>()) // if glass chest
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                itemsToPlaceInSunkenChestsChoice = Main.rand.Next(itemsToPlaceInSunkenChest.Length);
                                chest.item[0].SetDefaults(itemsToPlaceInSunkenChest[itemsToPlaceInSunkenChestsChoice]);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
