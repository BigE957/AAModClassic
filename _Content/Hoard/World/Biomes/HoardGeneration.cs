using AAModClassic._Content.Desert.__Hardmode.Items.Quest;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Parthenan.__Hardmode.Items.Weapons;
using AAModClassic._Content.Snow.__Hardmode.Items.Weapons;
using AAModClassic._Content.Underground.___PreHardmode.Items.Armor;
using AAModClassic.Structures;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Hoard.World.Biomes
{
    public class HoardSchematicAssets : ModSystem
    {
        public static ResolvedSchematic Hoard { get; private set; }
        public static HashSet<int> UnbreakableTiles { get; } = [];

        public override void PostSetupContent()
        {
            SchematicData data = SchematicLoader.ReadFromMod(AAMod.instance, "Structures/Schematics/Hoard.aasch");
            Hoard = SchematicLoader.Resolve(data);

            foreach (string warning in Hoard.Warnings)
                AAMod.instance.Logger.Warn("Hoard schematic: " + warning);

            UnbreakableTiles.Add(ModContent.TileType<GreedStone_Tile>());
            UnbreakableTiles.Add(ModContent.TileType<GreedBrick_Tile>());
        }

        public override void Unload()
        {
            Hoard = null;
            UnbreakableTiles.Clear();
        }
    }

    public class HoardGeneration : MicroBiome
    {
        private static readonly Point OdinChestFallback = new(38, 67);
        private static readonly Point RomulusChestFallback = new(67, 70);
        private static readonly Point AnubisChestFallback = new(131, 48);

        private static bool ShouldAvoidLocation(Point p, bool leniant)
        {
            Tile tile = Framing.GetTileSafely(p);

            if ((!leniant && (tile.TileType == TileID.MushroomGrass || tile.TileType == TileID.JungleGrass)) ||
                tile.TileType == TileID.Sandstone ||
                tile.TileType == TileID.HardenedSand ||
                tile.TileType == TileID.SnowBlock ||
                tile.TileType == TileID.IceBlock ||
                tile.TileType == TileID.Ash ||
                tile.TileType == TileID.LihzahrdBrick ||
                tile.TileType == TileID.BlueDungeonBrick ||
                tile.TileType == TileID.GreenDungeonBrick ||
                tile.TileType == TileID.PinkDungeonBrick)
            {
                return true;
            }

            return false;
        }

        public override bool Place(Point origin, StructureMap structures)
        {
            ResolvedSchematic hoard = HoardSchematicAssets.Hoard;
            if (hoard == null)
            {
                AAMod.instance.Logger.Warn("Hoard schematic isn't loaded!");
                return false;
            }

            int width = hoard.Data.Width;
            int height = hoard.Data.Height;

            int attempts = 0;
            int maxAttempts = 5000;
            Point placementPoint = origin;
            bool placementSucceeded = false;

            Point bestPlacementPoint = origin;
            int bestInvalidTiles = int.MaxValue;

            do
            {
                int invalidTiles = 0;
                bool tooManyInvalid = false;

                for (int x = placementPoint.X; x < placementPoint.X + width && !tooManyInvalid; x++)
                {
                    for (int y = placementPoint.Y; y < placementPoint.Y + height && !tooManyInvalid; y++)
                    {
                        if (ShouldAvoidLocation(new Point(x, y), attempts > 2500))
                        {
                            invalidTiles++;
                            if (bestInvalidTiles != int.MaxValue && invalidTiles >= bestInvalidTiles)
                                tooManyInvalid = true;
                        }
                    }
                }

                if (!tooManyInvalid)
                {
                    if (invalidTiles == 0 && structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, width, height)))
                    {
                        AAMod.instance.Logger.Info("Hoard successfully placed after " + attempts + " attempts.");
                        origin = placementPoint;
                        placementSucceeded = true;
                        break;
                    }

                    if (invalidTiles < bestInvalidTiles)
                    {
                        bestInvalidTiles = invalidTiles;
                        bestPlacementPoint = placementPoint;
                    }
                }

                placementPoint = origin + new Point(WorldGen.genRand.Next(-1000, 600), WorldGen.genRand.Next(-200, 300));
            }
            while (attempts++ < maxAttempts);

            if (!placementSucceeded)
            {
                AAMod.instance.Logger.Warn($"Hoard placement failed after {maxAttempts} attempts. Falling back to best candidate with {bestInvalidTiles} invalid tiles.");
                origin = bestPlacementPoint;
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, width, height), 20);

            Point odinChest = ResolveNamedPosition(hoard, origin, "OdinChest", OdinChestFallback);
            Point romulusChest = ResolveNamedPosition(hoard, origin, "RomulusChest", RomulusChestFallback);
            Point anubisChest = ResolveNamedPosition(hoard, origin, "AnubisChest", AnubisChestFallback);

            var options = new SchematicPlaceOptions
            {
                Anchor = SchematicAnchor.TopLeft,
                UnbreakableTiles = HoardSchematicAssets.UnbreakableTiles,
                OnChest = chest => FillHoardChest(chest, odinChest, romulusChest, anubisChest),
            };

            PlacedSchematic placed = SchematicPlacement.Place(hoard, origin, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Hoard placement: " + warning);

            return placed.Success;
        }

        private static Point ResolveNamedPosition(ResolvedSchematic hoard, Point origin, string markerName, Point fallbackLocal)
        {
            foreach (SchematicMarker marker in hoard.Data.GetMarkers(markerName))
                return origin + new Point(marker.X, marker.Y);

            return origin + fallbackLocal;
        }

        private static readonly int[] GreedChestLoot =
        [
            ItemID.GoldenChair,
            ItemID.GoldenToilet,
            ItemID.GoldenDoor,
            ItemID.GoldenTable,
            ItemID.GoldenBed,
            ItemID.GoldenPiano,
            ItemID.GoldenDresser,
            ItemID.GoldenSofa,
            ItemID.GoldenSink,
            ItemID.GoldenBathtub,
            ItemID.GoldenClock,
            ItemID.GoldenLamp,
            ItemID.GoldenBookcase,
            ItemID.GoldenChandelier,
            ItemID.GoldenLantern,
            ItemID.GoldenCandelabra,
            ItemID.GoldenCandle,
            ItemID.GoldenChest,
            ItemID.GoldenWorkbench,
            ItemID.GoldWatch,
            ItemID.GoldDust,
            ItemID.AncientGoldHelmet,
            ItemID.GoldBunny,
            ItemID.GoldButterfly,
            ItemID.GoldFrog,
            ItemID.GoldGrasshopper,
            ItemID.SquirrelGold,
            ItemID.GoldBird,
            ItemID.GoldMouse,
            ItemID.GoldWorm,
            ItemID.GoldCrown,
            ItemID.GoldenKey,
            ItemID.Goldfish,
            ItemID.ReflectiveGoldDye,
            ItemID.GoldGreaves,
            ItemID.GoldHelmet,
            ItemID.FindingGold,
            ItemID.GoldChainmail,
            ItemID.GoldShortsword,
            ItemID.GoldBroadsword,
            ItemID.GoldBow,
            ItemID.GoldHammer,
            ItemID.GoldPickaxe,
            ItemID.GoldenCrate
        ];

        private static readonly int[] Loot =
        [
            ItemID.CoinGun,
            ItemID.Cutlass,
            ItemID.DiscountCard,
            ItemID.GoldRing,
            ItemID.LuckyCoin,
        ];

        private static readonly int[] Loot2 =
        [
            ModContent.ItemType<AncientGoldChestplate>(),
            ModContent.ItemType<AncientGoldLeggings>(),
        ];

        private static void FillHoardChest(Chest chest, Point odinChest, Point romulusChest, Point anubisChest)
        {
            Point pos = new(chest.x, chest.y);

            int type;
            if (pos == odinChest)
                type = ModContent.ItemType<OdinsBlade>();
            else if (pos == romulusChest)
                type = ModContent.ItemType<RomulusTazesaber>();
            else if (pos == anubisChest)
                type = ModContent.ItemType<TheLifeAndEpicAdventuresOfAnubisTheWonderDog>();
            else if (WorldGen.genRand.Next(100) < 2f)
                type = Utils.Next(WorldGen.genRand, Loot2);
            else
                type = Utils.Next(WorldGen.genRand, Loot);

            chest.item[0].SetDefaults(type, false);

            chest.item[1].SetDefaults(ItemID.GoldBar);
            chest.item[1].stack = WorldGen.genRand.Next(70, 90);

            chest.item[2].SetDefaults(ItemID.FlaskofGold);
            chest.item[2].stack = WorldGen.genRand.Next(1, 4);

            chest.item[3].SetDefaults(ItemID.GoldCoin, false);
            chest.item[3].stack = WorldGen.genRand.Next(70, 90);

            for (int i = 0; i < 20; i++)
            {
                chest.item[i + 4].SetDefaults(Utils.Next(WorldGen.genRand, GreedChestLoot));
                if (chest.item[i + 4].maxStack > 1)
                    chest.item[i + 4].stack = WorldGen.genRand.Next(1, 3);
            }

            NetMessage.SendObjectPlacement(-1, chest.x, chest.y, ModContent.TileType<GreedChest_Tile>(), 1, 0, -1, -1);
        }
    }
}