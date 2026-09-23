using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic.Structures;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Acropolis.World.Biomes
{
    public class AcropolisSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Acropolis { get; private set; }
        public static HashSet<int> UnbreakableTiles { get; } = [];
        public static HashSet<int> UnbreakableWalls { get; } = [];

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/Acropolis.aasch");
            Acropolis = SchematicLoader.Resolve(data);

            foreach (string warning in Acropolis.Warnings)
                AAMod.instance.Logger.Warn("Acropolis schematic: " + warning);

            UnbreakableTiles.Add(ModContent.TileType<SkymarbleBrick_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<SkycrystalBrick_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<SkyCrystal_Tile>());

            UnbreakableWalls.Add(ModContent.WallType<AcropolisBrickWall_Wall>());
            UnbreakableWalls.Add(ModContent.WallType<AcropolisPillarWall_Wall>());
        }

        public override void Unload()
        {
            Acropolis = null;
            UnbreakableTiles.Clear();
            UnbreakableWalls.Clear();
        }
    }

    public class AcropolisGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic acropolis = AcropolisSchematicAssets.Acropolis;
            if (acropolis == null)
            {
                AAMod.instance.Logger.Warn("Acropolis schematic isn't loaded; skipping placement.");
                return false;
            }

            int width = acropolis.Width;
            int height = acropolis.Height;

            Point placementPoint = origin;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                const int maxAttempts = 5000;
                PlacementSearchResult search = StructurePlacementSearch.Find(
                    origin, width, height, maxAttempts, structures,
                    shouldAvoidTile: (p, attempts) => Framing.GetTileSafely(p).HasTile,
                    nextCandidate: attempts =>
                    {
                        int radius = 200 + attempts / 5;
                        int targetX = Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 200, Main.maxTilesX - 200);
                        return new Point(targetX, origin.Y);
                    },
                    logName: "Acropolis",
                    canPlace: rect => structures.CanPlace(rect, WorldGenUtils.AllTilesAllowed, 0));

                placementPoint = search.Position;
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, width, height), 20);
            AAWorld.acropolisPos = placementPoint;

            var options = new SchematicPlaceOptions
            {
                Anchor = SchematicAnchor.TopLeft,
                UnbreakableTiles = AcropolisSchematicAssets.UnbreakableTiles,
                UnbreakableWalls = AcropolisSchematicAssets.UnbreakableWalls,
            };

            PlacedSchematic placed = SchematicPlacement.Place(acropolis, placementPoint, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Acropolis placement: " + warning);

            return placed.Success;
        }
    }
}