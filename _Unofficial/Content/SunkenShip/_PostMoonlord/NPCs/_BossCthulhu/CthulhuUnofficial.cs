using AAModClassic.Music;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.SunkenShip._PostMoonlord.NPCs._BossCthulhu
{
    [AutoloadBossHead]
    public class CthulhuUnofficial : ModNPC
    {
        public override string Texture => MyPath + "Head";
        public override string BossHeadTexture => "AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/SoulOfCthulhu_Head_Boss";
        public const string MyPath = "AAModClassic/_Unofficial/Content/SunkenShip/_PostMoonlord/NPCs/_BossCthulhu/Cthulhu_";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhu, Cosmic Calamity");
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 222;
            NPC.height = 228;
            NPC.damage = 0;
            Music = MusicManagementSystem.MusicSlots["Cthulhu"];
            NPC.lifeMax = 1500000;
            NPC.dontTakeDamage = false;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.scale = 1f;
            NPC.HitSound = SoundID.NPCHit54;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
                NPC.buffImmune[k] = true;
            NPC.knockBackResist = 0f;
        }

        public Vector3 LeftHand = Vector3.Zero;
        public Vector3 RightHand = Vector3.Zero;

        #region emo punch cycle

        private const int WindupTime = 40;
        private const int ThrustTime = 14;
        private const int HoldTime = 16;
        private const int RetractTime = 36;
        private const int CycleLength = WindupTime + ThrustTime + HoldTime + RetractTime;

        public int ArmAttackTimer = 0;
        private static readonly Vector2 LeftArmDirection = Vector2.Normalize(new Vector2(-180, 20));
        private static readonly Vector2 RightArmDirection = Vector2.Normalize(new Vector2(180, 20));

        private static float RestReach => 181f;
        private static float WindupReach => 140f;
        private static float PunchReach => 240f;

        private static float EaseInQuad(float t) => t * t;
        private static float EaseOutQuad(float t) => 1f - (1f - t) * (1f - t);

        private void UpdateArmAttackCycle()
        {
            ArmAttackTimer++;
            if (ArmAttackTimer >= CycleLength)
                ArmAttackTimer = 0;

            int t = ArmAttackTimer;
            float reach;
            float z;

            if (t < WindupTime)
            {
                float p = EaseOutQuad(t / (float)WindupTime);
                reach = MathHelper.Lerp(RestReach, WindupReach, p);
                z = 0f;
            }
            else if (t < WindupTime + ThrustTime)
            {
                float p = EaseInQuad((t - WindupTime) / (float)ThrustTime);
                reach = MathHelper.Lerp(WindupReach, PunchReach, p);
                z = p;
            }
            else if (t < WindupTime + ThrustTime + HoldTime)
            {
                reach = PunchReach;
                z = 1f;
            }
            else
            {
                float p = EaseOutQuad((t - WindupTime - ThrustTime - HoldTime) / (float)RetractTime);
                reach = MathHelper.Lerp(PunchReach, RestReach, p);
                z = 1f - p;
            }

            Vector2 leftShoulder = NPC.Center + new Vector2(-180, -60);
            Vector2 rightShoulder = NPC.Center + new Vector2(180, -60);

            LeftHand = new Vector3(leftShoulder + LeftArmDirection * reach, z);
            RightHand = new Vector3(rightShoulder + RightArmDirection * reach, z);
        }

        #endregion


        public override void AI()
        {
            NPC.velocity.X = NPC.DirectionTo(Main.LocalPlayer.Center).X * 2f;
            NPC.velocity.Y = ((Main.LocalPlayer.Center.Y - 64) - NPC.Center.Y) / 120f;

            //UpdateArmAttackCycle();
            if (tentacles.Count == 0)
            {
                CthulhuTentacle.OccupiedTiles.Clear();
                for (int i = 0; i < 12; i++)
                    tentacles.Add(new(new(32, 8, 12), MathHelper.TwoPi / 12f * i));
            }

            foreach (var tentacle in tentacles)
                tentacle.Update();
        }

        private static float ZDepthScale => 200f;
        private static float UpperArmLength => 850f;
        private static float ForearmLength => 800f;

        private static (Vector2 elbow, float elbowZ) SolveArmIK(Vector2 start, Vector3 end, float A, float B, bool flip)
        {
            Vector3 start3D = new(start, 0f);
            Vector3 end3D = new(end.X, end.Y, end.Z * ZDepthScale);

            float C = Vector3.Distance(start3D, end3D);
            C = MathHelper.Clamp(C, Math.Abs(A - B) + 0.01f, A + B - 0.01f);

            float angle = (float)Math.Acos(MathHelper.Clamp((C * C + A * A - B * B) / (2f * C * A), -1f, 1f));
            if (flip)
                angle *= -1;

            Vector2 flatEnd = new(end.X, end.Y);
            Vector2 elbow2D = start + (angle + start.AngleTo(flatEnd)).ToRotationVector2() * A;

            float elbowZ = MathHelper.Lerp(start3D.Z, end3D.Z, A / C) / ZDepthScale;

            return (elbow2D, MathHelper.Clamp(elbowZ, 0f, 1f));
        }

        private enum ArmZStage { Flat, Bent, Straight }

        private static (Texture2D tex, float squash, ArmZStage stage) GetZStageTexture(float outstretch, Texture2D flatTex, Texture2D bentTex, Texture2D straightTex)
        {
            outstretch = MathHelper.Clamp(outstretch, 0f, 1f);

            Main.NewText(outstretch);

            float flatThreshold = 0.3f;
            float bentThreshold = 1f;

            if (outstretch < flatThreshold)
            {
                float t = EaseInQuad(outstretch / flatThreshold);
                float squash = MathHelper.Lerp(1f, (float)bentTex.Height / flatTex.Height, t);
                return (flatTex, squash, ArmZStage.Flat);
            }
            else if (outstretch < 1f)
            {
                float t = EaseInQuad((outstretch - flatThreshold) / (bentThreshold - flatThreshold));
                float squash = MathHelper.Lerp(1f, (float)straightTex.Height / bentTex.Height, t);
                return (bentTex, squash, ArmZStage.Bent);
            }

            return (straightTex, 1f, ArmZStage.Straight);
        }

        private static Texture2D GetCthulhuTexture(string suffix) => ModContent.Request<Texture2D>(MyPath + suffix).Value;

        public class CthulhuTentacle(FabrikLimb tentacle, float restAngle)
        {
            public static HashSet<Point> OccupiedTiles = [];

            public FabrikLimb Tentacle = tentacle;
            public Point GrippedTile = Point.Zero;
            public readonly Vector2 RestDir = restAngle.ToRotationVector2();
            private int stateCounter = Main.rand.Next(0, 255);
            private float sineCounter = 0f;
            private Vector2 previousTipPosition = Main.LocalPlayer.Center;
            private bool isGripped = false;

            private VertexPositionColorTexture[] _stripVertsCache;
            private readonly VertexPositionColorTexture[] _footVertsCache = new VertexPositionColorTexture[4];

            public void Update()
            {
                Vector2 goal = -Vector2.One;
                if (GrippedTile == Point.Zero)
                {
                    bool found = false;
                    int iters = 24;
                    for (int j = 0; j < 16; j++)
                    {
                        Vector2? result = CollisionUtils.RayCast(Main.LocalPlayer.Center, MathHelper.Pi / iters * j, Tentacle.Length, out _);
                        if (result.HasValue && !OccupiedTiles.Contains(result.Value.ToTileCoordinates()))
                        {
                            found = true;
                            GrippedTile = result.Value.ToTileCoordinates();
                            OccupiedTiles.Add(GrippedTile);
                            goal = result.Value;
                            stateCounter = 0;
                            break;
                        }

                        result = CollisionUtils.RayCast(Main.LocalPlayer.Center, -MathHelper.Pi / 16f * j, Tentacle.Length, out _);
                        if (result.HasValue && !OccupiedTiles.Contains(result.Value.ToTileCoordinates()))
                        {
                            found = true;
                            GrippedTile = result.Value.ToTileCoordinates();
                            OccupiedTiles.Add(GrippedTile);
                            goal = result.Value;
                            stateCounter = 0;
                            break;
                        }
                    }

                    if (!found)
                        goal = Main.LocalPlayer.Center + RestDir * 124;
                }
                else
                {
                    goal = GrippedTile.ToWorldCoordinates();
                    if (float.IsNaN(goal.X) || float.IsNaN(goal.Y) || Main.LocalPlayer.Center.Distance(goal) > Tentacle.Length * 0.75f)
                    {
                        OccupiedTiles.Remove(GrippedTile);
                        GrippedTile = Point.Zero;
                        goal = Main.LocalPlayer.Center + RestDir * 124;
                        stateCounter = 0;
                    }
                }

                float ampMult = 1f;

                if (GrippedTile == Point.Zero)
                {
                    Tentacle.Update(Main.LocalPlayer.Center, Tentacle.Points[^1] + ((goal - Tentacle.Points[^1]) / 10f));
                    isGripped = false;
                }
                else
                {
                    if (stateCounter >= 10)
                    {
                        Tentacle.Update(Main.LocalPlayer.Center, goal); //tentacle.Points[^1] + ((goal - tentacle.Points[^1]) / 10f))
                        ampMult = 0.1f;
                        isGripped = true;
                    }
                    else
                    {
                        if (stateCounter == 0)
                            previousTipPosition = Tentacle.Points[^1];

                        float lerp = MathUtils.CircOutEasing(stateCounter / 10f);
                        ampMult = MathHelper.Lerp(1f, 0.1f, lerp);
                        Tentacle.Update(Main.LocalPlayer.Center, Vector2.Lerp(previousTipPosition, goal, lerp));
                        stateCounter++;
                    }          
                }

                if (ampMult != 0)
                {
                    for (int j = 1; j < Tentacle.Count; j++)
                    {
                        float ratio = j / (float)(Tentacle.Count - 1);
                        Vector2 toCurrent = Tentacle.Points[j].DirectionFrom(Tentacle.Points[j - 1]);
                        Vector2 perpendicular = toCurrent.RotatedBy(MathHelper.PiOver2);

                        float amp = 2.5f * ampMult;
                        float freq = 2f;
                        float phase = sineCounter + ratio * freq * MathF.Tau;
                        float offset = MathF.Sin(phase) * amp;
                        Tentacle.Points[j] += perpendicular * offset;
                    }

                    sineCounter += 0.05f;
                }

                //foreach (var v in Tentacle.Points)
                //    Dust.NewDustPerfect(v, DustID.LifeDrain, Vector2.Zero);
            }

            public void Draw(GraphicsDevice gd, BasicEffect effect, Vector2 screenPos)
            {
                Texture2D texture = GetCthulhuTexture("Tentacle_" + (isGripped ? "Grounded" : "Idle"));
                effect.Texture = texture;

                var verts = BuildStrip(screenPos, Color.White, texture.Width * 0.5f);

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    gd.DrawUserPrimitives(PrimitiveType.TriangleStrip, verts, 0, verts.Length - 2);
                }

                if (isGripped)
                {
                    Texture2D footTexture = GetCthulhuTexture("Tentacle_Grounded_End");
                    effect.Texture = footTexture;

                    var footVerts = BuildFootQuad(screenPos, Color.White, footTexture);

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        gd.DrawUserPrimitives(PrimitiveType.TriangleStrip, footVerts, 0, footVerts.Length - 2);
                    }
                }
            }

            private VertexPositionColorTexture[] BuildStrip(Vector2 screenPos, Color color, float halfWidth)
            {
                Vector2[] points = Tentacle.Points;
                int numPoints = points.Length;
                int requiredLength = numPoints * 2;

                if (_stripVertsCache == null || _stripVertsCache.Length != requiredLength)
                    _stripVertsCache = new VertexPositionColorTexture[requiredLength];

                float totalLength = 0f;
                for (int i = 0; i < numPoints - 1; i++)
                    totalLength += points[i].Distance(points[i + 1]);

                float traveled = 0f;
                Vector2 prevPerp = Vector2.Zero;

                for (int i = 0; i < numPoints; i++)
                {
                    Vector2 tangent;
                    if (i == 0)
                        tangent = (points[1] - points[0]).SafeNormalize(Vector2.UnitX);
                    else if (i == numPoints - 1)
                        tangent = (points[i] - points[i - 1]).SafeNormalize(Vector2.UnitX);
                    else
                        tangent = (points[i + 1] - points[i - 1]).SafeNormalize(Vector2.UnitX);

                    Vector2 perp = new Vector2(-tangent.Y, tangent.X);

                    if (i > 0 && Vector2.Dot(perp, prevPerp) < 0)
                        perp = -perp;
                    prevPerp = perp;

                    perp *= halfWidth;

                    if (i > 0)
                        traveled += points[i - 1].Distance(points[i]);

                    // base (i = 0) -> bottom of texture (v = 1); tip -> top (v = 0)
                    float v = totalLength > 0f ? 1f - (traveled / totalLength) : 1f;

                    Vector2 world = points[i] - screenPos;
                    _stripVertsCache[i * 2 + 0] = new VertexPositionColorTexture(new Vector3(world - perp, 0f), color, new Vector2(0f, v));
                    _stripVertsCache[i * 2 + 1] = new VertexPositionColorTexture(new Vector3(world + perp, 0f), color, new Vector2(1f, v));
                }

                return _stripVertsCache;
            }

            private VertexPositionColorTexture[] BuildFootQuad(Vector2 screenPos, Color color, Texture2D footTexture)
            {
                Vector2 tipWorld = Tentacle.Points[^1] - screenPos;
                Vector2 tileWorld = GrippedTile.ToWorldCoordinates() - screenPos;

                Vector2 dir = Vector2.UnitY;
                Vector2 perp = new Vector2(-dir.Y, dir.X) * (footTexture.Width * 0.5f);

                Vector2 ankleWorld = tipWorld - dir * 8;

                // never compress below natural length — only stretch further if needed
                float length = MathF.Max(footTexture.Height, Vector2.Distance(ankleWorld, tileWorld));
                Vector2 soleWorld = ankleWorld + dir * length;

                _footVertsCache[0] = new VertexPositionColorTexture(new Vector3(ankleWorld - perp, 0f), color, new Vector2(0f, 1f));
                _footVertsCache[1] = new VertexPositionColorTexture(new Vector3(ankleWorld + perp, 0f), color, new Vector2(1f, 1f));
                _footVertsCache[2] = new VertexPositionColorTexture(new Vector3(soleWorld - perp, 0f), color, new Vector2(0f, 0f));
                _footVertsCache[3] = new VertexPositionColorTexture(new Vector3(soleWorld + perp, 0f), color, new Vector2(1f, 0f));

                return _footVertsCache;
            }
        }

        List<CthulhuTentacle> tentacles = [];

        private static BasicEffect _bodyEffect;

        private static BasicEffect GetBodyEffect(GraphicsDevice gd)
        {
            if (_bodyEffect == null || _bodyEffect.IsDisposed)
            {
                _bodyEffect = new BasicEffect(gd)
                {
                    TextureEnabled = true,
                    VertexColorEnabled = true
                };
            }
            return _bodyEffect;
        }

        public override void Unload()
        {
            try
            {
                if (_bodyEffect != null && !_bodyEffect.IsDisposed)
                    _bodyEffect.Dispose();
            }
            catch { }
            finally
            {
                _bodyEffect = null;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.End();

            var gd = Main.instance.GraphicsDevice;
            var effect = GetBodyEffect(gd);
            effect.World = Main.GameViewMatrix.TransformationMatrix;
            effect.View = Matrix.Identity;
            effect.Projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);

            var prevBlend = gd.BlendState;
            var prevRaster = gd.RasterizerState;
            gd.BlendState = BlendState.AlphaBlend;
            gd.RasterizerState = RasterizerState.CullNone;

            foreach (var cTent in tentacles)
                cTent.Draw(gd, effect, Main.screenPosition);

            gd.RasterizerState = prevRaster;
            gd.BlendState = prevBlend;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            return false;

            Texture2D handTex = GetCthulhuTexture("Arm_Hand");

            Texture2D forearmFlat = GetCthulhuTexture("Forearm_Flat");
            Texture2D forearmBent = GetCthulhuTexture("Forearm_Bent");
            Texture2D forearmStraight = GetCthulhuTexture("Forearm_Straight");

            Texture2D armFlat = GetCthulhuTexture("Arm_Flat");
            Texture2D armBent = GetCthulhuTexture("Arm_Bent");
            Texture2D armStraight = GetCthulhuTexture("Arm_Straight");

            Texture2D torsoTex = GetCthulhuTexture("Torso");

            void DrawLimbSegment(Vector2 start, Vector2 end, Texture2D tex, float squash, ArmZStage stage, float outstretch, SpriteEffects flipEffect)
            {
                float widen = MathHelper.Lerp(1f, 1.15f, MathHelper.Clamp(outstretch, 0f, 1f));

                if (stage == ArmZStage.Straight)
                {
                    spriteBatch.Draw(tex, end - Main.screenPosition, null, drawColor, 0f, tex.Size() * 0.5f, widen, flipEffect, 0);
                }
                else
                {
                    float dir = start.AngleTo(end);
                    float dist = start.Distance(end);
                    spriteBatch.Draw(tex, start - Main.screenPosition, null, drawColor, dir - MathHelper.PiOver2, new Vector2(tex.Width / 2f, 4f), new Vector2(widen, (dist / tex.Height) * squash), flipEffect, 0);
                }
            }

            void DrawArm(Vector2 shoulder, Vector3 hand, bool flip)
            {
                var (elbow, elbowZNorm) = SolveArmIK(shoulder, hand, UpperArmLength, ForearmLength, !flip);
                Vector2 handXY = new(hand.X, hand.Y);

                SpriteEffects flipEffect = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                var (tex, squash, stage) = GetZStageTexture(elbowZNorm, armFlat, armBent, armStraight);
                if(stage != ArmZStage.Straight)
                    DrawLimbSegment(shoulder, elbow, tex, squash, stage, elbowZNorm, flipEffect);
                (tex, squash, stage) = GetZStageTexture(elbowZNorm, forearmFlat, forearmBent, forearmStraight);
                DrawLimbSegment(elbow, handXY, tex, squash, stage, elbowZNorm, flipEffect);

                Rectangle handFrame = handTex.Frame(1, 4);
                spriteBatch.Draw(handTex, handXY - Main.screenPosition, handFrame, drawColor, 0f, handFrame.Size() * 0.5f, MathHelper.Lerp(1f, 2f, MathHelper.Clamp(elbowZNorm, 0f, 1f)), flipEffect, 0);
            }

            spriteBatch.Draw(torsoTex, NPC.Center - Main.screenPosition, null, drawColor, 0, torsoTex.Size() * 0.5f, NPC.scale, 0, 0);
            spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Center - (Vector2.UnitY * 800) - Main.screenPosition, null, drawColor, NPC.rotation, TextureAssets.Npc[Type].Size() * 0.5f, NPC.scale, 0, 0);

            Vector2 leftShoulder = NPC.Center + new Vector2(-180, -60);
            Vector2 rightShoulder = NPC.Center + new Vector2(180, -60);

            if (LeftHand != Vector3.Zero)
                DrawArm(leftShoulder, LeftHand, false);
            if (RightHand != Vector3.Zero)
                DrawArm(rightShoulder, RightHand, true);

            return false;
        }
    }
}