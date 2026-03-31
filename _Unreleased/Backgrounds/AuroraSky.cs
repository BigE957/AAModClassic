using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Unreleased.Backgrounds
{
    public class AuroraSky : CustomSky
    {

        private UnifiedRandom random = new UnifiedRandom();


        public static Texture2D PlanetTexture;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;

        public override void OnLoad()
        {
            PlanetTexture = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Backgrounds/AuroraSky_Aurora").Value;
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
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this.Intensity);
                var planetPos = new Vector2(Main.screenWidth * 0.3f, Main.screenHeight * 0.3f);
                spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * this.Intensity, 0f, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
            }
        }

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
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
}