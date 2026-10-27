using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Music;
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

        private static Asset<Texture2D> Jaw;
        private static Asset<Texture2D> Jaw_Back;

        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                Jaw = ModContent.Request<Texture2D>(path + "MushroomMadness_Jaw");
                Jaw_Back = ModContent.Request<Texture2D>(path + "MushroomMadness_Jaw_Back");
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

        private FabrikLimb[] Tendrils = null;

        private static FabrikLimb[] InitializeTendrils(NPC npc)
        {
            FabrikLimb[] tendrils = new FabrikLimb[6];
            for (int i = 0; i < tendrils.Length; i++)
            {
                tendrils[i] = new(Main.rand.Next(16, 26), 12, 10);
                for (int j = 0; j < tendrils[i].Count; j++)
                    tendrils[i].Points[j] = npc.Center;
            }

            return tendrils;
        }

        private static void UpdateTendrils(NPC mushmad, ref FabrikLimb[] tendrils, bool idle)
        {
            if(tendrils == null)
                tendrils = InitializeTendrils(mushmad);

            Vector2[] startOffsets =
            [
                new Vector2(-24, 32),
                new Vector2(24, 32),
                new Vector2(-16, 48),
                new Vector2(16, 48),
                new Vector2(-8, 56),
                new Vector2(8, 56),
            ];

            Vector2[] goalOffsets;
            if (idle)
            {
                goalOffsets =
                [
                    new Vector2(-240, 100),
                    new Vector2(240, 100),
                    new Vector2(-160, 160),
                    new Vector2(160, 160),
                    new Vector2(-80, 220),
                    new Vector2(80, 220),
                ];
            }
            else
            {
                goalOffsets =
                [
                    new Vector2(-160, -220),
                    new Vector2(160, -220),
                    new Vector2(-300, 0),
                    new Vector2(300, 0),
                    new Vector2(-160, 220),
                    new Vector2(160, 220),
                ];
            }

            for (int i = 0; i < tendrils.Length; i++)
            {
                Vector2 start = mushmad.Center + startOffsets[i];
                Vector2 goal = start + goalOffsets[i];
                tendrils[i].Update(start, tendrils[i].Points[^1] + ((goal - tendrils[i].Points[^1]) / 10f));

                float ampMult;
                float sineCounter;
                if (idle)
                {
                    ampMult = i < 2 ? 1f : i < 4 ? 1.5f : 2f;
                    sineCounter = i < 2 ? Main.GlobalTimeWrappedHourly / 1.5f : i < 4 ? Main.GlobalTimeWrappedHourly : Main.GlobalTimeWrappedHourly * 1.5f;
                }
                else
                {
                    ampMult = 4f;
                    sineCounter = Main.GlobalTimeWrappedHourly * 16f;
                    if (mushmad.ai[1] >= 90)
                        ampMult *= 1 - MathUtils.SineInEasing(MathHelper.Clamp((mushmad.ai[1] - 90) / 60f, 0f, 1f));
                }

                if (i % 2 == 0)
                    ampMult *= -1;

                if (ampMult != 0)
                {
                    for (int j = 1; j < tendrils[i].Count; j++)
                    {
                        float ratio = j / (float)(tendrils[i].Count - 1);
                        Vector2 toCurrent = tendrils[i].Points[j].DirectionFrom(tendrils[i].Points[j - 1]);
                        Vector2 perpendicular = toCurrent.RotatedBy(MathHelper.PiOver2);

                        float amp = 2.5f * ampMult;
                        float freq = 2f;
                        float phase = sineCounter + ratio * freq * MathF.Tau;
                        float offset = MathF.Sin(phase) * amp;
                        tendrils[i].Points[j] += perpendicular * offset;
                    }

                    //sineCounter += 0.05f;
                }
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
            UpdateTendrils(NPC, ref Tendrils, State == MushMadState.Idle);

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
            {
                var line = ModContent.Request<Texture2D>("AAModClassic/UI/Core/Line");
                for (int i = 0; i < Tendrils.Length; i++)
                {
                    FabrikLimb tendril = Tendrils[i];
                    bool blue = i == 0 || i == 3 || i == 4;
                    for (int j = 0; j < tendril.Count - 1; j++)
                    {
                        Vector2 toNext = tendril.Points[j + 1] - tendril.Points[j];
                        spriteBatch.Draw(line.Value, tendril.Points[j] - screenPos, null, blue ? Color.Blue : Color.Red, toNext.ToRotation(), new Vector2(2f, line.Height() / 2f), new Vector2(toNext.Length() / line.Width(), 8f), 0, 0);
                    }
                }
            }

            Vector2 jawBackOffset = new(4, -18);
            spriteBatch.Draw(Jaw_Back.Value, NPC.Center + drawOffset + ((jawBackOffset + new Vector2(jawX, jawY)) * NPC.scale) - screenPos, null, drawColor, NPC.rotation, Jaw_Back.Size() * 0.5f, NPC.scale, 0, 0);

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
