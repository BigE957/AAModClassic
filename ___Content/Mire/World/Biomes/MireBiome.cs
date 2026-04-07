using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata.Awakened;
using AAModClassic.___Content.Mire.World.Biomes.Water;
using AAModClassic.Backgrounds;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Biomes
{
    public class MireBiomeZone : ModBiome
    {
        public override string MapBackground => "AAModClassic/Map/MireMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = (AAWorld.mireTiles > 100) || BaseAI.GetNPC(player.Center, ModContent.NPCType<Yamata>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<YamataA>(), 5000) != -1;
            return player.GetModPlayer<AAPlayer>().ZoneMire = active;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool rllyActive = (isActive && player.Center.Y <= Main.worldSurface * 16) || player.GetModPlayer<AAPlayer>().MoonAltar;
            player.ManageSpecialBiomeVisuals("AAModClassic:MireSky", rllyActive);
        }

        public override int Music =>
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/MireUnderground") :
            AAWorld.downedAkuma && AAWorld.downedYamata ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/SleepingDragon") :
            Main.dayTime ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/DM") :
            MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/MireSurface");

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => Main.LocalPlayer.ZoneDesert ? ModContent.GetInstance<MireDesertBgStyle>() : !Main.LocalPlayer.ZoneSnow ? ModContent.GetInstance<MireSurfaceBgStyle>() : null;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<MireUgBgStyle>();

        public override ModWaterStyle WaterStyle => Main.dayTime && !AAWorld.downedYamata && Main.LocalPlayer.position.Y < Main.worldSurface * 16.0 && !Main.LocalPlayer.buffImmune[ModContent.BuffType<Buffs.Clueless_Buff>()] ? ModContent.GetInstance<FogWaterStyle>() : ModContent.GetInstance<MireWaterStyle>();
    }

    public class MireSky : CustomSky
    {
        public bool Active;
        public float Intensity;

        public override void OnLoad()
        {
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
            bool SilouetteMode = !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().Clueless;
            if (SilouetteMode)
            {
                return new Color(1, 1, 1);
            }
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Texture2D PlanetTexture = ModContent.Request<Texture2D>("AAModClassic/___Content/Mire/World/Biomes/MireBiome_Moon").Value;
            Texture2D SkyTexture = AAMod.GetTexture("AAModClassic/___Content/Mire/World/Biomes/MireBiome_Moon");
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (!Main.dayTime || (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().MoonAltar))
                {
                    spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * Intensity);
                    double bgTop = (int)((-Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    Main.ColorOfTheSkies = Color.White;
                    if (Main.gameMenu || Main.netMode == NetmodeID.Server)
                    {
                        bgTop = -200;
                    }
                    int num23 = (int)(Main.time / 32400.0 * (Main.screenWidth + TextureAssets.Moon[Main.moonType].Width() * 2)) - TextureAssets.Moon[Main.moonType].Width();
                    int num24 = 0;
                    Color white2 = Color.White;
                    float num25 = 1f;
                    float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
                    if (!Main.dayTime)
                    {
                        double num27;
                        if (Main.time < 16200.0)
                        {
                            num27 = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
                            num24 = (int)(bgTop + num27 * 250.0 + 180.0);
                        }
                        else
                        {
                            num27 = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
                            num24 = (int)(bgTop + num27 * 250.0 + 180.0);
                        }
                        num25 = (float)(1.2 - num27 * 0.4);
                    }
                    float num65 = 1f - Main.cloudAlpha * 1.5f;
                    if (num65 < 0f)
                    {
                        num65 = 0f;
                    }

                    num25 = MathHelper.Lerp(0.25f, num25, Intensity);

                    white2.R = (byte)(white2.R * num65);
                    white2.G = (byte)(white2.G * num65);
                    white2.B = (byte)(white2.B * num65);
                    white2.A = (byte)(white2.A * num65);
                    Main.spriteBatch.Draw(PlanetTexture, new Vector2(num23, num24 + (Main.gameMenu ? Main.moonModY + 200 : Main.moonModY)), new Rectangle?(new Rectangle(0, 0, PlanetTexture.Width, PlanetTexture.Width)), white2 * Intensity, rotation2, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Width / 2), num25, SpriteEffects.None, 0f);
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
        }

        public override void Deactivate(params object[] args)
        {
            if ((!Main.gameMenu && !Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire && !Main.LocalPlayer.GetModPlayer<AAPlayer>().MoonAltar && !Main.LocalPlayer.GetModPlayer<AAPlayer>().YamataAltar) || (args.Length > 0 && (bool)args[0] == true))
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

    public class MireSkyData : ScreenShaderData
    {
        public MireSkyData(string passName) : base(passName)
        {
        }

        private static void UpdateMireSky()
        {
            if (AAWorld.mireTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateMireSky();
            base.Apply();
        }
    }

    public class MireSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_SurfaceBackground");
        }
        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_SurfaceForeground2");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_SurfaceForeground1");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

            mireBGFog.Update(ModContent.Request<Texture2D>("AAModCLassic/Backgrounds/FogTex").Value);
            mireBGFog.Draw(ModContent.Request<Texture2D>("AAModCLassic/Backgrounds/FogTex").Value, true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
        }
    }

    public class MireUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_UndergroundTop");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_Underground");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_CavernTop");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_Cavern");
        }
    }

    public class MireDesertBgStyle : ModSurfaceBackgroundStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "___Content/Mire/World/Biomes/MireBiome_SurfaceBackground_Desert");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

            mireBGFog.Update(ModContent.Request<Texture2D>("AAModCLassic/Backgrounds/FogTex").Value);
            mireBGFog.Draw(ModContent.Request<Texture2D>("AAModCLassic/Backgrounds/FogTex").Value, true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
        }

    }
}
