using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Effects;
using AAModClassic.Music;
using AAModClassic.Particles;
using AAModClassic.Particles.Types;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Components;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unofficial.Content.Unimush
{
    public class MushroomMadness : ModNPC, IBestiaryCritterNPC
    {
        const string path = "AAModClassic/_Unofficial/Content/Unimush/";
        public override string Texture => path + "MushroomMadness_Body";

        internal static Asset<Texture2D> Jaw;
        internal static Asset<Texture2D> Jaw_Back;
        internal static Asset<Texture2D> Tendril;

        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                Jaw = ModContent.Request<Texture2D>(path + "MushroomMadness_Jaw");
                Jaw_Back = ModContent.Request<Texture2D>(path + "MushroomMadness_Jaw_Back");
                Tendril = ModContent.Request<Texture2D>(path + "MushroomMadness_Tendril");
            }
        }

        public override void SetDefaults()
        {
            NPC.width = 144;
            NPC.height = 210;

            NPC.lifeMax = 10;

            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = true;
            NPC.hide = true;

            NPC.rarity = 100;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["MushMad_Intro"];

            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
        }

        internal class MushroomMadnessTendril(NPC mushmad, Vector2 startOffset, Vector2 goalOffset, int pointCount, float segLength, int solveIterations) : FabrikLimb(pointCount, segLength, solveIterations)
        {
            public NPC myMushMad = mushmad;
            public Vector2 StartOffset = startOffset;
            public Vector2 GoalOffset = goalOffset;
            public float AmplitudeMultiplier = 1f;
            public float SineIncrement = 0.05f;

            private float sineCounter = Main.rand.NextFloat(0f, 100f);

            public void Update()
            {
                Vector2 start = myMushMad.Center + StartOffset;
                Vector2 goal = start + GoalOffset;
                Vector2 goalPerp = (goal - start).SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.PiOver2);
                goal += goalPerp * MathF.Sin(sineCounter / 4f) * 90f;

                //Dust.QuickDust(goal, Color.Lime);

                base.Update(start, Points[^1] + ((goal - Points[^1]) / 10f));

                if (AmplitudeMultiplier != 0)
                {
                    for (int j = 1; j < Count; j++)
                    {
                        float ratio = j / (float)(Count - 1);
                        Vector2 toCurrent = Points[j].DirectionFrom(Points[j - 1]);
                        Vector2 perpendicular = toCurrent.RotatedBy(MathHelper.PiOver2);

                        float amp = 2.5f * AmplitudeMultiplier;
                        float freq = 2f;
                        float phase = sineCounter + ratio * freq * MathF.Tau;
                        float offset = MathF.Sin(phase) * amp;
                        Points[j] += perpendicular * offset;
                    }

                    sineCounter += SineIncrement;
                }
            }

            public void Draw(SpriteBatch spriteBatch, Vector2 screenPos, int index)
            {
                Texture2D tendril = Tendril.Value;
                int frameX = 3;
                int frameY = 3;

                for (int j = Count - 1; j >= 0; j--)
                {
                    if (j == Count - 1)
                    {
                        Vector2 toNext = Points[j] - Points[j - 1];
                        Vector2 midPoint = (Points[j] + Points[j - 1]) / 2f;
                        Color light = Lighting.GetColor(midPoint.ToTileCoordinates());
                        Rectangle frame = tendril.Frame(4, 5, 0, 4);
                        frame.Width = 24;
                        spriteBatch.Draw(tendril, Points[j] - screenPos, frame, light, toNext.ToRotation(), new Vector2(0, frame.Height / 2f), 1f, 0, 0);

                        if (--frameX < 0)
                        {
                            frameX = 0;
                            if (--frameY < 0)
                            {
                                frameY = 1;
                                frameX = 3;
                            }
                        }
                    }
                    else
                    {
                        Vector2 toNext = Points[j + 1] - Points[j];
                        Vector2 midPoint = (Points[j + 1] + Points[j]) / 2f;
                        Color light = Lighting.GetColor(midPoint.ToTileCoordinates());
                        Rectangle frame = tendril.Frame(4, 5, frameX, frameY);

                        spriteBatch.Draw(tendril, Points[j] - screenPos, frame, light, toNext.ToRotation(), new Vector2(2, frame.Height / 2f), new Vector2(toNext.Length() / (frame.Width - 2), 1f), 0, 0);

                        if (--frameX < 0)
                        {
                            frameX = 0;
                            if (--frameY < 0)
                            {
                                frameY = 1;
                                frameX = 3;
                            }
                        }
                    }
                }
            }
        }

        private MushroomMadnessTendril[] Tendrils = null;

        private static MushroomMadnessTendril[] InitializeTendrils(NPC npc)
        {
            Vector2[] startOffsets =
            [
                new Vector2(-24, 32),
                new Vector2(24, 32),
                new Vector2(-16, 48),
                new Vector2(16, 48),
                new Vector2(-8, 56),
                new Vector2(8, 56),
            ];

            Vector2[] goalOffsets =
            [
                new Vector2(-240, 100),
                new Vector2(240, 100),
                new Vector2(-160, 160),
                new Vector2(160, 160),
                new Vector2(-80, 220),
                new Vector2(80, 220),
            ];

            int[] sizes =
            [
                Main.rand.Next(10, 18), Main.rand.Next(10, 18),
                Main.rand.Next(10, 18), Main.rand.Next(10, 18),
                Main.rand.Next(10, 18), Main.rand.Next(10, 18)
            ];

            MushroomMadnessTendril[] tendrils = new MushroomMadnessTendril[6];
            for (int i = 0; i < tendrils.Length; i++)
            {
                tendrils[i] = new(npc, startOffsets[i], goalOffsets[i], sizes[i], 9, 10);
                for (int j = 0; j < tendrils[i].Count; j++)
                    tendrils[i].Points[j] = npc.Center;
            }

            return tendrils;
        }

        private void UpdateTendrils(bool idle)
        {
            if(Tendrils == null)
                Tendrils = InitializeTendrils(NPC);

            for(int i = 0; i < Tendrils.Length; i++)
            {
                if (idle)
                {
                    Tendrils[i].AmplitudeMultiplier = 1 + ((i / 2) * 0.5f);
                    Tendrils[i].SineIncrement = 0.025f + 0.025f * (1 + i / 2);
                }
                else
                {
                    Tendrils[i].AmplitudeMultiplier = 4f;
                    Tendrils[i].SineIncrement = 0.3f;
                    if (NPC.ai[1] >= 90)
                        Tendrils[i].AmplitudeMultiplier = 2f + (2 * 1 - MathUtils.SineInEasing(MathHelper.Clamp((NPC.ai[1] - 90) / 60f, 0f, 1f)));
                }

                if (i % 2 == 0)
                    Tendrils[i].AmplitudeMultiplier *= -1;

                Tendrils[i].Update();
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
            ]);
        }

        public override void ModifyTypeName(ref string typeName)
        {
            if(!NPC.IsABestiaryIconDummy)
                typeName = "???";
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!AAWorld.downedAllAncients || MushroomMadnessSystem.Encountered || !spawnInfo.Player.ZoneSurface() || (!spawnInfo.Player.AAPlayer().ZoneMush && !spawnInfo.Player.ZoneGlowshroom) || NPC.AnyNPCs(Type))
                return 0f;

            return 0.5f;
        }

        private enum MushMadState
        {
            Idle,
            Flee
        }

        private MushMadState State { get => (MushMadState)NPC.ai[0]; set => NPC.ai[0] = (float)value; }

        private float jawX = 0f;
        private float jawRot = 0f;
        private float jawY = 64f;
        private float oldY = 64f;
        private Vector2 drawOffset = Vector2.Zero;

        public override void AI()
        {
            UpdateTendrils(State == MushMadState.Idle);

            if (State == MushMadState.Idle)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y = MathF.Sin(NPC.ai[1]) * 0.5f;

                jawX = MathHelper.Lerp(-1.5f, 1.5f, MathF.Sin(NPC.ai[1] * 3f) / 2f + 0.5f);
                jawRot = jawX / MathHelper.TwoPi / 18f;
                jawY = MathHelper.Lerp(50f, 60f, MathF.Cos(NPC.ai[1] * 1.5f) / 2f + 0.5f);

                NPC.ai[1] += 0.04f;

                if (Main.LocalPlayer.Distance(NPC.Center) < Main.screenWidth / 2.5f)
                {
                    State = MushMadState.Flee;
                    NPC.velocity = Vector2.Zero;
                    NPC.ai[1] = 0;
                    NPC.netUpdate = true;

                    Vector2[] nonIdleGoalOffsets =
                    [
                        new Vector2(-160, -220),
                        new Vector2(160, -220),
                        new Vector2(-300, 0),
                        new Vector2(300, 0),
                        new Vector2(-160, 220),
                        new Vector2(160, 220),
                    ];

                    for (int i = 0; i < Tendrils.Length; i++)
                        Tendrils[i].GoalOffset = nonIdleGoalOffsets[i];
                }
            }
            else
            {
                if (NPC.ai[1] == 0)
                {
                    oldY = jawY;
                    SoundEngine.PlaySound(SoundID.Roar);
                    MushroomMadnessSystem.Encountered = true;
                    Main.BestiaryTracker.Sights.RegisterWasNearby(NPC);
                    NPC.velocity.Y = -18f;
                    NPC.hide = false;
                }

                if (NPC.ai[1] <= 30f)
                {
                    NPC.velocity.Y *= 0.9f;

                    float ease = MathUtils.SineOutEasing(NPC.ai[1] / 30f);
                    jawY = MathHelper.Lerp(oldY, 68f, ease);

                    Vector2 jawOff = Main.rand.NextVector2Circular(6f, 6f) * ease;
                    jawX = jawOff.X;
                    jawY += jawOff.Y;

                    drawOffset = Main.rand.NextVector2Circular(4f, 4f) * ease;
                }
                else if(NPC.ai[1] < 90)
                {
                    NPC.velocity.Y *= 0.9f;

                    Vector2 jawOff = Main.rand.NextVector2Circular(6f, 6f);
                    jawX = jawOff.X;
                    jawY = 68f + jawOff.Y;

                    drawOffset = Main.rand.NextVector2Circular(4f, 4f);
                }
                else
                {
                    NPC.hide = true;

                    float ease = MathUtils.SineInEasing(MathHelper.Clamp((NPC.ai[1] - 90) / 60f, 0f, 1f));
                    jawY = MathHelper.Lerp(68f, 54f, ease);

                    Vector2 jawOff = Main.rand.NextVector2Circular(4f, 4f) * (1 - ease);
                    jawX = jawOff.X;
                    jawY += jawOff.Y;

                    drawOffset = Main.rand.NextVector2Circular(4f, 4f) * (1 - ease);

                    NPC.velocity += Vector2.UnitY * 0.2f;

                    if (NPC.ai[1] > 200)
                    {
                        NPC.active = false;
                        NPC.netUpdate = true;
                        return;
                    }
                }

                if (NPC.ai[1] < 120)
                {
                    if (NPC.ai[1] <= 30f)
                        RadialBlurSystem.SetRadialBlur(MathHelper.Lerp(0, 0.5f, NPC.ai[1] / 30f), NPC.Center);
                    else
                        RadialBlurSystem.SetRadialBlur(0.5f, NPC.Center);

                    /*
                    for (int i = 0; i < 3; i++)
                    {
                        Point start = Target.Center.ToTileCoordinates() + new Point(Main.rand.Next(-64, 65), 48);
                        for (int j = 0; j < 96; j++)
                        {
                            Point current = start - new Point(0, j);
                            if (!Main.tile[current].IsTileSolid() && Main.tile[current - new Point(0, 1)].TileType == TileID.Crimstone)
                                Dust.NewDust(current.ToWorldCoordinates(0, -16), 16, 16, DustID.Crimstone, 0, 3);
                        }
                    }

                    CameraSystem.StartScreenShake(NPC.Center, Vector2.Zero, 6 * BrainOfCthulhuSystem.ScreenBlurStrength * distanceScaleFactor, 10, 60);
                    */

                    if (NPC.ai[1] % 15 == 0)
                    {
                        BossRoar pulse = new(NPC.Center, Color.Black, Main.rand.NextFloatDirection(), 0.1f, 3f, 30);
                        ParticleSystem.SpawnParticle(pulse);
                    }
                }
                NPC.ai[1]++;
            }
        }

        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCsMoonMoon.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Tendrils != null)
                for (int i = 0; i < Tendrils.Length; i++)
                    Tendrils[i].Draw(spriteBatch, screenPos, i);

            Vector2 mouthPos = NPC.Center + new Vector2(4, 32) * NPC.scale;
            Vector2 jawPos = NPC.Center + drawOffset + new Vector2(jawX, jawY) * NPC.scale;
            Vector2 jawBackPos = (mouthPos + jawPos) / 2f;
            spriteBatch.Draw(Jaw_Back.Value, jawBackPos - screenPos, null, drawColor, NPC.rotation, Jaw_Back.Size() * 0.5f, NPC.scale, 0, 0);

            spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Center + drawOffset - screenPos, null, drawColor, NPC.rotation, TextureAssets.Npc[Type].Size() * 0.5f, NPC.scale, 0, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {          
            spriteBatch.Draw(Jaw.Value, NPC.Center + drawOffset + new Vector2(jawX, jawY) * NPC.scale - screenPos, null, drawColor, jawRot, Jaw.Size() * 0.5f, NPC.scale, 0, 0);
        }
    }

    public class MushroomMadnessSystem : ModSystem
    {
        internal static bool Encountered = false;

        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("MushMadSeen", Encountered);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (!tag.TryGet("MushMadSeen", out Encountered))
                Encountered = false;
        }
    }
}
