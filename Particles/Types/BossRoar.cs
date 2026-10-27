using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AAModClassic.Particles.Types
{
    public class BossRoar : Particle
    {
        private static Asset<Texture2D> Texture;
        public override void Load()
        {
            Texture = ModContent.Request<Texture2D>("AAModClassic/Particles/Types/BossRoar");
        }

        private float OriginalScale;
        private float FinalScale;
        private float BaseOpacity;
        private float opacity;
        private Color BaseColor;

        public BossRoar(Vector2 position, Color color, float rotation, float originalScale, float finalScale, int lifeTime, float baseOpacity = 1f)
        {
            Position = position;
            BaseColor = color;
            OriginalScale = originalScale;
            FinalScale = finalScale;
            Scale = originalScale * Vector2.One;
            Lifetime = lifeTime;
            BaseOpacity = baseOpacity;
            Rotation = rotation;
        }

        public override void Update()
        {
            Scale = MathHelper.Lerp(OriginalScale, FinalScale, LifeRatio) * Vector2.One;

            opacity = 1f;
            if (LifeRatio < 0.1f)
                opacity = MathHelper.Lerp(0f, 1f, LifeRatio * 10);

            Color = BaseColor * opacity;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = Texture.Value;
            spriteBatch.Draw(tex, Position - Main.screenPosition, null, Color * BaseOpacity, Rotation, tex.Size() / 2f, Scale, SpriteEffects.None, 0);
        }
    }
}
