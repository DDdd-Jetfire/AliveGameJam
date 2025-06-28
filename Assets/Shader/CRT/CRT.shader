Shader "Custom/CRTScreenEffect"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Distortion ("Distortion", Float) = 0.1
        _ScanlineIntensity ("Scanline Intensity", Float) = 0.2
        _ScanlineCount ("Scanline Count", Float) = 240
        _ColorOffset ("Color Offset", Float) = 1.0
        _Speed("Speed",Float) = 1.0
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Distortion;
            float _ScanlineIntensity;
            float _ScanlineCount;
            float _ColorOffset;
            float _Speed;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                // Barrel distortion
                float2 centered = uv - 0.5;
                float r = dot(centered, centered);
                uv = uv + centered * r * _Distortion;
                uv = clamp(uv, 0.001, 0.999);
                // RGB Offset (Chromatic Aberration)
                float2 offset = float2(_ColorOffset / _ScreenParams.x, 0);
                float rCol = tex2D(_MainTex, uv + offset).r;
                float gCol = tex2D(_MainTex, uv).g;
                float bCol = tex2D(_MainTex, uv - offset).b;

                float3 color = float3(rCol, gCol, bCol);

                // Scanlines
                float scanline = sin(uv.y * _ScanlineCount * 3.14159 + _Speed * _Time.y);
                color *= 1.0 - _ScanlineIntensity * (0.5 * scanline + 0.5);

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}
