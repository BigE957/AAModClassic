using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Structures
{
    public enum SchematicAnchor
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public sealed class SchematicPlaceOptions
    {
        public SchematicAnchor Anchor { get; set; } = SchematicAnchor.TopLeft;

        public bool FlipHorizontal { get; set; }

        public HashSet<int> UnbreakableTiles { get; set; }
        public HashSet<int> UnbreakableWalls { get; set; }

        public Action<Chest> OnChest { get; set; }

        public bool SyncToClients { get; set; }
    }

    public readonly record struct PlacedMarker(string Name, Rectangle Area);

    public sealed class PlacedSchematic
    {
        public bool Success { get; internal set; }
        public Rectangle Area { get; }
        public bool Flipped { get; }
        public List<PlacedMarker> Markers { get; } = [];
        public List<string> Warnings { get; } = [];

        internal PlacedSchematic(Rectangle area, bool flipped)
        {
            Area = area;
            Flipped = flipped;
        }

        public IEnumerable<PlacedMarker> GetMarkers(string name)
        {
            foreach (PlacedMarker marker in Markers)
                if (marker.Name == name)
                    yield return marker;
        }
    }

    public static class SchematicPlacement
    {
        public static Rectangle ResolveArea(Point pos, SchematicAnchor anchor, int width, int height)
        {
            int x = pos.X;
            int y = pos.Y;

            switch (anchor)
            {
                case SchematicAnchor.TopCenter: x -= width / 2; break;
                case SchematicAnchor.TopRight: x -= width; break;
                case SchematicAnchor.CenterLeft: y -= height / 2; break;
                case SchematicAnchor.Center: x -= width / 2; y -= height / 2; break;
                case SchematicAnchor.CenterRight: x -= width; y -= height / 2; break;
                case SchematicAnchor.BottomLeft: y -= height; break;
                case SchematicAnchor.BottomCenter: x -= width / 2; y -= height; break;
                case SchematicAnchor.BottomRight: x -= width; y -= height; break;
            }

            return new Rectangle(x, y, width, height);
        }

        public static PlacedSchematic Place(ResolvedSchematic resolved, Point pos, SchematicPlaceOptions options = null)
        {
            options ??= new SchematicPlaceOptions();
            SchematicData s = resolved.Data;

            Rectangle area = ResolveArea(pos, options.Anchor, s.Width, s.Height);
            var result = new PlacedSchematic(area, options.FlipHorizontal);
            result.Warnings.AddRange(resolved.Warnings);

            if (area.X < 0 || area.Y < 0 || area.Right > Main.maxTilesX || area.Bottom > Main.maxTilesY)
            {
                result.Warnings.Add($"Placement failed: the {s.Width}x{s.Height} area at ({area.X}, {area.Y}) extends outside the world.");
                return result;
            }

            var unhandledFlipTypes = new HashSet<int>();

            ClearFootprint(s, area, options.FlipHorizontal);
            WriteCells(resolved, area, options, unhandledFlipTypes);
            if (!WorldGen.generatingWorld)
                Reframe(area);
            PlaceChests(resolved, area, options, result);
            PlaceSigns(resolved, area, options, result);
            AddMarkers(s, area, options.FlipHorizontal, result);

            foreach (int type in unhandledFlipTypes)
                result.Warnings.Add($"Tile '{TileName(type)}' has no flip handling and was mirrored as-is; check how it looks.");

            if (options.SyncToClients)
                NetMessage.SendTileSquare(-1, area.X, area.Y, area.Width, area.Height);

            result.Success = true;
            return result;
        }

        private static void ClearFootprint(SchematicData s, Rectangle area, bool flip)
        {
            for (int c = 0; c < Main.chest.Length; c++)
            {
                Chest chest = Main.chest[c];
                if (chest == null || !area.Contains(chest.x, chest.y) || IsTileKept(s, area, chest.x, chest.y, flip))
                    continue;

                Chest.DestroyChestDirect(chest.x, chest.y, c);
            }

            for (int x = area.X; x < area.Right; x++)
            {
                for (int y = area.Y; y < area.Bottom; y++)
                {
                    if (IsTileKept(s, area, x, y, flip))
                        continue;

                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile && Main.tileFrameImportant[tile.TileType])
                        WorldGen.KillTile(x, y, false, false, true);
                }
            }
        }

        private static bool IsTileKept(SchematicData s, Rectangle area, int worldX, int worldY, bool flip)
        {
            int localX = worldX - area.X;
            if (flip)
                localX = s.Width - 1 - localX;
            return SchematicCell.Has(s.Flags[s.CellIndex(localX, worldY - area.Y)], SchematicCell.KeepTile);
        }

        private static void WriteCells(ResolvedSchematic r, Rectangle area, SchematicPlaceOptions options, HashSet<int> unhandledFlipTypes)
        {
            SchematicData s = r.Data;
            bool flip = options.FlipHorizontal;

            for (int x = 0; x < s.Width; x++)
            {
                int sourceX = flip ? s.Width - 1 - x : x;

                for (int y = 0; y < s.Height; y++)
                {
                    int i = s.CellIndex(sourceX, y);
                    uint flags = s.Flags[i];

                    bool keepTile = SchematicCell.Has(flags, SchematicCell.KeepTile);
                    bool keepWall = SchematicCell.Has(flags, SchematicCell.KeepWall);
                    if (keepTile && keepWall)
                        continue;

                    int wx = area.X + x;
                    int wy = area.Y + y;
                    Tile tile = Main.tile[wx, wy];

                    if (!keepTile)
                        WriteTileLayer(r, i, flags, tile, wx, wy, options, unhandledFlipTypes);
                    if (!keepWall)
                        WriteWallLayer(r, i, flags, tile, wx, wy, options);

                    WriteWiresAndLiquid(s, i, flags, tile, wx, wy);
                }
            }
        }

        private static void WriteTileLayer(ResolvedSchematic r, int i, uint flags, Tile tile, int wx, int wy, SchematicPlaceOptions options, HashSet<int> unhandledFlipTypes)
        {
            SchematicData s = r.Data;
            int slot = s.TileIndex[i];
            int type = slot == 0 ? -1 : r.TileIds[slot];

            if (!SchematicCell.Has(flags, SchematicCell.HasTile) || type < 0)
            {
                tile.ClearTile();
                tile.TileFrameX = 0;
                tile.TileFrameY = 0;
                tile.TileColor = PaintID.None;
                tile.HasActuator = false;
                tile.IsActuated = false;
                tile.IsTileInvisible = false;
                tile.IsTileFullbright = false;
                return;
            }

            bool flip = options.FlipHorizontal;

            tile.HasTile = true;
            tile.TileType = (ushort)type;
            tile.TileFrameX = s.FrameX[i];
            tile.TileFrameY = s.FrameY[i];
            tile.TileFrameNumber = SchematicCell.GetTwoBit(flags, SchematicCell.TileFrameNumberShift);
            tile.TileColor = s.TileColor[i];

            int slope = SchematicCell.GetSlope(flags);
            if (flip && slope != 0)
                slope += slope % 2 == 0 ? -1 : 1;
            tile.Slope = (SlopeType)slope;

            tile.IsHalfBlock = SchematicCell.Has(flags, SchematicCell.HalfBlock);
            tile.HasActuator = SchematicCell.Has(flags, SchematicCell.HasActuator);
            tile.IsActuated = SchematicCell.Has(flags, SchematicCell.Actuated);
            tile.IsTileInvisible = SchematicCell.Has(flags, SchematicCell.TileInvisible);
            tile.IsTileFullbright = SchematicCell.Has(flags, SchematicCell.TileFullbright);

            if (flip)
                FlipTileFrames(tile, unhandledFlipTypes);

            if (options.UnbreakableTiles != null && options.UnbreakableTiles.Contains(type))
                TileProtectionSystem.UnbreakableTiles.Add(new(wx, wy));
        }

        private static void WriteWallLayer(ResolvedSchematic r, int i, uint flags, Tile tile, int wx, int wy, SchematicPlaceOptions options)
        {
            SchematicData s = r.Data;
            int slot = s.WallIndex[i];
            int type = slot == 0 ? 0 : Math.Max(0, r.WallIds[slot]);

            tile.WallType = (ushort)type;
            tile.WallColor = type == 0 ? (byte)0 : s.WallColor[i];
            tile.IsWallInvisible = type != 0 && SchematicCell.Has(flags, SchematicCell.WallInvisible);
            tile.IsWallFullbright = type != 0 && SchematicCell.Has(flags, SchematicCell.WallFullbright);
            tile.WallFrameNumber = SchematicCell.GetTwoBit(flags, SchematicCell.WallFrameNumberShift);

            if (type != 0 && options.UnbreakableWalls != null && options.UnbreakableWalls.Contains(type))
                TileProtectionSystem.UnbreakableWalls.Add(new(wx, wy));
        }

        private static void WriteWiresAndLiquid(SchematicData s, int i, uint flags, Tile tile, int wx, int wy)
        {
            tile.RedWire = SchematicCell.Has(flags, SchematicCell.RedWire);
            tile.BlueWire = SchematicCell.Has(flags, SchematicCell.BlueWire);
            tile.GreenWire = SchematicCell.Has(flags, SchematicCell.GreenWire);
            tile.YellowWire = SchematicCell.Has(flags, SchematicCell.YellowWire);

            byte amount = s.Liquid[i];
            tile.LiquidAmount = amount;
            tile.LiquidType = amount > 0 ? SchematicCell.GetTwoBit(flags, SchematicCell.LiquidTypeShift) : 0;

            if (amount > 0)
                Liquid.AddWater(wx, wy);
        }

        private static void FlipTileFrames(Tile tile, HashSet<int> unhandled)
        {
            int type = tile.TileType;
            if (!Main.tileFrameImportant[type])
                return;

            if (type == TileID.Pots)
            {
                tile.TileFrameX += (short)(tile.TileFrameX / 18 == 0 ? 18 : -18);
                return;
            }
            if (type == TileID.HolidayLights)
            {
                if (tile.TileFrameY / 18 == 3) tile.TileFrameY -= 18;
                else if (tile.TileFrameY / 18 == 2) tile.TileFrameY += 18;
                return;
            }
            if (type == TileID.ExposedGems)
            {
                if (tile.TileFrameY / 54 == 3) tile.TileFrameY -= 54;
                else if (tile.TileFrameY / 54 == 2) tile.TileFrameY += 54;
                return;
            }
            if (TileID.Sets.Torch[type])
            {
                int column = tile.TileFrameX / 22;
                if (column == 1) tile.TileFrameX += 22;
                else if (column == 2) tile.TileFrameX -= 22;
                return;
            }
            if (type == TileID.Trees || type == TileID.PineTree || type == TileID.Cactus)
            {
                unhandled.Add(type);
                return;
            }

            if (TileID.Sets.Platforms[type] || type == TileID.MinecartTrack)
                return;

            int style = 0, alt = 0;
            TileObjectData.GetTileInfo(tile, ref style, ref alt);
            TileObjectData data = TileObjectData.GetTileData(type, style, alt);
            if (data == null)
            {
                unhandled.Add(type);
                return;
            }

            int sheetSquare = 16 + data.CoordinatePadding;

            if (data.Width > 1)
            {
                int column = tile.TileFrameX / sheetSquare % data.Width;
                tile.TileFrameX += (short)((data.Width - 1 - 2 * column) * sheetSquare);
            }

            if (data.Direction != TileObjectDirection.None)
            {
                int range = Math.Max(1, data.RandomStyleRange);
                if (tile.TileFrameX / sheetSquare % (data.Width * data.StyleMultiplier * range) < data.Width)
                    tile.TileFrameX += (short)(sheetSquare * data.Width);
                else
                    tile.TileFrameX -= (short)(sheetSquare * data.Width);
            }
        }

        private static void Reframe(Rectangle area)
        {
            int x0 = Math.Max(0, area.X - 1);
            int y0 = Math.Max(0, area.Y - 1);
            int x1 = Math.Min(Main.maxTilesX - 1, area.Right);
            int y1 = Math.Min(Main.maxTilesY - 1, area.Bottom);

            for (int x = x0; x <= x1; x++)
            {
                for (int y = y0; y <= y1; y++)
                {
                    WorldGen.TileFrame(x, y, false, true);
                    if (Main.tile[x, y].WallType != WallID.None)
                        Framing.WallFrame(x, y);
                }
            }
        }

        private static void PlaceChests(ResolvedSchematic r, Rectangle area, SchematicPlaceOptions options, PlacedSchematic result)
        {
            SchematicData s = r.Data;

            foreach (SchematicChest exported in s.Chests)
            {
                int width = ObjectWidth(r, exported.X, exported.Y);
                int localX = options.FlipHorizontal ? s.Width - exported.X - width : exported.X;
                int wx = area.X + localX;
                int wy = area.Y + exported.Y;

                int index = Chest.CreateChest(wx, wy);
                if (index < 0)
                {
                    result.Warnings.Add($"Could not create a chest at ({wx}, {wy}).");
                    continue;
                }

                Chest chest = Main.chest[index];
                foreach (SchematicChestItem entry in exported.Items)
                {
                    if (entry.Slot >= chest.item.Length || !r.ItemIds.TryGetValue(entry.Item, out int itemType))
                        continue;

                    var item = new Item();
                    item.SetDefaults(itemType);
                    item.stack = entry.Stack;
                    if (entry.Prefix > 0)
                        item.Prefix(entry.Prefix);
                    chest.item[entry.Slot] = item;
                }

                options.OnChest?.Invoke(chest);
            }
        }

        private static void PlaceSigns(ResolvedSchematic r, Rectangle area, SchematicPlaceOptions options, PlacedSchematic result)
        {
            SchematicData s = r.Data;

            foreach (SchematicSign sign in s.Signs)
            {
                int width = ObjectWidth(r, sign.X, sign.Y);
                int localX = options.FlipHorizontal ? s.Width - sign.X - width : sign.X;
                int wx = area.X + localX;
                int wy = area.Y + sign.Y;

                int index = Sign.ReadSign(wx, wy, true);
                if (index < 0)
                {
                    result.Warnings.Add($"Could not create a sign at ({wx}, {wy}).");
                    continue;
                }
                Sign.TextSign(index, sign.Text);
            }
        }

        private static void AddMarkers(SchematicData s, Rectangle area, bool flip, PlacedSchematic result)
        {
            foreach (SchematicMarker marker in s.Markers)
            {
                int localX = flip ? s.Width - marker.X - marker.Width : marker.X;
                result.Markers.Add(new PlacedMarker(marker.Name, new Rectangle(area.X + localX, area.Y + marker.Y, marker.Width, marker.Height)));
            }
        }

        private static int ObjectWidth(ResolvedSchematic r, int localX, int localY)
        {
            SchematicData s = r.Data;
            int slot = s.TileIndex[s.CellIndex(localX, localY)];
            int type = slot == 0 ? -1 : r.TileIds[slot];
            if (type < 0)
                return 2;

            return TileObjectData.GetTileData(type, 0)?.Width ?? 2;
        }

        private static string TileName(int type) => type < TileID.Count ? TileID.Search.GetName(type) : TileLoader.GetTile(type)?.FullName ?? type.ToString();
    }
}