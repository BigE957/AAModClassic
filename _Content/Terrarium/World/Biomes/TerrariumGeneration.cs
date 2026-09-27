using AAModClassic._Content.Terrarium.World.Tiles;
using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Terrarium.World.Biomes
{
    public class TerrariumSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Small { get; private set; }
        public static ResolvedSchematic Medium { get; private set; }
        public static HashSet<int> UnbreakableTiles { get; } = [];

        public override void PostSetupContent()
        {
            Small = LoadAndResolve("Terrarium_Small");
            Medium = LoadAndResolve("Terrarium_Medium");

            UnbreakableTiles.Add(ModContent.TileType<TerraCrystal_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<PermeableTerraWood_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<TerraLeaves_Tile>());
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
            Small = null;
            Medium = null;
            UnbreakableTiles.Clear();
        }
    }

    public class TerrariumGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            int worldSize = WorldGenUtils.GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            ResolvedSchematic terrarium = worldSize == 1 ? TerrariumSchematicAssets.Small : TerrariumSchematicAssets.Medium;
            if (terrarium == null)
            {
                AAMod.instance.Logger.Warn("Terrarium schematic isn't loaded; skipping placement.");
                return false;
            }

            WorldUtils.Gen(origin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new WorldGenUtils.InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            ]));

            var options = new SchematicPlaceOptions
            {
                Anchor = SchematicAnchor.Center,
                UnbreakableTiles = TerrariumSchematicAssets.UnbreakableTiles,
            };

            //Rectangle area = SchematicPlacement.ResolveArea(origin, options.Anchor, terrarium.Width, terrarium.Height);
            //WorldGenUtils.AddProtectedStructure(area, 20);

            PlacedSchematic placed = SchematicPlacement.Place(terrarium, origin, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Terrarium placement: " + warning);

            return placed.Success;
        }
    }
}