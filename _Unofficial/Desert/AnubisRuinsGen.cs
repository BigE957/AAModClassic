using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unofficial.Desert
{
    public class AnubisRuinsSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Ruins { get; private set; }

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/AnubisRuins.aasch");
            Ruins = SchematicLoader.Resolve(data);

            foreach (string warning in Ruins.Warnings)
                AAMod.instance.Logger.Warn("Anubis Ruins schematic: " + warning);
        }

        public override void Unload() => Ruins = null;
    }

    public class AnubisRuinsGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic Ruins = AnubisRuinsSchematicAssets.Ruins;
            if (Ruins == null)
            {
                AAMod.instance.Logger.Warn("Anubis Ruins schematic isn't loaded; skipping placement.");
                return false;
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, Ruins.Width, Ruins.Height), 20);

            PlacedSchematic placed = SchematicPlacement.Place(Ruins, origin, new SchematicPlaceOptions { Anchor = SchematicAnchor.Center, FlipHorizontal = Main.dungeonX > Main.spawnTileX });

            //Vector2 anubisSpawn = placed.Markers[0].Area.Center.ToWorldCoordinates(0, 0);
            AAMod.instance.Logger.Info(origin);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Anubis Ruins placement: " + warning);

            return placed.Success;
        }
    }
}
