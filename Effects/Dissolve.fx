sampler baseTexture : register(s0);
sampler noiseTexture : register(s1);

float noiseScale;
float dissolveIntensity;
float2 sampleOffset;
float4 transitionColor;
float transitionOffset;

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR
{
    float4 color = tex2D(baseTexture, coords) * sampleColor;
    
    // Sample noise texture for dithering
    float noise = tex2D(noiseTexture, (coords * noiseScale) + sampleOffset).r;
    
    if (dissolveIntensity > noise)
    {
        if (dissolveIntensity - transitionOffset < noise)
        {
            float a = color.a;
            color = transitionColor;
            color.a = a;
        }
        else
            color.a = 0;
    }
        
    return color;
}

technique Technique1
{
    pass DissolvePass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}