using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Conversions;
using AAModClassic.Structures;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Content.Mire.World.Biomes
{
    public class MireSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Lake { get; private set; }

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/Mire_Lake.aasch");
            Lake = SchematicLoader.Resolve(data);

            foreach (string warning in Lake.Warnings)
                AAMod.instance.Logger.Warn("Rising Moon Lake schematic: " + warning);
        }

        public override void Unload() => Lake = null;
    }

    public class MireGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic lake = MireSchematicAssets.Lake;
            if (lake == null)
            {
                AAMod.instance.Logger.Warn("Rising Moon Lake schematic isn't loaded; skipping placement.");
                return false;
            }

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;


            WorldUtils.Gen(origin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new ConvertTile(ModContent.GetInstance<MireConversion>().Type)
            ]));

            Point pos = new(origin.X, origin.Y - 30);
            PlacedSchematic placed = SchematicPlacement.Place(lake, pos, new SchematicPlaceOptions { Anchor = SchematicAnchor.TopCenter });

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Rising Moon Lake placement: " + warning);

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int altarX = xAxis - 45; altarX < xAxis + 45; altarX++)
                    for (int altarY = yAxis - 45; altarY < yAxis + 45; altarY++)
                        if (Main.rand.NextBool(15))
                            WorldGen.PlaceObject(altarX, altarY - 1, ModContent.TileType<AbyssAltarUnsafe_Tile>());
            }

            return placed.Success;
        }
    }

    public class BogwoodCon : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ushort livingWood = (ushort)ModContent.TileType<LivingBogwood_Tile>(), livingLeaves = (ushort)ModContent.TileType<LivingBogleaf_Tile>();
            ushort bogwoodWall = (ushort)ModContent.WallType<LivingBogwoodWall_Wall>(), leafWall = (ushort)ModContent.WallType<LivingBogleafWall_Wall>();

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;
            Point newOrigin = new(origin.X, origin.Y - 10);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.OnlyTiles([TileID.LivingMahogany, TileID.LivingWood]),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(livingWood, true, true)
            ]));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.OnlyTiles([TileID.LivingMahoganyLeaves, TileID.LeafBlock]),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(livingLeaves, true, true)
            ]));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.OnlyWalls([WallID.LivingWood]),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(bogwoodWall, true)
            ]));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(
            [
                new InWorld(),
                new Modifiers.OnlyWalls([WallID.LivingLeaf]),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(leafWall, true)
            ]));

            return true;
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) return 1;
            if (Main.maxTilesX == 6400) return 2;
            if (Main.maxTilesX == 8400) return 3;
            return 1;
        }
    }
}