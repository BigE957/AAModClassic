using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        private readonly Point StartPosition;
        private readonly Point[] ColumnPositions;
        private readonly float[] ColumnOffsets;
        private readonly int Depth = 1;
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

        public GroundWave(Point tilePosition, int depth, int count, bool rightwards, float peak, int columnDelay = 0, int duration = 30)
        {
            StartPosition = tilePosition;
            Position = tilePosition.ToWorldCoordinates();
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
            for (int i = 0; i < Count; i++)
            {
                ColumnPositions[i] = CollisionUtils.FindSurfaceBelow(StartPosition + new Point(i * Direction, -8), true);
                ColumnOffsets[i] = 0f;
            }

            MaxShiftTiles = (int)MathF.Ceiling(Peak / 16f);

            int requiredDepth = depth;
            for (int i = 0; i < Count; i++)
            {
                int myY = ColumnPositions[i].Y;

                int leftY = (i > 0) ? ColumnPositions[i - 1].Y : myY;
                int rightY = (i < Count - 1) ? ColumnPositions[i + 1].Y : myY;

                int maxDrop = Math.Max(leftY - myY, rightY - myY);

                int neededDepth = maxDrop + MaxShiftTiles + 2;
                if (neededDepth > requiredDepth)
                    requiredDepth = neededDepth;
            }

            Depth = requiredDepth;
            GridHeight = MaxShiftTiles + Depth;

            _localLight = new Vector3[Count * GridHeight];
            _localFilled = new bool[Count * GridHeight];
        }

        public override void Update()
        {
            for (int i = 0; i < Count; i++)
            {
                float ratio = MathHelper.Clamp((Time - (ColumnDelay * i)) / (float)Duration, 0f, 1f);

                float heightRatio = MathHelper.Lerp(0.05f, 0.95f, i / (float)Count);
                float myPeak = MathF.Sin(heightRatio * MathHelper.Pi) * Peak;
                ColumnOffsets[i] = MathF.Sin(ratio * MathHelper.Pi) * myPeak;

                if (Time == (ColumnDelay * i))
                {
                    Tile t = Framing.GetTileSafely(ColumnPositions[i]);
                    int amt = WorldGen.KillTile_GetTileDustAmount(false, t, ColumnPositions[i].X, ColumnPositions[i].Y);
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

        public override void Draw(SpriteBatch spritebatch)
        {
            PropagateLocalLight();

            for (int i = 0; i < Count; i++)
            {
                Point start = ColumnPositions[i];
                float offset = ColumnOffsets[i];
                int shiftTiles = (int)(offset / 16f);
                int topRow = MaxShiftTiles - shiftTiles;

                for (int j = 0; j < Depth; j++)
                {
                    Point myTilePosition = start + new Point(0, j);
                    Tile t = Framing.GetTileSafely(myTilePosition);

                    if (t == null || !t.HasTile)
                        continue;

                    Texture2D tileTex = TextureAssets.Tile[t.TileType].Value;
                    Vector2 drawPosition = myTilePosition.ToWorldCoordinates() + new Vector2(0f, -offset) - Main.screenPosition;

                    int r = topRow + j;

                    if (t.IsHalfBlock)
                    {
                        Color color = new Color(GetLocalLight(i, r));
                        Vector2 topLeft = drawPosition - new Vector2(8f, 0f);

                        Rectangle top = new Rectangle(t.TileFrameX, t.TileFrameY, 16, 12);
                        spritebatch.Draw(tileTex, topLeft, top, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                        Rectangle lip = new Rectangle(144, 66, 16, 4);
                        spritebatch.Draw(tileTex, topLeft + new Vector2(0f, 12f), lip, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                        continue;
                    }
                    else if (t.Slope != SlopeType.Solid)
                    {
                        GetLocalTileSlices(i, r, ref _blendedSlices);

                        Color color = new(_blendedSlices[4]);

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

                            spritebatch.Draw(tileTex, drawPosition + new Vector2(num6, k * num2 + num3), new Rectangle(t.TileFrameX + num6, t.TileFrameY + num5, num2, num4), color, 0f, new Vector2(8f, 8f), 1f, 0, 0f);
                        }

                        int num7 = ((num <= 2) ? 14 : 0);
                        Main.spriteBatch.Draw(tileTex, drawPosition + new Vector2(0f, num7), new Rectangle(t.TileFrameX, t.TileFrameY + num7, 16, 2), color, 0f, new Vector2(8f, 8f), 1f, 0, 0f);
                        continue;
                    }

                    GetLocalTileSlices(i, r, ref _blendedSlices);

                    for (int s = 0; s < 9; s++)
                    {
                        Rectangle slice = SliceOffsets[s];
                        Rectangle source = new(t.TileFrameX + slice.X, t.TileFrameY + slice.Y, slice.Width, slice.Height);

                        Color sliceColor = new(_blendedSlices[s]);
                        Rectangle destination = new((int)(drawPosition.X + slice.X - 8), (int)(drawPosition.Y + slice.Y - 8), slice.Width + 1, slice.Height + 1);

                        spritebatch.Draw(tileTex, destination, source, sliceColor, Rotation, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        private int Index(int i, int r) => i * GridHeight + r;

        #region Vanilla Lighting Approximation Shit
        private static Vector3 SampleRealLight(int x, int y) => Lighting.GetColor(x, y).ToVector3();

        private bool IsFilled(int i, int r)
        {
            int shiftTiles = (int)(ColumnOffsets[i] / 16f);
            int top = MaxShiftTiles - shiftTiles;
            return r >= top && r < top + Depth;
        }

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
                for (int r = 0; r < GridHeight; r++)
                {
                    int idx = Index(i, r);
                    int shiftTiles = (int)(ColumnOffsets[i] / 16f);
                    int top = MaxShiftTiles - shiftTiles;

                    bool filled = r >= top && r < top + Depth;
                    _localFilled[idx] = filled;

                    if (filled)
                    {
                        int depthInColumn = r - top;
                        _localLight[idx] = SampleRealLight(StartPosition.X + i * Direction, ColumnPositions[i].Y + depthInColumn);
                    }
                    else
                    {
                        _localLight[idx] = SampleRealLight(StartPosition.X + i * Direction, ColumnPositions[i].Y - MaxShiftTiles + r);
                    }
                }
            }

            for (int pass = 0; pass < 2; pass++)
            {
                for (int i = 0; i < Count; i++)
                {
                    SweepColumn(i, true);
                    SweepColumn(i, false);
                }
                for (int r = 0; r < GridHeight; r++)
                {
                    SweepRow(r, true);
                    SweepRow(r, false);
                }
            }
        }

        private void SweepColumn(int i, bool topToBottom)
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
                running *= _localFilled[idx] ? LightDecayThroughSolid : LightDecayThroughAir;
            }
        }

        private void SweepRow(int r, bool leftToRight)
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
                running *= _localFilled[idx] ? LightDecayThroughSolid : LightDecayThroughAir;
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
}