using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Void.World.Biomes.Water;
using AAModClassic._CrossMod;
using AAModClassic._Unreleased;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Achievements;
using AAModClassic.Assets;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
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

namespace AAModClassic._Content.Void.World.Biomes
{
    public class VoidBiome : ModBiome
    {
        public override string MapBackground => "AAModClassic/_Content/Void/World/Biomes/Backgrounds/VoidMap";

        public override string BackgroundPath => "AAModClassic/_Content/Void/World/Biomes/Backgrounds/VoidMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = (AAWorld.voidTiles > 20 && player.ZoneSkyHeight) || (AAWorld.voidTiles > 100 && !player.ZoneSkyHeight) || BaseAI.GetNPC(player.Center, ModContent.NPCType<Zero>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<ZeroA>(), 5000) != -1;
            if (active && player.whoAmI == Main.myPlayer)
                VoidDiscovered.Condition.Complete();
            return active;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:VoidSky", isActive || player.GetModPlayer<ZAAPlayer>().VoidUnit);
        }

        public override int Music =>
            (AADowned.DownedZero && !AAWorld_Unreleased.DownedIZ) ? MusicManagementSystem.MusicSlots["Void_PreIZ"] :
            NPC.downedMoonlord ? MusicManagementSystem.MusicSlots["Void_PostML"] :
            MusicManagementSystem.MusicSlots["Void"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<VoidSurfaceBgStyle>();

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<VoidUGBG>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<VoidWaterStyle>();
    }

    public class VoidSkySystem : ModPlayer
    {
        public override void OnEnterWorld()
        {
            if (AADowned.DownedZero)
                VoidSky.Alpha = 1f;
        }
    }

    public class VoidSky : CustomSky
    {
        private readonly UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;

            public int Life;

            public bool IsAlive;
        }

        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;
        public static float Alpha = -1f;

        public static Asset<Texture2D> BlackHoleTex;
        public static Asset<Texture2D> BlackHoleLightningTex;
        public static Asset<Texture2D> AsteroidTex0;
        public static Asset<Texture2D> AsteroidTex1;
        public static Asset<Texture2D> AsteroidTex2;
        public static Asset<Texture2D> EchoTex;
        public static Asset<Texture2D> BoltTex;
        public static Asset<Texture2D> FlashTex;
        public static Asset<Texture2D> SkyTex;

        public override void OnLoad()
        {
            BlackHoleTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/VoidBH");
            BlackHoleLightningTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/LB");
            AsteroidTex0 = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/Asteroids0");
            AsteroidTex1 = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/Asteroids1");
            AsteroidTex2 = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/Asteroids2");
            EchoTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/Echo");
            BoltTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/VoidBolt");
            FlashTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/VoidFlash");
            SkyTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/Void_Starfield");
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.gameMenu && AADowned.DownedZero && Alpha != -1)
            {
                Alpha += 0.05f;
                if (Alpha > 1f) Alpha = 1f;
            }

            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
                if (Intensity < 0.001f)
                    Intensity = 0f;
            }

            if (Intensity > 0 && WeakReferences.RealisticSky != null)
                WeakReferences.RealisticSky.Call("temporarilydisable");
            
            if (Main.gameMenu || NPC.downedMoonlord)
            {
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

        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public float asteroidPercent1 = 0f;
        public float asteroidPercent2 = 0f;
        public float asteroidPercent3 = 0f;
        public float Rotation = 0;
        public float LBRotation = 0;
        public NPC IZ;


        public Color infinityGlowRed = new Color(233, 53, 53);
        public Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
        }

        public static Color GetAlpha(Color newColor, float alph)
        {
            int alpha = 255 - (int)(255 * alph);
            float alphaDiff = (255 - alpha) / 255f;
            int newR = (int)(newColor.R * alphaDiff);
            int newG = (int)(newColor.G * alphaDiff);
            int newB = (int)(newColor.B * alphaDiff);
            int newA = newColor.A - alpha;
            if (newA < 0) newA = 0;
            if (newA > 255) newA = 255;
            return new Color(newR, newG, newB, newA);
        }
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (AAMod.instance == null)
                return;

            Texture2D PlanetTexture = BlackHoleTex.Value;
            Texture2D LB = BlackHoleLightningTex.Value;
            Texture2D Asteroids1 = AsteroidTex0.Value;
            Texture2D Asteroids2 = AsteroidTex1.Value;
            Texture2D Asteroids3 = AsteroidTex2.Value;
            Texture2D Echo = EchoTex.Value;
            Texture2D boltTexture = BoltTex.Value;
            Texture2D flashTexture = FlashTex.Value;
            Texture2D Stars = SkyTex.Value;

            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                var planetPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var echoPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos1 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos2 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos3 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                asteroidPercent1 += 0.004f;
                asteroidPercent2 += 0.005f;
                asteroidPercent3 += 0.006f;
                if (asteroidPercent1 > (float)Math.PI) asteroidPercent1 = 0f;
                if (asteroidPercent2 > (float)Math.PI) asteroidPercent2 = 0f;
                if (asteroidPercent3 > (float)Math.PI) asteroidPercent3 = 0f;
                Rotation -= .0008f;
                LBRotation += .0005f;
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent1) * 16f;
                Asteroidpos2.Y += (float)Math.Sin(asteroidPercent2) * -30f;
                Asteroidpos3.Y += (float)Math.Sin(asteroidPercent3) * 20f;
                if (!AADowned.DownedZero || Alpha <= 0)
                {
                    spriteBatch.Draw(Stars, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * Intensity);
                }
                else if (Alpha > 0)
                {
                    spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    float riftIntensity = Intensity * Intensity * Intensity;
                    spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * riftIntensity * Alpha, Rotation, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
                    float lightningIntensity = BaseUtility.MultiLerp(Main.LocalPlayer.miscCounter % 100f / 100f, 0.2f, 0.8f, 0.2f);
                    spriteBatch.Draw(LB, planetPos, null, Color.White * 0.9f * riftIntensity * Alpha * lightningIntensity, LBRotation, new Vector2(LB.Width >> 1, LB.Height >> 1), 1f, SpriteEffects.None, 1f);

                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !AAWorld_Unreleased.DownedIZ)
                    {
                        bool anyIZ = false;
                        foreach(NPC n in Main.ActiveNPCs)
                        {
                            if (n.type != ModContent.NPCType<InfinityZeroSpawn1>() && n.type != ModContent.NPCType<InfinityZero>())
                                continue;

                            anyIZ = true;
                            break;
                        }

                        if(!anyIZ)
                            spriteBatch.Draw(Echo, echoPos, null, GetGlowAlpha(true) * riftIntensity * Alpha, 0f, new Vector2(Echo.Width >> 1, Echo.Height >> 1), AADowned.DownedAllAncients ? 0.4f : .3f, SpriteEffects.None, 1f);
                    }
                }
                Color astroGlow = Color.White * MathHelper.Lerp(0.7f, 1f, Main.mouseTextColor / 255f);
                astroGlow.A = (byte)(255f * Intensity);
                spriteBatch.Draw(Asteroids1, Asteroidpos1, null, (NPC.downedMoonlord ? astroGlow : Color.White) * Intensity, 0f, new Vector2(Asteroids1.Width >> 1, Asteroids1.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids2, Asteroidpos2, null, (NPC.downedMoonlord ? astroGlow : Color.White) * Intensity, 0f, new Vector2(Asteroids2.Width >> 1, Asteroids2.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids3, Asteroidpos3, null, (NPC.downedMoonlord ? astroGlow : Color.White) * Intensity, 0f, new Vector2(Asteroids3.Width >> 1, Asteroids3.Height >> 1), 1f, SpriteEffects.None, 1f);
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 screenCenter = Main.gameMenu ? (new(Main.screenWidth / 2, Main.screenHeight / 2)) : Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Vector2 screenPos = Main.gameMenu ? Vector2.Zero : Main.screenPosition;
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive && bolts[i].Depth > minDepth && bolts[i].Depth < maxDepth)
                {
                    Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                    Vector2 position = (bolts[i].Position - screenCenter) * value4 + screenCenter - screenPos;
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        Texture2D texture = boltTexture;
                        int life = bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = flashTexture;
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
            if (!Main.gameMenu || (args.Length > 0 && (bool)args[0] == true))
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

    public class VoidSkyData(string passName) : ScreenShaderData(passName)
    {
        private static void UpdateVoidSky()
        {
            if (AAWorld.voidTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateVoidSky();
            base.Apply();
        }
    }

    public class VoidSurfaceBgStyle : ModSurfaceBackgroundStyle
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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, FilePathUtils.RemoveModNameHeaderFromFilePath(AssetDirectory.General.Nothing));
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, FilePathUtils.RemoveModNameHeaderFromFilePath(AssetDirectory.General.Nothing));
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, FilePathUtils.RemoveModNameHeaderFromFilePath(AssetDirectory.General.Nothing));
        }
    }

    public class VoidUGBG : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Void/World/Biomes/Backgrounds/VoidUG");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Void/World/Biomes/Backgrounds/VoidUG");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Void/World/Biomes/Backgrounds/VoidUG");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Void/World/Biomes/Backgrounds/VoidUG");
        }
    }
}
