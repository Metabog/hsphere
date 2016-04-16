
//////////////////////////////////////////////////////////////////////////////////////////
//																						//
// Flashback '94 Shader Pack for Unity 3D												//
// © 2015 George Khandaker-Kokoris														//
//																						//
// Cubemap unlit shader with vertex snapping and affine texture mapping					//
// Based on a Cg implementation of Unity's built-in VertexLit shader					//
// http://wiki.unity3d.com/index.php/CGVertexLit										//
//																						//
//////////////////////////////////////////////////////////////////////////////////////////

Shader "Flashback 94/Object Shader/Cubemap Unlit" {
	Properties {
		_Snapping ("Vertex Snapping", Range (1, 100)) = 25
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Cube ("Reflection Map", Cube) = "" {}
		_Reflect ("Reflection Strength", Range (0, 1)) = 0.25
	}
	 
	SubShader {
		Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }
		
		LOD 100
		ZWrite On
	 
		Pass {
			Tags { "LightMode" = "Vertex" }
			
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			half _Snapping;
			fixed4 _Color;
			half _Reflect;
	 
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			samplerCUBE _Cube;
	 
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float3 normalDir : TEXCOORD2;
				float3 viewDir : TEXCOORD3;
				float persp : TEXCOORD4;
			};
	 
			v2f vert (appdata_full v)
			{
			    v2f o;
			    
			    // Snap vertex to position based on snapping amount
			    half snapValue = _Snapping / 1000;
			    float4 realpos = mul (UNITY_MATRIX_MVP, v.vertex);
			    float4 steps = floor((realpos / snapValue) + 0.5);
			    o.pos = steps * snapValue;
			    
			    // Save position to another float to undo perspective correction
			    o.persp = o.pos.w;
			    o.uv_MainTex = TRANSFORM_TEX (v.texcoord, _MainTex) * o.persp;
				
				o.viewDir = mul(_Object2World, v.vertex).xyz - _WorldSpaceCameraPos;
				o.normalDir = normalize(mul(float4(v.normal, 0), _World2Object).xyz);
	 
				return o;
			}
	 
			fixed4 frag (v2f i) : COLOR {
				fixed4 c;
				fixed4 mainTex = tex2D (_MainTex, i.uv_MainTex / i.persp);
				float3 reflectedDir = reflect(i.viewDir, normalize(i.normalDir));
				
				c.rgb = ((mainTex.rgb * _Color.rgb) + (texCUBE(_Cube, reflectedDir).rgb * _Reflect));
				c.a = 1;
	 
				return c;
			}
	 
			ENDCG
		}
	}
	 
	Fallback "VertexLit"
}
