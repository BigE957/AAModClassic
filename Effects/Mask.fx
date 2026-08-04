sampler TextureSampler : register(s0);
sampler noise : register(s1);
float2 offset;
float2 noiseScale = (1, 1);

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 vertexColor : COLOR0) : COLOR0
{
    // Sample the texture
    float4 textureColor = tex2D(TextureSampler, coords);
    if(textureColor.r == 0)
        return float4(0, 0, 0, 0);
    
    float4 finalColor = tex2D(noise, coords * noiseScale + offset);
    return finalColor * vertexColor;
}

technique Greyscale
{
    pass AutoloadPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}