using AAModClassic._Content.Acropolis.__Hardmode.Items.Tiles;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Acropolis.World.Biomes
{
    public class AcropolisTexGenAssets : ModSystem
    {
        public static TexGenData AcropolisTileData;
        public static TexGenData AcropolisWallData;
        public static TexGenData AcropolisRoofData;

        public override void OnModLoad()
        {
            AcropolisTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Acropolis/World/Biomes/Acropolis");
            AcropolisWallData = TexGen.GetTextureForGen("AAModClassic/_Content/Acropolis/World/Biomes/AcropolisWalls");
            AcropolisRoofData = TexGen.GetTextureForGen("AAModClassic/_Content/Acropolis/World/Biomes/AcropolisRoof");;
        }
    }

    public class AcropolisGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            int attempts = 0;
            int maxAttempts = 5000;
            Point placementPoint = origin;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                do
                {
                    //AAMod.instance.Logger.Info("Attempting to Place Acropolis at: " + placementPoint);

                    bool canGenerateInLocation = true;

                    if (!structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, AcropolisTexGenAssets.AcropolisTileData.Width, AcropolisTexGenAssets.AcropolisTileData.Height), WorldGenUtils.AllTilesAllowed, 0))
                    {
                        //AAMod.instance.Logger.Info("Acropolis Placement Failed, Encountered a Pre-Existing Structure");
                        canGenerateInLocation = false;
                    }

                    if (canGenerateInLocation)
                    {
                        int fullX = placementPoint.X + AcropolisTexGenAssets.AcropolisTileData.Width;
                        int fullY = placementPoint.Y + AcropolisTexGenAssets.AcropolisTileData.Height;

                        for (int x = placementPoint.X; x < fullX; x++)
                        {
                            for (int y = placementPoint.Y; y < fullY; y++)
                            {
                                if (Framing.GetTileSafely(x, y).HasTile)//ShouldAvoidLocation(new Point(x, y), attempts > 1000, attempts > 4000))
                                {
                                    canGenerateInLocation = false;
                                    break;
                                }
                            }
                            if (!canGenerateInLocation)
                                break;
                        }
                    }

                    if (canGenerateInLocation)
                    {
                        AAMod.instance.Logger.Info("Acropolis successfully placed after " + attempts + " attempts.");
                        break;
                    }

                    int radius = 200 + attempts / 5;
                    int targetX = Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 200, Main.maxTilesX - 200);
                    int targetY = origin.Y;
                    placementPoint = new Point(targetX, targetY);

                } while (attempts++ < maxAttempts);
            }
            WorldGenUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, AcropolisTexGenAssets.AcropolisTileData.Width, AcropolisTexGenAssets.AcropolisTileData.Height), 20);

            AAWorld.acropolisPos = placementPoint;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<SkymarbleBrick_Tile>(),
                [new Color(128, 128, 128)] = ModContent.TileType<SkycrystalBrick_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<SkyCrystal_Tile>(),
                [new Color(0, 255, 255)] = TileID.Grass,
                [new Color(0, 255, 0)] = TileID.Dirt,
                [new Color(0, 0, 255)] = TileID.Cloud,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            HashSet<int> protectedTiles = [
                ModContent.TileType<SkymarbleBrick_Tile>(),
                ModContent.TileType<SkycrystalBrick_Tile>(),
                ModContent.TileType<SkyCrystal_Tile>(),
            ];

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.WallType<AcropolisBrickWall_Wall>(),
                [new Color(0, 255, 255)] = ModContent.WallType<AcropolisPillarWall_Wall>(),
                [new Color(0, 255, 0)] = WallID.Dirt,
                [new Color(0, 0, 255)] = WallID.Cloud,
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            HashSet<int> protectedWalls = [
                ModContent.WallType<AcropolisBrickWall_Wall>(),
                ModContent.WallType<AcropolisPillarWall_Wall>(),
            ];

            TexGen gen = TexGen.GetTexGenerator(AcropolisTexGenAssets.AcropolisTileData, colorToTile, AcropolisTexGenAssets.AcropolisWallData, colorToWall, null, AcropolisTexGenAssets.AcropolisRoofData, unbreakableTiles: protectedTiles, unbreakableWalls: protectedWalls);

            gen.Generate(placementPoint.X, placementPoint.Y, true, true);

            WorldGen.PlaceObject(placementPoint.X + 79, placementPoint.Y + 86, (ushort)ModContent.TileType<AcropolisAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, placementPoint.X + 79, placementPoint.Y + 87, (ushort)ModContent.TileType<AcropolisAltar_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }
}
