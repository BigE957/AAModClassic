using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Particles.Types;

public class CircleGlow : Particle
{
    public override bool Additive => false;

    private static Asset<Texture2D> Texture;
    private static Asset<Texture2D> White;

    public float DecayRate;
    public float RotationOffset;
    public float RotationSpin;
    public bool AffectedByGravity;
    public bool RotationSpinImpactedByVelocity;
    public bool FakeTintDrawing;

    public CircleGlow(Vector2 position, Vector2 velocity, float scale, Color color, float decayRate = 0.97f,  float rotationOffset = 0, float rotationSpin = 0, bool affectedByGravity = false, bool rotationSpinImpactedByVelocity = false, bool fakeTintDrawing = false)
    {
        Position = position;
        Velocity = velocity;
        Scale = Vector2.One * scale;
        Color = color;

        DecayRate = decayRate;
        AffectedByGravity = affectedByGravity;
        RotationOffset = rotationOffset;
        RotationSpin = rotationSpin;
        RotationSpinImpactedByVelocity = rotationSpinImpactedByVelocity;
        FakeTintDrawing = fakeTintDrawing;

        Lifetime = 100;
        Layer = DrawLayer.BeforeProjectiles;
    }

    public override void Load()
    {
        Texture = ModContent.Request<Texture2D>(AssetDirectory.Particles.CircleGlow);
        White = ModContent.Request<Texture2D>(AssetDirectory.Particles.CircleGlow_White);
    }

    public override void Update()
    {
        Velocity *= 0.95f;
        if (AffectedByGravity)
        {
            Velocity.X *= 0.995f;
            Velocity.Y += 0.25f;
        }

        if (RotationSpin != 0 && RotationSpinImpactedByVelocity)
            Rotation = (RotationSpin * Velocity.Length() * 0.25f) + MathHelper.PiOver2 + RotationOffset;
        else if (RotationSpin != 0)
            Rotation = RotationSpin + MathHelper.PiOver2 + RotationOffset;
        else
            Rotation = Velocity.ToRotation() + MathHelper.PiOver2 + RotationOffset;
        RotationSpin += RotationSpin;

        Lifetime = 100;
        Scale *= DecayRate;
        if (Scale.X <= 0.001f && Scale.Y <= 0.001f)
            Lifetime = 0;

        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture.Value, Position - Main.screenPosition, null, Color, Rotation, Texture.Size() * 0.5f, Scale, 0, 0f);
        if (FakeTintDrawing)
            spriteBatch.Draw(White.Value, Position - Main.screenPosition, null, Color.White, Rotation, Texture.Size() * 0.5f, Scale, 0, 0f);
    }
}
