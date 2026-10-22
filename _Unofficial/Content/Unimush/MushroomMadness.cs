using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unofficial.Content.Unimush
{
    public class MushroomMadness : ModNPC
    {
        public override string Texture => "AAModClassic/_Unofficial/Content/Unimush/MushroomMadness_Body";

        private static Asset<Texture2D> Jaw;

        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
                Jaw = ModContent.Request<Texture2D>("AAModClassic/_Unofficial/Content/Unimush/MushroomMadness_Jaw");
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
        }

        public override void ModifyTypeName(ref string typeName)
        {
            typeName = "???";
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!AAWorld.downedAllAncients || MushroomMadnessSystem.Encountered || (!spawnInfo.Player.AAPlayer().ZoneMush && !spawnInfo.Player.ZoneGlowshroom) || NPC.AnyNPCs(Type))
                return 0f;

            return 0.001f;
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

        public override void AI()
        {
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
                    NPC.velocity.Y = -16f;
                    NPC.hide = false;
                }

                if (NPC.ai[1] <= 30f)
                {
                    NPC.velocity.Y *= 0.9f;

                    float ease = MathUtils.SineOutEasing(NPC.ai[1] / 30f);
                    jawY = MathHelper.Lerp(oldY, 68f, ease);

                    Vector2 jawOff = Main.rand.NextVector2Circular(4f, 4f) * ease;
                    jawX = jawOff.X;
                    jawY += jawOff.Y;
                }
                else if(NPC.ai[1] < 90)
                {
                    NPC.velocity.Y *= 0.9f;

                    Vector2 jawOff = Main.rand.NextVector2Circular(6f, 6f);
                    jawX = jawOff.X;
                    jawY = 68f + jawOff.Y;
                }
                else
                {
                    NPC.hide = true;

                    float ease = MathUtils.SineInEasing(MathHelper.Clamp((NPC.ai[1] - 90) / 60f, 0f, 1f));
                    jawY = MathHelper.Lerp(68f, 54f, ease);

                    Vector2 jawOff = Main.rand.NextVector2Circular(4f, 4f) * ease;
                    jawX = jawOff.X;
                    jawY += jawOff.Y;

                    NPC.velocity += Vector2.UnitY * 0.2f;
                }

                NPC.ai[1]++;
            }
        }

        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCsMoonMoon.Add(index);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            
            spriteBatch.Draw(Jaw.Value, NPC.Center + new Vector2(jawX, jawY) * NPC.scale - screenPos, null, drawColor, jawRot, Jaw.Size() * 0.5f, NPC.scale, 0, 0);
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
