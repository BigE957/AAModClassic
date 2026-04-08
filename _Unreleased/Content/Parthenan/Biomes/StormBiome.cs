using AAModClassic.Backgrounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Unreleased.Content.Parthenan.Biomes
{
    public class StormBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneStorm = AAWorld_Unreleased.StormTiles >= 1;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:StormSky", isActive && player.Center.Y <= Main.worldSurface * 16);
        }

        public override int Music => MusicLoader.GetMusicSlot(AAMod.instance, "_Unreleased/Sounds/Music/Maelstrom");

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<StormSurfaceBgStyle>();
    }

    public class StormSky : CustomSky
    {

        StormSky_Clouds BGClouds = new StormSky_Clouds(true);

        private UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;

            public int Life;

            public bool IsAlive;
        }

        private static Asset<Texture2D> texture;
        public static Asset<Texture2D> boltTexture;
        public static Asset<Texture2D> flashTexture;
        
        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;
        private bool _isLeaving = false;
        private bool _isActive = false;
        private readonly float fogOpacity1 = 0.5f;
        private readonly float fogOpacity2 = 0.4f;
        private int _fogTimer = 300;
        private int _fogTimer2 = 300;

        public override void OnLoad()
        {
            texture = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/Biomes/StormBiome_Clouds");
            boltTexture = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/Biomes/StormBiome_Bolt");
            flashTexture = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/Biomes/StormBiome_Flash");;
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
            _fogTimer--;
            _fogTimer2 -= 3;
            if (_fogTimer <= 0)
            {
                _fogTimer = texture.Width();
            }

            if (_fogTimer2 <= 0)
            {
                _fogTimer2 = texture.Width();
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
                bolts[num].Position.X = random.NextFloat() * (Main.maxTilesX * 16f + 4000f) - 2000f;
                bolts[num].Position.Y = random.NextFloat() * 500f;
                bolts[num].Depth = random.NextFloat() * 8f + 2f;
                bolts[num].Life = 30;
            }
            ticksUntilNextBolt--;
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive)
                {
                    Bolt[] expr168cp0 = bolts;
                    int expr168cp1 = i;
                    expr168cp0[expr168cp1].Life = expr168cp0[expr168cp1].Life - 1;
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
            BGClouds.Update(texture.Value);
            BGClouds.Draw(texture.Value, true, new Color(160, 100, 180));
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive && bolts[i].Depth > minDepth && bolts[i].Depth < maxDepth)
                {
                    Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                    Vector2 position = (bolts[i].Position - value3) * value4 + value3 - Main.screenPosition;
                    position = bolts[i].Position; //TODOSIEGE i really just dont know what he was trying to do in the lines above. figure it out?
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        Texture2D texture = boltTexture.Value;
                        int life = bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = flashTexture.Value;
                        }
                        float scale2 = life / 30f;
                        spriteBatch.Draw(texture, position, null, Color.White * scale * scale2 * Intensity, 0f, Vector2.Zero, value4.X * 5f, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
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

    public class StormSkyData : ScreenShaderData
    {
        private int DataIndex;

        public StormSkyData(string passName) : base(passName)
        {
        }

        private void UpdateStormSky()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            if (AAWorld_Unreleased.StormTiles < 1)
            {
                return;
            }
            DataIndex = -1;
        }

        public override void Apply()
        {
            UpdateStormSky();
            base.Apply();
        }
    }

    public class StormSurfaceBgStyle : ModSurfaceBackgroundStyle
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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Content/Parthenan/Biomes/Backgrounds/StormBiome_BG");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Content/Parthenan/Biomes/Backgrounds/StormBiome_BG");
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Content/Parthenan/Biomes/Backgrounds/StormBiome_BG");
        }
    }
}