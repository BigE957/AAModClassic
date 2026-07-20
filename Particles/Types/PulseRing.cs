using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Particles.Types;

public class PulseRing : Particle
{
    private static Asset<Texture2D> Texture;

    public override bool Additive => true;

    private readonly float OriginalScale;
    private readonly float FinalScale;
    private float opacity;

    public override void Load()
    {
        Texture = ModContent.Request<Texture2D>("AAModClassic\\Particles\\Types\\HollowCircleHardEdge");
    }

    public PulseRing(Vector2 position, Vector2 velocity, Color color, float originalScale, float finalScale, int lifeTime)
    {
        Position = position;
        Velocity = velocity;
        Color = color;
        OriginalScale = originalScale;
        FinalScale = finalScale;
        Scale = originalScale * Vector2.One;
        Lifetime = lifeTime;
        Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
    }

    public override void Update()
    {
        float pulseProgress = MathUtils.PolyOutEasing(LifeRatio, 1);
        Scale = MathHelper.Lerp(OriginalScale, FinalScale, pulseProgress) * Vector2.One;

        opacity = (float)Math.Sin(MathHelper.PiOver2 + LifeRatio * MathHelper.PiOver2);

        Color c = Color * opacity;
        Lighting.AddLight(Position, c.R / 255f, c.G / 255f, c.B / 255f);
        Velocity *= 0.95f;

        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Texture2D tex = Texture.Value;
        spriteBatch.Draw(tex, Position - Main.screenPosition, null, Color * opacity, Rotation, tex.Size() / 2f, Scale, SpriteEffects.None, 0);
    }
}
