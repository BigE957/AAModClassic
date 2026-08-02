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

public class LensFlareAttached : Particle
{
    public override bool Additive => false;

    private static Asset<Texture2D> Texture;
    private static Asset<Texture2D> Bloom;

    public Func<Vector2> AnchorPosition;
    public Vector2 Offset;
    public float FadeInScale = 0f;
    public Vector2 FlareStretch;

    public LensFlareAttached(Func<Vector2> anchorPosition, Vector2 offset, Vector2 rotation, int lifetime, float scale, Color color, DrawLayer drawLayer = DrawLayer.AfterEverything, Vector2? flareStretch = null)
    {
        AnchorPosition = anchorPosition;
        Offset = offset;
        Rotation = rotation.ToRotation();
        Lifetime = lifetime;
        FadeInScale = scale;
        Color = color;
        Layer = drawLayer;
        FlareStretch = flareStretch == null ? new Vector2(0.5f, 1f) : (Vector2)flareStretch;
    }

    public override void Load()
    {
        Texture = ModContent.Request<Texture2D>(AssetDirectory.General.LensFlare_SmallBlank);
        Bloom = ModContent.Request<Texture2D>(AssetDirectory.General.Bloom_Medium);
    }

    public override void Update()
    {
        if (AnchorPosition != null)
            Position = AnchorPosition() + Offset;

        if ((float)Time / (float)Lifetime < 0.5f)
            Scale = Vector2.Lerp(Scale, FadeInScale * Vector2.One, 0.2f);
        else
            Scale = Vector2.Lerp(Scale, FadeInScale * Vector2.One, -0.21f);

        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Vector2 scale = FlareStretch * Scale;
        Texture2D texture = Texture.Value;
        Texture2D bloom = Bloom.Value;

        Color c = Color;

        float horizFlareRotation = new Vector2(0.1f, 0.75f).ToRotation();

        // colored bloom
        // middle, vertical, horizontal
        float colorMult = 0.5f;
        spriteBatch.Draw(bloom, Position - Main.screenPosition, null, c * colorMult, Rotation, bloom.Size() * 0.5f, scale, 0, 0f);
        spriteBatch.Draw(bloom, Position - Main.screenPosition, null, c * colorMult, Rotation, bloom.Size() * 0.5f, scale * new Vector2(0.3f, 1.25f), 0, 0f);
        spriteBatch.Draw(bloom, Position - Main.screenPosition, null, c * colorMult, Rotation + horizFlareRotation, bloom.Size() * 0.5f, scale * new Vector2(0.2f, 0.8f), 0, 0f);

        // red flare
        // vertical, horizontal
        spriteBatch.Draw(texture, Position - Main.screenPosition, null, c, Rotation, texture.Size() * 0.5f, scale * 1.3f, 0, 0f);
        spriteBatch.Draw(texture, Position - Main.screenPosition, null, c, Rotation + horizFlareRotation, texture.Size() * 0.5f, (scale * 0.6f) * 1.3f, 0, 0f);

        // white flare 
        // vertical, horizontal
        spriteBatch.Draw(texture, Position - Main.screenPosition, null, Color.White, Rotation, texture.Size() * 0.5f, scale, 0, 0f);
        spriteBatch.Draw(texture, Position - Main.screenPosition, null, Color.White, Rotation + horizFlareRotation, texture.Size() * 0.5f, scale * new Vector2(0.6f, 0.4f), 0, 0f);
    }
}
