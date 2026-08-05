using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Stars.World.Altar;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Stars.World.Biomes
{
    public class EquinoxShrineTexGenAssets : ModSystem
    {
        public static TexGenData EquinoxTileData;
        public static TexGenData EquinoxSlopeData;

        public override void OnModLoad()
        {
            EquinoxTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Stars/World/Biomes/EquinoxAltar");
            EquinoxSlopeData = TexGen.GetTextureForGen("AAModClassic/_Content/Stars/World/Biomes/EquinoxAltarSlope");
        }
    }
    public class EquinoxShrineGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            int attempts = 0;
            int maxAttempts = 5000;
            Point placementPoint = origin;
            do
            {
                //AAMod.instance.Logger.Info("Attempting to Place Equinox Shrine at: " + placementPoint);

                bool canGenerateInLocation = true;

                if (!structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, EquinoxShrineTexGenAssets.EquinoxTileData.Width, EquinoxShrineTexGenAssets.EquinoxTileData.Height), WorldGenUtils.AllTilesAllowed, 0))
                {
                    //AAMod.instance.Logger.Info("Equinox Shrine Placement Failed, Encountered a Pre-Existing Structure");
                    canGenerateInLocation = false;
                }

                if (canGenerateInLocation)
                {
                    int fullX = placementPoint.X + EquinoxShrineTexGenAssets.EquinoxTileData.Width;
                    int fullY = placementPoint.Y + EquinoxShrineTexGenAssets.EquinoxTileData.Height;

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
                    AAMod.instance.Logger.Info("Equinox Shrine successfully placed after " + attempts + " attempts.");
                    break;
                }

                int radius = 200 + attempts / 2;
                int targetX = Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 40, Main.maxTilesX - (40 + EquinoxShrineTexGenAssets.EquinoxTileData.Width));
                int targetY = origin.Y;
                placementPoint = new Point(targetX, targetY);

            } while (attempts++ < maxAttempts);

            WorldGenUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, EquinoxShrineTexGenAssets.EquinoxTileData.Width, EquinoxShrineTexGenAssets.EquinoxTileData.Height), 20);

            Dictionary<Color, int> colorToTile = new()
            {
                [new Color(255, 0, 0)] = ModContent.TileType<GreedBrick_Tile>(),
                [new Color(0, 255, 255)] = ModContent.TileType<DayCrystal_Tile>(),
                [new Color(0, 255, 0)] = ModContent.TileType<NightCrystal_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<DaybringerBrick_Tile>(),
                [new Color(0, 0, 255)] = ModContent.TileType<NightcrawlerBrick_Tile>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = TexGen.GetTexGenerator(EquinoxShrineTexGenAssets.EquinoxTileData, colorToTile, null, null, null, EquinoxShrineTexGenAssets.EquinoxSlopeData, unbreakableTiles: [ModContent.TileType<GreedBrick_Tile>()]);

            gen.Generate(placementPoint.X, placementPoint.Y, true, true);

            WorldGen.PlaceObject(placementPoint.X + 36, placementPoint.Y + 39, ModContent.TileType<WormAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, placementPoint.X + 36, placementPoint.Y + 39, ModContent.TileType<WormAltar_Tile>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(placementPoint.X + 30, placementPoint.Y + 42, ModContent.TileType<StarAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, placementPoint.X + 30, placementPoint.Y + 42, ModContent.TileType<StarAltar_Tile>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(placementPoint.X + 45, placementPoint.Y + 42, ModContent.TileType<GravAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, placementPoint.X + 80, placementPoint.Y + 88, ModContent.TileType<GravAltar_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }
}
