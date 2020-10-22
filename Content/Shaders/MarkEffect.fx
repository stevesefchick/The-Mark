sampler TextureSampler : register(s0);

float4 main(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    return float4(0.5, 0.5, 0.5, 0.5);
}


technique Desaturate
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 main();
    }
}
