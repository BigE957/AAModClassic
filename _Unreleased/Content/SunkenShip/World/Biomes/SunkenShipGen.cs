using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic._CrossMod.Thorium;
using AAModClassic._Unofficial.Content.SunkenShip.___PreHardmode.Items;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic.Structures;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
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
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            ResolvedSchematic schem = unofficial ? SunkenShipSchematicAssets.Unofficial : SunkenShipSchematicAssets.Official;

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

            Rectangle? captainChest = ResolveMarkerArea(schem.Data, area, options.FlipHorizontal, "Chest_Captain");
            Rectangle? kitchenChest = unofficial ? ResolveMarkerArea(schem.Data, area, options.FlipHorizontal, "Chest_Kitchen") : null;
            Rectangle? medicalChest = unofficial ? ResolveMarkerArea(schem.Data, area, options.FlipHorizontal, "Chest_Medical") : null;

            if (!captainChest.HasValue)
                AAMod.instance.Logger.Warn("Sunken Ship: no 'Chest_Captain' marker found in the schematic.");

            options.OnChest = chest => FillChest(chest, unofficial, captainChest, kitchenChest, medicalChest);

            PlacedSchematic placed = SchematicPlacement.Place(schem, origin, options);

            foreach (string warning in placed.Warnings)
                AAMod.instance.Logger.Warn("Sunken Ship placement: " + warning);

            AAWorld_Unreleased.shipPos = new Point(newOriginX, newOriginY);

            if (unofficial)
            {
                Rectangle? soc = ResolveMarkerArea(schem.Data, area, options.FlipHorizontal, "Soul_Of_Cthulhu");
                AAWorld_Unreleased.AmbientSoCPos = soc.HasValue ? soc.Value.Center : Point.Zero;
            }

            return placed.Success;
        }

        private static Rectangle? ResolveMarkerArea(SchematicData data, Rectangle placedArea, bool flip, string markerName)
        {
            foreach (SchematicMarker marker in data.GetMarkers(markerName))
            {
                int localX = flip ? data.Width - marker.X - marker.Width : marker.X;
                return new Rectangle(placedArea.X + localX, placedArea.Y + marker.Y, marker.Width, marker.Height);
            }

            return null;
        }

        private static void FillChest(Chest chest, bool unofficial, Rectangle? captainChest, Rectangle? kitchenChest, Rectangle? medicalChest)
        {
            if (!unofficial)
            {
                int[] itemsToPlaceInSunkenChest = [ModContent.ItemType<CursedCompass>()];
                int choice = Main.rand.Next(itemsToPlaceInSunkenChest.Length);
                chest.item[0].SetDefaults(itemsToPlaceInSunkenChest[choice]);
                return;
            }

            Point pos = new(chest.x, chest.y);
            int lootPoolIndex;

            if (captainChest.HasValue && captainChest.Value.Contains(pos))
                lootPoolIndex = 0;
            else if (kitchenChest.HasValue && kitchenChest.Value.Contains(pos))
                lootPoolIndex = 1;
            else if (medicalChest.HasValue && medicalChest.Value.Contains(pos))
                lootPoolIndex = 2;
            else
                lootPoolIndex = 3;

            var lootPool = GetLootPool(lootPoolIndex);
            List<int> validIndices = Enumerable.Range(0, chest.item.Length).ToList();

            foreach (var (type, min, max) in lootPool)
            {
                int myStack = Main.rand.Next(min, max + 1);
                if (myStack == 0)
                    continue;

                int rand = Main.rand.Next(validIndices.Count);
                int myIndex = validIndices[rand];
                validIndices.RemoveAt(rand);

                chest.item[myIndex].SetDefaults(type);
                chest.item[myIndex].stack = myStack;
            }

            int webCount = Main.rand.Next(4, 8);
            for (int i = 0; i < webCount; i++)
            {
                int rand = Main.rand.Next(validIndices.Count);
                int myIndex = validIndices[rand];
                validIndices.RemoveAt(rand);

                chest.item[myIndex].SetDefaults(ItemID.Cobweb);
            }
        }

        private static List<(int type, int min, int max)> GetLootPool(int chestID)
        {
            return chestID switch
            {
                0 => [   //Captain's Quarters
                            (ModContent.ItemType<CursedCompass>(), 1, 1),
                            (ItemID.TrifoldMap, 0, 1),
                            (ItemID.Binoculars, 0, 1),
                            (ItemID.Sextant, 0, 1),
                            (ItemID.GoldBar, 8, 12),
                            (ItemID.FlintlockPistol, 1, 1),
                            (ItemID.Book, 1, 4),
                        ],
                1 => [   //Medical Ward
                            (ItemID.HealingPotion, 3, 5),
                            (ItemID.ManaPotion, 2, 4),
                            (ModContent.ItemType<ShatteredMirror>(), 1, 1),
                            (ItemID.RegenerationPotion, 0, 2),
                            (ItemID.IronskinPotion, 0, 2),
                            (ItemID.Silk, 8, 12),
                            (ItemID.LifeCrystal, 1, 2),
                        ],
                2 => [   //Kitchen
                            (ItemID.Bass, 4, 8),
                            (ItemID.Tuna, 4, 8),
                            (ItemID.Trout, 4, 8),
                            (ItemID.FruitJuice, 3, 5),
                            (ItemID.ShuckedOyster, 2, 3),
                            (ItemID.BottledWater, 12, 18),
                            (ItemID.Lemon, 3, 5),
                        ],
                _ => [   //Supplies Storage
                            (ItemID.Rope, 18, 32),
                            (ItemID.Sail, 6, 18),
                            (ItemID.Rope, 18, 32),
                            (ItemID.IronHammer, 0, 1),
                            (ItemID.IronAxe, 0, 1),
                            (ItemID.IronBar, 3, 6),
                            (ItemID.Wood, 24, 48),
                        ],
            };
        }

    }
}