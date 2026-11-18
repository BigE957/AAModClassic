sampler2D Bloom : register(s0);
sampler2D Target : register(s1);

float2 Position;
float2 Size;
float Threshold;
float Ceiling;
float BaseDarken;
float Opacity;

float3 ShadowTint;
float TintStrength;
float ChromaBoost;

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR
{
    float g = tex2D(Bloom, coords).a;
    float3 scene = tex2D(Target, Position + coords * Size).rgb;
    float3 lumaWeights = float3(0.299, 0.587, 0.114);
    float luma = dot(scene, lumaWeights);

    float ceiling = lerp(1.0, Ceiling, g);
    float headroom = max(ceiling - Threshold, 0.0);
    float over = max(luma - Threshold, 0.0);
    float newLuma = luma - over * (1.0 - headroom / (1.0 - Threshold));

    float factor = newLuma / max(luma, 0.001);
    factor *= 1.0 - BaseDarken * g;

    float dark = saturate(1.0 - factor);

    float3 ratio = max(scene, 0.02) / max(luma, 0.02);
    float3 boost = pow(ratio, ChromaBoost * dark);
    float boostedLuma = dot(scene * boost, lumaWeights);
    boost *= luma / max(boostedLuma, 0.001);

    float3 tint = lerp(float3(1.0, 1.0, 1.0), ShadowTint, saturate(dark * TintStrength));

    float3 rgb = saturate(factor * boost * tint);
    rgb = lerp(float3(1.0, 1.0, 1.0), rgb, Opacity);

    return float4(rgb, 1.0);
}

technique DialogueBloom
{
    pass AutoloadPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}