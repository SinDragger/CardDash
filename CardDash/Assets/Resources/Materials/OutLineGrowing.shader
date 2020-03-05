Shader "Image/OutLine"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		_MainTex("Textures", 2D) = "white" {}
		_EdgeAlphaThreshold("Edge Alpha Threshold", Float) = 1.0					//边界透明度和的阈值
		_LeftFix("Left Fix", Float) = 1.0					//左侧的偏移
		_LeftFixColor("Left Fix Color", Color) = (0,0,0,1)					//左侧的偏移颜色
		_RightFix("Right Fix", Float) = 1.0					//左侧的偏移
		_RightFixColor("Right Fix Color", Color) = (0,0,0,1)					//左侧的偏移颜色
		_UpFix("Up Fix", Float) = 1.0					//左侧的偏移
		_UpFixColor("Up Fix Color", Color) = (0,0,0,1)					//左侧的偏移颜色
		_DownFix("Down Fix", Float) = 1.0					//左侧的偏移
		_DownFixColor("Down Fix Color", Color) = (0,0,0,1)					//左侧的偏移颜色
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			sampler2D  _MainTex;
			half4 _MainTex_TexelSize;
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

			float _LeftFix;	
			fixed4 _LeftFixColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv=v.uv+ _MainTex_TexelSize.xy * half2(_LeftFix, 0);
				return o;
			}



			fixed4 frag(v2f i) : SV_Target
			{
					half4 col = tex2D(_MainTex, i.uv);
					return half4(_LeftFixColor.x, _LeftFixColor.y, _LeftFixColor.z, col.a);
			}
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			sampler2D  _MainTex;
			half4 _MainTex_TexelSize;
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

			float _RightFix;
			fixed4 _RightFixColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv + _MainTex_TexelSize.xy * half2(-_RightFix, 0);
				return o;
			}



			fixed4 frag(v2f i) : SV_Target
			{
					half4 col = tex2D(_MainTex, i.uv);
					return half4(_RightFixColor.x, _RightFixColor.y, _RightFixColor.z, col.a);
			}
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			sampler2D  _MainTex;
			half4 _MainTex_TexelSize;
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

			float _UpFix;
			fixed4 _UpFixColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv + _MainTex_TexelSize.xy * half2(0, -_UpFix);
				return o;
			}



			fixed4 frag(v2f i) : SV_Target
			{
					half4 col = tex2D(_MainTex, i.uv);
					return half4(_UpFixColor.x, _UpFixColor.y, _UpFixColor.z, col.a);
			}
			ENDCG
		}

				Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"
				sampler2D  _MainTex;
				half4 _MainTex_TexelSize;
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

				float _DownFix;
				fixed4 _DownFixColor;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv + _MainTex_TexelSize.xy * half2(0, _DownFix);
					return o;
				}



				fixed4 frag(v2f i) : SV_Target
				{
						half4 col = tex2D(_MainTex, i.uv);
						return half4(_DownFixColor.x, _DownFixColor.y, _DownFixColor.z, col.a);
				}
				ENDCG
			}


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			sampler2D  _MainTex;
			half4 _MainTex_TexelSize;
			fixed _EdgeAlphaThreshold;
			fixed4 _EdgeColor;
			float _EdgeDampRate;
			float _OriginAlphaThreshold;
			struct appdata
			{
				float4 vertex : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				//float2 uvs : TEXCOORD0;
				float2 uv[9] : TEXCOORD0;
				//float4 worldPosition : TEXCOORD1;
				float4 color    : COLOR;
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;
			fixed4 _ClipRect;

			half CalculateAlphaSumAround(v2f i)
			{
				half texAlpha;
				half alphaSum = 0;
				bool a = false;
				for (int it = 0; it < 9; it++)
				{
					texAlpha = tex2D(_MainTex, i.uv[it]).a;
					if (texAlpha > 0.1)a = true;
					alphaSum += texAlpha;
				}
				if (a) return alphaSum;

				return -1;
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uvs = v.uv;
				half2 uv = v.uv;
				o.uv[0] = uv + _MainTex_TexelSize.xy * half2(-1, -1);
				o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0, -1);
				o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1, -1);
				o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1, 0);
				o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0, 0);
				o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1, 0);
				o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1, 1);
				o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0, 1);
				o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1, 1);
				o.color = v.color * _Color;
				//o.worldPosition = v.vertex;
				return o;
			}



			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D(_MainTex, i.uv[4]);

			}
			ENDCG
		}
	}
}


//
//
//
//
//{
//	Properties
//	{
//		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
//		 _Color("Tint", Color) = (1, 1, 1, 1)
//
//		_StencilComp("Stencil Comparison", Float) = 8
//		_Stencil("Stencil ID", Float) = 0
//		_StencilOp("Stencil Operation", Float) = 0
//		_StencilWriteMask("Stencil Write Mask", Float) = 255
//		_StencilReadMask("Stencil Read Mask", Float) = 255
//
//		_ColorMask("Color Mask", Float) = 15
//		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
//}
//
//	SubShader
//{
//	   Tags
//					   {
//						   "Queue" = "Transparent"
//						   "IgnoreProjector" = "True"
//						   "RenderType" = "Transparent"
//						   "PreviewType" = "Plane"
//						   "CanUseSpriteAtlas" = "True"
//					   }
//
//				   Stencil
//				   {
//					   Ref[_Stencil]
//					   Comp[_StencilComp]
//					   Pass[_StencilOp]
//						   ReadMask[_StencilReadMask]
//						   WriteMask[_StencilWriteMask]
//					   }
//
//				   Cull Off
//				   Lighting Off
//				   ZWrite Off
//					   ZTest[unity_GUIZTestMode]
//					   Blend SrcAlpha OneMinusSrcAlpha
//					   ColorMask[_ColorMask]
//
//					   Pass
//					   {
//						   Name "Default"
//						   CGPROGRAM
//							   #pragma vertex vert
//							   #pragma fragment frag
//							   #pragma target 2.0
//
//							   #include "UnityCG.cginc"
//							   #include "UnityUI.cginc"
//
//							   #pragma multi_compile __ UNITY_UI_CLIP_RECT
//							   #pragma multi_compile __ UNITY_UI_ALPHACLIP
//
//						half4 _MainTex_TexelSize;
//						   struct appdata_t
//							   {
//							   float4 vertex   : POSITION;
//							   float4 color    : COLOR;
//							   float2 texcoord : TEXCOORD0;
//							   UNITY_VERTEX_INPUT_INSTANCE_ID
//						   };
//							struct v2f
//							   {
//								   float4 vertex   : SV_POSITION;
//								   fixed4 color : COLOR;
//								   float2 uv[9] : TEXCOORD0;
//								   float4 worldPosition : TEXCOORD1;
//								   UNITY_VERTEX_OUTPUT_STEREO
//							   };
//
//						   sampler2D _MainTex;
//						   fixed4 _Color;
//						   fixed4 _TextureSampleAdd;
//						   float4 _ClipRect;
//						   float4 _MainTex_ST;
//							v2f vert(appdata_t v)
//							   {
//								   v2f OUT;
//								   UNITY_SETUP_INSTANCE_ID(v);
//								   UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
//								   OUT.worldPosition = v.vertex;
//								   OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
//
//								   //OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
//								   //OUT.texcoord = v.texcoord;
//								   OUT.uv[0] = v.texcoord + _MainTex_TexelSize.xy * half2(-1, -1);
//								   OUT.uv[1] = v.texcoord + _MainTex_TexelSize.xy * half2(0, -1);
//								   OUT.uv[2] = v.texcoord + _MainTex_TexelSize.xy * half2(1, -1);
//								   OUT.uv[3] = v.texcoord + _MainTex_TexelSize.xy * half2(-1, 0);
//								   OUT.uv[4] = v.texcoord + _MainTex_TexelSize.xy * half2(0, 0);
//								   OUT.uv[5] = v.texcoord + _MainTex_TexelSize.xy * half2(1, 0);
//								   OUT.uv[6] = v.texcoord + _MainTex_TexelSize.xy * half2(-1, 1);
//								   OUT.uv[7] = v.texcoord + _MainTex_TexelSize.xy * half2(0, 1);
//								   OUT.uv[8] = v.texcoord + _MainTex_TexelSize.xy * half2(1, 1);
//
//
//								   OUT.color = v.color * _Color;
//								   return OUT;
//							   }
//
//								fixed4 frag(v2f IN) : SV_Target
//							   {
//								   //half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
//								   //half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
//								   half4 color = tex2D(_MainTex, IN.uv[4]) * IN.color;
//
//									   #ifdef UNITY_UI_CLIP_RECT
//									   //color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
//								 #endif
//
//									   #ifdef UNITY_UI_ALPHACLIP
//									 //clip(color.a - 0.001);
//								  #endif
//
//							 return color;
//					   }
//			 ENDCG
//		}
//	}
//}