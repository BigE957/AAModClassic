#if DEBUG
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Structures.Tools
{
    public static class SchematicExporter
    {
        private static string _outputDirectory;

        public static string OutputDirectory
        {
            get => _outputDirectory ??= Path.Combine(Main.SavePath, "AAModClassic", "Schematics");
            set => _outputDirectory = value;
        }

        public static string ExportToFile(SchematicEditSession session, List<string> warnings)
        {
            SchematicData data = Export(session, warnings);

            string defaultFileName = SanitizeFileName(session.Name) + ".aasch";
            string path = Path.Combine(OutputDirectory, defaultFileName);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                path = PromptWindowsSavePath(path) ?? path;
            }

            string targetDirectory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            using (FileStream stream = File.Create(path))
                SchematicIO.Write(stream, data);

            return path;
        }

        private static string PromptWindowsSavePath(string defaultPath)
        {
            var ofn = new OpenFileName
            {
                lStructSize = Marshal.SizeOf(typeof(OpenFileName)),
                lpstrFilter = "AA Schematic (*.aasch)\0*.aasch\0All Files (*.*)\0*.*\0",
                lpstrFile = Path.GetFileName(defaultPath) + new string('\0', 256),
                nMaxFile = 260,
                lpstrInitialDir = Path.GetDirectoryName(defaultPath),
                lpstrTitle = "Save Schematic As...",
                lpstrDefExt = "aasch",
                Flags = 0x00000002 | 0x00080000 // OFN_OVERWRITEPROMPT | OFN_EXPLORER
            };

            if (GetSaveFileName(ofn))
            {
                return ofn.lpstrFile;
            }

            return null;
        }

        #region Native Win32 Dialog Imports
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class OpenFileName
        {
            public int lStructSize = 0;
            public IntPtr hwndOwner = IntPtr.Zero;
            public IntPtr hInstance = IntPtr.Zero;
            public string lpstrFilter = null;
            public string lpstrCustomFilter = null;
            public int nMaxCustFilter = 0;
            public int nFilterIndex = 0;
            public string lpstrFile = null;
            public int nMaxFile = 0;
            public string lpstrFileTitle = null;
            public int nMaxFileTitle = 0;
            public string lpstrInitialDir = null;
            public string lpstrTitle = null;
            public int Flags = 0;
            public short nFileOffset = 0;
            public short nFileExtension = 0;
            public string lpstrDefExt = null;
            public IntPtr lCustData = IntPtr.Zero;
            public IntPtr lpfnHook = IntPtr.Zero;
            public string lpTemplateName = null;
            public IntPtr pvReserved = IntPtr.Zero;
            public int dwReserved = 0;
            public int FlagsEx = 0;
        }

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
        #endregion

        public static SchematicData Export(SchematicEditSession session, List<string> warnings)
        {
            if (session.Phase != SchematicToolPhase.Editing)
                throw new InvalidOperationException("There is no selected region to export.");

            Rectangle r = session.Region;
            if (r.X < 0 || r.Y < 0 || r.Right > Main.maxTilesX || r.Bottom > Main.maxTilesY)
                throw new InvalidOperationException("The selected region extends outside the world.");

            var data = new SchematicData(r.Width, r.Height);
            var warnedTiles = new HashSet<int>();
            var warnedWalls = new HashSet<int>();

            for (int x = 0; x < r.Width; x++)
            {
                for (int y = 0; y < r.Height; y++)
                {
                    int wx = r.X + x;
                    int wy = r.Y + y;
                    int i = data.CellIndex(x, y);

                    bool keepTile = session.KeepTiles[wx, wy];
                    bool keepWall = session.KeepWalls[wx, wy];

                    uint flags = 0;
                    if (keepTile)
                        flags |= SchematicCell.KeepTile;
                    if (keepWall)
                        flags |= SchematicCell.KeepWall;

                    if (!(keepTile && keepWall))
                    {
                        Tile tile = Main.tile[wx, wy];

                        if (!keepTile)
                            flags = ExportTileLayer(data, i, tile, flags, warnedTiles, warnings);
                        if (!keepWall)
                            flags = ExportWallLayer(data, i, tile, flags, warnedWalls, warnings);

                        flags = ExportWiresAndLiquid(data, i, tile, flags);
                    }

                    data.Flags[i] = flags;
                }
            }

            ExportMarkers(session, data, warnings);
            ExportChests(session, data, warnings);
            ExportSigns(session, data);
            CheckBoundary(session, warnings);

            return data;
        }

        #region Cell layers
        private static uint ExportTileLayer(SchematicData data, int i, Tile tile, uint flags, HashSet<int> warned, List<string> warnings)
        {
            if (!tile.HasTile)
                return flags;

            string name = TileName(tile.TileType);
            if (name == null)
            {
                if (warned.Add(tile.TileType))
                    warnings.Add($"Tile type {tile.TileType} has no resolvable name; those tiles were skipped.");
                return flags;
            }

            flags |= SchematicCell.HasTile;
            data.TileIndex[i] = data.Tiles.GetOrAdd(name);
            data.FrameX[i] = tile.TileFrameX;
            data.FrameY[i] = tile.TileFrameY;
            data.TileColor[i] = tile.TileColor;

            flags = SchematicCell.WithSlope(flags, (int)tile.Slope);
            flags = SchematicCell.WithTwoBit(flags, SchematicCell.TileFrameNumberShift, tile.TileFrameNumber);

            if (tile.IsHalfBlock) flags |= SchematicCell.HalfBlock;
            if (tile.HasActuator) flags |= SchematicCell.HasActuator;
            if (tile.IsActuated) flags |= SchematicCell.Actuated;
            if (tile.IsTileInvisible) flags |= SchematicCell.TileInvisible;
            if (tile.IsTileFullbright) flags |= SchematicCell.TileFullbright;

            return flags;
        }

        private static uint ExportWallLayer(SchematicData data, int i, Tile tile, uint flags, HashSet<int> warned, List<string> warnings)
        {
            if (tile.WallType == WallID.None)
                return flags;

            string name = WallName(tile.WallType);
            if (name == null)
            {
                if (warned.Add(tile.WallType))
                    warnings.Add($"Wall type {tile.WallType} has no resolvable name; those walls were skipped.");
                return flags;
            }

            data.WallIndex[i] = data.Walls.GetOrAdd(name);
            data.WallColor[i] = tile.WallColor;
            flags = SchematicCell.WithTwoBit(flags, SchematicCell.WallFrameNumberShift, tile.WallFrameNumber);

            if (tile.IsWallInvisible) flags |= SchematicCell.WallInvisible;
            if (tile.IsWallFullbright) flags |= SchematicCell.WallFullbright;

            return flags;
        }

        private static uint ExportWiresAndLiquid(SchematicData data, int i, Tile tile, uint flags)
        {
            if (tile.RedWire) flags |= SchematicCell.RedWire;
            if (tile.BlueWire) flags |= SchematicCell.BlueWire;
            if (tile.GreenWire) flags |= SchematicCell.GreenWire;
            if (tile.YellowWire) flags |= SchematicCell.YellowWire;

            if (tile.LiquidAmount > 0)
            {
                data.Liquid[i] = tile.LiquidAmount;
                flags = SchematicCell.WithTwoBit(flags, SchematicCell.LiquidTypeShift, tile.LiquidType);
            }

            return flags;
        }
        #endregion

        #region Markers, chests, signs
        private static void ExportMarkers(SchematicEditSession session, SchematicData data, List<string> warnings)
        {
            Rectangle r = session.Region;

            foreach (SessionMarker marker in session.Markers)
            {
                Rectangle clipped = Rectangle.Intersect(marker.Area, r);
                if (clipped.IsEmpty)
                {
                    warnings.Add($"Marker '{marker.Name}' lies outside the region and was skipped.");
                    continue;
                }
                if (clipped != marker.Area)
                    warnings.Add($"Marker '{marker.Name}' extends past the region and was clipped.");

                data.Markers.Add(new SchematicMarker(marker.Name, clipped.X - r.X, clipped.Y - r.Y, clipped.Width, clipped.Height));
            }
        }

        private static void ExportChests(SchematicEditSession session, SchematicData data, List<string> warnings)
        {
            Rectangle r = session.Region;

            for (int c = 0; c < Main.chest.Length; c++)
            {
                Chest chest = Main.chest[c];
                if (chest == null || !r.Contains(chest.x, chest.y) || session.KeepTiles[chest.x, chest.y])
                    continue;

                var exported = new SchematicChest { X = chest.x - r.X, Y = chest.y - r.Y };

                for (int slot = 0; slot < chest.item.Length && slot <= byte.MaxValue; slot++)
                {
                    Item item = chest.item[slot];
                    if (item == null || item.IsAir)
                        continue;

                    string name = ItemName(item.type);
                    if (name == null)
                    {
                        warnings.Add($"Item type {item.type} in chest at ({chest.x}, {chest.y}) has no resolvable name and was skipped.");
                        continue;
                    }

                    exported.Items.Add(new SchematicChestItem(slot, name, item.stack, item.prefix));
                }

                data.Chests.Add(exported);
            }
        }

        private static void ExportSigns(SchematicEditSession session, SchematicData data)
        {
            Rectangle r = session.Region;

            for (int s = 0; s < Main.sign.Length; s++)
            {
                Sign sign = Main.sign[s];
                if (sign == null || !r.Contains(sign.x, sign.y) || session.KeepTiles[sign.x, sign.y])
                    continue;

                Tile tile = Main.tile[sign.x, sign.y];
                if (!tile.HasTile || !Main.tileSign[tile.TileType])
                    continue;

                data.Signs.Add(new SchematicSign(sign.x - r.X, sign.y - r.Y, sign.text ?? string.Empty));
            }
        }
        #endregion

        #region Validation
        private static void CheckBoundary(SchematicEditSession session, List<string> warnings)
        {
            Rectangle r = session.Region;
            var seenOrigins = new HashSet<long>();
            bool warnedGrowth = false;

            foreach (Point p in Perimeter(r))
            {
                if (session.KeepTiles[p.X, p.Y])
                    continue;

                Tile tile = Main.tile[p.X, p.Y];
                if (!tile.HasTile)
                    continue;

                if (!warnedGrowth && (tile.TileType == TileID.Trees || tile.TileType == TileID.PineTree || tile.TileType == TileID.Cactus))
                {
                    warnings.Add($"A tree or cactus touches the region edge near ({p.X}, {p.Y}); it may be exported partially.");
                    warnedGrowth = true;
                }

                TileObjectData objectData = TileObjectData.GetTileData(tile);
                if (objectData == null || (objectData.Width == 1 && objectData.Height == 1))
                    continue;

                int sheet = 16 + objectData.CoordinatePadding;
                int originX = p.X - tile.TileFrameX / sheet % objectData.Width;
                int originY = p.Y - tile.TileFrameY / sheet % objectData.Height;

                if (!seenOrigins.Add(((long)originX << 32) | (uint)originY))
                    continue;

                var footprint = new Rectangle(originX, originY, objectData.Width, objectData.Height);
                if (!r.Contains(footprint))
                    warnings.Add($"A multi-tile object ({TileName(tile.TileType) ?? "unknown"}) at ({originX}, {originY}) crosses the region edge.");
            }
        }

        private static IEnumerable<Point> Perimeter(Rectangle r)
        {
            for (int x = 0; x < r.Width; x++)
            {
                yield return new Point(r.X + x, r.Y);
                if (r.Height > 1)
                    yield return new Point(r.X + x, r.Bottom - 1);
            }

            for (int y = 1; y < r.Height - 1; y++)
            {
                yield return new Point(r.X, r.Y + y);
                if (r.Width > 1)
                    yield return new Point(r.Right - 1, r.Y + y);
            }
        }
        #endregion

        #region Names
        private static string TileName(int type)
        {
            if (type < TileID.Count)
                return "Terraria/" + TileID.Search.GetName(type);
            return TileLoader.GetTile(type)?.FullName;
        }

        private static string WallName(int type)
        {
            if (type < WallID.Count)
                return "Terraria/" + WallID.Search.GetName(type);
            return WallLoader.GetWall(type)?.FullName;
        }

        private static string ItemName(int type)
        {
            if (type < ItemID.Count)
                return "Terraria/" + ItemID.Search.GetName(type);
            return ItemLoader.GetItem(type)?.FullName;
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Untitled";

            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name.Trim();
        }
        #endregion

        public static SchematicData Capture(Action build, Point origin, int width, int height, ushort dummyTile, ushort dummyWall, List<string> warnings = null)
        {
            ArgumentNullException.ThrowIfNull(build);
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Capture dimensions must be positive.");
            if (origin.X < 0 || origin.Y < 0 || origin.X + width > Main.maxTilesX || origin.Y + height > Main.maxTilesY)
                throw new ArgumentException("Capture area extends outside the world.");
            if (Main.netMode != NetmodeID.SinglePlayer)
                throw new InvalidOperationException("Schematic capture can only run in single-player.");

            warnings ??= [];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile t = Main.tile[origin.X + x, origin.Y + y];
                    t.ClearEverything();
                    t.HasTile = true;
                    t.TileType = dummyTile;
                    t.WallType = dummyWall;
                }
            }

            for (int x = origin.X; x < origin.X + width; x++)
                for (int y = origin.Y; y < origin.Y + height; y++)
                    WorldGen.TileFrame(x, y, false, false);

            build();

            var data = new SchematicData(width, height);
            var warnedTiles = new HashSet<int>();
            var warnedWalls = new HashSet<int>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int i = data.CellIndex(x, y);
                    Tile tile = Main.tile[origin.X + x, origin.Y + y];

                    bool untouchedTile = tile.HasTile && tile.TileType == dummyTile;
                    bool untouchedWall = tile.WallType == dummyWall;

                    uint flags = 0;
                    if (untouchedTile) flags |= SchematicCell.KeepTile;
                    if (untouchedWall) flags |= SchematicCell.KeepWall;

                    if (!(untouchedTile && untouchedWall))
                    {
                        if (!untouchedTile)
                            flags = ExportTileLayer(data, i, tile, flags, warnedTiles, warnings);
                        if (!untouchedWall)
                            flags = ExportWallLayer(data, i, tile, flags, warnedWalls, warnings);
                        flags = ExportWiresAndLiquid(data, i, tile, flags);
                    }

                    data.Flags[i] = flags;
                }
            }

            return data;
        }

        public static string CaptureToFile(Action build, Point origin, int width, int height, ushort dummyTile, ushort dummyWall, string fileName, List<string> warnings = null)
        {
            SchematicData data = Capture(build, origin, width, height, dummyTile, dummyWall, warnings);

            Directory.CreateDirectory(OutputDirectory);
            string path = Path.Combine(OutputDirectory, SanitizeFileName(fileName) + ".aasch");
            using (FileStream stream = File.Create(path))
                SchematicIO.Write(stream, data);
            return path;
        }
    }
}
#endif