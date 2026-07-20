#nullable enable

using AAModClassic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AAModClassic.Utilities;

internal struct SpriteBatchParameters
{
    public SpriteSortMode? SortMode { get; set; }
    public BlendState? BlendState { get; set; }
    public SamplerState? SamplerState { get; set; }
    public DepthStencilState? DepthStencilState { get; set; }
    public RasterizerState? RasterizerState { get; set; }
    public Effect? CustomEffect { get; set; }
    public Matrix? TransformMatrix { get; set; }

    public SpriteBatchParameters(
        SpriteSortMode? sortMode = null,
        BlendState? blendState = null,
        SamplerState? samplerState = null,
        DepthStencilState? depthStencilState = null,
        RasterizerState? rasterizerState = null,
        Effect? customEffect = null,
        Matrix? transformMatrix = null
    )
    {
        SortMode = sortMode;
        BlendState = blendState;
        SamplerState = samplerState;
        DepthStencilState = depthStencilState;
        RasterizerState = rasterizerState;
        CustomEffect = customEffect;
        TransformMatrix = transformMatrix;
    }

    public readonly SpriteBatchSnapshot ToSnapshot(SpriteBatchSnapshot defaultValues)
    {
        return new SpriteBatchSnapshot(
            SortMode ?? defaultValues.SortMode,
            BlendState ?? defaultValues.BlendState,
            SamplerState ?? defaultValues.SamplerState,
            DepthStencilState ?? defaultValues.DepthStencilState,
            RasterizerState ?? defaultValues.RasterizerState,
            CustomEffect ?? defaultValues.CustomEffect,
            TransformMatrix ?? defaultValues.TransformMatrix
        );
    }
}

internal struct SpriteBatchSnapshot
{
    public SpriteSortMode SortMode { get; set; }
    public BlendState BlendState { get; set; }
    public SamplerState SamplerState { get; set; }
    public DepthStencilState DepthStencilState { get; set; }
    public RasterizerState RasterizerState { get; set; }
    public Effect? CustomEffect { get; set; }
    public Matrix TransformMatrix { get; set; }

    public SpriteBatchSnapshot(
        SpriteSortMode sortMode,
        BlendState blendState,
        SamplerState samplerState,
        DepthStencilState depthStencilState,
        RasterizerState rasterizerState,
        Effect? customEffect,
        Matrix transformMatrix
    )
    {
        SortMode = sortMode;
        BlendState = blendState;
        SamplerState = samplerState;
        DepthStencilState = depthStencilState;
        RasterizerState = rasterizerState;
        CustomEffect = customEffect;
        TransformMatrix = transformMatrix;
    }

    private static readonly Func<SpriteBatch, SpriteSortMode> GetSortMode = CreateFieldGetter<SpriteSortMode>("sortMode");
    private static readonly Func<SpriteBatch, BlendState> GetBlendState = CreateFieldGetter<BlendState>("blendState");
    private static readonly Func<SpriteBatch, SamplerState> GetSamplerState = CreateFieldGetter<SamplerState>("samplerState");
    private static readonly Func<SpriteBatch, DepthStencilState> GetDepthStencilState = CreateFieldGetter<DepthStencilState>("depthStencilState");
    private static readonly Func<SpriteBatch, RasterizerState> GetRasterizerState = CreateFieldGetter<RasterizerState>("rasterizerState");
    private static readonly Func<SpriteBatch, Effect?> GetCustomEffect = CreateFieldGetter<Effect?>("customEffect");
    private static readonly Func<SpriteBatch, Matrix> GetTransformMatrix = CreateFieldGetter<Matrix>("transformMatrix");

    private static Func<SpriteBatch, T> CreateFieldGetter<T>(string fieldName)
    {
        FieldInfo field = typeof(SpriteBatch).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new MissingFieldException(nameof(SpriteBatch), fieldName);

        ParameterExpression instance = Expression.Parameter(typeof(SpriteBatch), "sb");
        MemberExpression fieldAccess = Expression.Field(instance, field);
        return Expression.Lambda<Func<SpriteBatch, T>>(fieldAccess, instance).Compile();
    }

    public SpriteBatchSnapshot(SpriteBatch spriteBatch)
    {
        SortMode = GetSortMode(spriteBatch);
        BlendState = GetBlendState(spriteBatch);
        SamplerState = GetSamplerState(spriteBatch);
        DepthStencilState = GetDepthStencilState(spriteBatch);
        RasterizerState = GetRasterizerState(spriteBatch);
        CustomEffect = GetCustomEffect(spriteBatch);
        TransformMatrix = GetTransformMatrix(spriteBatch);
    }

    public readonly SpriteBatchParameters ToParameters()
    {
        return new SpriteBatchParameters(
            SortMode,
            BlendState,
            SamplerState,
            DepthStencilState,
            RasterizerState,
            CustomEffect,
            TransformMatrix
        );
    }
}

internal static class SpriteBatchSnapshotExtensions
{
    public static void End(this SpriteBatch sb, out SpriteBatchSnapshot ss)
    {
        ss = new SpriteBatchSnapshot(sb);
        sb.End();
    }

    public static void Begin(this SpriteBatch sb, in SpriteBatchSnapshot ss)
    {
        sb.Begin(
            ss.SortMode,
            ss.BlendState,
            ss.SamplerState,
            ss.DepthStencilState,
            ss.RasterizerState,
            ss.CustomEffect,
            ss.TransformMatrix
        );
    }

    public static void Restart(this SpriteBatch sb, in SpriteBatchSnapshot ss)
    {
        sb.End();
        sb.Begin(ss);
    }
}