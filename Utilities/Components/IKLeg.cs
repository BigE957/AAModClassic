using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Components
{
    public enum LegState
    {
        Planted,
        Moving,
        Hanging
    }

    public class IKLeg
    {
        public NPC Body;
        public IKLeg PairedLeg;  // Diagonal partner
        public IKLeg SisterLeg;  // Same-side partner

        public Vector2 OriginOffset { get; private set; }
        public float BaseLengthA { get; private set; }
        public float BaseLengthB { get; private set; }
        public bool FrontSet { get; private set; }
        public bool LeftSet { get; private set; }

        private readonly float GroundVisualOffset = 0;
        private readonly int StepWidth = 0;
        private readonly float StompVolumeStrength = 0.4f;

        public bool ForceLocked;

        // Desired foot position
        private static float DesiredOutwardScale => 1.2f;
        private static float DesiredDownScale => 0.9f;

        // Minimum acceptable grip distance
        private static float TooCloseMinFraction => 0.85f;
        private float TooCloseMinDist => LengthA * TooCloseMinFraction;

        // Release thresholds (fractions of MaxLength).
        private static float MaxExtensionBase => 0.90f; // releases when over-extended
        private static float MaxExtSisterReduction => 0.15f; // sister stepping tightens this
        private static float TrailingExtPenalty => 0.20f; // extra tightening for trailing back legs
        private static float MinExtensionBase => 0.4f; // releases when too compressed
        private static float MinExtSisterReduction => 0.16f; // sister stepping loosens this
        private static float LagThresholdBase => 40f;   // pixels trailing leg lags before a step fires
        private static float LeadingMinExtension => 50f;   // foot must be this far ahead of Start before the leading leg will hold; releases when it drops below this

        // Step animation.
        private static float StepArcHeight => 24f; // peak lift during a normal step
        private static float StepSnapArcHeight => 4.5f;  // lift during the post-fall MoveTowards snap
        private static float StepBaseTime => 0.40f; // seconds for a full step at rest
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
        private static float LandingStabExtraDepth => 10f;   // additional pierce on landing (ease-in)

        // Hanging / flail.
        private static float FlailVelocityThreshold => 2f;    // velocity.Y above which airborne flail starts
        private static float FlailHeightOffset => 100f;  // how far above desired the flail centre sits
        private static float FlailRampFrames => 8f;    // frames over which flail lerp ramps to max
        private static float FlailMaxLerpRate => 0.1f;  // max lerp fraction per frame toward flail target
        private static float FlailGripForgetTime => 10f;   // FallTime before previous grip is cleared
        private static float DroopSpeed => 4.2f;   // pixels/frame the leg droops when gripless on ground

        // Velocity offset
        private static float VelocityOffsetThreshold => 2f;   // body speed needed to apply offset
        private static float VelocityOffsetAmount => 140f; // pixels of lateral shift

        // Leading leg extension
        private static float LeadingExtendReach => 0.90f;

        public Vector2 Start => Body.Center + (OriginOffset * Body.scale);
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

        public float LengthA => BaseLengthA * Body.scale;
        public float LengthB => BaseLengthB * Body.scale;
        public float MaxLength => LengthA + LengthB;
        public int Direction => LeftSet ? -1 : 1;
        public float SisterInfluence => SisterLeg?.LatchedOn == true ? SisterLeg.StrideTimer : 1f;

        private float VelocityXOffset
        {
            get
            {
                if (Math.Abs(Body.velocity.X) > VelocityOffsetThreshold && Body.velocity.X * Direction < 0f)
                    return Math.Sign(Body.velocity.X) * VelocityOffsetAmount * Math.Clamp(Math.Abs(Body.velocity.X) / VelocityOffsetThreshold, 0f, 1f);
                return 0f;
            }
        }

        public IKLeg(NPC body, Vector2 originOffset, float lengthA, float lengthB, bool frontSet, bool leftSet, int visualYOffset = 0, int footWidth = 8, float stompVolumeMult = 0.4f)
        {
            Body = body;
            OriginOffset = originOffset;
            BaseLengthA = lengthA;
            BaseLengthB = lengthB;
            FrontSet = frontSet;
            LeftSet = leftSet;
            Middle = Start + Vector2.UnitX * LengthA * Direction;
            LegTip = Middle + Vector2.UnitY * LengthB;
            End = LegTip;
            GroundVisualOffset = visualYOffset;
            StepWidth = footWidth;
            StompVolumeStrength = stompVolumeMult;
        }

        public void Update(List<IKLeg> allLegs)
        {

            UpdateDesiredGrabPosition();

            if (GrabDelay > 0f)
                GrabDelay--;

            bool wasLatched = LatchedOn;
            LatchedOn = GrabPosition.HasValue && Vector2.Distance(LegTip, GrabPosition.Value) < 10f;
            if (LatchedOn)
                LegTip = GrabPosition.Value;

            if (LatchedOn)
            {
                if (!wasLatched)
                    Land();

                IEnumerable<IKLeg> sameSideLegs = allLegs?.Where(l => l.LeftSet == LeftSet);
                HandlePlanted(sameSideLegs);
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

            End = LegTip + Vector2.UnitY * GroundVisualOffset;
            if (LatchedOn && StepTimer < 1f)
                End.Y += LandingStabExtraDepth * (float)Math.Pow(StepTimer, 2f);

            Vector2 toEnd = End - Start;
            float dist = toEnd.Length();
            if (dist > MaxLength)
                End = Start + toEnd / dist * MaxLength;

            SolveIK();
        }

        private void HandlePlanted(IEnumerable<IKLeg> sameSideLegs)
        {
            State = LegState.Planted;
            FallTime = 0f;

            if (!ForceLocked && ShouldRelease(sameSideLegs, out bool noDelay))
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
                LegTip = Vector2.Lerp(PreviousGrabPosition.Value, GrabPosition.Value, progress);
                LegTip.Y -= StepArcHeight * (float)Math.Sin((1f - StrideTimer) * MathHelper.Pi);
            }
            else
            {
                float speed = LandSnapBaseSpeed + Utils.GetLerpValue(LandSnapFallStart, LandSnapFallEnd, FallTime, true) * LandSnapFallBonus;
                LegTip = LegTip.MoveTowards(GrabPosition.Value, speed);
                LegTip.Y -= StepSnapArcHeight * Utils.GetLerpValue(0f, 50f, Math.Abs(LegTip.X - GrabPosition.Value.X), true);
            }

            float stepTime = StepBaseTime - StepFastTimeReduction * Utils.GetLerpValue(StepFastSpeedMin, StepFastSpeedMax, Math.Abs(Body.velocity.X), true);
            StrideTimer -= 1f / (60f * stepTime);
            if (StrideTimer <= 0.1f)
                Land();

            StepTimer = 1f;
        }


        private void Land()
        {
            StrideTimer = 0f;
            LegTip = GrabPosition.Value;
            LatchedOn = true;
            StepTimer = 1f;
            float stepEffectForce = FallTime == 0 ? 0.5f : 2f;
            FallTime = 0f;

            float volume = stepEffectForce * StompVolumeStrength;
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Stomp") with { Volume = volume, MaxInstances = 10, Pitch = Main.rand.NextFloat(-0.1f, 0.33f) }, LegTip);
            Collision.HitTiles(LegTip - Vector2.UnitX * (StepWidth / 2), Vector2.Zero, StepWidth, 12);
        }

        private void ReleaseGrip()
        {
            if (PairedLeg != null && PairedLeg.GrabDelay < 1f && GrabDelay < 1f)
            {
                GrabDelay = GrabDelayOnRelease;
                PairedLeg.GrabDelay = GrabDelayOnRelease;
            }

            if (SisterLeg != null && SisterLeg.GrabDelay < 1f)
            {
                SisterLeg.GrabDelay = GrabDelayOnRelease;
            }

            StrideTimer = 1f;
            PreviousGrabPosition = GrabPosition ?? LegTip;
            GrabPosition = null;
            LatchedOn = false;
        }

        private bool ShouldRelease(IEnumerable<IKLeg> sameSideLegs, out bool noDelay)
        {
            noDelay = false;

            float extension = Vector2.Distance(LegTip, Start);
            bool isLeading = Math.Sign(Body.velocity.X) == Direction;

            if (extension > MaxLength * 0.98f)
            {
                noDelay = true;
                return true;
            }

            float lag = (Start.X - LegTip.X) * Direction;
            if (!isLeading && lag > LagThresholdBase * 2.5f && StepTimer <= 0f)
            {
                noDelay = true;
                return true;
            }

            if (SisterLeg != null && SisterLeg.StrideTimer > 0f)
                return false;

            if (SisterLeg != null && !SisterLeg.LatchedOn && SisterLeg.State != LegState.Hanging)
                return false;

            if (sameSideLegs != null)
            {
                int groundedCount = sameSideLegs.Count(l => l.LatchedOn);
                if (groundedCount <= 1)
                    return false;
            }

            float maxExt = MaxExtensionBase - SisterInfluence * MaxExtSisterReduction;

            if (!FrontSet && !isLeading)
                maxExt -= (1f - SisterInfluence) * TrailingExtPenalty;

            if (Math.Abs(Body.velocity.X) < 1.4f)
                maxExt = MaxExtensionBase;

            float minExt = MinExtensionBase - SisterInfluence * MinExtSisterReduction;
            float lagThresh = (0.25f + SisterInfluence * 0.75f) * LagThresholdBase;

            if (extension > MaxLength * maxExt)
                return true;

            if (extension < MaxLength * minExt)
            {
                noDelay = true;
                return true;
            }

            if (isLeading)
            {
                if ((DesiredGrabPosition.X - LegTip.X) * Direction > LeadingMinExtension)
                {
                    noDelay = true;
                    return true;
                }
            }
            else if (lag > lagThresh && StepTimer <= 0f)
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
            bool isLeading = Body.velocity.X * Direction > 0f;
            bool moving = Math.Abs(Body.velocity.X) > VelocityOffsetThreshold;

            float stepTime = StepBaseTime - StepFastTimeReduction * Utils.GetLerpValue(StepFastSpeedMin, StepFastSpeedMax, Math.Abs(Body.velocity.X), true);
            Vector2 predictedMovement = Body.velocity * stepTime;

            if (isLeading && moving)
            {
                DesiredGrabPosition = Start + new Vector2(Direction * MaxLength * LeadingExtendReach, LengthA * DesiredDownScale);
            }
            else
            {
                DesiredGrabPosition = Start + new Vector2(Direction * LengthB * DesiredOutwardScale, LengthA * DesiredDownScale);
                DesiredGrabPosition.X += VelocityXOffset;
            }

            DesiredGrabPosition += predictedMovement;

            if (DesiredGrabPosition.Distance(Start) > MaxLength)
                DesiredGrabPosition = Start + Vector2.Normalize(DesiredGrabPosition - Start) * MaxLength;
        }

        private void FindGrabPosition()
        {
            if (GrabDelay > 0f)
                return;

            bool isLeading = Math.Sign(Body.velocity.X) == Direction;

            Point? best = null;
            bool tooClose = false;
            Point? directHit = null;

            if (isLeading && Math.Abs(Body.velocity.X) > VelocityOffsetThreshold)
            {
                float castX = Direction * MaxLength * LeadingExtendReach;
                directHit = best = RaycastToTile(Start + new Vector2(castX, -16f), Start + new Vector2(castX, MaxLength));
                if (best.HasValue && TileToGripPoint(best.Value).Distance(Start) < TooCloseMinDist)
                {
                    tooClose = true;
                    best = null;
                }
            }

            if (best == null && !tooClose)
            {
                Vector2 target = DesiredGrabPosition;
                if (Vector2.Distance(target, Start) > MaxLength)
                    target = Start + Vector2.Normalize(target - Start) * MaxLength;

                directHit = best = RaycastToTile(Start, target);
                if (best.HasValue && TileToGripPoint(best.Value).Distance(Start) < TooCloseMinDist)
                {
                    tooClose = true;
                    best = null;
                }
            }

            if (best == null && !tooClose)
                best = RadialDownScan(4, 1.2f, ref tooClose);

            if (best == null || tooClose)
            {
                float radius = MaxLength * (FrontSet ? 0.8f : 0.6f);
                float startAngle = isLeading ? MathHelper.PiOver4 : MathHelper.PiOver2 * 0.8f;
                best = RadialArcScan(startAngle, MathHelper.Pi * 0.95f, radius);
            }

            if (best == null && directHit.HasValue)
                best = directHit;

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
                bool isLeading = Math.Sign(Body.velocity.X) == Direction;
                if (isLeading)
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

            float heightBias = 0.2f + 0.8f * Utils.GetLerpValue(100f, 10f, Math.Abs(Start.Y - world.Y), true);
            return (angleFit * distFit - penalty) * heightBias;
        }

        private void TryConfirmGrabPos(Point candidate)
        {
            Vector2 point = TileToGripPoint(candidate);

            if (GrabPosition.HasValue && GrabPosition.Value.ToTileCoordinates() == candidate)
                return;

            if (GrabPosition == null || point.Distance(DesiredGrabPosition) < GrabPosition.Value.Distance(DesiredGrabPosition))
            {
                ApplySlopeOffsets(ref point);
                GrabPosition = point;
            }
        }

        public static void ApplySlopeOffsets(ref Vector2 footPos)
        {
            Point tileCoords = footPos.ToTileCoordinates();
            Tile tile = Framing.GetTileSafely(tileCoords);
            if (!tile.HasUnactuatedTile)
                return;

            Vector2 groundSnap = new(tileCoords.X * 16f, tileCoords.Y * 16f + 16f);
            float interp = (footPos.X % 16f) / 16f;

            if (tile.IsHalfBlock)
                footPos.Y = groundSnap.Y - 8f;
            else if (tile.Slope == SlopeType.SlopeDownLeft)
                footPos.Y = groundSnap.Y - MathHelper.Lerp(16f, 0f, interp) + 2f;
            else if (tile.Slope == SlopeType.SlopeDownRight)
                footPos.Y = groundSnap.Y - MathHelper.Lerp(0f, 16f, interp) + 2f;
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

        public void DrawDebug(SpriteBatch spritebatch)
        {
            Color c = (FrontSet, LeftSet) switch
            {
                (true, true) => Color.Red,
                (true, false) => Color.Yellow,
                (false, true) => Color.Green,
                _ => Color.Blue
            };

            Texture2D line = ModContent.Request<Texture2D>("AAModClassic/_Unofficial/Desert/Line").Value;
            float startToMid = Start.AngleTo(Middle);
            Main.spriteBatch.Draw(line, Start - Main.screenPosition, null, c.MultiplyRGB(Color.White * 0.5f), startToMid, new Vector2(0, line.Height / 2f), new Vector2(LengthA / line.Width, 4), 0, 0);

            float midToEnd = Middle.AngleTo(End);
            Main.spriteBatch.Draw(line, Middle - Main.screenPosition, null, c, midToEnd, new Vector2(0, line.Height / 2f), new Vector2(LengthB / line.Width, 4), 0, 0);
        }
    }
}