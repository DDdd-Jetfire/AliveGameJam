Shader "Custom/VideoChromaKeyTransparent"
{
    Properties
    {
        _VideoTex("Video Texture", 2D) = "black" {}
        _KeyColor("Key Color", Color) = (0,1,0,1)
        _Threshold("Threshold", Range(0,1)) = 0.1
        _Smoothness("Smoothness", Range(0,1)) = 0.1
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _VideoTex;
                float4 _VideoTex_ST;
                float3 _KeyColor;
                float _Threshold;
                float _Smoothness;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _VideoTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // ����Ƶ�������
                    fixed4 videoColor = tex2D(_VideoTex, i.uv);

                    // ������ɫ����
                    float colorDist = distance(videoColor.rgb, _KeyColor);

                    // ����alphaֵ
                    float alpha = smoothstep(_Threshold - _Smoothness,
                                          _Threshold + _Smoothness,
                                          colorDist);

                // �����ɫ��������ƵRGB�ͼ������alpha
                return fixed4(videoColor.rgb, alpha);
            }
            ENDCG
        }
        }
            FallBack "Transparent/Diffuse"
}