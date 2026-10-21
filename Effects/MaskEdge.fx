sampler baseTexture : register(s0);
float2 screenSize;
float edgeWidth; // number of pixels to spread in each direction

struct PixelInput
{
    float4 color : COLOR0;
    float2 texCoords : TEXCOORD0;
};

float4 PixelShaderFunction(PixelInput input) : COLOR0
{
    float2 coords = input.texCoords;
    
    float2 texelSize = 1.0 / screenSize;
    
    float maskCenter = tex2D(baseTexture, coords).r;
    
    // Sample the furthest neighbors in each direction to determine spread
    float maskUp = tex2D(baseTexture, coords + float2(0, -texelSize.y * edgeWidth)).r;
    float maskDown = tex2D(baseTexture, coords + float2(0, texelSize.y * edgeWidth)).r;
    float maskLeft = tex2D(baseTexture, coords + float2(-texelSize.x * edgeWidth, 0)).r;
    float maskRight = tex2D(baseTexture, coords + float2(texelSize.x * edgeWidth, 0)).r;
    
    // A pixel is within the edge band if neighbors disagree with each other
    float maxNeighbor = max(max(maskUp, maskDown), max(maskLeft, maskRight));
    float minNeighbor = min(min(maskUp, maskDown), min(maskLeft, maskRight));
    float edge = step(0.5, maxNeighbor) * (1.0 - step(0.5, minNeighbor));
    
    return input.color * edge;
}

technique Technique1
{
    pass EdgePass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}