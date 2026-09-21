#if DEBUG
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AAModClassic.Structures.Tools
{
    public enum SchematicToolPhase
    {
        Idle,
        AwaitingSecondCorner,
        Editing
    }

    public sealed record SessionMarker(string Name, Rectangle Area);

    public sealed class CellMask
    {
        private readonly Dictionary<long, ulong> _chunks = [];

        private static long Key(int x, int y) => ((long)(x >> 3) << 32) | (uint)(y >> 3);
        private static ulong Bit(int x, int y) => 1UL << (((y & 7) << 3) | (x & 7));

        public bool this[int x, int y]
        {
            get => x >= 0 && y >= 0 && _chunks.TryGetValue(Key(x, y), out ulong bits) && (bits & Bit(x, y)) != 0;
            set
            {
                if (x < 0 || y < 0)
                    return;

                long key = Key(x, y);
                _chunks.TryGetValue(key, out ulong bits);
                bits = value ? bits | Bit(x, y) : bits & ~Bit(x, y);

                if (bits == 0)
                    _chunks.Remove(key);
                else
                    _chunks[key] = bits;
            }
        }

        public bool IsEmpty => _chunks.Count == 0;

        public void Clear() => _chunks.Clear();
    }

    public sealed class SchematicEditSession
    {
        public SchematicToolPhase Phase { get; private set; } = SchematicToolPhase.Idle;
        public Point FirstCorner { get; private set; }
        public Rectangle Region { get; private set; }

        public string Name { get; set; } = "Untitled";

        public CellMask KeepTiles { get; } = new();
        public CellMask KeepWalls { get; } = new();
        public List<SessionMarker> Markers { get; } = [];

        public static Point ClampToWorld(Point p) =>
            new(Math.Clamp(p.X, 0, Main.maxTilesX - 1), Math.Clamp(p.Y, 0, Main.maxTilesY - 1));

        public static Rectangle RectFromCorners(Point a, Point b)
        {
            int x1 = Math.Min(a.X, b.X);
            int y1 = Math.Min(a.Y, b.Y);
            int x2 = Math.Max(a.X, b.X);
            int y2 = Math.Max(a.Y, b.Y);
            return new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
        }

        public void BeginSelection(Point tile)
        {
            FirstCorner = ClampToWorld(tile);
            Phase = SchematicToolPhase.AwaitingSecondCorner;
        }

        public void CompleteSelection(Point tile)
        {
            Region = RectFromCorners(FirstCorner, ClampToWorld(tile));
            Phase = SchematicToolPhase.Editing;
        }

        public void SetRegion(Rectangle region)
        {
            Point topLeft = ClampToWorld(new Point(region.X, region.Y));
            Point bottomRight = ClampToWorld(new Point(region.Right - 1, region.Bottom - 1));
            Region = RectFromCorners(topLeft, bottomRight);
        }

        public void AddMarker(string name, Rectangle area) => Markers.Add(new SessionMarker(name, area));

        public void Cancel()
        {
            Phase = SchematicToolPhase.Idle;
            FirstCorner = default;
            Region = default;
            Name = "Untitled";
            KeepTiles.Clear();
            KeepWalls.Clear();
            Markers.Clear();
        }
    }
}
#endif