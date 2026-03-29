using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic.Removed.Backgrounds
{
    public class CthulhuSky : CustomSky
    {

        ScreenCthulhuFog BGClouds = new ScreenCthulhuFog(true);

        private UnifiedRandom random = new UnifiedRandom();
        
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;
        private int _fogTimer = 300;
        private int _fogTimer2 = 300;
        private Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Removed/Backgrounds/CthulhuClouds").Value;

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            
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
                _fogTimer = texture.Width;
            }

            if (_fogTimer2 <= 0)
            {
                _fogTimer2 = texture.Width;
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            var planetPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(texture, planetPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(texture.Width >> 1, texture.Height >> 1), 1f, SpriteEffects.None, 1f);

            }
            BGClouds.Update(ModContent.Request<Texture2D>("AAModClassic/Removed/Backgrounds/CthulhuClouds").Value);
            BGClouds.Draw(ModContent.Request<Texture2D>("AAModClassic/Removed/Backgrounds/CthulhuClouds").Value, true, new Color(130, 130, 130));
        }

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
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