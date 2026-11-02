sampler baseTexture : register(s0);
sampler maskTexture : register(s1);
float2 screenSize;

struct PixelInput
{
    float4 color : COLOR0;
    float2 texCoords : TEXCOORD0;
    float2 vpos : VPOS;
};

float4 PixelShaderFunction(PixelInput input) : COLOR0
{
    float4 color = tex2D(baseTexture, input.texCoords) * input.color;
    float2 screenCoords = input.vpos / screenSize;
    float mask = tex2D(maskTexture, screenCoords).r;

    if (mask < 0.5)
        return float4(0, 0, 0, 0);

    return color;
}

technique Technique1
{
    pass MaskingPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}