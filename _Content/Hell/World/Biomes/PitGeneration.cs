using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Hell.World.Biomes
{
    public class PitSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Pit { get; private set; }
        public static ResolvedSchematic PitTeaser { get; private set; }

        public override void PostSetupContent()
        {
            Pit = LoadAndResolve("Pit");
            PitTeaser = LoadAndResolve("Pit_Teaser");
        }

        private static ResolvedSchematic LoadAndResolve(string name)
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, $"Structures/Schematics/{name}.aasch");
            ResolvedSchematic resolved = SchematicLoader.Resolve(data);

            foreach (string warning in resolved.Warnings)
                AAMod.instance.Logger.Warn($"{name} schematic: " + warning);

            return resolved;
        }

        public override void Unload()
        {
            Pit = null;
            PitTeaser = null;
        }
    }

    public class PitGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic pit = PitSchematicAssets.Pit;
            if (pit == null)
            {
                AAMod.instance.Logger.Warn("Pit schematic isn't loaded; skipping placement.");
                return false;
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, pit.Width, pit.Height), 20);

            PlacedSchematic placed = SchematicPlacement.Place(pit, origin, new SchematicPlaceOptions { Anchor = SchematicAnchor.TopLeft });

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Pit placement: " + warning);

            return placed.Success;
        }
    }

    public class PitTeaserGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic teaser = PitSchematicAssets.PitTeaser;
            if (teaser == null)
            {
                AAMod.instance.Logger.Warn("Pit Teaser schematic isn't loaded; skipping placement.");
                return false;
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, teaser.Width, teaser.Height), 20);

            PlacedSchematic placed = SchematicPlacement.Place(teaser, origin, new SchematicPlaceOptions { Anchor = SchematicAnchor.TopLeft });

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Pit Teaser placement: " + warning);

            return placed.Success;
        }
    }
}