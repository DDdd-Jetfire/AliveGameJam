Shader "Custom/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "HorizontalBlur"
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BlurSize;
            float4 _MainTex_TexelSize; // x = 1/width, y = 1/height

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 color = fixed4(0,0,0,0);

                // Sample horizontally
                color += tex2D(_MainTex, uv + float2(-4.0, 0.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.05;
                color += tex2D(_MainTex, uv + float2(-2.0, 0.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.09;
                color += tex2D(_MainTex, uv)                                             * 0.62;
                color += tex2D(_MainTex, uv + float2(2.0, 0.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.09;
                color += tex2D(_MainTex, uv + float2(4.0, 0.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.05;

                return color;
            }
            ENDCG
        }

        Pass
        {
            Name "VerticalBlur"
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BlurSize;
            float4 _MainTex_TexelSize;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 color = fixed4(0,0,0,0);

                // Sample vertically
                color += tex2D(_MainTex, uv + float2(0.0, -4.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.05;
                color += tex2D(_MainTex, uv + float2(0.0, -2.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.09;
                color += tex2D(_MainTex, uv)                                                * 0.62;
                color += tex2D(_MainTex, uv + float2(0.0,  2.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.09;
                color += tex2D(_MainTex, uv + float2(0.0,  4.0) * _BlurSize * _MainTex_TexelSize.xy) * 0.05;

                return color;
            }
            ENDCG
        }
    }
}
