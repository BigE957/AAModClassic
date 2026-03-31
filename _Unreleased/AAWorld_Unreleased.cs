using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Unreleased.Items.BossSummons;
using AAModClassic._Unreleased.Tiles;
using AAModClassic._Unreleased.Tiles.Fulgurite.Parthenan;
using AAModClassic._Unreleased.Tiles.Fulgurite.Parthenan.Ancient;
using AAModClassic._Unreleased.World;
using AAModClassic.Tiles;
using AAModClassic.Tiles.Keep;
using AAModClassic.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased
{
    public class AAWorld_Unreleased : ModSystem
    {
        public static bool doUnreleasedContent = true; // has no function but u can see where unreleased content is placed elsewhere

        private Vector2 shipPos = new Vector2(0, 0);
        private int shipSide = 0;

        public static bool downedSoC;
        public static bool downedIZ;

        public static int StormTiles = 0;

        public static bool Anticheat = true;

        #region stupid bullshit
        public override void PreWorldGen()
        {
            downedSoC = false;
            downedIZ = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downedUnreleased = new List<string>();
            if (downedSoC) downedUnreleased.Add("SoC");
            if (downedIZ) downedUnreleased.Add("IZ");

            tag.Add("downedUnreleased", downedUnreleased);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downedUnreleased = tag.GetList<string>("downedUnreleased");
            downedSoC = downedUnreleased.Contains("SoC");
            downedIZ = downedUnreleased.Contains("IZ");
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
            if (doUnreleasedContent)
            {
                int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
                int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
                int chaosBiomeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));

                tasks.Insert(shiniesIndex2, new PassLegacy("Parthenan", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    ParthenanIsland(progress);
                }));

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
            StormTiles = tileCounts[ModContent.TileType<StormCloud>()] + tileCounts[ModContent.TileType<AncientFulguritePlatingS>()] + tileCounts[ModContent.TileType<AncientFulguriteBrickS>()] + tileCounts[ModContent.TileType<AncientFulgurGlassS>()];
        }

        private void Mush_Refactored(GenerationProgress progress)
        {
            progress.Message = "Growing Shrooms";

            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            int center = Main.maxTilesX / 2;

            int worldSize = BaseWorldGen.GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;
            int biomeWidthHalf = biomeWidth / 2;
            int biomeHeightHalf = biomeHeight / 2;

            int attempts = 5000;
            while (attempts > 0)
            {
                Point origin = new Point();
                Point newOrigin = new Point();

                bool isInBounds = false;
                bool isInCenter = false;
                while (attempts > 0 && (!isInBounds || isInCenter || origin == new Point() || newOrigin == new Point()))
                {
                    origin = new Point(WorldGen.genRand.Next(0, x), (int)GenVars.worldSurfaceLow);
                    origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);

                    // do some stuff to make it so the old origin is the position
                    newOrigin = new Point(origin.X, origin.Y);
                    origin.X -= biomeHeightHalf;
                    origin.Y -= biomeHeightHalf;

                    if (origin.X > 300 || origin.X < x - 300)
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
                    TileID.Sand,
                    TileID.SnowBlock,
                    TileID.IceBlock,
                    TileID.BlueDungeonBrick,
                    TileID.PinkDungeonBrick,
                    TileID.GreenDungeonBrick,
                    TileID.JungleGrass,
                    TileID.Mud,
                    TileID.CorruptGrass,
                    TileID.Ebonstone,
                    TileID.Ebonsand,
                    TileID.CrimsonGrass,
                    TileID.Crimstone,
                    TileID.Crimsand,
                    (ushort)ModContent.TileType<MireGrass>(),
                    (ushort)ModContent.TileType<Depthstone>(),
                    (ushort)ModContent.TileType<Depthsand>(),
                    (ushort)ModContent.TileType<IndigoIce>(),
                    (ushort)ModContent.TileType<InfernoGrass>(),
                    (ushort)ModContent.TileType<Torchstone>(),
                    (ushort)ModContent.TileType<Torchsand>(),
                    (ushort)ModContent.TileType<TorchAsh>(),
                    (ushort)ModContent.TileType<AAModClassic.Tiles.Torchice>(),
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

                int dontGenThreshold = worldSize == 3 ? 800 : worldSize == 2 ? 600 : 400;
                int grassCountThreshold = worldSize == 3 ? 100 : worldSize == 2 ? 75 : 50;
                int IceBlockBiomeCount = dictionary[TileID.SnowBlock] + dictionary[TileID.IceBlock] + dictionary[(ushort)ModContent.TileType<IndigoIce>()] + dictionary[(ushort)ModContent.TileType<TorchAsh>()] + dictionary[(ushort)ModContent.TileType<AAModClassic.Tiles.Torchice>()];
                int sandBiomeCount = dictionary[TileID.Sand] + dictionary[TileID.Ebonsand] + dictionary[TileID.Crimsand] + dictionary[(ushort)ModContent.TileType<Depthsand>()] + dictionary[(ushort)ModContent.TileType<Torchsand>()];
                int dungeonBiomeCount = dictionary[TileID.BlueDungeonBrick] + dictionary[TileID.PinkDungeonBrick] + dictionary[TileID.GreenDungeonBrick];
                int jungleBiomeCount = dictionary[TileID.JungleGrass] + dictionary[TileID.Mud] + dictionary[(ushort)ModContent.TileType<MireGrass>()];
                int evilBiomeCount = dictionary[TileID.CorruptGrass] + dictionary[TileID.Ebonstone] + dictionary[TileID.Ebonsand] + dictionary[TileID.CrimsonGrass] + dictionary[TileID.Crimstone] + dictionary[TileID.Crimsand] + dictionary[(ushort)ModContent.TileType<MireGrass>()] + dictionary[(ushort)ModContent.TileType<Depthstone>()] + dictionary[(ushort)ModContent.TileType<Depthsand>()] + dictionary[(ushort)ModContent.TileType<InfernoGrass>()] + dictionary[(ushort)ModContent.TileType<Torchstone>()] + dictionary[(ushort)ModContent.TileType<Torchsand>()];
                if (grassCount > grassCountThreshold && dungeonBiomeCount <= 0 && IceBlockBiomeCount < dontGenThreshold && sandBiomeCount < dontGenThreshold && jungleBiomeCount < dontGenThreshold && evilBiomeCount < dontGenThreshold)
                {
                    attempts = 0;
                    SurfaceMushroomGen_Refactored biome = new SurfaceMushroomGen_Refactored();
                    biome.Place(origin, GenVars.structures);
                }
                attempts--;
            }
        }

        // kept for posterity
        private void Mush(GenerationProgress progress)
        {
            progress.Message = "Growing Shrooms";

            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int biomes = 0; biomes < 0; biomes++)
            {
                Point origin = new Point(WorldGen.genRand.Next(0, x), (int)GenVars.worldSurfaceLow);
                origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
                SurfaceMushroom biome = new SurfaceMushroom();
                biome.Place(origin, GenVars.structures);
            }
        }

        private void ParthenanIsland(GenerationProgress progress)
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
            shipSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
            shipPos.X = (shipSide == 1 ? (Main.maxTilesX - 90) : 90);
            progress.Message = "Sinking the ship";

            Point origin = new Point((int)shipPos.X, (int)GenVars.worldSurfaceLow - 200);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            ShipGen biome = new ShipGen();
            biome.Place(origin, GenVars.structures);
        }

        public override void PostWorldGen()
        {
            if (doUnreleasedContent)
            {
                int[] itemsToPlaceInSunkenChest = new int[] { ModContent.ItemType<CursedCompass>() };
                int itemsToPlaceInSunkenChestsChoice = 0;
                for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
                {
                    Chest chest = Main.chest[chestIndex];
                    if (chest != null && Main.tile[chest.x, chest.y].TileType == ModContent.TileType<SunkenChest>()) // if glass chest
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
