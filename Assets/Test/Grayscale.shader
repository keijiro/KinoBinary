Shader "Grayscale"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            #pragma multi_compile _ UNITY_COLORSPACE_GAMMA

            #include "UnityCG.cginc"

            float2 Vertex(
                float4 position : POSITION,
                float2 texcoord : TEXCOORD,
                out float4 outPosition : SV_Position
            ) : TEXCOORD
            {
                outPosition = UnityObjectToClipPos(position);
                return texcoord;
            }

            half4 Fragment(float2 texcoord : TEXCOORD) : SV_Target
            {
            #ifdef UNITY_COLORSPACE_GAMMA
                return texcoord.x;
            #else
                return GammaToLinearSpace(texcoord.x).x;
            #endif
            }

            ENDCG
        }
    }
}
