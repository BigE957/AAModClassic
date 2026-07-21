using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace AAModClassic.Particles.Types;

public class Spark : Particle
{
    public Color InitialColor;
    public bool AffectedByGravity;
    public bool AffectedByLight;
    public bool FadeIn = false;
    public float FadeInScale = 0f;

    public override bool Additive => true;

    public Spark(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, bool fadeIn = false, bool affectedByLight = false)
    {
        Position = relativePosition;
        Velocity = velocity;
        AffectedByGravity = affectedByGravity;
        AffectedByLight = affectedByLight;
        Scale = Vector2.One * scale;
        FadeInScale = scale;
        Lifetime = lifetime;
        Color = InitialColor = color;
        FadeIn = fadeIn;

        if (FadeIn)
            Scale = Vector2.One * 0f;
    }

    public override void Update()
    {
        if (!FadeIn)
        {
            Scale *= 0.95f;
            Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(LifeRatio, 3D));
        }
        else
        {
            if ((float)Time / (float)Lifetime < 0.5f)
            {
                Scale = Vector2.Lerp(Scale, FadeInScale * Vector2.One, 0.2f);
            }
            else
            {
                Scale = Vector2.Lerp(Scale, FadeInScale * Vector2.One, -0.21f);
            }
        }
        Velocity *= 0.95f;
        if (Velocity.Length() < 12f && AffectedByGravity)
        {
            Velocity.X *= 0.94f;
            Velocity.Y += 0.25f;
        }
        Rotation = Velocity.ToRotation() + MathHelper.PiOver2;

        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Vector2 scale = new Vector2(0.5f, 1.6f) * Scale;
        Texture2D texture = TextureAssets.Extra[ExtrasID.ThePerfectGlow].Value;

        Color col = Color;

        if (AffectedByLight)
        {
            col = Lighting.GetColor((Position / 16).ToPoint()).MultiplyRGB(Color);
        }

        spriteBatch.Draw(texture, Position - Main.screenPosition, null, col, Rotation, texture.Size() * 0.5f, scale, 0, 0f);
        spriteBatch.Draw(texture, Position - Main.screenPosition, null, col, Rotation, texture.Size() * 0.5f, scale * new Vector2(0.45f, 1f), 0, 0f);
    }
}
