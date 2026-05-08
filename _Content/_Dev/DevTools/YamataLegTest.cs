using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.DevTools
{
    public enum LegState
    {
        Planted,
        Moving,
        Hanging
    }

    public class IKLeg
    {
        public readonly Entity Body;
        public IKLeg PairedLeg;  // Diagonal partner
        public IKLeg SisterLeg;  // Same-side partner

        private readonly Vector2 OriginOffset;
        private readonly float BaseLengthA;
        private readonly float BaseLengthB;
        public readonly bool FrontSet;
        public readonly bool LeftSet;
        public bool ForceLocked;

        public Vector2 Start => Body.Center + OriginOffset;
        public Vector2 Middle;
        public Vector2 End;

        public bool LatchedOn;
        public Vector2? GrabPosition; 
        public Vector2? PreviousGrabPosition;
        public Vector2 DesiredGrabPosition;
        public Vector2 LegTip;

        public float GrabDelay;
        public float StrideTimer;
        public float StepTimer;
        public float FallTime;

        public LegState State { get; private set; } = LegState.Hanging;

        public float LengthA => BaseLengthA; //* Body.scale;
        public float LengthB => BaseLengthB; //* Body.scale;
        public float MaxLength => LengthA + LengthB;
        public int Direction => LeftSet ? -1 : 1;

        public float SisterInfluence => SisterLeg?.LatchedOn == true ? SisterLeg.StrideTimer : 1f;

        private float VelocityXOffset => Math.Abs(Body.velocity.X) > 2f && Body.velocity.X * Direction < 0f ? Math.Sign(Body.velocity.X) * 140f : 0f;

        public IKLeg(Entity body, Vector2 originOffset, float lengthA, float lengthB, bool frontSet, bool leftSet)
        {
            Body = body;
            OriginOffset = originOffset;
            BaseLengthA = lengthA;
            BaseLengthB = lengthB;
            FrontSet = frontSet;
            LeftSet = leftSet;
            LegTip = Start + Vector2.UnitY * MaxLength;
            Middle = Start + Vector2.UnitY * LengthA;
            End = LegTip;
        }

        public void Update()
        {
            UpdateDesiredGrabPosition();

            Dust.NewDustPerfect(DesiredGrabPosition, DustID.LifeDrain, Vector2.Zero);
            if(GrabPosition.HasValue)
                Dust.NewDustPerfect(GrabPosition.Value, DustID.Shadowflame, Vector2.Zero);

            if (GrabDelay > 0f)
                GrabDelay--;

            LatchedOn = GrabPosition.HasValue && Vector2.Distance(LegTip, GrabPosition.Value) < 10f;
            if (LatchedOn)
                LegTip = GrabPosition.Value;

            if (LatchedOn)
            {
                HandlePlanted();
            }
            else
            {
                if (!GrabPosition.HasValue)
                    FindGrabPosition();

                if (!GrabPosition.HasValue)
                    HandleHanging();
                else
                    HandleMoving();
            }

            End = LegTip + Vector2.UnitY * 7f;
            if (LatchedOn && StepTimer < 1f)
                End.Y += 10f * (float)Math.Pow(StepTimer, 2f);

            Vector2 toEnd = End - Start;
            float dist = toEnd.Length();
            if (dist > MaxLength)
                End = Start + toEnd / dist * MaxLength;

            SolveIK();
        }

        private void HandlePlanted()
        {
            State = LegState.Planted;
            FallTime = 0f;

            if (!ForceLocked && ShouldRelease(out bool noDelay))
            {
                ReleaseGrip();
                if (noDelay)
                    GrabDelay = 0f;
            }

            StepTimer = Math.Max(0f, StepTimer - 1f / (60f * 0.3f));
        }

        private void HandleHanging()
        {
            State = LegState.Hanging;

            if (Body.velocity.Y > 2f)
            {
                FallTime++;

                Vector2 flailTarget = DesiredGrabPosition - Vector2.UnitY * 100f;
                flailTarget += new Vector2(
                    (float)Math.Sin(Main.GlobalTimeWrappedHourly * 30f) * 40f,
                    21f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 40f) * 70f
                );

                if (!FrontSet)
                    flailTarget.X -= Direction * 30f;

                flailTarget.X -= VelocityXOffset;

                float lerpAmount = Math.Min(1f, FallTime / 8f) * 0.1f;
                LegTip = Vector2.Lerp(LegTip, flailTarget, lerpAmount);

                if (FallTime > 10f)
                    PreviousGrabPosition = null;
            }
            else
            {
                LegTip.Y += 4.2f;
                if (Collision.SolidCollision(LegTip - Vector2.One, 2, 2))
                    LegTip.Y -= 4.2f;
            }

            StepTimer = 1f;
        }

        private void HandleMoving()
        {
            State = LegState.Moving;

            if (PreviousGrabPosition.HasValue)
            {
                float progress = MathUtils.PolyInOutEasing(1f - StrideTimer, 2f);
                LegTip = Vector2.Lerp(PreviousGrabPosition.Value, GrabPosition!.Value, progress);
                LegTip.Y -= 11.2f * (float)Math.Sin((1f - StrideTimer) * MathHelper.Pi);
            }
            else
            {
                float speed = 10f + Utils.GetLerpValue(20f, 40f, FallTime, true) * 15f;
                LegTip = LegTip.MoveTowards(GrabPosition!.Value, speed);
                LegTip.Y -= 4.5f * Utils.GetLerpValue(0f, 50f, Math.Abs(LegTip.X - GrabPosition.Value.X), true);
            }

            float stepTime = 0.3f - 0.12f * Utils.GetLerpValue(4f, 8f, Math.Abs(Body.velocity.X), true);
            StrideTimer -= 1f / (60f * stepTime);

            if (Vector2.Distance(Start, GrabPosition!.Value) > MaxLength)
            {
                ReleaseGrip();
                return;
            }

            if (StrideTimer <= 0f)
                Land();

            StepTimer = 1f;
        }

        private void Land()
        {
            StrideTimer = 0f;
            LegTip = GrabPosition!.Value;
            LatchedOn = true;
            StepTimer = 1f;
            FallTime = 0f;

            // TODO: Add Stomp Sounds + Effects
            // float volume = Utils.GetLerpValue(0.5f, 1f, stepEffectForce);
            // SoundEngine.PlaySound(StepSound with { Volume = volume * 0.4f }, LegTip);
            // Collision.HitTiles(LegTip, Vector2.Zero, 9, 9);
        }

        private void ReleaseGrip()
        {
            if (PairedLeg != null && PairedLeg.GrabDelay < 1f && GrabDelay < 1f)
                GrabDelay = 3f;

            StrideTimer = 1f;
            PreviousGrabPosition = GrabPosition ?? LegTip;
            GrabPosition = null;
            LatchedOn = false;
        }

        private bool ShouldRelease(out bool noDelay)
        {
            noDelay = false;

            float extension = Vector2.Distance(LegTip, Start);
            bool isLeading = Math.Sign(Body.velocity.X) == Direction;

            float maxExt = 1f - SisterInfluence * 0.15f;
            if (!FrontSet && !isLeading)
                maxExt -= (1f - SisterInfluence) * 0.2f;
            
            if (Math.Abs(Body.velocity.X) < 1.4f)
                maxExt = 1f;

            float minExt = 0.26f - SisterInfluence * 0.16f;
            float lagThresh = (0.25f + SisterInfluence * 0.75f) * 40f;

            if (extension > MaxLength * maxExt)
                return true;

            if (extension < MaxLength * minExt)
            {
                noDelay = true;
                return true;
            }

            if ((Start.X - LegTip.X) * Direction > lagThresh && (isLeading || StepTimer <= 0f))
            {
                noDelay = true;
                return true;
            }

            if (Start.Y - LegTip.Y > 30f && (LegTip.X - Start.X) * Direction < MaxLength * 0.2f)
                return true;

            return false;
        }

        private void UpdateDesiredGrabPosition()
        {
            Vector2 hangDir = (Vector2.UnitX * Direction * 0.9f + Vector2.UnitY * 0.6f).SafeNormalize(Vector2.UnitY);
            //Dust.QuickDustLine(Start, Start + hangDir * 100, 8, Color.Purple);
            DesiredGrabPosition = Start + hangDir * MaxLength * 0.9f;
            DesiredGrabPosition.X += VelocityXOffset;

            float d = Vector2.Distance(DesiredGrabPosition, Start);
            if (d >= MaxLength)
                DesiredGrabPosition = Start + (DesiredGrabPosition - Start) / d * MaxLength;
        }

        private void FindGrabPosition()
        {
            if (GrabDelay > 0f)
                return;

            bool isLeading = Math.Sign(Body.velocity.X) == Direction;

            Vector2 shoulder = Start;
            Vector2 target = DesiredGrabPosition;

            if (isLeading)
            {
                shoulder.X += Body.velocity.X * 40f;
                target.X += Body.velocity.X * 10f;
                target.Y -= 20f;

                if (Vector2.Distance(target, Start) > MaxLength) target = Start + Vector2.Normalize(target - Start) * MaxLength;
                if (Vector2.Distance(shoulder, Start) > MaxLength) shoulder = Start + Vector2.UnitX * Direction * MaxLength;
            }

            Point? best = RaycastToTile(shoulder, target);
            bool tooClose = false;

            if (best.HasValue && TileToGripPoint(best.Value).Distance(Start) < MaxLength * 0.45f)
            {
                tooClose = true;
                best = null;
            }

            if (best == null && !tooClose)
                best = RadialDownScan(4, 1.2f, ref tooClose);

            if (best == null || tooClose)
            {
                float radius = MaxLength * (FrontSet ? 0.8f : 0.6f);
                float startAngle = isLeading ? MathHelper.PiOver4 : MathHelper.PiOver2 * 0.8f;
                best = RadialArcScan(startAngle, MathHelper.Pi * 0.95f, radius);
            }

            if (best.HasValue)
                TryConfirmGrabPos(best.Value);
        }

        private Point? RadialDownScan(int iterations, float totalAngle, ref bool tooClose)
        {
            Vector2 toDesired = Vector2.Normalize(DesiredGrabPosition - Start);

            for (int i = 0; i < iterations; i++)
            {
                Vector2 dir = toDesired.RotatedBy(i / (float)iterations * totalAngle * Direction);
                Point? hit = RaycastToTile(Start, Start + dir * MaxLength * 0.95f);

                if (!hit.HasValue)
                    continue;

                if (TileToGripPoint(hit.Value).Distance(Start) < MaxLength * 0.45f)
                {
                    tooClose = true;
                    continue;
                }

                tooClose = false;
                return hit;
            }

            return null;
        }

        private Point? RadialArcScan(float angleStart, float totalAngle, float radius)
        {
            Vector2 origin = Start;

            if (Math.Abs(Body.velocity.X) > 2f)
            {
                if (Body.velocity.X * Direction < 0f)
                    origin.X += Math.Sign(Body.velocity.X) * 140f;
                else
                    radius = Math.Min(MaxLength, radius * 1.2f);
            }

            float step = ArcAngle(8f, radius) / totalAngle;
            float progress = 0f;
            bool lastAir = false;

            var surface = new List<Point>();
            var interior = new List<Point>();

            while (progress <= 1f)
            {
                float angle = (angleStart + progress * totalAngle) * Direction;
                Vector2 pos = origin + (-Vector2.UnitY).RotatedBy(angle) * radius;
                Point tile = pos.ToTileCoordinates();
                Tile t = Framing.GetTileSafely(tile);

                bool solid = t.HasUnactuatedTile &&
                             (Main.tileSolid[t.TileType] || (Main.tileSolidTop[t.TileType] && t.TileFrameY == 0));

                if (solid)
                {
                    if (lastAir) surface.Add(tile);
                    else interior.Add(tile);
                }

                lastAir = !solid && (!t.HasUnactuatedTile || !TileID.Sets.Platforms[t.TileType]);
                progress += step;
            }

            if (surface.Count > 0) return surface.OrderByDescending(RateTileCandidate).First();
            if (interior.Count > 0) return interior.OrderByDescending(RateTileCandidate).First();
            return null;
        }

        private float RateTileCandidate(Point p)
        {
            Vector2 world = TileToGripPoint(p);
            float dist = world.Distance(Start);

            Vector2 pos = world;
            if (pos.X < Start.X) pos.X += (Start.X - pos.X) * 2f;
            float angle = Start.AngleTo(pos);

            Vector2 ideal = DesiredGrabPosition;
            if (ideal.X < Start.X) ideal.X += (Start.X - ideal.X) * 2f;
            float idealAngle = Start.AngleTo(ideal);

            float angleFit = 1f - Math.Abs(angle - idealAngle) / MathHelper.PiOver2;
            float distFit = Utils.GetLerpValue(0f, MaxLength * 0.85f, dist, true);

            float penalty = 0f;
            if (Main.tileSolidTop[Framing.GetTileSafely(p).TileType])
                penalty = Utils.GetLerpValue(16f, 80f, Start.Y - world.Y, true);

            return angleFit * distFit - penalty;
        }

        private void TryConfirmGrabPos(Point candidate)
        {
            Vector2 point = TileToGripPoint(candidate);

            if (GrabPosition == null ||
                point.Distance(DesiredGrabPosition) < GrabPosition.Value.Distance(DesiredGrabPosition))
            {
                GrabPosition = point;
            }
        }

        private Vector2 TileToGripPoint(Point tilePos)
        {
            Tile t = Framing.GetTileSafely(tilePos);
            Vector2 world = tilePos.ToWorldCoordinates();
            Vector2 min = world - new Vector2(9f);
            Vector2 max = world + new Vector2(9f);

            if (t.IsHalfBlock || t.Slope != SlopeType.Solid)
                min.Y += 8f;

            return Vector2.Clamp(Start, min, max);
        }

        private static Point? RaycastToTile(Vector2 from, Vector2 to, int steps = 24)
        {
            for (int i = 0; i <= steps; i++)
            {
                Vector2 pos = Vector2.Lerp(from, to, i / (float)steps);
                Point tile = pos.ToTileCoordinates();
                Tile t = Framing.GetTileSafely(tile);

                if (t.HasUnactuatedTile &&
                    (Main.tileSolid[t.TileType] || (Main.tileSolidTop[t.TileType] && t.TileFrameY == 0)))
                    return tile;
            }

            return null;
        }

        private static float ArcAngle(float chord, float radius) => 2f * (float)Math.Asin(Math.Min(1f, chord / (2f * radius)));

        private void SolveIK()
        {
            Vector2 origin = Start;
            Vector2 target = End;
            Vector2 toTarget = target - origin;
            float dist = toTarget.Length();

            if (dist < 1e-4f)
            {
                Middle = origin + new Vector2(Direction, 1f) * (LengthA * 0.70710678f);
                return;
            }

            float clampedDist = MathHelper.Clamp(dist, Math.Abs(LengthA - LengthB) + 0.01f, MaxLength - 0.01f);
            toTarget = toTarget / dist * clampedDist;

            float cosA = (LengthA * LengthA + clampedDist * clampedDist - LengthB * LengthB) / (2f * LengthA * clampedDist);
            cosA = MathHelper.Clamp(cosA, -1f, 1f);

            float alpha = (float)Math.Acos(cosA);
            float baseAngle = (float)Math.Atan2(toTarget.Y, toTarget.X);

            float midAngle = baseAngle + alpha * (LeftSet ? 1f : -1f);
            Middle = origin + new Vector2((float)Math.Cos(midAngle), (float)Math.Sin(midAngle)) * LengthA;

            End = origin + toTarget;
        }
    }

    public class YamataLegTest : ModSystem
    {
        public override void Load()
        {
            On_Main.DrawNPCs += DrawYamataLegTest;
        }

        IKLeg[] TestLegs = null;

        public override void PreUpdateWorld()
        {
            if(TestLegs == null)
            {
                TestLegs = new IKLeg[4];
                TestLegs[0] = new(Main.LocalPlayer, new(-20, -60), 100, 75, true, true);
                TestLegs[1] = new(Main.LocalPlayer, new(20, -60), 100, 75, true, false);
                TestLegs[2] = new(Main.LocalPlayer, new(-60, -60), 100, 75, true, true);
                TestLegs[3] = new(Main.LocalPlayer, new(60, -60), 100, 75, true, false);

                TestLegs[0].SisterLeg = TestLegs[1];
                TestLegs[1].SisterLeg = TestLegs[0];

                TestLegs[0].PairedLeg = TestLegs[2];
                TestLegs[2].PairedLeg = TestLegs[0];

                TestLegs[2].PairedLeg = TestLegs[3];
                TestLegs[3].PairedLeg = TestLegs[2];

                TestLegs[1].PairedLeg = TestLegs[3];
                TestLegs[3].PairedLeg = TestLegs[1];
            }

            foreach (IKLeg leg in TestLegs)
                leg.Update();
        }

        private void DrawYamataLegTest(On_Main.orig_DrawNPCs orig, Main self, bool behindTiles)
        {
            orig(self, behindTiles);
            if (TestLegs != null)
            {
                int i = 0;
                foreach (IKLeg leg in TestLegs)
                {
                    Color c = i switch
                    {
                        0 => Color.Red,
                        1 => Color.Yellow,
                        2 => Color.Green,
                        _ => Color.Blue
                    };
                    Texture2D line = ModContent.Request<Texture2D>("AAModClassic/_Unofficial/Desert/Line").Value;
                    float startToMid = leg.Start.AngleTo(leg.Middle);
                    Main.spriteBatch.Draw(line, leg.Start - Main.screenPosition, null, c.MultiplyRGB(Color.White * 0.5f), startToMid, new Vector2(0, line.Height / 2f), new Vector2(leg.LengthA / line.Width, 4), 0, 0);

                    float midToEnd = leg.Middle.AngleTo(leg.LegTip);
                    Main.spriteBatch.Draw(line, leg.Middle - Main.screenPosition, null, c, midToEnd, new Vector2(0, line.Height / 2f), new Vector2(leg.LengthB / line.Width, 4), 0, 0);
                    i++;
                }
            }
        }

        public static Vector2 InverseKinematic(Vector2 start, Vector2 end, float lengthA, float lengthB, bool flip)
        {
            float dist = Vector2.Distance(start, end);
            float angle = (float)Math.Acos(Math.Clamp((dist * dist + lengthA * lengthA - lengthB * lengthB) / (2f * dist * lengthA), -1f, 1));
            if (flip)
                angle *= -1;
            return start + (angle + start.AngleTo(end)).ToRotationVector2() * lengthA;
        }
    }
}
