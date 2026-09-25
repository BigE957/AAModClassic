using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Terrarium.World.Tiles;
using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Biomes
{
    public class LostKeepSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Keep { get; private set; }
        public static HashSet<int> UnbreakableTiles { get; } = [];

        public static bool[] AlwaysAvoid { get; private set; }
        public static bool[] AvoidUnlessLeniant { get; private set; }
        public static bool[] AvoidUnlessDesperate { get; private set; }

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/LostKeep.aasch");
            Keep = SchematicLoader.Resolve(data);

            foreach (string warning in Keep.Warnings)
                AAMod.instance.Logger.Warn("Lost Keep schematic: " + warning);

            UnbreakableTiles.Add(TileID.Glass);
            UnbreakableTiles.Add(ModContent.TileType<TerraWood_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<PermeableTerraWood_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<TerraLeaves_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<ScorchedShingles_Tile>());

            AvoidUnlessLeniant = new bool[TileLoader.TileCount];
            AvoidUnlessLeniant[TileID.JungleGrass] = true;
            AvoidUnlessLeniant[TileID.Hive] = true;

            AvoidUnlessDesperate = new bool[TileLoader.TileCount];
            AvoidUnlessDesperate[TileID.Sandstone] = true;
            AvoidUnlessDesperate[TileID.SnowBlock] = true;
            AvoidUnlessDesperate[TileID.IceBlock] = true;

            AlwaysAvoid = new bool[TileLoader.TileCount];
            AlwaysAvoid[TileID.Ash] = true;
            AlwaysAvoid[TileID.Crimstone] = true;
            AlwaysAvoid[TileID.Ebonstone] = true;
            AlwaysAvoid[TileID.LihzahrdBrick] = true;
            AlwaysAvoid[TileID.BlueDungeonBrick] = true;
            AlwaysAvoid[TileID.GreenDungeonBrick] = true;
            AlwaysAvoid[TileID.PinkDungeonBrick] = true;
            AlwaysAvoid[ModContent.TileType<TerraCrystal_Tile>()] = true;
        }

        public override void Unload()
        {
            Keep = null;
            UnbreakableTiles.Clear();
            AlwaysAvoid = null;
            AvoidUnlessLeniant = null;
            AvoidUnlessDesperate = null;
        }
    }

    public class LostKeepGeneration : MicroBiome
    {
        private static bool ShouldAvoidLocation(Point p, bool leniant, bool desperate)
        {
            ushort type = Framing.GetTileSafely(p).TileType;

            if (LostKeepSchematicAssets.AlwaysAvoid[type])
                return true;
            if (!leniant && LostKeepSchematicAssets.AvoidUnlessLeniant[type])
                return true;
            if (!desperate && LostKeepSchematicAssets.AvoidUnlessDesperate[type])
                return true;

            return false;
        }

        public static Point FindValidLostKeepPosition(Point origin, StructureMap structures)
        {
            ResolvedSchematic keep = LostKeepSchematicAssets.Keep;
            if (keep == null)
            {
                AAMod.instance.Logger.Warn("Lost Keep schematic isn't loaded; using the given origin unchecked.");
                return origin;
            }

            int width = keep.Data.Width;
            int height = keep.Data.Height;
            const int maxAttempts = 20000;

            int maxHeightUp = WorldGenUtils.GetWorldSize() == 1 ? 300 : 550;

            PlacementSearchResult search = StructurePlacementSearch.Find(
                origin, width, height, maxAttempts, structures,
                shouldAvoidTile: (p, attempts) => ShouldAvoidLocation(p, attempts > 4000, attempts > 12500),
                nextCandidate: attempts =>
                {
                    int radius = (int)MathHelper.Lerp(200, 1600, attempts / (float)maxAttempts);
                    int targetX = System.Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 50, Main.maxTilesX - (50 + width));
                    int targetY = Main.maxTilesY - 450 - Main.rand.Next(0, maxHeightUp);
                    return new Point(targetX, targetY);
                },
                logName: "Lost Keep",
                canPlace: rect => structures.CanPlace(rect, WorldGenUtils.AllTilesAllowed, 0));

            return search.Position;
        }

        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic keep = LostKeepSchematicAssets.Keep;
            if (keep == null)
            {
                AAMod.instance.Logger.Warn("Lost Keep schematic isn't loaded; skipping placement.");
                return false;
            }

            var options = new SchematicPlaceOptions
            {
                Anchor = SchematicAnchor.TopLeft,
                UnbreakableTiles = LostKeepSchematicAssets.UnbreakableTiles,
            };

            PlacedSchematic placed = SchematicPlacement.Place(keep, origin, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Lost Keep placement: " + warning);

            return placed.Success;
        }
    }
}