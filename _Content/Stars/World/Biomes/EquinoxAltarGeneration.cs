using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Stars.World.Biomes
{
    public class EquinoxAltarSchematicAssets : ModSystem
    {
        public static ResolvedSchematic EquinoxAltar { get; private set; }
        public static HashSet<int> UnbreakableTiles { get; } = [];

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/EquinoxAltar.aasch");
            EquinoxAltar = SchematicLoader.Resolve(data);

            foreach (string warning in EquinoxAltar.Warnings)
                AAMod.instance.Logger.Warn("Equinox Altar schematic: " + warning);

            UnbreakableTiles.Add(ModContent.TileType<GreedBrick_Tile>());
        }

        public override void Unload()
        {
            EquinoxAltar = null;
            UnbreakableTiles.Clear();
        }
    }

    public class EquinoxAltarGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic altar = EquinoxAltarSchematicAssets.EquinoxAltar;
            if (altar == null)
            {
                AAMod.instance.Logger.Warn("Equinox Altar schematic isn't loaded; skipping placement.");
                return false;
            }

            int width = altar.Width;
            int height = altar.Height;
            const int maxAttempts = 5000;

            PlacementSearchResult search = StructurePlacementSearch.Find(
                origin, width, height, maxAttempts, structures,
                shouldAvoidTile: (p, attempts) => Framing.GetTileSafely(p).HasTile,
                nextCandidate: attempts =>
                {
                    int radius = 200 + attempts / 2;
                    int targetX = Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 40, Main.maxTilesX - (40 + width));
                    return new Point(targetX, origin.Y);
                },
                logName: "Equinox Altar",
                canPlace: rect => structures.CanPlace(rect, WorldGenUtils.AllTilesAllowed, 0));

            Point placementPoint = search.Position;

            WorldGenUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, width, height), 20);

            var options = new SchematicPlaceOptions
            {
                Anchor = SchematicAnchor.TopLeft,
                UnbreakableTiles = EquinoxAltarSchematicAssets.UnbreakableTiles,
            };

            PlacedSchematic placed = SchematicPlacement.Place(altar, placementPoint, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Equinox Altar placement: " + warning);

            return placed.Success;
        }
    }
}