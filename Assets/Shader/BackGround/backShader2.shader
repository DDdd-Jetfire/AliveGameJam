Shader "Custom/backShader2"
{
    Properties
    {
        _Battery("Battery", Range(0,1)) = 1.0
        _TimeScale("Time Scale", Float) = 1.0
        _MainTex("Main Texture (RGB)", 2D) = "white" {}
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

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
                float2 screenUV : TEXCOORD1;
            };

            float _Battery;
            float _TimeScale;
            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * 2.0 - 1.0;
                o.uv.x *= _ScreenParams.x / _ScreenParams.y; // 保持宽高比
                return o;
            }

            float sun(float2 uv, float battery)
            {
                float val = smoothstep(0.3, 0.29, length(uv));
                float bloom = smoothstep(0.7, 0.0, length(uv));
                float cut = 3.0 * sin((uv.y + _Time.y * 0.2 * (battery + 0.02)) * 100.0)
                            + clamp(uv.y * 14.0 + 1.0, -6.0, 6.0);
                cut = clamp(cut, 0.0, 1.0);
                return clamp(val * cut, 0.0, 1.0) + bloom * 0.6;
            }

            float grid(float2 uv, float battery)
            {
                float2 size = float2(uv.y, uv.y * uv.y * 0.2) * 0.01;
                uv += float2(0.0, _Time.y * 4.0 * (battery + 0.05));
                uv = abs(frac(uv) - 0.5);
                float2 lines = smoothstep(size, float2(0.0,0.0), uv);
                lines += smoothstep(size * 5.0, float2(0.0,0.0), uv) * 0.4 * battery;
                return clamp(lines.x + lines.y, 0.0, 3.0);
            }

            float sdCloud(float2 p, float2 a1, float2 b1, float2 a2, float2 b2, float w)
            {
                float totalLength = 2.0;
                float2 dir = normalize(b1 - a1);
                float2 normal = float2(-dir.y, dir.x);
                float localX = dot(p - a1, dir);
                float localY = dot(p - a1, normal);

                float amplitude = distance(a1, a2) * 0.5;
                float baseFrequency = 2.0 * 3.1415926 / totalLength * 2.0;
                float waveY = amplitude * sin(baseFrequency * localX - _Time.y * 2.0);

                float dist = abs(localY - waveY) - w + 0.05;
                dist = max(dist, -localX);
                dist = max(dist, localX - totalLength);
                return dist;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float battery = _Battery;
                float fog = smoothstep(0.1, -0.02, abs(uv.y + 0.2));
                float3 col = float3(0.0, 0.1, 0.2);
                fixed4 texColor = tex2D(_MainTex, i.uv);

                if (uv.y < -0.2)
                {
                    uv.y = 3.0 / (abs(uv.y + 0.2) + 0.05);
                    uv.x *= uv.y * 1.0;
                    float gridVal = grid(uv, battery);

                    col = lerp(col, float3(1.0, 1.0, 1.0), gridVal);

                    float gradient = smoothstep(0.0, 1.0, uv.y * 0.1);
                    float3 gradientColor = lerp(
                        float3(1.0, 0.5, 0.8),
                        float3(0.8, 0.2, 1.0),
                        gradient
                    );
                    col = lerp(gradientColor, col, gridVal * 0.7);
                }
                else
                {
                    float fujiD = min(uv.y * 4.5 - 0.5, 1.0);
                    uv.y -= battery * 1.1 - 0.51;

             
                    float2 sunUV = uv;
                    sunUV += float2(0.0, 0.2);
                    col += float3(0.992, 0.824, 0.486);
                    float sunVal = sun(sunUV, battery);

                    col = lerp(col, float3(1.0, 0.624, 0.486), sunUV.y * 2.0 + 0.2);
                    

                    // 提取RGB通道
                    float3 albedo = texColor.rgb;

                    col = lerp(albedo, col, sunVal);

                    // 云层
                    float2 cloudUV = uv;
                    cloudUV.x = fmod(cloudUV.x + _Time.y * 0.1, 4.0) - 2.0;
                    float cloudTime = _Time.y * 0.5;
                    float cloudY = -0.5;

                    float3 cloudCol1 = float3(1.0, 0.886, 0.584);
                    float3 cloudCol2 = float3(1.0, 0.886, 0.584);
                    float cloudVal1 = sdCloud(cloudUV * 6.0 + float2(0.0,0.4),
                                         float2(0.1 + sin(cloudTime + 140.5) * 0.1,cloudY),
                                         float2(1.05 + cos(cloudTime * 0.9 - 36.56) * 0.1, cloudY),
                                         float2(0.2 + cos(cloudTime * 0.867 + 387.165) * 0.1,0.25 + cloudY),
                                         float2(0.5 + cos(cloudTime * 0.9675 - 15.162) * 0.09, 0.25 + cloudY), 0.075);
                    cloudY = -0.6;
                    float cloudVal2 = sdCloud(cloudUV * 4.0 + float2(3.0,-0.6),
                                          float2(0.1 + sin(cloudTime + 140.5) * 0.1,cloudY),
                                         float2(1.05 + cos(cloudTime * 0.9 - 36.56) * 0.1, cloudY),
                                         float2(0.2 + cos(cloudTime * 0.867 + 387.165) * 0.1,0.25 + cloudY),
                                         float2(0.5 + cos(cloudTime * 0.9675 - 15.162) * 0.09, 0.25 + cloudY), 0.075);

                    float cloudFill1 = 1.0 - smoothstep(0.0, 0.075, cloudVal1);
                    float cloudFill2 = 1.0 - smoothstep(0.0, 0.075, cloudVal2);
                    col = lerp(col, cloudCol1, cloudFill1);
                    col = lerp(col, cloudCol2, cloudFill2);
                }

                col += fog * fog * fog;
                col = lerp(float3(col.r, col.r, col.r) * 0.5, col, battery * 0.7);

                return float4(col, 1.0);
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}