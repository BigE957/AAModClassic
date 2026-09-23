using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic._CrossMod.Thorium;
using AAModClassic.Structures;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Biomes
{
    public class SunkenShipSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Official { get; private set; }
        public static ResolvedSchematic Unofficial { get; private set; }

        public override void PostSetupContent()
        {
            bool thorium = ThoriumMod.IsEnabled;
            Official = LoadAndResolve(thorium ? "SunkenShip_Official_Thorium" : "SunkenShip_Official");
            Unofficial = LoadAndResolve(thorium ? "SunkenShip_Unofficial_Thorium" : "SunkenShip_Unofficial");
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
            Official = null;
            Unofficial = null;
        }
    }

    public class SunkenShipGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            origin.Y -= 28;
            ResolvedSchematic schem = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? SunkenShipSchematicAssets.Unofficial : SunkenShipSchematicAssets.Unofficial;

            int newOriginX = origin.X - (schem.Width / 2);
            int newOriginY = origin.Y - (schem.Height / 2) + 10;

            var options = new SchematicPlaceOptions
            {
                Anchor = SchematicAnchor.Center,
                UnbreakableTiles = TerrariumSchematicAssets.UnbreakableTiles,
                FlipHorizontal = origin.X < Main.maxTilesX / 2
            };

            Rectangle area = SchematicPlacement.ResolveArea(origin, options.Anchor, schem.Width, schem.Height);
            WorldGenUtils.AddProtectedStructure(area, 20);

            PlacedSchematic placed = SchematicPlacement.Place(schem, origin, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Terrarium placement: " + warning);

            AAWorld_Unreleased.shipPos = new Point(newOriginX, newOriginY);

            return true;
        }
    }
}