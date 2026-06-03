using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Inferno.World.Biomes.Waters;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
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

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public class InfernoBiome : ModBiome
    {
        public override string MapBackground => "AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/InfernoMap";

        public override string BackgroundPath => "AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/InfernoMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.infernoTiles > 100 || BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaHead>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaAHead>(), 5000) != -1;
            return player.GetModPlayer<AAPlayer>().ZoneInferno = active;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool rllyActive = (isActive && player.Center.Y <= Main.worldSurface * 16) || player.GetModPlayer<AAPlayer>().SunAltar;
            player.ManageSpecialBiomeVisuals("AAModClassic:InfernoSky", rllyActive);
            player.ManageSpecialBiomeVisuals("HeatDistortion", rllyActive && Main.UseHeatDistortion);
        }

        public override int Music =>
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicManagementSystem.MusicSlots["Inferno_Underground"] :
            AAWorld.downedAkuma && AAWorld.downedYamata ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            !Main.dayTime ? MusicManagementSystem.MusicSlots["Inferno_Night"] :
            MusicManagementSystem.MusicSlots["Inferno_Surface"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
        {
            get
            {
                if ((Main.LocalPlayer.ZoneDesert && Main.LocalPlayer.ZoneSnow) && ModLoader.TryGetMod("SpiritReforged", out var spirit))
                {
                    //Rectangle saltFlatsArea = (Rectangle)spirit.Call("GetSaltFlatsArea");
                    //bool playerInSaltFlats = saltFlatsArea.Contains(Main.LocalPlayer.Center.ToTileCoordinates());
                    //Main.NewText(saltFlatsArea);
                    //if (playerInSaltFlats)
                    return null;
                }

                return Main.LocalPlayer.ZoneDesert ? ModContent.GetInstance<InfernoDesertBgStyle>() : !Main.LocalPlayer.ZoneSnow ? ModContent.GetInstance<InfernoSurfaceBgStyle>() : null;
            }
        }

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<InfernoUgBgStyle>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<InfernoWaterStyle>();
    }

    public class UndergroundInfernoBiome : ModBiome
    {
        public override string BackgroundPath => "AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/InfernoMap";
    }

    public class InfernoSky : CustomSky
    {
        public bool Active;
        public float Intensity;
        private struct Meteor
        {
            public Vector2 Position;

            public float Depth;

            public int FrameCounter;

            public float Scale;

            public float StartX;
        }
        private Meteor[] Meteors;

        private readonly UnifiedRandom _random = new UnifiedRandom();

        public static Asset<Texture2D> SunTex;
        public static Asset<Texture2D> DemonSunTex;
        public static Asset<Texture2D> MeteorTex;
        public static Asset<Texture2D> SkyTex;

        public override void OnLoad()
        {
            SunTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/Sun");
            DemonSunTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/DemonSun");
            MeteorTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/Meteor");
            SkyTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/SkyTex");
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

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (AAMod.instance == null)
                return;

            Texture2D PlanetTexture = SunTex.Value;
            Texture2D demonSun = DemonSunTex.Value;
            Texture2D MeteorTexture = MeteorTex.Value;
            Texture2D SkyTexture = SkyTex.Value;

            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (Main.dayTime || (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().SunAltar))
                {
                    spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    if (Main.gameMenu)
                        spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.OrangeRed * Intensity);
                    else
                        spriteBatch.Draw(SkyTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.OrangeRed * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * Intensity));
                    float num64 = 1f;
                    num64 -= Main.cloudAlpha * 1.5f;
                    if (num64 < 0f)
                    {
                        num64 = 0f;
                    }
                    int num20 = (int)(Main.time / 54000.0 * (Main.screenWidth + TextureAssets.Sun.Value.Width * 2)) - TextureAssets.Sun.Value.Width;
                    int num21 = 0;
                    float num22 = 1f;
                    float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;
                    double bgTop = (-Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0;
                    if (Main.dayTime)
                    {
                        double num26;
                        if (Main.time < 27000.0)
                        {
                            num26 = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
                            num21 = (int)(bgTop + num26 * 250.0 + 180.0);
                        }
                        else
                        {
                            num26 = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
                            num21 = (int)(bgTop + num26 * 250.0 + 180.0);
                        }
                        num22 = (float)(1.2 - num26 * 0.4);
                    }

                    num22 = MathHelper.Lerp(0.25f, num22, Intensity);

                    Color color6 = new Color((byte)(255f * num64), (byte)(Color.White.G * num64), (byte)(Color.White.B * num64), (byte)(255f * num64));
                    if (!Main.gameMenu && BasePlayer.HasAccessory(Main.LocalPlayer, ModContent.ItemType<Items.Vanity.HappySunSticker>(), true, true))
                        Main.spriteBatch.Draw(demonSun, new Vector2(num20, num21 + Main.sunModY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, demonSun.Width, demonSun.Height)), color6 * Intensity, rotation, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Height / 2), num22, SpriteEffects.None, 0f);
                    else
                        Main.spriteBatch.Draw(PlanetTexture, new Vector2(num20, num21 + (Main.gameMenu ? Main.sunModY + 240 : Main.sunModY)), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, PlanetTexture.Width, PlanetTexture.Height)), color6 * Intensity, rotation, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Height / 2), num22, SpriteEffects.None, 0f);
                }
            }
            int num = -1;
            int num2 = 0;
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaHead>()))
            {
                for (int i = 0; i < Meteors.Length; i++)
                {
                    float depth = Meteors[i].Depth;
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
                float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
                Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
                Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
                for (int j = num; j < num2; j++)
                {
                    Vector2 value4 = new Vector2(1f / Meteors[j].Depth, 0.9f / Meteors[j].Depth);
                    Vector2 position = (Meteors[j].Position - value3) * value4 + value3 - Main.screenPosition;
                    int num3 = Meteors[j].FrameCounter / 3;
                    Meteors[j].FrameCounter = (Meteors[j].FrameCounter + 1) % 12;
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        spriteBatch.Draw(MeteorTexture, position, new Rectangle?(new Rectangle(0, num3 * (MeteorTexture.Height / 4), MeteorTexture.Width, MeteorTexture.Height / 4)), Color.White * scale * Intensity, 0f, Vector2.Zero, value4.X * 5f * Meteors[j].Scale, SpriteEffects.None, 0f);
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
            Meteors = new Meteor[150];
            for (int i = 0; i < Meteors.Length; i++)
            {
                float num = i / (float)Meteors.Length;
                Meteors[i].Position.X = num * (Main.maxTilesX * 16f) + _random.NextFloat() * 40f - 20f;
                Meteors[i].Position.Y = _random.NextFloat() * -((float)Main.worldSurface * 16f + 10000f) - 10000f;
                if (_random.NextBool(3))
                {
                    Meteors[i].Depth = _random.NextFloat() * 3f + 1.8f;
                }
                else
                {
                    Meteors[i].Depth = _random.NextFloat() * 5f + 4.8f;
                }
                Meteors[i].FrameCounter = _random.Next(12);
                Meteors[i].Scale = _random.NextFloat() * 0.5f + 1f;
                Meteors[i].StartX = Meteors[i].Position.X;
            }
            Array.Sort(Meteors, new Comparison<Meteor>(SortMethod));
        }
        private int SortMethod(Meteor meteor1, Meteor meteor2)
        {
            return meteor2.Depth.CompareTo(meteor1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            if ((!Main.gameMenu && !Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneInferno && !Main.LocalPlayer.GetModPlayer<AAPlayer>().SunAltar && !Main.LocalPlayer.GetModPlayer<AAPlayer>().AkumaAltar) || (args.Length > 0 && (bool)args[0] == true))
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

    public class InfernoSkyData : ScreenShaderData
    {
        public InfernoSkyData(string passName) : base(passName)
        {

        }

        private static void UpdateInfernoSky()
        {
            if (AAWorld.infernoTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateInfernoSky();
            base.Apply();
        }
    }

    public class InfernoSurfaceBgStyle : ModSurfaceBackgroundStyle
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

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoBG");
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return base.ChooseCloseTexture(ref scale, ref parallax, ref a, ref b);
        }

        public override int ChooseMiddleTexture()
        {
            return base.ChooseMiddleTexture();
        }
    }

    public class InfernoUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoUnderground1");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoUnderground");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoCavern1");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoCavern");
        }
    }

    public class InfernoDesertBgStyle : ModSurfaceBackgroundStyle
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

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoDesertBG");
        }

    }
}