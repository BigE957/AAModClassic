using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content._Dev.World.Biomes
{
    public class CrystalOfMemoriesSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Crystal { get; private set; }

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/CrystalOfMemories.aasch");
            Crystal = SchematicLoader.Resolve(data);

            foreach (string warning in Crystal.Warnings)
                AAMod.instance.Logger.Warn("Crystal of Memories schematic: " + warning);
        }

        public override void Unload() => Crystal = null;
    }

    public class CrystalOfMemoriesGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic crystal = CrystalOfMemoriesSchematicAssets.Crystal;
            if (crystal == null)
            {
                AAMod.instance.Logger.Warn("Crystal of Memories schematic isn't loaded; skipping placement.");
                return false;
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, crystal.Width, crystal.Height), 20);

            PlacedSchematic placed = SchematicPlacement.Place(crystal, origin, new SchematicPlaceOptions { Anchor = SchematicAnchor.TopLeft });

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Crystal of Memories placement: " + warning);

            return placed.Success;
        }
    }
}