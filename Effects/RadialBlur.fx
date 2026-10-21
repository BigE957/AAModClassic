sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{    
    float2 pixelCoords = coords;
    
    float2 dir = uDirection - pixelCoords;
    float2 blur = dir * uIntensity * 0.005;
    float4 baseColor = 0;
    float2 sampleUV = coords;
    
    for (int i = 0; i < uSaturation; i++)
    {
        baseColor += tex2D(uImage0, sampleUV);
        sampleUV += blur;
        
        if (i > 20)
            break;
    }
    
    return baseColor / uSaturation;
}

technique Technique1
{
    pass RadialBlurPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}