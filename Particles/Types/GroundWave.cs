using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Particles.Types
{
    public class GroundWave : Particle
    {
        private const int DarkByte = 1;
        private const int MaxDepthCap = 40;

        private const float CullPadding = 32f;
        private const int ExtraRows = 2;

        #region Cached draw data

        private enum TileKind : byte 
        { 
            None, 
            Normal, 
            Slope, 
            Notch 
        }

        private struct TileDraw
        {
            public TileKind Kind;

            public ushort Type;
            public byte Paint;
            public bool PaintPending;
            public Texture2D Texture;
            public SpriteEffects Effects;

            public bool Sliced;
            public Rectangle Source;
            public Vector2 Offset;
            public Texture2D Glow;
            public Rectangle GlowSource;
            public Color GlowColor;

            public SlopeType Slope;
            public short FrameX;
            public short FrameY;

            public bool LeftHalf;
            public bool RightHalf;
            public bool AboveSame;
            public bool SmoothBorders;
            public int AddFrX;
            public int AddFrY;
            public int TileWidth;
            public int TileHeight;
        }

        #endregion

        private readonly Point StartPosition;
        private readonly Point[] ColumnPositions;
        private readonly float[] ColumnOffsets;
        private readonly float[] ColumnVelocities;
        private readonly float[] ColumnGravities;

        private readonly int Count = 1;
        private readonly int Direction = 1;
        private readonly float Peak = 16f;
        private readonly int ColumnDelay = 0;
        private readonly int Duration = 30;
        private readonly float HumpPosition = 0.75f;
        private readonly bool Strong = false;

        private bool _initialized;
        private int MaxShiftTiles;
        private int[] ColumnDepths;
        private Rectangle _worldBounds;

        private List<Point> _hiddenRealTiles = [];
        private bool _hiddenRegistered;
        private bool _hiddenReleased;

        private TileDraw[][] _tiles;

        private Vector3[][] _carried;

        private int[] _shift;

        public GroundWave(Point tilePosition, int count, bool rightwards, float peak, int columnDelay = 0, int duration = 30, float humpPosition = 0.25f, bool strong = false)
        {
            StartPosition = tilePosition;
            Position = tilePosition.ToWorldCoordinates(0, 0);
            Velocity = Vector2.Zero;
            Scale = Vector2.One;
            Duration = Math.Max(1, duration);
            Lifetime = Duration + (columnDelay * count);
            Color = Color.White;
            Count = count;
            Peak = peak;
            ColumnDelay = columnDelay;
            Direction = rightwards ? 1 : -1;
            HumpPosition = MathHelper.Clamp(humpPosition, 0.001f, 0.999f);
            Strong = strong;

            ColumnPositions = new Point[Count];
            ColumnOffsets = new float[Count];
            ColumnVelocities = new float[Count];
            ColumnGravities = new float[Count];

            int surfaceOffset = 0;
            for (int i = 0; i < Count; i++)
            {
                ColumnPositions[i] = CollisionUtils.FindSurfaceAround(StartPosition + new Point(i * Direction, surfaceOffset));
                surfaceOffset = ColumnPositions[i].Y - StartPosition.Y;
                ColumnOffsets[i] = 0f;

                float myPeak = GetColumnPeak(i);

                ColumnVelocities[i] = (4f * myPeak) / Duration;
                ColumnGravities[i] = (8f * myPeak) / (Duration * Duration);
            }
        }

        public override bool Important => true;

        public override void OnSpawn()
        {
            MaxShiftTiles = Math.Max(1, ShiftRowsFor(Peak));
            ColumnDepths = new int[Count];
            _hiddenRealTiles = [];

            for (int i = 0; i < Count; i++)
            {
                int naturalDepth = ComputeColumnDepth(i, out bool endedOnGap);

                if (endedOnGap)
                {
                    int revealCount = Math.Min(naturalDepth, ShiftRowsFor(GetColumnPeak(i)));
                    for (int k = 0; k < revealCount; k++)
                        _hiddenRealTiles.Add(ColumnPositions[i] + new Point(0, naturalDepth - 1 - k));
                }

                int columnDepth = Math.Max(1, naturalDepth);

                if (!endedOnGap)
                    columnDepth += ExtraRows;

                ColumnDepths[i] = columnDepth;
            }

            if (_hiddenRealTiles.Count > 0)
            {
                GroundWaveGlobalTile.RegisterHiddenTiles(_hiddenRealTiles);
                _hiddenRegistered = true;
            }

            int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
            for (int i = 0; i < Count; i++)
            {
                Point col = ColumnPositions[i];
                minX = Math.Min(minX, col.X);
                maxX = Math.Max(maxX, col.X);
                minY = Math.Min(minY, col.Y - MaxShiftTiles);
                maxY = Math.Max(maxY, col.Y + ColumnDepths[i]);
            }

            _worldBounds = new Rectangle(minX * 16, minY * 16, (maxX - minX + 1) * 16, (maxY - minY + 1) * 16);

            _shift = new int[Count];

            _initialized = true;
        }

        public override void OnKill(bool wasEvicted)
        {
            ReleaseHiddenTiles();
        }

        private void ReleaseHiddenTiles()
        {
            if (!_hiddenRegistered || _hiddenReleased)
                return;

            GroundWaveGlobalTile.UnregisterHiddenTiles(_hiddenRealTiles);
            _hiddenReleased = true;
        }

        private int ShiftRowsFor(float peak)
        {
            float overshoot = (Duration + 1f) * (Duration + 1f) / ((float)Duration * Duration);
            return (int)MathF.Ceiling(peak * overshoot / 16f);
        }

        private int ShiftTiles(float offset) => Math.Clamp((int)MathF.Round(offset / 16f), 0, MaxShiftTiles);

        private int ComputeColumnDepth(int column, out bool endedOnGap)
        {
            endedOnGap = false;
            Point columnStart = ColumnPositions[column];
            int shiftRows = ShiftRowsFor(GetColumnPeak(column));
            int maxDepth = MaxDepthCap + shiftRows;

            int consecutiveDarkTiles = 0;
            for (int k = 0; k < maxDepth; k++)
            {
                Point p = columnStart + new Point(0, k);
                Tile t = Framing.GetTileSafely(p);

                if (t == null || !t.HasTile)
                {
                    endedOnGap = true;
                    return k;
                }

                Color light = Lighting.GetColor(p.X, p.Y);
                bool dark = light.R < DarkByte && light.G < DarkByte && light.B < DarkByte;

                if (dark)
                {
                    consecutiveDarkTiles++;
                    if (consecutiveDarkTiles >= 2)
                        return Math.Min(k + shiftRows, maxDepth);
                }
                else
                {
                    consecutiveDarkTiles = 0;
                }
            }

            return maxDepth;
        }

        private float GetColumnPeak(int i)
        {
            float t;
            if (Count <= 1)
                t = 0.5f;
            else
                t = i / (float)(Count - 1);

            float heightRatio = t <= HumpPosition ? MathF.Sin((t / HumpPosition) * MathHelper.PiOver2) : MathF.Cos(((t - HumpPosition) / (1f - HumpPosition)) * MathHelper.PiOver2);

            return heightRatio * Peak;
        }

        public override void Update()
        {
            if (_initialized && Time >= Lifetime - 4)
                ReleaseHiddenTiles();

            for (int i = 0; i < Count; i++)
            {
                int myStartTime = ColumnDelay * i;
                int myEndTime = myStartTime + Duration;

                if (Time >= myStartTime && Time <= myEndTime)
                {
                    float myPeak = GetColumnPeak(i);

                    if (Time == myStartTime)
                    {
                        Tile t = Framing.GetTileSafely(ColumnPositions[i]);
                        int amt = WorldGen.KillTile_GetTileDustAmount(false, t, ColumnPositions[i].X, ColumnPositions[i].Y);
                        amt = (int)(amt * (myPeak / 96f));
                        for (int j = 0; j < amt; j++)
                        {
                            int d = WorldGen.KillTile_MakeTileDust(ColumnPositions[i].X, ColumnPositions[i].Y, t);
                            Main.dust[d].position.Y -= myPeak * 0.5f * Main.rand.NextFloat();
                            Main.dust[d].velocity.Y -= myPeak / 20f * Main.rand.NextFloat();
                        }

                        if (Strong)
                        {
                            //SoundEngine.PlaySound(SoundID.Dig with { Volume = 0.5f, Pitch = (myPeak / Peak) - 0.5f });
                            if (t.TileType == TileID.Sand)
                            {
                                Vector2 spawnPos = ColumnPositions[i].ToWorldCoordinates();
                                for (int j = 0; j < 3; j++)
                                {
                                    LargeDust d = new(spawnPos + Main.rand.NextVector2Circular(8f, 4f), new Vector2(Main.rand.NextFloat(-2, 2), -Main.rand.NextFloat(ColumnVelocities[i] / 10f, ColumnVelocities[i] * 1.25f)), new Color(212, 192, 100), new Color(212, 192, 100), Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                    ParticleSystem.SpawnParticle(d, DrawLayer.AfterPlayers);
                                }
                                LargeDust du = new(spawnPos + Main.rand.NextVector2Circular(8f, 4f), new Vector2(Main.rand.NextFloat(-2, 2), -ColumnVelocities[i] * 1.25f), new Color(212, 192, 100), new Color(212, 192, 100), Main.rand.NextFloat(0.75f, 1.5f), 200, Main.rand.NextFloat(0.01f, 0.05f));
                                ParticleSystem.SpawnParticle(du, DrawLayer.AfterPlayers);
                            }
                        }
                    }

                    ColumnOffsets[i] += ColumnVelocities[i];
                    ColumnVelocities[i] -= ColumnGravities[i];

                    if (ColumnOffsets[i] <= 0f && Time > myStartTime)
                    {
                        ColumnOffsets[i] = 0f;
                        ColumnVelocities[i] = 0f;
                    }
                }
            }
            Time++;
        }

        #region Vanilla must burn
        private static Color _highQualityLighting;
        private static Color _mediumQualityLighting;
        private static bool _thresholdsLoaded;

        private static void LoadThresholds()
        {
            if (_thresholdsLoaded)
                return;

            _thresholdsLoaded = true;

            try
            {
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;
                object renderer = Main.instance.TilesRenderer;
                Type type = renderer.GetType();

                _highQualityLighting = (Color)type.GetField("_highQualityLightingRequirement", flags).GetValue(renderer);
                _mediumQualityLighting = (Color)type.GetField("_mediumQualityLightingRequirement", flags).GetValue(renderer);
            }
            catch
            {
                _highQualityLighting = new Color(0, 0, 0);
                _mediumQualityLighting = new Color(0, 0, 0);
            }
        }

        private static readonly Rectangle[] SliceRects9 =
        [
            new(0, 0, 4, 4), new(4, 0, 8, 4), new(12, 0, 4, 4),
            new(0, 4, 4, 8), new(4, 4, 8, 8), new(12, 4, 4, 8),
            new(0, 12, 4, 4), new(4, 12, 8, 4), new(12, 12, 4, 4),
        ];

        private static readonly Rectangle[] SliceRects4 =
        [
            new(0, 0, 8, 8), new(8, 0, 8, 8),
            new(0, 8, 8, 8), new(8, 8, 8, 8),
        ];

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!_initialized)
                return;

            Vector2 screenSize = new(Main.screenWidth, Main.screenHeight);
            Vector2 viewSize = Main.ViewSize;
            Vector2 screenPos = new((int)Main.screenPosition.X, (int)Main.screenPosition.Y);

            Vector2 inset = (screenSize - viewSize) * 0.5f;

            int pad = (int)CullPadding;
            Rectangle view = new((int)(screenPos.X + inset.X) - pad, (int)(screenPos.Y + inset.Y) - pad, (int)viewSize.X + pad * 2, (int)viewSize.Y + pad * 2);
            if (!view.Intersects(_worldBounds))
                return;

            if (_tiles == null)
                BuildTileCache();

            LoadThresholds();

            for (int i = 0; i < Count; i++)
            {
                _shift[i] = ShiftTiles(ColumnOffsets[i]);

                TileDraw[] tiles = _tiles[i];
                Vector3[] carried = _carried[i];
                Point origin = ColumnPositions[i];

                for (int j = 0; j < tiles.Length; j++)
                {
                    if (tiles[j].Kind != TileKind.None)
                        carried[j] = RealRaw(origin.X, origin.Y + j);
                }
            }

            bool sliced = Lighting.NotRetro;
            float brightness = Lighting.GlobalBrightness;

            for (int i = 0; i < Count; i++)
            {
                Point col = ColumnPositions[i];
                float px = MathF.Floor(col.X * 16f - screenPos.X);
                if (px < inset.X - CullPadding || px > inset.X + viewSize.X + CullPadding)
                    continue;

                TileDraw[] column = _tiles[i];
                float offset = ColumnOffsets[i];
                int shift = _shift[i];

                int wholeRows = (int)(offset / 16f);
                bool straddles = offset - wholeRows * 16f > 0.01f;

                for (int j = 0; j < column.Length; j++)
                {
                    ref TileDraw td = ref column[j];
                    if (td.Kind == TileKind.None)
                        continue;

                    Vector2 drawPos = new(px, MathF.Floor((col.Y + j) * 16f - offset - screenPos.Y));
                    if (drawPos.Y < inset.Y - CullPadding || drawPos.Y > inset.Y + viewSize.Y + CullPadding)
                        continue;

                    int cellX = col.X;
                    int cellY = col.Y + j - shift;

                    Color flat = PackFlat(_carried[i][j], brightness);

                    if (flat.R == 0 && flat.G == 0 && flat.B == 0)
                    {
                        int lower = col.Y + j - wholeRows;
                        Color beneath = Lighting.GetColor(cellX, lower);
                        bool hidesNothing = beneath.R == 0 && beneath.G == 0 && beneath.B == 0;

                        if (hidesNothing && straddles)
                        {
                            Color above = Lighting.GetColor(cellX, lower - 1);
                            hidesNothing = above.R == 0 && above.G == 0 && above.B == 0;
                        }

                        if (hidesNothing)
                            continue;
                    }

                    if (td.PaintPending)
                    {
                        td.Texture = ResolveTexture(td.Type, td.Paint, out bool pending);
                        td.PaintPending = pending;
                    }

                    switch (td.Kind)
                    {
                        case TileKind.Normal:
                            Vector2 pos = drawPos + td.Offset;

                            if (sliced && td.Sliced)
                                DrawSlicedBlock(spritebatch, in td, pos, flat, cellX, cellY, brightness);
                            else
                                spritebatch.Draw(td.Texture, pos, td.Source, flat, 0f, Vector2.Zero, 1f, td.Effects, 0f);

                            if (td.Glow != null)
                                spritebatch.Draw(td.Glow, pos, td.GlowSource, td.GlowColor, 0f, Vector2.Zero, 1f, td.Effects, 0f);
                            break;

                        case TileKind.Slope:
                            DrawSlope(spritebatch, in td, drawPos, flat);
                            break;

                        case TileKind.Notch:
                            DrawHalfBrickNotch(spritebatch, in td, drawPos, flat);
                            break;
                    }
                }
            }
        }

        private static Vector3 RealRaw(int x, int y) => Lighting.GetSubLight(new Vector2(x * 16 + 8, y * 16 + 8));

        private Vector3 GridRaw(int x, int y)
        {
            int ci = (x - StartPosition.X) * Direction;
            if (ci >= 0 && ci < Count)
            {
                int j = y - ColumnPositions[ci].Y + _shift[ci];
                if (j >= 0 && j < _tiles[ci].Length && _tiles[ci][j].Kind != TileKind.None)
                    return _carried[ci][j];
            }

            return RealRaw(x, y);
        }

        private static Color PackFlat(Vector3 raw, float brightness)
        {
            float k = brightness * 255f;
            int r = Math.Min(255, (int)(raw.X * k));
            int g = Math.Min(255, (int)(raw.Y * k));
            int b = Math.Min(255, (int)(raw.Z * k));

            Color c = default;
            c.PackedValue = (uint)(r | (g << 8) | (b << 16)) | 0xFF000000u;
            return c;
        }

        private static Color PackBlend(Vector3 slice, Vector3 flat)
        {
            Vector3 v = (slice + flat) * 0.5f;
            int r = Math.Min(255, (int)(v.X * 255f));
            int g = Math.Min(255, (int)(v.Y * 255f));
            int b = Math.Min(255, (int)(v.Z * 255f));

            Color c = default;
            c.PackedValue = (uint)(r | (g << 8) | (b << 16)) | 0xFF000000u;
            return c;
        }

        private void DrawSlicedBlock(SpriteBatch spritebatch, in TileDraw td, Vector2 pos, Color flat, int x, int y, float brightness)
        {
            Vector3 flatVec = flat.ToVector3();
            int frameX = td.Source.X;
            int frameY = td.Source.Y;

            if (flat.R > _highQualityLighting.R || flat.G > _highQualityLighting.G || flat.B > _highQualityLighting.B)
            {
                Span<Vector3> slices = stackalloc Vector3[9];
                int n = 0;
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                        slices[n++] = GridRaw(x + dx, y + dy) * brightness;
                }

                for (int k = 0; k < 9; k++)
                {
                    Rectangle r = SliceRects9[k];
                    spritebatch.Draw(td.Texture, pos + new Vector2(r.X, r.Y), new Rectangle(frameX + r.X, frameY + r.Y, r.Width, r.Height), PackBlend(slices[k], flatVec), 0f, Vector2.Zero, 1f, td.Effects, 0f);
                }
            }
            else if (flat.R > _mediumQualityLighting.R || flat.G > _mediumQualityLighting.G || flat.B > _mediumQualityLighting.B)
            {
                Vector3 up = GridRaw(x, y - 1);
                Vector3 down = GridRaw(x, y + 1);
                Vector3 left = GridRaw(x - 1, y);
                Vector3 right = GridRaw(x + 1, y);

                float sumUp = up.X + up.Y + up.Z;
                float sumDown = down.X + down.Y + down.Z;
                float sumRight = right.X + right.Y + right.Z;
                float sumLeft = left.X + left.Y + left.Z;

                Span<Vector3> quads = stackalloc Vector3[4];
                quads[0] = (sumUp >= sumLeft ? left : up) * brightness;
                quads[1] = (sumUp >= sumRight ? right : up) * brightness;
                quads[2] = (sumDown >= sumLeft ? left : down) * brightness;
                quads[3] = (sumDown >= sumRight ? right : down) * brightness;

                for (int k = 0; k < 4; k++)
                {
                    Rectangle r = SliceRects4[k];
                    spritebatch.Draw(td.Texture, pos + new Vector2(r.X, r.Y), new Rectangle(frameX + r.X, frameY + r.Y, r.Width, r.Height), PackBlend(quads[k], flatVec), 0f, Vector2.Zero, 1f, td.Effects, 0f);
                }
            }
            else
            {
                spritebatch.Draw(td.Texture, pos, td.Source, flat, 0f, Vector2.Zero, 1f, td.Effects, 0f);
            }
        }

        private static Texture2D ResolveTexture(ushort type, byte paint, out bool pending)
        {
            Texture2D painted = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady(type, 0, paint);
            pending = painted == null && paint != 0;
            return painted ?? TextureAssets.Tile[type].Value;
        }

        private void BuildTileCache()
        {
            _tiles = new TileDraw[Count][];
            _carried = new Vector3[Count][];

            for (int i = 0; i < Count; i++)
            {
                Point start = ColumnPositions[i];
                int depth = ColumnDepths[i];
                TileDraw[] column = new TileDraw[depth];
                _tiles[i] = column;
                _carried[i] = new Vector3[depth];

                int leftI = i - Direction;
                int rightI = i + Direction;
                int leftYDiff = (leftI >= 0 && leftI < Count) ? start.Y - ColumnPositions[leftI].Y : 0;
                int rightYDiff = (rightI >= 0 && rightI < Count) ? start.Y - ColumnPositions[rightI].Y : 0;

                for (int j = 0; j < depth; j++)
                {
                    Point pos = start + new Point(0, j);
                    Tile t = Framing.GetTileSafely(pos);

                    if (t == null || !t.HasTile)
                        continue;

                    Main.instance.TilesRenderer.GetTileDrawData(pos.X, pos.Y, t, t.TileType, ref t.TileFrameX, ref t.TileFrameY, out int tileWidth, out int tileHeight, out int tileTop, out int halfBrickHeight, out int addFrX, out int addFrY, out SpriteEffects tileSpriteEffect, out Texture2D glowTexture, out Rectangle glowSourceRect, out Color glowColor);

                    ref TileDraw td = ref column[j];
                    td.Type = t.TileType;
                    td.Paint = t.TileColor;
                    td.Effects = tileSpriteEffect;
                    td.Texture = ResolveTexture(td.Type, td.Paint, out bool pending);
                    td.PaintPending = pending;
                    td.FrameX = t.TileFrameX;
                    td.FrameY = t.TileFrameY;

                    if (t.Slope != SlopeType.Solid && !t.IsHalfBlock)
                    {
                        td.Kind = TileKind.Slope;
                        td.Slope = t.Slope;
                        continue;
                    }

                    Tile leftTile = Framing.GetTileSafely((leftI >= 0 && leftI < Count) ? ColumnPositions[leftI] + new Point(0, j + leftYDiff) : pos + new Point(-1, 0));
                    Tile rightTile = Framing.GetTileSafely((rightI >= 0 && rightI < Count) ? ColumnPositions[rightI] + new Point(0, j + rightYDiff) : pos + new Point(1, 0));

                    bool leftIsHalfBrick = leftTile != null && leftTile.IsHalfBlock;
                    bool rightIsHalfBrick = rightTile != null && rightTile.IsHalfBlock;

                    bool needsHalfBrickNotch = !TileID.Sets.Platforms[t.TileType]
                        && !TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[t.TileType]
                        && Main.tileSolid[t.TileType]
                        && !TileID.Sets.NotReallySolid[t.TileType]
                        && !t.IsHalfBlock
                        && (leftIsHalfBrick || rightIsHalfBrick);

                    if (needsHalfBrickNotch)
                    {
                        Tile above = Framing.GetTileSafely(pos + new Point(0, -1));

                        td.Kind = TileKind.Notch;
                        td.LeftHalf = leftIsHalfBrick;
                        td.RightHalf = rightIsHalfBrick;
                        td.AboveSame = above != null && above.HasTile && !above.BottomSlope && above.TileType == t.TileType;
                        td.SmoothBorders = TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[t.TileType];
                        td.AddFrX = addFrX;
                        td.AddFrY = addFrY;
                        td.TileWidth = tileWidth;
                        td.TileHeight = tileHeight;
                        continue;
                    }

                    td.Kind = TileKind.Normal;
                    td.Sliced = Main.tileSolid[t.TileType] && !t.IsHalfBlock && !TileID.Sets.DontDrawTileSliced[t.TileType];
                    int visibleHeight = Math.Min(16, tileHeight - halfBrickHeight);
                    td.Source = new Rectangle(t.TileFrameX + addFrX, t.TileFrameY + addFrY, 16, visibleHeight);
                    td.Offset = new Vector2(0f, tileTop + halfBrickHeight);

                    if (glowTexture != null)
                    {
                        td.Glow = glowTexture;
                        td.GlowSource = new Rectangle(glowSourceRect.X, glowSourceRect.Y, 16, visibleHeight);
                        td.GlowColor = glowColor;
                    }
                }
            }
        }

        private static void DrawSlope(SpriteBatch spritebatch, in TileDraw td, Vector2 drawPosition, Color color)
        {
            int num = (int)td.Slope;
            int num2 = 2;
            for (int k = 0; k < 8; k++)
            {
                int num3 = k * -2;
                int num4 = 16 - k * 2;
                int num5 = 16 - num4;
                int num6;
                switch (num)
                {
                    case 1:
                        num3 = 0;
                        num6 = k * 2;
                        num4 = 14 - k * 2;
                        num5 = 0;
                        break;
                    case 2:
                        num3 = 0;
                        num6 = 16 - k * 2 - 2;
                        num4 = 14 - k * 2;
                        num5 = 0;
                        break;
                    case 3:
                        num6 = k * 2;
                        break;
                    default:
                        num6 = 16 - k * 2 - 2;
                        break;
                }

                spritebatch.Draw(td.Texture, drawPosition + new Vector2(num6, k * num2 + num3), new Rectangle(td.FrameX + num6, td.FrameY + num5, num2, num4), color, 0f, Vector2.Zero, 1f, 0, 0f);
            }

            int num7 = (num <= 2) ? 14 : 0;
            spritebatch.Draw(td.Texture, drawPosition + new Vector2(0f, num7), new Rectangle(td.FrameX, td.FrameY + num7, 16, 2), color, 0f, Vector2.Zero, 1f, 0, 0f);
        }

        private static void DrawHalfBrickNotch(SpriteBatch spritebatch, in TileDraw td, Vector2 drawPosition, Color color)
        {
            Texture2D tileTex = td.Texture;
            int frameX = td.FrameX;
            int frameY = td.FrameY;
            int addFrX = td.AddFrX;
            int addFrY = td.AddFrY;
            int tileWidth = td.TileWidth;
            int tileHeight = td.TileHeight;
            SpriteEffects fx = td.Effects;

            if (td.LeftHalf && td.RightHalf)
            {
                spritebatch.Draw(tileTex, drawPosition + new Vector2(0f, 8f), new Rectangle(frameX + addFrX, addFrY + frameY + 8, tileWidth, 8), color, 0f, Vector2.Zero, 1f, fx, 0f);

                Rectangle notch = new(126 + addFrX, addFrY, 16, 8);
                if (td.AboveSame)
                    notch = new Rectangle(90 + addFrX, addFrY, 16, 8);

                spritebatch.Draw(tileTex, drawPosition, notch, color, 0f, Vector2.Zero, 1f, fx, 0f);
            }
            else if (td.LeftHalf)
            {
                int inset = td.SmoothBorders ? 2 : 4;

                spritebatch.Draw(tileTex, drawPosition + new Vector2(0f, 8f), new Rectangle(frameX + addFrX, addFrY + frameY + 8, tileWidth, 8), color, 0f, Vector2.Zero, 1f, fx, 0f);
                spritebatch.Draw(tileTex, drawPosition + new Vector2(inset, 0f), new Rectangle(frameX + inset + addFrX, addFrY + frameY, tileWidth - inset, tileHeight), color, 0f, Vector2.Zero, 1f, fx, 0f);
                spritebatch.Draw(tileTex, drawPosition, new Rectangle(144 + addFrX, addFrY, inset, 8), color, 0f, Vector2.Zero, 1f, fx, 0f);

                if (inset == 2)
                    spritebatch.Draw(tileTex, drawPosition, new Rectangle(148 + addFrX, addFrY, 2, 2), color, 0f, Vector2.Zero, 1f, fx, 0f);
            }
            else
            {
                int inset = td.SmoothBorders ? 2 : 4;

                spritebatch.Draw(tileTex, drawPosition + new Vector2(0f, 8f), new Rectangle(frameX + addFrX, addFrY + frameY + 8, tileWidth, 8), color, 0f, Vector2.Zero, 1f, fx, 0f);
                spritebatch.Draw(tileTex, drawPosition, new Rectangle(frameX + addFrX, addFrY + frameY, tileWidth - inset, tileHeight), color, 0f, Vector2.Zero, 1f, fx, 0f);
                spritebatch.Draw(tileTex, drawPosition + new Vector2(16 - inset, 0f), new Rectangle(144 + (16 - inset), 0, inset, 8), color, 0f, Vector2.Zero, 1f, fx, 0f);

                if (inset == 2)
                    spritebatch.Draw(tileTex, drawPosition + new Vector2(14f, 0f), new Rectangle(156, 0, 2, 2), color, 0f, Vector2.Zero, 1f, fx, 0f);
            }
        }

        #endregion
    }

    public class GroundWaveGlobalTile : GlobalTile
    {
        private static readonly Dictionary<Point, int> HiddenTileRefCounts = [];

        public static void RegisterHiddenTiles(List<Point> tiles)
        {
            foreach (Point p in tiles)
            {
                HiddenTileRefCounts.TryGetValue(p, out int count);
                HiddenTileRefCounts[p] = count + 1;
            }
        }

        public static void UnregisterHiddenTiles(List<Point> tiles)
        {
            foreach (Point p in tiles)
            {
                if (!HiddenTileRefCounts.TryGetValue(p, out int count))
                    continue;

                if (count <= 1)
                    HiddenTileRefCounts.Remove(p);
                else
                    HiddenTileRefCounts[p] = count - 1;
            }
        }

        public static void Clear() => HiddenTileRefCounts.Clear();

        public override void Unload() => Clear();

        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (HiddenTileRefCounts.Count > 0 && HiddenTileRefCounts.ContainsKey(new Point(i, j)))
                return false;

            return base.PreDraw(i, j, type, spriteBatch);
        }
    }

    public class GroundWaveCleanupSystem : ModSystem
    {
        public override void OnWorldUnload() => GroundWaveGlobalTile.Clear();
    }
}