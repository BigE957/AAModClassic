using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
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

        Point TilePosition;
        private float Offset = 0f;
        private readonly int Depth = 1;
        private readonly float Peak = 16f;
        private readonly int Duration = 30;

        private Vector3[] _sliceA = new Vector3[9];
        private Vector3[] _sliceB = new Vector3[9];
        private readonly Vector3[] _blendedSlices = new Vector3[9];

        public GroundWave(Point tilePosition, int depth, float peak, int startDelay = 0, int duration = 30)
        {
            TilePosition = tilePosition;
            Position = tilePosition.ToWorldCoordinates();
            Velocity = Vector2.Zero;
            Scale = Vector2.One;
            Duration = duration;
            Lifetime = Duration + startDelay;
            Color = Color.White;
            Depth = depth;
            Peak = peak;
            Time = -startDelay;
        }

        public override void Update()
        {
            float ratio = MathHelper.Clamp(Time / (float)Duration, 0f, 1f);

            Offset = MathF.Sin(ratio * MathHelper.Pi) * Peak;

            Time++;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < Depth; i++)
            {
                Point myTilePosition = TilePosition + new Point(0, i);
                Tile t = Framing.GetTileSafely(myTilePosition);

                if (t == null || !t.HasTile)
                    continue;

                Texture2D tileTex = TextureAssets.Tile[t.TileType].Value;
                
                Vector2 drawPosition = Position + new Vector2(0f, (i * 16f) - Offset) - Main.screenPosition;

                int shiftedTiles = (int)(Offset / 16f);
                Point lightingTile = myTilePosition - new Point(0, shiftedTiles);
                Point nextLightingTile = myTilePosition - new Point(0, shiftedTiles + 1);
                float lightingRatio = Utils.GetLerpValue(lightingTile.Y * 16, nextLightingTile.Y * 16, myTilePosition.Y * 16 - Offset, true);

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
                    for (int j = 0; j < 8; j++)
                    {
                        int num3 = j * -2;
                        int num4 = 16 - j * 2;
                        int num5 = 16 - num4;
                        int num6;
                        switch (num)
                        {
                            case 1:
                                num3 = 0;
                                num6 = j * 2;
                                num4 = 14 - j * 2;
                                num5 = 0;
                                break;
                            case 2:
                                num3 = 0;
                                num6 = 16 - j * 2 - 2;
                                num4 = 14 - j * 2;
                                num5 = 0;
                                break;
                            case 3:
                                num6 = j * 2;
                                break;
                            default:
                                num6 = 16 - j * 2 - 2;
                                break;
                        }

                        spritebatch.Draw(tileTex, drawPosition + new Vector2(num6, j * num2 + num3), new Rectangle(t.TileFrameX /*+ drawData.addFrX*/ + num6, t.TileFrameY /*+ drawData.addFrY*/ + num5, num2, num4), color, 0f, new Vector2(8f, 8f), 1f, 0 /*drawData.tileSpriteEffect*/, 0f);
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

        private static void GetVanillaTileSlices(Point tilePos, ref Vector3[] outSlices)
        {
            Lighting.GetColor9Slice(tilePos.X, tilePos.Y, ref outSlices);
            Vector3 flat = Lighting.GetColor(tilePos.X, tilePos.Y).ToVector3();

            for (int i = 0; i < 9; i++)
                outSlices[i] = (outSlices[i] + flat) * 0.5f;
        }
    }
}