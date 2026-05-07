using AAModClassic._Content.Desert._PostMoonlord._BossAnubisA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic.Backgrounds
{
    public class AnubisSkyScene : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player) => NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>());

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:AnubisSky", isActive);
        }
    }

    public class AnubisSky : CustomSky
    {
        public bool Active;
        public float Intensity;
        private struct LightPillar
        {
            public Vector2 Position;

            public float Depth;
        }

        private LightPillar[] _pillars;

        private readonly UnifiedRandom _random = new UnifiedRandom();

        public static Asset<Texture2D> BeamTex;
        public static Asset<Texture2D>[] RuneTex = new Asset<Texture2D>[8];

        public override void OnLoad()
        {
            BeamTex = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AnubisBeam");
            for (int i = 0; i < RuneTex.Length; i++)
            {
                RuneTex[i] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/Runes/Rune" + i);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        readonly AAMod mod = AAMod.instance;

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Texture2D BeamTexture = BeamTex.Value;
            Texture2D[] RuneTextures = new Texture2D[RuneTex.Length];
            for (int i = 0; i < RuneTex.Length; i++)
            {
                RuneTextures[i] = RuneTex[i].Value;
            }
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
            }
            int num = -1;
            int num2 = 0;
            for (int i = 0; i < _pillars.Length; i++)
            {
                float depth = _pillars[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }
            Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            for (int j = num; j < num2; j++)
            {
                Vector2 value4 = new Vector2(1f / _pillars[j].Depth, 0.9f / _pillars[j].Depth);
                Vector2 vector = _pillars[j].Position;
                vector = (vector - value3) * value4 + value3 - Main.screenPosition;
                if (rectangle.Contains((int)vector.X, (int)vector.Y))
                {
                    float num3 = value4.X * 450f;
                    spriteBatch.Draw(BeamTexture, vector, null, Color.White * 0.2f * scale * Intensity, 0f, Vector2.Zero, new Vector2(num3 / 70f, num3 / 45f), SpriteEffects.None, 0f);
                    int num4 = 0;
                    for (float num5 = 0f; num5 <= 1f; num5 += 0.03f)
                    {
                        float num6 = 1f - (num5 + Main.GlobalTimeWrappedHourly * 0.02f + (float)Math.Sin((float)j)) % 1f;
                        spriteBatch.Draw(RuneTextures[num4], vector + new Vector2((float)Math.Sin(num5 * 1582f) * (num3 * 0.5f) + num3 * 0.5f, num6 * 2000f), null, Color.White * num6 * scale * Intensity, num6 * 20f, new Vector2(RuneTextures[num4].Width >> 1, RuneTextures[num4].Height >> 1), 0.9f, SpriteEffects.None, 0f);
                        num4 = (num4 + 1) % RuneTextures.Length;
                    }
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            _pillars = new LightPillar[40];
            for (int i = 0; i < _pillars.Length; i++)
            {
                _pillars[i].Position.X = i / (float)_pillars.Length * (Main.maxTilesX * 16f + 20000f) + _random.NextFloat() * 40f - 20f - 20000f;
                _pillars[i].Position.Y = _random.NextFloat() * 200f - 2000f;
                _pillars[i].Depth = _random.NextFloat() * 8f + 7f;
            }
            Array.Sort(_pillars, new Comparison<LightPillar>(SortMethod));
        }

        private int SortMethod(LightPillar pillar1, LightPillar pillar2)
        {
            return pillar2.Depth.CompareTo(pillar1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
        }

        public override void Reset()
        {
            Active = false;
        }

        public override bool IsActive()
        {
            return Active || Intensity > 0.001f;
        }
    }

    public class AnubisSkyData : ScreenShaderData
    {
        private int anuIndex;

        public AnubisSkyData(string passName) : base(passName)
        {
        }

        private void UpdateAnuIndex()
        {
            int anubis = AAMod.instance.Find<ModNPC>("ForsakenAnubis").Type;
            if (anuIndex >= 0 && Main.npc[anuIndex].active && Main.npc[anuIndex].type == anubis)
            {
                return;
            }
            anuIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == anubis)
                {
                    anuIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateAnuIndex();
            if (anuIndex != -1)
            {
                UseTargetPosition(Main.npc[anuIndex].Center);
            }
            base.Apply();
        }
    }
}