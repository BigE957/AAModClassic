using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic.Conversions;
using AAModClassic.Structures;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public class InfernoSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Volcano { get; private set; }

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/Inferno_Volcano.aasch");
            Volcano = SchematicLoader.Resolve(data);

            foreach (string warning in Volcano.Warnings)
                AAMod.instance.Logger.Warn("Rising Sun Pagoda schematic: " + warning);
        }

        public override void Unload() => Volcano = null;
    }

    public class InfernoGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic volcano = InfernoSchematicAssets.Volcano;
            if (volcano == null)
            {
                AAMod.instance.Logger.Warn("Rising Sun Pagoda schematic isn't loaded; skipping placement.");
                return false;
            }

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;

            Point newOrigin = new(origin.X, origin.Y + 60);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new ConvertTile(ModContent.GetInstance<InfernoConversion>().Type)
            ]));

            PlacedSchematic placed = SchematicPlacement.Place(volcano, newOrigin, new SchematicPlaceOptions { Anchor = SchematicAnchor.Center });

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Rising Sun Pagoda placement: " + warning);

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                        if (Main.rand.NextBool(15))
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<DragonAltarUnsafe_Tile>());
            }

            return true;
        }
    }
}
