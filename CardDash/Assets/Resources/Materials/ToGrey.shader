Shader "Image/ToGrey"
{
    Properties
    {
		_Color("Tint", Color) = (1,1,1,1)
        _MainTex ("Textures", 2D) = "white" {}
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

		Stencil
		{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
		}
		ColorMask[_ColorMask]
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "UnityUI.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
			    float4 color    : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
				float4 color    : COLOR;
                float4 vertex : SV_POSITION;
            };

			fixed4 _Color;
			fixed4 _ClipRect;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.color = v.color * _Color;
				o.worldPosition = v.vertex;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv)*i.color;
				clip(col.a - 0.001);
				float grey = dot(col.rgb, fixed3(0.22, 0.707, 0.071));
				return half4(grey, grey, grey, col.a);
				return col;
            }
            ENDCG
        }
    }
}
