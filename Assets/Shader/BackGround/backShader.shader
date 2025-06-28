Shader "Custom/backShader" {
    Properties{
        _Scale("Noise Scale", Float) = 10.0
        _Speed("Flow Speed", Float) = 1.0
        _Direction("Flow Direction", Vector) = (1, -1, 0, 0)
        _Color1("Color 1 (Pink)", Color) = (1.00, 0.78, 0.79, 1.0) // #FFC7C9
        _Color2("Color 2 (Warm Yellow)", Color) = (0.992, 0.824, 0.486, 1.0) // #FDD27C
        _Color3("Color 3 (Soft Yellow)", Color) = (1.0, 0.965, 0.651, 1.0) // #FFF6A6
        _Color4("Color 4 (Mint Green)", Color) = (0.737, 1.0, 0.765, 1.0) // #BCFFC3
        _Color5("Color 5 (Ocean Blue)", Color) = (0.596, 0.98, 0.918, 1.0) // #98FAEA
        _Color6("Color 6 (Lavender)", Color) = (0.765, 0.549, 0.902, 1.0) // #C38CE6
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float _Scale;
                float _Speed;
                float2 _Direction;
                fixed4 _Color1, _Color2, _Color3, _Color4, _Color5, _Color6;

                float rand(float2 uv) {
                    return frac(sin(dot(uv, float2(12.985489, 5.889))) * 155894.0);
                }

                float noise(float2 uv) {
                    float2 i_uv = floor(uv);
                    float2 d_uv = frac(uv);
                    d_uv = d_uv * d_uv * (3.0 - 2.0 * d_uv);

                    float shade_x_0 = lerp(rand(i_uv), rand(i_uv + float2(1.0, 0.0)), d_uv.x);
                    float shade_x_1 = lerp(rand(i_uv + float2(0.0,1.0)), rand(i_uv + float2(1.0, 1.0)), d_uv.x);
                    return lerp(shade_x_0, shade_x_1, d_uv.y) * lerp(shade_x_0, shade_x_1, d_uv.y);
                }

                float motion_noise(float2 uv, float s, float2 direction, float speed) {
                    float shade = noise(uv + _Time.y * direction * speed * s) * 30.0;
                    shade += 0.5 * noise(uv);
                    float addition_wave = noise(uv + direction * speed * s * 1.5 * (0.5 * _Time.y));
                    shade += addition_wave;
                    return 0.4 * shade;
                }

                float recursive_noise(float2 uv, float2 direction, float speed) {
                    float shade = motion_noise(uv * 0.1, 0.3, direction, speed);
                    shade = motion_noise(uv * 0.2 + shade, 0.5, direction, speed);
                    shade = motion_noise(uv * 0.2 + shade, 0.2, direction, speed);
                    return shade;
                }

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float col = recursive_noise(i.uv * _Scale, _Direction, _Speed);
                    col = smoothstep(0.0, 1.0, (sin(col) + 1.0) / 2.0);

                    if (col <= 1.0 && col > 0.99) return _Color1;
                    else if (col <= 0.99 && col > 0.8) return _Color2;
                    else if (col <= 0.8 && col > 0.45) return _Color3;
                    else if (col <= 0.45 && col > 0.3) return _Color4;
                    else if (col <= 0.3 && col > 0.15) return _Color5;
                    else if (col <= 0.15 && col >= 0.0) return _Color6;
                    else return fixed4(col, 1.0, 1.0, 1.0);
                }
                ENDCG
            }
    }
        FallBack "Diffuse"
}