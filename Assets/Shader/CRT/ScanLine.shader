Shader "Unlit/ScanLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Direction("Direction", Vector) = (1.0,1.0,1.0,1.0) 
        _Speed("Speed",Float) = 10
        _ScanLineAmount("ScanLine Amount",Range(0,10)) = 5
        _Power("Power",Float) = 5
        _Color("Color",Color) = (0,1,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 positionOS : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float3 _Direction;
            float _Speed;
            float _ScanLineAmount;
            float _Power;
            float4 _Color;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.positionOS = v.vertex;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float value = dot(i.positionOS.xyz,_Direction);
                float value2 = value + _Time.x * _Speed;
                float value3 = value2 * _ScanLineAmount;
                float value4 = pow(frac(value3),_Power);
                float4 color = _Color * value4;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return color;
            }
            ENDCG
        }
    }
}
