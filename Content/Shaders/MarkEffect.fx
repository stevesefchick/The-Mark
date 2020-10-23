sampler TextureSampler : register(s0);
float blueinfluence;
float redinfluence;
float greeninfluence;

float4 main(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(TextureSampler, texCoord);
    color.b += blueinfluence;
    color.r += redinfluence;
    color.g += greeninfluence;
    return color;
}

technique Desaturate
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 main();
    }
}
