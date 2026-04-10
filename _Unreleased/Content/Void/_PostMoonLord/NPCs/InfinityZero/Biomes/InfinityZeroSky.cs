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

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero.Biomes
{
    public class InfinityZeroSkyScene : ModSceneEffect
    {
        public override string MapBackground => "AAModClassic/Map/VoidMap";

        public override bool IsSceneEffectActive(Player player)
        {
            return !Main.gameMenu && (NPC.AnyNPCs(ModContent.NPCType<InfinityZero>()) || NPC.AnyNPCs(ModContent.NPCType<InfinityZeroSpawn1>()));
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:InfinityZeroSky", isActive && player.Center.Y <= Main.worldSurface * 16);
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<IZSurfaceBgStyle>();
    }

    public class InfinityZeroSky : CustomSky
    {

        private UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;
			public float Rotation;

            public int Life;

            public bool IsAlive;
        }

        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;

        public static Asset<Texture2D> BoltTex;
        public static Asset<Texture2D> FlashTex;

        public override void OnLoad()
        {
            BoltTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/Biomes/InfinityZeroSky_Bolt");
            FlashTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/Biomes/InfinityZeroSky_Flash");
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
            if (ticksUntilNextBolt <= 0)
            {
                ticksUntilNextBolt = random.Next(5, 20);
                int num = 0;
                while (bolts[num].IsAlive && num != bolts.Length - 1)
                {
                    num++;
                }
                bolts[num].IsAlive = true;
                bolts[num].Position.X = random.NextFloat() * 2000f;
                bolts[num].Position.Y = random.NextFloat() * 1000f;
				bolts[num].Rotation = random.NextFloat() * ((float)Math.PI * 2f);
                bolts[num].Depth = random.NextFloat() * 8f + 2f;
                bolts[num].Life = 30;
            }
            ticksUntilNextBolt--;
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive)
                {
                    bolts[i].Life -= 1;
                    if (bolts[i].Life <= 0)
                    {
                        bolts[i].IsAlive = false;
                    }
                }
            }

        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                
            }
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive)
                {
                    Vector2 position = bolts[i].Position;
					float scale = MathHelper.Lerp(0.5f, 0.25f, Math.Max(0f, Math.Min(1f, position.X / 1000f)));
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                        Texture2D texture = BoltTex.Value;
                        int life = bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = FlashTex.Value;
                        }
                        float scale2 = life / 30f;
                        spriteBatch.Draw(texture, position, null, Color.White * scale * scale2 * Intensity, bolts[i].Rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);
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

            bolts = new Bolt[500];
            for (int i = 0; i < bolts.Length; i++)
            {
                bolts[i].IsAlive = false;
            }
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

    public class InfinityZeroSkyData : ScreenShaderData
    {
        private int IZIndex;

        public InfinityZeroSkyData(string passName) : base(passName)
        {
        }

        private void UpdateIZIndex()
        {
            int IZType = ModContent.NPCType<InfinityZero>();
            int IZSpawmType = ModContent.NPCType<InfinityZeroSpawn1>();

            if (IZIndex >= 0 && Main.npc[IZIndex].active && (Main.npc[IZIndex].type == IZType || Main.npc[IZIndex].type == IZSpawmType))
            {
                return;
            }
            IZIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == IZType)
                {
                    IZIndex = i;
                    break;
                }
                if (Main.npc[i].active && Main.npc[i].type == IZSpawmType)
                {
                    IZIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateIZIndex();
            if (IZIndex != -1)
            {
                UseTargetPosition(Main.npc[IZIndex].Center);
            }
            base.Apply();
        }
    }

    public class IZSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/Biomes/Backgrounds/InfinityZeroSky_BG");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/Biomes/Backgrounds/InfinityZeroSky_BG");
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/Biomes/Backgrounds/InfinityZeroSky_BG");
        }
    }
}