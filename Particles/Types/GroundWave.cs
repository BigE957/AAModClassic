using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Particles.Types
{
    public class GroundWave : Particle
    {
        private static readonly Rectangle[] SliceOffsets =
        [
            new Rectangle(0, 0, 4, 4),
            new Rectangle(4, 0, 8, 4),
            new Rectangle(12, 0, 4, 4),
            new Rectangle(0, 4, 4, 8),
            new Rectangle(4, 4, 8, 8),
            new Rectangle(12, 4, 4, 8),
            new Rectangle(0, 12, 4, 4),
            new Rectangle(4, 12, 8, 4),
            new Rectangle(12, 12, 4, 4),
        ];

        private const float NoLightThreshold = 0.001f;

        private readonly Point StartPosition;
        private readonly Point[] ColumnPositions;
        private readonly float[] ColumnOffsets;
        private readonly int[] ColumnDepths;

        private readonly int Count = 1;
        private readonly int Direction = 1;
        private readonly float Peak = 16f;
        private readonly int ColumnDelay = 0;
        private readonly int Duration = 30;

        private Vector3[] _blendedSlices = new Vector3[9];
        private readonly int MaxShiftTiles;
        private readonly int GridHeight;
        private readonly Vector3[] _localLight;
        private readonly bool[] _localFilled;
        private readonly Vector3[] _staticRealLight;
        private readonly bool[] _staticRealFilled;

        private readonly List<Point> _hiddenRealTiles;
        private bool _hiddenTilesUnregistered;

        private static float LightDecayThroughAir
        {
            get
            {
                float LightDecayThroughAir = 0.91f;
                if (Main.LocalPlayer.nightVision)
                    LightDecayThroughAir *= 1.03f;

                if (Main.LocalPlayer.blind)
                    LightDecayThroughAir *= 0.95f;

                if (Main.LocalPlayer.blackout)
                    LightDecayThroughAir *= 0.85f;

                if (Main.LocalPlayer.headcovered)
                    LightDecayThroughAir *= 0.85f;

                LightDecayThroughAir *= Player.airLightDecay;

                //TML: Shouldn't this just ref to LightMap values instead?
                float throughAir = 1f;
                float throughSolid = 1f;

                SystemLoader.ModifyLightingBrightness(ref throughAir, ref throughSolid);

                return LightDecayThroughAir * throughAir;
            }
        }
        private static float LightDecayThroughSolid
        {
            get
            {
                float LightDecayThroughSolid = 0.56f;
                if (Main.LocalPlayer.nightVision)
                    LightDecayThroughSolid *= 1.03f;

                if (Main.LocalPlayer.blind)
                    LightDecayThroughSolid *= 0.95f;

                if (Main.LocalPlayer.blackout)
                    LightDecayThroughSolid *= 0.85f;

                if (Main.LocalPlayer.headcovered)
                    LightDecayThroughSolid *= 0.85f;

                LightDecayThroughSolid *= Player.solidLightDecay;

                //TML: Shouldn't this just ref to LightMap values instead?
                float throughAir = 1f;
                float throughSolid = 1f;

                SystemLoader.ModifyLightingBrightness(ref throughAir, ref throughSolid);

                return LightDecayThroughSolid * throughSolid;
            }
        }

        public GroundWave(Point tilePosition, int count, bool rightwards, float peak, int columnDelay = 0, int duration = 30)
        {
            StartPosition = tilePosition;
            Position = tilePosition.ToWorldCoordinates(0, 0);
            Velocity = Vector2.Zero;
            Scale = Vector2.One;
            Duration = duration;
            Lifetime = Duration + (columnDelay * count);
            Color = Color.White;
            Count = count;
            Peak = peak;
            ColumnDelay = columnDelay;
            Direction = rightwards ? 1 : -1;

            ColumnPositions = new Point[Count];
            ColumnOffsets = new float[Count];
            int surfaceOffset = 0;
            for (int i = 0; i < Count; i++)
            {
                ColumnPositions[i] = CollisionUtils.FindSurfaceAround(StartPosition + new Point(i * Direction, surfaceOffset));
                surfaceOffset = ColumnPositions[i].Y - StartPosition.Y;
                ColumnOffsets[i] = 0f;
            }

            MaxShiftTiles = (int)MathF.Ceiling(Peak / 16f);

            ColumnDepths = new int[Count];
            _hiddenRealTiles = new List<Point>();
            int maxColumnDepth = 0;
            for (int i = 0; i < Count; i++)
            {
                int naturalDepth = ComputeColumnDepth(i, out bool endedOnGap);

                if (endedOnGap)
                {
                    int revealCount = Math.Min(naturalDepth, (int)MathF.Ceiling(GetColumnPeak(i) / 16f));
                    for (int k = 0; k < revealCount; k++)
                    {
                        int row = naturalDepth - 1 - k; // from the bottom tile upward
                        _hiddenRealTiles.Add(ColumnPositions[i] + new Point(0, row));
                    }
                }

                int columnDepth = Math.Max(1, naturalDepth);
                ColumnDepths[i] = columnDepth;
                if (columnDepth > maxColumnDepth)
                    maxColumnDepth = columnDepth;
            }

            if (_hiddenRealTiles.Count > 0)
                GroundWaveGlobalTile.RegisterHiddenTiles(_hiddenRealTiles);

            GridHeight = MaxShiftTiles + maxColumnDepth;

            _staticRealLight = new Vector3[Count * GridHeight];
            _staticRealFilled = new bool[Count * GridHeight];
            for (int i = 0; i < Count; i++)
            {
                for (int r = 0; r < GridHeight; r++)
                {
                    int idx = Index(i, r);
                    int worldX = StartPosition.X + i * Direction;
                    int worldY = ColumnPositions[i].Y - MaxShiftTiles + r;

                    _staticRealLight[idx] = SampleRealLight(worldX, worldY);

                    Tile t = Framing.GetTileSafely(new Point(worldX, worldY));
                    _staticRealFilled[idx] = t != null && t.HasTile;
                }
            }

            _localLight = new Vector3[Count * GridHeight];
            _localFilled = new bool[Count * GridHeight];
        }

        private int ComputeColumnDepth(int column, out bool endedOnGap)
        {
            endedOnGap = false;
            Point columnStart = ColumnPositions[column];
            int maxDepth = GetTilesToScreenBottom(columnStart.Y) + (int)MathF.Ceiling(GetColumnPeak(column) / 16f);

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

                Vector3 light = SampleRealLight(p.X, p.Y);
                bool noLight = light.X <= NoLightThreshold && light.Y <= NoLightThreshold && light.Z <= NoLightThreshold;

                if (noLight)
                {
                    consecutiveDarkTiles++;
                    if (consecutiveDarkTiles >= 2)
                        return (int)MathF.Min(k + (int)MathF.Ceiling(GetColumnPeak(column) / 16f), maxDepth);
                }
                else
                {
                    consecutiveDarkTiles = 0;
                }
            }

            return maxDepth;
        }

        private static int GetTilesToScreenBottom(int startTileY)
        {
            int screenBottomWorldY = (int)Main.screenPosition.Y + Main.screenHeight;
            int screenBottomTileY = screenBottomWorldY / 16;
            return Math.Max(screenBottomTileY - startTileY + 1, 1);
        }

        private float GetColumnPeak(int i)
        {
            float heightRatio = MathHelper.Lerp(0.05f, 0.95f, i / (float)Count);
            return MathF.Sin(heightRatio * MathHelper.Pi) * Peak;
        }

        public override void Update()
        {
            if (!_hiddenTilesUnregistered && Time >= Lifetime - 4 && _hiddenRealTiles.Count > 0)
            {
                GroundWaveGlobalTile.UnregisterHiddenTiles(_hiddenRealTiles);
                _hiddenTilesUnregistered = true;
            }

            for (int i = 0; i < Count; i++)
            {
                float ratio = MathHelper.Clamp((Time - (ColumnDelay * i)) / (float)Duration, 0f, 1f);

                float myPeak = GetColumnPeak(i);
                ColumnOffsets[i] = MathF.Sin(ratio * MathHelper.Pi) * myPeak;

                if (Time == (ColumnDelay * i))
                {
                    Tile t = Framing.GetTileSafely(ColumnPositions[i]);
                    int amt = WorldGen.KillTile_GetTileDustAmount(false, t, ColumnPositions[i].X, ColumnPositions[i].Y);
                    amt = (int)(amt * (myPeak / 96f));
                    for (int j = 0; j < amt; j++)
                    {
                        int d = WorldGen.KillTile_MakeTileDust(ColumnPositions[i].X, ColumnPositions[i].Y, t);
                        Main.dust[d].position.Y -= myPeak * 0.5f * Main.rand.NextFloat();
                        Main.dust[d].velocity.Y -= myPeak / 18f * Main.rand.NextFloat();
                    }
                }
            }
            Time++;
        }

        public override void OnKill(bool wasEvicted)
        {
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            PropagateLocalLight();

            Vector2 tileScreenPosition = new((int)Main.screenPosition.X, (int)Main.screenPosition.Y);

            for (int i = 0; i < Count; i++)
            {
                Point start = ColumnPositions[i];
                float offset = ColumnOffsets[i];
                int shiftTiles = (int)(offset / 16f);
                int topRow = MaxShiftTiles - shiftTiles;
                int myDepth = ColumnDepths[i];

                for (int j = 0; j < myDepth; j++)
                {
                    Point myTilePosition = start + new Point(0, j);
                    Tile t = Framing.GetTileSafely(myTilePosition);

                    if (t == null || !t.HasTile)
                        continue;

                    Main.instance.TilesRenderer.GetTileDrawData(myTilePosition.X, myTilePosition.Y, t, t.TileType, ref t.TileFrameX, ref t.TileFrameY, out int tileWidth, out int tileHeight, out int tileTop, out int halfBrickHeight, out int addFrX, out int addFrY, out SpriteEffects tileSpriteEffect, out Texture2D glowTexture, out Rectangle glowSourceRect, out Color glowColor);

                    Texture2D tileTex = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady(t.TileType, 0, t.TileColor) ?? TextureAssets.Tile[t.TileType].Value;
                    Vector2 drawPosition = myTilePosition.ToWorldCoordinates(0, 0) + new Vector2(0f, -offset) - tileScreenPosition;
                    drawPosition = new Vector2(MathF.Floor(drawPosition.X), MathF.Floor(drawPosition.Y));
                    int r = topRow + j;

                    if (t.Slope != SlopeType.Solid && !t.IsHalfBlock)
                    {
                        Color color = new(GetLocalLight(i, r));

                        int num = (int)t.Slope;
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

                            spritebatch.Draw(tileTex, drawPosition + new Vector2(num6, k * num2 + num3), new Rectangle(t.TileFrameX + num6, t.TileFrameY + num5, num2, num4), color, 0f, Vector2.Zero, 1f, 0, 0f);
                        }

                        int num7 = ((num <= 2) ? 14 : 0);
                        Main.spriteBatch.Draw(tileTex, drawPosition + new Vector2(0f, num7), new Rectangle(t.TileFrameX, t.TileFrameY + num7, 16, 2), color, 0f, Vector2.Zero, 1f, 0, 0f);
                        continue;
                    }

                    GetLocalTileSlices(i, r, ref _blendedSlices);

                    int actualVisibleHeight = tileHeight - halfBrickHeight;

                    for (int s = 0; s < 9; s++)
                    {
                        Rectangle slice = SliceOffsets[s];

                        int clampedHeight = slice.Height;
                        if (slice.Y + clampedHeight > actualVisibleHeight)
                            clampedHeight = actualVisibleHeight - slice.Y;

                        if (clampedHeight <= 0)
                            continue;

                        Rectangle source = new(t.TileFrameX + addFrX + slice.X, t.TileFrameY + addFrY + slice.Y, slice.Width, clampedHeight);
                        Color sliceColor = new(_blendedSlices[s]);
                        Rectangle destination = new(
                            (int)(drawPosition.X + slice.X),
                            (int)(drawPosition.Y + slice.Y + tileTop + halfBrickHeight),
                            slice.Width,
                            clampedHeight
                        );

                        spritebatch.Draw(tileTex, destination, source, sliceColor, Rotation, Vector2.Zero, tileSpriteEffect, 0f);

                        if (glowTexture != null)
                        {
                            Rectangle glowSource = new(glowSourceRect.X + slice.X, glowSourceRect.Y + slice.Y, slice.Width, clampedHeight);
                            spritebatch.Draw(glowTexture, destination, glowSource, glowColor, Rotation, Vector2.Zero, tileSpriteEffect, 0f);
                        }
                    }
                }
            }
        }

        private int Index(int i, int r) => i * GridHeight + r;

        #region Vanilla Lighting Approximation Shit
        private static Vector3 SampleRealLight(int x, int y) => Lighting.GetColor(x, y).ToVector3();

        private Vector3 GetLocalLight(int i, int r)
        {
            if (i >= 0 && i < Count && r >= 0 && r < GridHeight)
                return _localLight[Index(i, r)];

            int clampedI = (int)MathHelper.Clamp(i, 0, Count - 1);
            return SampleRealLight(StartPosition.X + i * Direction, ColumnPositions[clampedI].Y - MaxShiftTiles + r);
        }

        private void PropagateLocalLight()
        {
            for (int i = 0; i < Count; i++)
            {
                int myDepth = ColumnDepths[i];

                for (int r = 0; r < GridHeight; r++)
                {
                    int idx = Index(i, r);
                    int shiftTiles = (int)(ColumnOffsets[i] / 16f);
                    int top = MaxShiftTiles - shiftTiles;

                    bool withinTrackedDepth = r >= top && r < top + myDepth;
                    _localFilled[idx] = withinTrackedDepth || _staticRealFilled[idx];

                    int depthInColumn = r - top;
                    _localLight[idx] = withinTrackedDepth ? SampleRealLight(StartPosition.X + i * Direction, ColumnPositions[i].Y + depthInColumn) : _staticRealLight[idx];
                }
            }

            float decayAir = LightDecayThroughAir;
            float decaySolid = LightDecayThroughSolid;

            for (int pass = 0; pass < 2; pass++)
            {
                for (int i = 0; i < Count; i++)
                {
                    SweepColumn(i, true, decayAir, decaySolid);
                    SweepColumn(i, false, decayAir, decaySolid);
                }
                for (int r = 0; r < GridHeight; r++)
                {
                    SweepRow(r, true, decayAir, decaySolid);
                    SweepRow(r, false, decayAir, decaySolid);
                }
            }
        }

        private void SweepColumn(int i, bool topToBottom, float decayAir, float decaySolid)
        {
            Vector3 running = Vector3.Zero;
            int start = topToBottom ? 0 : GridHeight - 1;
            int end = topToBottom ? GridHeight : -1;
            int step = topToBottom ? 1 : -1;

            for (int r = start; r != end; r += step)
            {
                int idx = Index(i, r);
                running = Vector3.Max(running, _localLight[idx]);
                _localLight[idx] = running;
                running *= _localFilled[idx] ? decaySolid : decayAir;
            }
        }

        private void SweepRow(int r, bool leftToRight, float decayAir, float decaySolid)
        {
            Vector3 running = Vector3.Zero;
            int start = leftToRight ? 0 : Count - 1;
            int end = leftToRight ? Count : -1;
            int step = leftToRight ? 1 : -1;

            for (int i = start; i != end; i += step)
            {
                int idx = Index(i, r);
                running = Vector3.Max(running, _localLight[idx]);
                _localLight[idx] = running;
                running *= _localFilled[idx] ? decaySolid : decayAir;
            }
        }

        private void GetLocalTileSlices(int i, int r, ref Vector3[] outSlices)
        {
            int leftI = i - Direction;
            int rightI = i + Direction;

            int myY = ColumnPositions[i].Y;
            int leftYDiff = (leftI >= 0 && leftI < Count) ? myY - ColumnPositions[leftI].Y : 0;
            int rightYDiff = (rightI >= 0 && rightI < Count) ? myY - ColumnPositions[rightI].Y : 0;

            outSlices[0] = GetLocalLight(leftI, r + leftYDiff - 1);
            outSlices[1] = GetLocalLight(i, r - 1);
            outSlices[2] = GetLocalLight(rightI, r + rightYDiff - 1);

            outSlices[3] = GetLocalLight(leftI, r + leftYDiff);
            outSlices[4] = GetLocalLight(i, r);
            outSlices[5] = GetLocalLight(rightI, r + rightYDiff);

            outSlices[6] = GetLocalLight(leftI, r + leftYDiff + 1);
            outSlices[7] = GetLocalLight(i, r + 1);
            outSlices[8] = GetLocalLight(rightI, r + rightYDiff + 1);

            Vector3 flat = outSlices[4];
            for (int s = 0; s < 9; s++)
                outSlices[s] = (outSlices[s] + flat) * 0.5f;
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

        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (HiddenTileRefCounts.Count > 0 && HiddenTileRefCounts.ContainsKey(new Point(i, j)))
                return false;

            return base.PreDraw(i, j, type, spriteBatch);
        }
    }
}