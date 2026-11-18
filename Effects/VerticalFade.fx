sampler baseTexture : register(s0);
bool fadeBottom = true;
float fadeStart = 0.5;

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR
{
    float4 color = tex2D(baseTexture, coords) * sampleColor;
    float opacity = 1.0;
    
    if (fadeBottom && coords.y > fadeStart)
    {
        opacity = 1.0 - (coords.y - fadeStart) / (1.0 - fadeStart);

    }
    else if (!fadeBottom && coords.y < fadeStart)
    {
        opacity = (fadeStart - coords.y) / (1.0 - fadeStart);
    }
    
    return color * opacity;
}

technique VerticalFade
{
    pass AutoloadPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}