using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AAModClassic.Structures
{
    public static class SchematicCell
    {
        public const uint SlopeMask = 0b111;
        public const uint HasTile = 1u << 3;
        public const uint HalfBlock = 1u << 4;
        public const uint HasActuator = 1u << 5;
        public const uint Actuated = 1u << 6;
        public const uint RedWire = 1u << 7;
        public const uint BlueWire = 1u << 8;
        public const uint GreenWire = 1u << 9;
        public const uint YellowWire = 1u << 10;
        public const uint TileInvisible = 1u << 11;
        public const uint TileFullbright = 1u << 12;
        public const uint WallInvisible = 1u << 13;
        public const uint WallFullbright = 1u << 14;
        public const uint KeepTile = 1u << 15;
        public const uint KeepWall = 1u << 16;
        public const int TileFrameNumberShift = 17;
        public const int WallFrameNumberShift = 19;
        public const int LiquidTypeShift = 21;
        public const uint Protected = 1u << 23;

        public static bool Has(uint flags, uint bit) => (flags & bit) != 0;
        public static uint Set(uint flags, uint bit, bool on) => on ? flags | bit : flags & ~bit;

        public static int GetSlope(uint flags) => (int)(flags & SlopeMask);
        public static uint WithSlope(uint flags, int slope) => (flags & ~SlopeMask) | ((uint)slope & SlopeMask);

        public static int GetTwoBit(uint flags, int shift) => (int)((flags >> shift) & 0b11u);
        public static uint WithTwoBit(uint flags, int shift, int value) => (flags & ~(0b11u << shift)) | (((uint)value & 0b11u) << shift);
    }

    public sealed record SchematicMarker(string Name, int X, int Y, int Width = 1, int Height = 1);

    public sealed record SchematicChestItem(int Slot, string Item, int Stack, int Prefix);

    public sealed class SchematicChest
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<SchematicChestItem> Items { get; } = [];
    }

    public sealed record SchematicSign(int X, int Y, string Text);

    public sealed record SchematicChunk(string Tag, byte[] Data);

    public sealed class SchematicPalette
    {
        private readonly List<string> _names = [string.Empty];
        private readonly Dictionary<string, ushort> _lookup = [];

        public int Count => _names.Count;

        public string this[int index] => _names[index];

        public ushort GetOrAdd(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;
            if (_lookup.TryGetValue(name, out ushort existing))
                return existing;
            if (_names.Count >= ushort.MaxValue)
                throw new InvalidOperationException("Schematic palette is full.");

            ushort index = (ushort)_names.Count;
            _names.Add(name);
            _lookup[name] = index;
            return index;
        }

        internal void Write(BinaryWriter w)
        {
            w.Write((ushort)(_names.Count - 1));
            for (int i = 1; i < _names.Count; i++)
                w.Write(_names[i]);
        }

        internal void Read(BinaryReader r)
        {
            int count = r.ReadUInt16();
            for (int i = 0; i < count; i++)
            {
                string name = r.ReadString();
                if (string.IsNullOrEmpty(name) || _lookup.ContainsKey(name))
                    throw new InvalidDataException("Schematic palette contains an empty or duplicate name.");
                GetOrAdd(name);
            }
        }
    }

    public sealed class SchematicData
    {
        public const int CurrentVersion = 1;
        private const long MaxCells = 25_000_000;

        public int Width { get; }
        public int Height { get; }

        public SchematicPalette Tiles { get; } = new();
        public SchematicPalette Walls { get; } = new();

        public uint[] Flags { get; }
        public ushort[] TileIndex { get; }
        public ushort[] WallIndex { get; }
        public short[] FrameX { get; }
        public short[] FrameY { get; }
        public byte[] TileColor { get; }
        public byte[] WallColor { get; }
        public byte[] Liquid { get; }

        public List<SchematicMarker> Markers { get; } = [];
        public List<SchematicChest> Chests { get; } = [];
        public List<SchematicSign> Signs { get; } = [];
        public List<SchematicChunk> UnknownChunks { get; } = [];

        public SchematicData(int width, int height)
        {
            if (width <= 0 || height <= 0 || width > ushort.MaxValue || height > ushort.MaxValue || (long)width * height > MaxCells)
                throw new ArgumentOutOfRangeException(nameof(width), $"Invalid schematic size {width}x{height}.");

            Width = width;
            Height = height;
            int n = width * height;
            Flags = new uint[n];
            TileIndex = new ushort[n];
            WallIndex = new ushort[n];
            FrameX = new short[n];
            FrameY = new short[n];
            TileColor = new byte[n];
            WallColor = new byte[n];
            Liquid = new byte[n];
        }

        public int CellCount => Width * Height;

        public int CellIndex(int x, int y) => x * Height + y;

        public IEnumerable<SchematicMarker> GetMarkers(string name)
        {
            foreach (SchematicMarker marker in Markers)
                if (marker.Name == name)
                    yield return marker;
        }

        internal void Validate()
        {
            for (int i = 0; i < Flags.Length; i++)
            {
                if (TileIndex[i] >= Tiles.Count)
                    throw new InvalidDataException($"Cell {i} uses tile palette slot {TileIndex[i]}, but the palette only has {Tiles.Count - 1} entries.");
                if (WallIndex[i] >= Walls.Count)
                    throw new InvalidDataException($"Cell {i} uses wall palette slot {WallIndex[i]}, but the palette only has {Walls.Count - 1} entries.");
            }

            foreach (SchematicMarker m in Markers)
                CheckInBounds(m.X, m.Y, $"Marker '{m.Name}'");
            foreach (SchematicChest c in Chests)
                CheckInBounds(c.X, c.Y, "Chest");
            foreach (SchematicSign sign in Signs)
                CheckInBounds(sign.X, sign.Y, "Sign");
        }

        private void CheckInBounds(int x, int y, string what)
        {
            if ((uint)x >= (uint)Width || (uint)y >= (uint)Height)
                throw new InvalidDataException($"{what} at ({x}, {y}) lies outside the {Width}x{Height} schematic.");
        }
    }

    public static class SchematicIO
    {
        private static readonly byte[] Magic = [(byte)'A', (byte)'A', (byte)'S', (byte)'C'];
        private const ushort HeaderFlagDeflate = 1;
        private const int MaxChunkBytes = 64 * 1024 * 1024;

        private const string TagMarkers = "MARK";
        private const string TagChests = "CHST";
        private const string TagSigns = "SIGN";

        #region Write
        public static void Write(Stream output, SchematicData s)
        {
            using var header = new BinaryWriter(output, Encoding.UTF8, leaveOpen: true);
            header.Write(Magic);
            header.Write((ushort)SchematicData.CurrentVersion);
            header.Write(HeaderFlagDeflate);
            header.Write((ushort)s.Width);
            header.Write((ushort)s.Height);
            header.Flush();

            using var deflate = new DeflateStream(output, CompressionLevel.Optimal, leaveOpen: true);
            using var w = new BinaryWriter(deflate, Encoding.UTF8, leaveOpen: true);
            WritePayload(w, s);
            w.Flush();
        }

        private static void WritePayload(BinaryWriter w, SchematicData s)
        {
            s.Tiles.Write(w);
            s.Walls.Write(w);

            WritePlane(w, s.Flags);
            WritePlane(w, s.TileIndex);
            WritePlane(w, s.WallIndex);
            WritePlane(w, s.FrameX);
            WritePlane(w, s.FrameY);
            w.Write(s.TileColor);
            w.Write(s.WallColor);
            w.Write(s.Liquid);

            List<SchematicChunk> chunks = BuildChunks(s);
            w.Write((ushort)chunks.Count);
            foreach (SchematicChunk chunk in chunks)
            {
                w.Write(Encoding.ASCII.GetBytes(chunk.Tag));
                w.Write(chunk.Data.Length);
                w.Write(chunk.Data);
            }
        }

        private static List<SchematicChunk> BuildChunks(SchematicData s)
        {
            var chunks = new List<SchematicChunk>();

            if (s.Markers.Count > 0)
            {
                chunks.Add(new SchematicChunk(TagMarkers, Build(w =>
                {
                    w.Write((ushort)s.Markers.Count);
                    foreach (SchematicMarker m in s.Markers)
                    {
                        w.Write(m.Name);
                        w.Write((ushort)m.X);
                        w.Write((ushort)m.Y);
                        w.Write((ushort)m.Width);
                        w.Write((ushort)m.Height);
                    }
                })));
            }

            if (s.Chests.Count > 0)
            {
                chunks.Add(new SchematicChunk(TagChests, Build(w =>
                {
                    w.Write((ushort)s.Chests.Count);
                    foreach (SchematicChest chest in s.Chests)
                    {
                        w.Write((ushort)chest.X);
                        w.Write((ushort)chest.Y);
                        w.Write((byte)chest.Items.Count);
                        foreach (SchematicChestItem item in chest.Items)
                        {
                            w.Write((byte)item.Slot);
                            w.Write(item.Item);
                            w.Write((ushort)item.Stack);
                            w.Write((byte)item.Prefix);
                        }
                    }
                })));
            }

            if (s.Signs.Count > 0)
            {
                chunks.Add(new SchematicChunk(TagSigns, Build(w =>
                {
                    w.Write((ushort)s.Signs.Count);
                    foreach (SchematicSign sign in s.Signs)
                    {
                        w.Write((ushort)sign.X);
                        w.Write((ushort)sign.Y);
                        w.Write(sign.Text ?? string.Empty);
                    }
                })));
            }

            chunks.AddRange(s.UnknownChunks);
            return chunks;
        }

        private static byte[] Build(Action<BinaryWriter> body)
        {
            using var ms = new MemoryStream();
            using (var w = new BinaryWriter(ms, Encoding.UTF8, leaveOpen: true))
                body(w);
            return ms.ToArray();
        }

        private static void WritePlane(BinaryWriter w, uint[] plane) => WriteBlock(w, plane, sizeof(uint));
        private static void WritePlane(BinaryWriter w, ushort[] plane) => WriteBlock(w, plane, sizeof(ushort));
        private static void WritePlane(BinaryWriter w, short[] plane) => WriteBlock(w, plane, sizeof(short));

        private static void WriteBlock(BinaryWriter w, Array plane, int elementSize)
        {
            byte[] bytes = new byte[plane.Length * elementSize];
            Buffer.BlockCopy(plane, 0, bytes, 0, bytes.Length);
            w.Write(bytes);
        }
        #endregion

        #region Read
        public static SchematicData Read(Stream input)
        {
            using var header = new BinaryReader(input, Encoding.UTF8, leaveOpen: true);

            byte[] magic = header.ReadBytes(4);
            if (magic.Length != 4 || !magic.AsSpan().SequenceEqual(Magic))
                throw new InvalidDataException("Not a schematic file (bad magic).");

            int version = header.ReadUInt16();
            if (version == 0 || version > SchematicData.CurrentVersion)
                throw new InvalidDataException($"Unsupported schematic version {version}; this build reads up to {SchematicData.CurrentVersion}.");

            ushort headerFlags = header.ReadUInt16();
            int width = header.ReadUInt16();
            int height = header.ReadUInt16();
            if (width == 0 || height == 0)
                throw new InvalidDataException("Schematic has a zero dimension.");

            var s = new SchematicData(width, height);

            using var payload = new MemoryStream();
            if ((headerFlags & HeaderFlagDeflate) != 0)
            {
                using var deflate = new DeflateStream(input, CompressionMode.Decompress, leaveOpen: true);
                deflate.CopyTo(payload);
            }
            else
                input.CopyTo(payload);

            payload.Position = 0;

            using var r = new BinaryReader(payload, Encoding.UTF8, leaveOpen: true);
            ReadPayload(r, s);

            s.Validate();
            return s;
        }

        private static void ReadPayload(BinaryReader r, SchematicData s)
        {
            s.Tiles.Read(r);
            s.Walls.Read(r);

            ReadPlane(r, s.Flags);
            ReadPlane(r, s.TileIndex);
            ReadPlane(r, s.WallIndex);
            ReadPlane(r, s.FrameX);
            ReadPlane(r, s.FrameY);
            ReadPlane(r, s.TileColor);
            ReadPlane(r, s.WallColor);
            ReadPlane(r, s.Liquid);

            int chunkCount = r.ReadUInt16();
            for (int i = 0; i < chunkCount; i++)
            {
                byte[] tagBytes = r.ReadBytes(4);
                if (tagBytes.Length != 4)
                    throw new InvalidDataException("Unexpected end of schematic data.");
                string tag = Encoding.ASCII.GetString(tagBytes);

                int length = r.ReadInt32();
                if (length < 0 || length > MaxChunkBytes)
                    throw new InvalidDataException($"Chunk '{tag}' has an invalid length ({length}).");

                byte[] data = r.ReadBytes(length);
                if (data.Length != length)
                    throw new InvalidDataException("Unexpected end of schematic data.");

                ReadChunk(s, tag, data);
            }
        }

        private static void ReadChunk(SchematicData s, string tag, byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var r = new BinaryReader(ms, Encoding.UTF8);

            switch (tag)
            {
                case TagMarkers:
                    {
                        int count = r.ReadUInt16();
                        for (int i = 0; i < count; i++)
                            s.Markers.Add(new SchematicMarker(r.ReadString(), r.ReadUInt16(), r.ReadUInt16(), r.ReadUInt16(), r.ReadUInt16()));
                        break;
                    }
                case TagChests:
                    {
                        int count = r.ReadUInt16();
                        for (int i = 0; i < count; i++)
                        {
                            var chest = new SchematicChest { X = r.ReadUInt16(), Y = r.ReadUInt16() };
                            int itemCount = r.ReadByte();
                            for (int j = 0; j < itemCount; j++)
                                chest.Items.Add(new SchematicChestItem(r.ReadByte(), r.ReadString(), r.ReadUInt16(), r.ReadByte()));
                            s.Chests.Add(chest);
                        }
                        break;
                    }
                case TagSigns:
                    {
                        int count = r.ReadUInt16();
                        for (int i = 0; i < count; i++)
                            s.Signs.Add(new SchematicSign(r.ReadUInt16(), r.ReadUInt16(), r.ReadString()));
                        break;
                    }
                default:
                    s.UnknownChunks.Add(new SchematicChunk(tag, data));
                    break;
            }
        }

        private static void ReadPlane(BinaryReader r, uint[] plane) => ReadBlock(r, plane, sizeof(uint));
        private static void ReadPlane(BinaryReader r, ushort[] plane) => ReadBlock(r, plane, sizeof(ushort));
        private static void ReadPlane(BinaryReader r, short[] plane) => ReadBlock(r, plane, sizeof(short));
        private static void ReadPlane(BinaryReader r, byte[] plane) => ReadBlock(r, plane, sizeof(byte));

        private static void ReadBlock(BinaryReader r, Array plane, int elementSize)
        {
            int byteCount = plane.Length * elementSize;
            byte[] bytes = r.ReadBytes(byteCount);
            if (bytes.Length != byteCount)
                throw new InvalidDataException("Unexpected end of schematic data.");
            Buffer.BlockCopy(bytes, 0, plane, 0, byteCount);
        }
        #endregion
    }
}