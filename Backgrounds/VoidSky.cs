using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using Terraria.Graphics.Shaders;
//using AAMod.NPCs.Bosses.Infinity;

namespace AAMod.Backgrounds
{
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
        public float Alpha = 0;


        public override void Update(GameTime gameTime)
        {
            if (AAWorld.downedZero)
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
            }
            if (NPC.downedMoonlord) //NPC.downedMoonlord)
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

        public Color GetAlpha(Color newColor, float alph)
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

        readonly AAMod mod = AAMod.instance;

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Texture2D PlanetTexture = mod.GetTexture("Backgrounds/VoidBH");
            Texture2D Asteroids1 = mod.GetTexture("Backgrounds/Asteroids1");
            Texture2D Asteroids2 = mod.GetTexture("Backgrounds/Asteroids2");
            Texture2D Asteroids3 = mod.GetTexture("Backgrounds/Asteroids3");
            Texture2D Echo = mod.GetTexture("Backgrounds/Echo");
            Texture2D LB = mod.GetTexture("Backgrounds/LB");
            Texture2D boltTexture = mod.GetTexture("Backgrounds/VoidBolt");
            Texture2D flashTexture = mod.GetTexture("Backgrounds/VoidFlash");
            Texture2D Stars = mod.GetTexture("Backgrounds/Void_Starfield");

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
				if(asteroidPercent1 > (float)Math.PI) asteroidPercent1 = 0f;
				if(asteroidPercent2 > (float)Math.PI) asteroidPercent2 = 0f;
				if(asteroidPercent3 > (float)Math.PI) asteroidPercent3 = 0f;
                Rotation -= .0008f;
                LBRotation += .0005f;
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent1) * 16f;
                Asteroidpos2.Y += (float)Math.Sin(asteroidPercent2) * -30f;
                Asteroidpos3.Y += (float)Math.Sin(asteroidPercent3) * 20f;
                if (!AAWorld.downedZero)
                {
                    spriteBatch.Draw(Stars, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
                }
                else
                {
                    spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * Intensity, Rotation, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
                    float lightningIntensity = BaseUtility.MultiLerp(Main.LocalPlayer.miscCounter % 100f / 100f, 0.2f, 0.8f, 0.2f);
                    spriteBatch.Draw(LB, planetPos, null, Color.White * 0.9f * Intensity * lightningIntensity, LBRotation, new Vector2(LB.Width >> 1, LB.Height >> 1), 1f, SpriteEffects.None, 1f);
                    if (AAWorld.downedZero)
                    {
                        spriteBatch.Draw(Echo, echoPos, null, GetGlowAlpha(true), 0f, new Vector2(Echo.Width >> 1, Echo.Height >> 1), AAWorld.downedAllAncients ? 0.4f : .3f, SpriteEffects.None, 1f);
                    }
                }
				Color astroGlow = Color.White * MathHelper.Lerp(0.7f, 1f, Main.mouseTextColor / 255f);
				astroGlow.A = (byte)(255f * Intensity);
                spriteBatch.Draw(Asteroids1, Asteroidpos1, null, NPC.downedMoonlord ? astroGlow : Color.White, 0f, new Vector2(Asteroids1.Width >> 1, Asteroids1.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids2, Asteroidpos2, null, NPC.downedMoonlord ? astroGlow : Color.White, 0f, new Vector2(Asteroids2.Width >> 1, Asteroids2.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids3, Asteroidpos3, null, NPC.downedMoonlord ? astroGlow : Color.White, 0f, new Vector2(Asteroids3.Width >> 1, Asteroids3.Height >> 1), 1f, SpriteEffects.None, 1f);
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive && bolts[i].Depth > minDepth && bolts[i].Depth < maxDepth)
                {
                    Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                    Vector2 position = (bolts[i].Position - value3) * value4 + value3 - Main.screenPosition;
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

    public class VoidSkyData : ScreenShaderData
    {
        public VoidSkyData(string passName) : base(passName)
        {
        }

        private void UpdateVoidSky()
        {
            AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
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
}