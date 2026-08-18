using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace AAModClassic.Particles.Types
{
    public class LargeDust : Particle
    {
        private static Asset<Texture2D> Texture;

        public static int FrameVariants => 3;

        private float Opacity;
        private readonly Color ColorFire;
        private readonly Color ColorFade;
        private readonly float Spin;
        private readonly int Variant;

        public override void Load()
        {
            Texture = ModContent.Request<Texture2D>("AAModClassic/Particles/Types/LargeDust");
        }

        public LargeDust(Vector2 position, Vector2 velocity, Color colorFire, Color colorFade, float scale, float opacity, float rotationSpeed = 0f)
        {
            Position = position;
            Velocity = velocity;
            ColorFire = colorFire;
            ColorFade = colorFade;
            Scale = scale * Vector2.One;
            Opacity = opacity;
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Spin = rotationSpeed;
            Variant = Main.rand.Next(FrameVariants);
        }

        public override void Update()
        {
            Rotation += Spin * ((Velocity.X > 0) ? 1f : -1f);
            Velocity *= 0.85f;

            if (Opacity > 90)
            {
                Scale += new Vector2(0.01f, 0.01f);
                Opacity -= 3;
            }
            else
            {
                Scale *= 0.975f;
                Opacity -= 2;
            }

            if (Opacity < 0)
                ParticleSystem.RemoveParticle(this);

            Color = Color.Lerp(ColorFire, ColorFade, MathHelper.Clamp((float)((255 - Opacity) - 100) / 80f, 0f, 1f)) * (Opacity / 255f);

            base.Update();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            Rectangle frame = Texture.Frame(1, FrameVariants, Variant);
            Color lightColor = Color.MultiplyRGB(Lighting.GetColor(Position.ToTileCoordinates()));
            lightColor.A = Color.A;
            spritebatch.Draw(Texture.Value, Position - Main.screenPosition, frame, lightColor, Rotation, frame.Size() * 0.5f, Scale, 0, 0);
        }
    }
}
