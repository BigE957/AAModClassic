using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

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

        private Vector3[] _sliceA = new Vector3[9];
        private Vector3[] _sliceB = new Vector3[9];
        private readonly Vector3[] _blendedSlices = new Vector3[9];

        public GroundWave(Point tilePosition, int depth, int count, bool rightwards, float peak, int columnDelay = 0, int duration = 30)
        {
            StartPosition = tilePosition;
            Position = tilePosition.ToWorldCoordinates();
            Velocity = Vector2.Zero;
            Scale = Vector2.One;
            Duration = duration;
            Lifetime = Duration + (columnDelay * count);
            Color = Color.White;
            Depth = depth;
            Count = count;
            Peak = peak;
            ColumnDelay = columnDelay;
            Direction = rightwards ? 1 : -1;
            ColumnPositions = new Point[Count];
            ColumnOffsets = new float[Count];
            for(int i = 0; i < Count; i++)
            {
                ColumnPositions[i] = CollisionUtils.FindSurfaceBelow(StartPosition + new Point(i * Direction, -8), true);
                ColumnOffsets[i] = 0f;
            }
        }

        public override void Update()
        {
            for (int i = 0; i < Count; i++)
            {
                float ratio = MathHelper.Clamp((Time - (ColumnDelay * i)) / (float)Duration, 0f, 1f);

                float heightRatio = MathHelper.Lerp(0.1f, 0.9f, i / (float)Count);
                float myPeak = MathF.Sin(heightRatio * MathHelper.Pi) * Peak;
                ColumnOffsets[i] = MathF.Sin(ratio * MathHelper.Pi) * myPeak;

                if (Time == (ColumnDelay * i))
                {
                    Tile t = Framing.GetTileSafely(ColumnPositions[i]);
                    int amt = WorldGen.KillTile_GetTileDustAmount(false, t, ColumnPositions[i].X, ColumnPositions[i].Y);
                    for (int j = 0; j < amt; j++)
                    {
                        int d = WorldGen.KillTile_MakeTileDust(ColumnPositions[i].X, ColumnPositions[i].Y, t);
                        Main.dust[d].velocity.Y -= myPeak / 18f * Main.rand.NextFloat();
                    }
                }
            }
            Time++;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            for(int i = 0; i < Count; i++)
            {
                Point start = ColumnPositions[i];
                float offset = ColumnOffsets[i];

                for (int j = 0; j < Depth; j++)
                {
                    Point myTilePosition = start + new Point(0, j);
                    Tile t = Framing.GetTileSafely(myTilePosition);

                    if (t == null || !t.HasTile)
                        continue;

                    Texture2D tileTex = TextureAssets.Tile[t.TileType].Value;

                    Vector2 drawPosition = myTilePosition.ToWorldCoordinates() + new Vector2(0f, -offset) - Main.screenPosition;

                    int shiftedTiles = (int)(offset / 16f);
                    Point lightingTile = myTilePosition - new Point(0, shiftedTiles);
                    Point nextLightingTile = myTilePosition - new Point(0, shiftedTiles + 1);
                    float lightingRatio = Utils.GetLerpValue(lightingTile.Y * 16, nextLightingTile.Y * 16, myTilePosition.Y * 16 - offset, true);

                    if (t.IsHalfBlock)
                    {
                        Color color = Color.Lerp(Lighting.GetColor(lightingTile), Lighting.GetColor(nextLightingTile), lightingRatio);
                        Vector2 topLeft = drawPosition - new Vector2(8f, 0f);

                        Rectangle top = new Rectangle(t.TileFrameX, t.TileFrameY, 16, 12);
                        spritebatch.Draw(tileTex, topLeft, top, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                        Rectangle lip = new Rectangle(144, 66, 16, 4);
                        spritebatch.Draw(tileTex, topLeft + new Vector2(0f, 12f), lip, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                        continue;
                    }
                    else if (t.Slope != SlopeType.Solid)
                    {
                        Color color = Color.Lerp(Lighting.GetColor(lightingTile), Lighting.GetColor(nextLightingTile), lightingRatio);

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

                            spritebatch.Draw(tileTex, drawPosition + new Vector2(num6, k * num2 + num3), new Rectangle(t.TileFrameX /*+ drawData.addFrX*/ + num6, t.TileFrameY /*+ drawData.addFrY*/ + num5, num2, num4), color, 0f, new Vector2(8f, 8f), 1f, 0 /*drawData.tileSpriteEffect*/, 0f);
                        }

                        int num7 = ((num <= 2) ? 14 : 0);
                        Main.spriteBatch.Draw(tileTex, drawPosition + new Vector2(0f, num7), new Rectangle(t.TileFrameX /*+ drawData.addFrX*/, t.TileFrameY /*+ drawData.addFrY*/ + num7, 16, 2), color, 0f, new Vector2(8f, 8f), 1f, 0 /*drawData.tileSpriteEffect*/, 0f);
                        continue;
                    }

                    GetVanillaTileSlices(lightingTile, ref _sliceA);
                    GetVanillaTileSlices(nextLightingTile, ref _sliceB);
                    for (int s = 0; s < 9; s++)
                        _blendedSlices[s] = Vector3.Lerp(_sliceA[s], _sliceB[s], lightingRatio);

                    for (int s = 0; s < 9; s++)
                    {
                        Rectangle slice = SliceOffsets[s];
                        Rectangle source = new(t.TileFrameX + slice.X, t.TileFrameY + slice.Y, slice.Width, slice.Height);

                        Vector2 sliceOrigin = new(8f - slice.X, 8f - slice.Y);
                        Color sliceColor = new(_blendedSlices[s]);

                        spritebatch.Draw(tileTex, drawPosition, source, sliceColor, Rotation, sliceOrigin, Scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        private static void GetVanillaTileSlices(Point tilePos, ref Vector3[] outSlices)
        {
            Lighting.GetColor9Slice(tilePos.X, tilePos.Y, ref outSlices);
            Vector3 flat = Lighting.GetColor(tilePos.X, tilePos.Y).ToVector3();

            for (int i = 0; i < 9; i++)
                outSlices[i] = (outSlices[i] + flat) * 0.5f;
        }
    }
}