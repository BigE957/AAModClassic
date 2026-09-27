using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.Parthenan.World.Biomes
{
    public class ParthenanSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Parthenan { get; private set; }

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/Parthenan.aasch");
            Parthenan = SchematicLoader.Resolve(data);

            foreach (string warning in Parthenan.Warnings)
                AAMod.instance.Logger.Warn("Parthenan schematic: " + warning);
        }

        public override void Unload() => Parthenan = null;
    }

    public class ParthenanGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic parthenan = ParthenanSchematicAssets.Parthenan;
            if (parthenan == null)
            {
                AAMod.instance.Logger.Warn("Parthenan schematic isn't loaded; skipping placement.");
                return false;
            }

            int width = parthenan.Width;
            int height = parthenan.Height;
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
                logName: "Parthenan",
                canPlace: rect => structures.CanPlace(rect, WorldGenUtils.AllTilesAllowed, 0));

            Point placementPoint = search.Position;

            // The original protected the rectangle at `origin`, not the searched `placementPoint`, even
            // though generation itself used placementPoint - a mismatch if the search ever moved the
            // structure. Fixed here to protect where the structure actually lands. Padding (5, not the
            // usual 20) is unchanged.
            WorldGenUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, width, height), 5);

            PlacedSchematic placed = SchematicPlacement.Place(parthenan, placementPoint, new SchematicPlaceOptions { Anchor = SchematicAnchor.TopLeft });

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Parthenan placement: " + warning);

            return placed.Success;
        }
    }
}