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
        public Entity Body;
        public IKLeg PairedLeg;  // Diagonal partner
        public IKLeg SisterLeg;  // Same-side partner

        public Vector2 OriginOffset;
        public float BaseLengthA;
        public float BaseLengthB;
        public bool FrontSet;
        public bool LeftSet;
        public bool ForceLocked;

        private static float DesiredOutwardScale => 1.2f;
        private static float DesiredDownScale => 0.9f;

        private static float TooCloseMinFraction => 0.85f;
        private float TooCloseMinDist => LengthA * TooCloseMinFraction;

        // Release thresholds (fractions of MaxLength).
        private static float MaxExtensionBase => 0.90f; // releases when over-extended
        private static float MaxExtSisterReduction => 0.15f; // sister stepping tightens this
        private static float TrailingExtPenalty => 0.20f; // extra tightening for trailing back legs
        private static float MinExtensionBase => 0.26f; // releases when too compressed
        private static float MinExtSisterReduction => 0.16f; // sister stepping loosens this
        private static float LagThresholdBase => 40f;   // pixels trailing leg lags before a step fires
        private static float LeadingLagThreshold => 60f; // pixels desired can be ahead of a leading foot before stepping

        // Step animation.
        private static float StepArcHeight => 11.2f; // peak lift during a normal step
        private static float StepSnapArcHeight => 4.5f;  // lift during the post-fall MoveTowards snap
        private static float StepBaseTime => 0.30f; // seconds for a full step at rest
        private static float StepFastTimeReduction => 0.12f; // max time reduction at high body speed
        private static float StepFastSpeedMin => 4f;    // speed at which reduction begins
        private static float StepFastSpeedMax => 8f;    // speed at which max reduction is reached

        // Landing.
        private static float GrabDelayOnRelease => 3f;    // frames both legs in a pair wait after one releases
        private static float LandSnapBaseSpeed => 10f;   // MoveTowards speed for the first post-fall plant
        private static float LandSnapFallBonus => 15f;   // extra speed added at full fall time
        private static float LandSnapFallStart => 20f;   // FallTime at which snap bonus begins
        private static float LandSnapFallEnd => 40f;   // FallTime at which snap bonus is maxed
        private static float LandingStabDuration => 0.30f; // seconds the ground-stab animation plays

        // Visual.
        private static float GroundPierceDepth => 7f;    // pixels End extends below LegTip normally
        private static float LandingStabExtraDepth => 10f;   // additional pierce on landing (ease-in)

        // Hanging / flail.
        private static float FlailVelocityThreshold => 2f;    // velocity.Y above which airborne flail starts
        private static float FlailHeightOffset => 100f;  // how far above desired the flail centre sits
        private static float FlailRampFrames => 8f;    // frames over which flail lerp ramps to max
        private static float FlailMaxLerpRate => 0.1f;  // max lerp fraction per frame toward flail target
        private static float FlailGripForgetTime => 10f;   // FallTime before previous grip is cleared
        private static float DroopSpeed => 4.2f;   // pixels/frame the leg droops when gripless on ground

        // Velocity offset: trailing legs shift their desired grab toward the direction of travel.
        private static float VelocityOffsetThreshold => 2f;   // body speed needed to apply offset
        private static float VelocityOffsetAmount => 140f; // pixels of lateral shift
        private static float LeadingVelocityLead => 12;

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

        private float VelocityXOffset => Math.Abs(Body.velocity.X) > VelocityOffsetThreshold && Body.velocity.X * Direction < 0f ? Math.Sign(Body.velocity.X) * VelocityOffsetAmount : 0f;

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

            End = LegTip + Vector2.UnitY * GroundPierceDepth;
            if (LatchedOn && StepTimer < 1f)
                End.Y += LandingStabExtraDepth * (float)Math.Pow(StepTimer, 2f);

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

            StepTimer = Math.Max(0f, StepTimer - 1f / (60f * LandingStabDuration));
        }

        private void HandleHanging()
        {
            State = LegState.Hanging;

            if (Body.velocity.Y > FlailVelocityThreshold)
            {
                FallTime++;

                Vector2 flailTarget = DesiredGrabPosition - Vector2.UnitY * FlailHeightOffset;
                flailTarget += new Vector2(
                    (float)Math.Sin(Main.GlobalTimeWrappedHourly * 30f) * 40f,
                    21f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 40f) * 70f
                );

                if (!FrontSet)
                    flailTarget.X -= Direction * 30f;

                flailTarget.X -= VelocityXOffset;

                float t = Math.Min(1f, FallTime / FlailRampFrames) * FlailMaxLerpRate;
                LegTip = Vector2.Lerp(LegTip, flailTarget, t);

                if (FallTime > FlailGripForgetTime)
                    PreviousGrabPosition = null;
            }
            else
            {
                LegTip.Y += DroopSpeed;
                if (Collision.SolidCollision(LegTip - Vector2.One, 2, 2))
                    LegTip.Y -= DroopSpeed;
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
                LegTip.Y -= StepArcHeight * (float)Math.Sin((1f - StrideTimer) * MathHelper.Pi);
            }
            else
            {
                float speed = LandSnapBaseSpeed + Utils.GetLerpValue(LandSnapFallStart, LandSnapFallEnd, FallTime, true) * LandSnapFallBonus;
                LegTip = LegTip.MoveTowards(GrabPosition!.Value, speed);
                LegTip.Y -= StepSnapArcHeight * Utils.GetLerpValue(0f, 50f, Math.Abs(LegTip.X - GrabPosition.Value.X), true);
            }

            float stepTime = StepBaseTime - StepFastTimeReduction * Utils.GetLerpValue(StepFastSpeedMin, StepFastSpeedMax, Math.Abs(Body.velocity.X), true);
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
                GrabDelay = GrabDelayOnRelease;

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

            float maxExt = MaxExtensionBase - SisterInfluence * MaxExtSisterReduction;
            if (!FrontSet && !isLeading)
                maxExt -= (1f - SisterInfluence) * TrailingExtPenalty;

            if (Math.Abs(Body.velocity.X) < 1.4f)
                maxExt = MaxExtensionBase;

            float minExt = MinExtensionBase - SisterInfluence * MinExtSisterReduction;
            float lagThresh = (0.25f + SisterInfluence * 0.75f) * LagThresholdBase;

            // Too extended: foot is being dragged out of reach.
            if (extension > MaxLength * maxExt)
                return true;

            // Too compressed: body has moved over the foot.
            if (extension < MaxLength * minExt)
            {
                noDelay = true;
                return true;
            }

            // Foot has been left too far behind the body laterally.
            // Leading legs use a separate check: how far the geometric desired position
            // has moved ahead of the current foot in the direction of travel. This fires
            // much sooner than waiting for the body to fully overtake the foot.
            if (isLeading)
            {
                float baseDesiredX = Start.X + Direction * LengthB * DesiredOutwardScale;
                if ((baseDesiredX - LegTip.X) * Direction > LeadingLagThreshold)
                {
                    noDelay = true;
                    return true;
                }
            }
            // Trailing leg: foot has been left too far behind the body laterally.
            else if ((Start.X - LegTip.X) * Direction > lagThresh && StepTimer <= 0f)
            {
                noDelay = true;
                return true;
            }

            // Foot is too high and too close — body has jumped over it.
            if (Start.Y - LegTip.Y > 30f && (LegTip.X - Start.X) * Direction < MaxLength * 0.2f)
                return true;

            return false;
        }

        private void UpdateDesiredGrabPosition()
        {
            DesiredGrabPosition = Start + new Vector2(Direction * LengthB * DesiredOutwardScale, LengthA * DesiredDownScale);
            DesiredGrabPosition.X += VelocityXOffset;
            if (Body.velocity.X * Direction > 0f && Math.Abs(Body.velocity.X) > VelocityOffsetThreshold)
                DesiredGrabPosition.X += Body.velocity.X * LeadingVelocityLead;
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

            if (best.HasValue && TileToGripPoint(best.Value).Distance(Start) < TooCloseMinDist)
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

                if (TileToGripPoint(hit.Value).Distance(Start) < TooCloseMinDist)
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

            if (Math.Abs(Body.velocity.X) > VelocityOffsetThreshold)
            {
                if (Body.velocity.X * Direction < 0f)
                    origin.X += Math.Sign(Body.velocity.X) * VelocityOffsetAmount;
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

            if (GrabPosition == null || point.Distance(DesiredGrabPosition) < GrabPosition.Value.Distance(DesiredGrabPosition))
                GrabPosition = point;
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

                if (t.HasUnactuatedTile && (Main.tileSolid[t.TileType] || (Main.tileSolidTop[t.TileType] && t.TileFrameY == 0)))
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
            if (TestLegs == null)
            {
                TestLegs = new IKLeg[4];
                TestLegs[0] = new(Main.LocalPlayer, new(-60, -60), 100, 75, true, true); //Back Left
                TestLegs[1] = new(Main.LocalPlayer, new(60, -60), 100, 75, true, false); //Back Right
                TestLegs[2] = new(Main.LocalPlayer, new(-20, -60), 75, 75, true, true); //Front Left
                TestLegs[3] = new(Main.LocalPlayer, new(20, -60), 75, 75, true, false); //Front Right

                TestLegs[0].SisterLeg = TestLegs[3];
                TestLegs[3].SisterLeg = TestLegs[0];

                TestLegs[0].PairedLeg = TestLegs[2];
                TestLegs[2].PairedLeg = TestLegs[0];

                TestLegs[1].SisterLeg = TestLegs[2];
                TestLegs[2].SisterLeg = TestLegs[1];

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