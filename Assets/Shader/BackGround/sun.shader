Shader "Custom/sun"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Battery ("Battery", Range(0, 1)) = 1.0
        //_Speed ("Speed", Range(1, 3)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // 材质属性
            sampler2D _MainTex;
            float _Battery;
            //float Speed;
 

            // 计算太阳的光辉效果
            float sun(float2 uv, float battery)
            {
                float val = smoothstep(0.3, 0.29, length(uv)); // 计算和中心点的距离，并生成平滑的边缘
                float bloom = smoothstep(0.7, 0.0, length(uv)); // 计算太阳的辉光效果
                float cut = 3.0 * sin((uv.y + _Time * 2 * (battery + 0.02)) * 100.0) 
                          + clamp(uv.y * 14.0 + 1.0, -6.0, 6.0);
                cut = clamp(cut, 0.0, 1.0);
                return clamp(val * cut, 0.0, 1.0) + bloom * 0.6; // 返回经过调节的太阳值
            }

            // 顶点着色器
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // 片段着色器
            half4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv * 2.0 - 1.0; // 归一化坐标，将 uv 转换到 [-1, 1]
                // 假设你的目标是使材质保持在正方形的比例
                //float2 iResolution = _ScreenParams.xy;
                //float2 uv = (2.0 * i.uv - iResolution.xy) / iResolution.y;
                //uv.x *= _ScreenParams.x / _ScreenParams.y; // 保持屏幕纵横比
                uv /= 2.0;
                // 调用 sun 函数来计算太阳的颜色值
                float sunVal = sun(uv, _Battery);

                // 设置太阳的颜色
                half3 col = half3(1.0, 0.2, 1.0); // 初始太阳颜色
                col = lerp(col, half3(1.0, 0.4, 0.2), uv.y * 2.0 + 0.2);
                col = lerp(half3(0.0, 0.0, 0.0), col, sunVal);

                // 返回最终颜色
                return half4(col, 1.0);
            }

            ENDCG
        }
    }
    Fallback "Diffuse"
}