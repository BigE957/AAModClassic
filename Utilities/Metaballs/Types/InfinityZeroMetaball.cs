using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Metaballs.Types;
/*
public class IZMetaballTestSYstem : ModSystem
{
    public override void PreUpdatePlayers()
    {
        InfinityZeroMetaballParticle.SpawnParticle(Main.MouseWorld, Main.rand.NextVector2Circular(4f, 4f), Main.rand.NextFloat(64f, 72f));
    }
}
*/
public class InfinityZeroMetaball : Metaball
{
    public static InfinityZeroMetaball Instance => instance == null ? instance = ModContent.GetInstance<InfinityZeroMetaball>() : instance;
    private static InfinityZeroMetaball instance;

    public override bool ShouldRender => true;

    public override IEnumerable<Texture2D> LayerTextures
    {
        get
        {
            for (int i = 0; i < layerAssets.Length; i++)
                yield return layerAssets[i].Value;
        }
    }

    public override Vector2 CalculateManualOffsetForLayer(int layerIndex)
    {
        switch (layerIndex)
        {
            case 0:
                return Vector2.UnitX * Main.GlobalTimeWrappedHourly * 30f;

            case 1:
                return Vector2.UnitX * Main.GlobalTimeWrappedHourly * 60f + Vector2.UnitY * MathF.Sin(Main.GlobalTimeWrappedHourly) * 30f;
            case 2:
                Vector2 offset = Vector2.One * MathF.Cos(Main.GlobalTimeWrappedHourly * 0.04f) * 1000f;
                offset = -offset.RotatedBy(Main.GlobalTimeWrappedHourly * 0.08f) * 0.76f;
                return offset;
            case 3:
                offset = Vector2.One * MathF.Cos(Main.GlobalTimeWrappedHourly * 0.045f) * 1000f;
                offset = -offset.RotatedBy(Main.GlobalTimeWrappedHourly * 0.08f) * 0.84f;
                return offset;
            case 4:
                offset = Vector2.One * MathF.Cos(Main.GlobalTimeWrappedHourly * 0.05f) * 1000f;
                offset = -offset.RotatedBy(Main.GlobalTimeWrappedHourly * 0.08f) * 0.97f;
                return offset;
        }
        return Vector2.Zero;
    }

    public override Color EdgeColor(int layer) => Color.DarkRed;

    public override Color DrawLayerColor(int layer) => layer == 1 ? Color.White : Color.Gray;

    public override DrawLayer[] DrawLayers => [DrawLayer.BeforeAllTiles, DrawLayer.AfterProjectiles, DrawLayer.AfterEverything];

    private static Asset<Texture2D>[] layerAssets = null;

    public override void Load()
    {
        if (Main.dedServ)
            return;

        layerAssets = [ModContent.Request<Texture2D>($"AAModClassic/Utilities/Metaballs/Types/InfinityZeroMetaball")];

        base.Load();
    }

    public override void SetStaticDefaults()
    {
        instance = ModContent.GetInstance<InfinityZeroMetaball>();
    }

    internal override void Update()
    {
        foreach (var p in InfinityZeroMetaballParticle.IZParticles)
            p.Update();

        InfinityZeroMetaballParticle.IZParticles.RemoveAll(p => p.Size <= 0.05f);
    }

    internal override void PreDrawCallContent()
    {
        foreach (var p in InfinityZeroMetaballParticle.IZParticles)
            p.Draw();
    }

    internal override void ClearInstances()
    {
        InfinityZeroMetaballParticle.IZParticles.Clear();
    }
}

public class InfinityZeroMetaballParticle
{
    internal static List<InfinityZeroMetaballParticle> IZParticles { get; private set; } = [];

    public float Size;

    public Vector2 Velocity;

    public Vector2 Center;

    public byte Layer;

    public float SpeedDecay;

    public float SizeDecay;

    private InfinityZeroMetaballParticle(Vector2 center, Vector2 velocity, float size, byte layer, float speedDecay, float sizeDecay)
    {
        Center = center;
        Velocity = velocity;
        Size = size / 60f;
        Layer = layer;
        SpeedDecay = speedDecay;
        SizeDecay = sizeDecay;
    }

    public static void SpawnParticle(Vector2 center, Vector2 velocity, float size, byte layer = 1, float speedDecay = 0.99f, float sizeDecay = 0.9f)
    {
        IZParticles.Add(new(center, velocity, size, layer, speedDecay, sizeDecay));
    }

    public void Update()
    {
        Velocity *= 0.99f;
        Size *= 0.9f;
        Center += Velocity;
    }

    public void Draw()
    {
        InfinityZeroMetaball.Instance.AddMetaballFill(new(MetaballSystem.Circle.Value, Center - Main.screenPosition, null, 0f, MetaballSystem.Circle.Size() * 0.5f, Size, 0), Layer);
    }
}

