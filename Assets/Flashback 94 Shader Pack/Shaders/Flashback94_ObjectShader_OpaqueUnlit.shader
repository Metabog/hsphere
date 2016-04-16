
//////////////////////////////////////////////////////////////////////////////////////////
//																						//
// Flashback '94 Shader Pack for Unity 3D												//
// © 2015 George Khandaker-Kokoris														//
//																						//
// Opaque unlit shader with vertex snapping and affine texture mapping					//
// Based on a Cg implementation of Unity's built-in VertexLit shader					//
// http://wiki.unity3d.com/index.php/CGVertexLit										//
//																						//
//////////////////////////////////////////////////////////////////////////////////////////

Shader "Flashback 94/Object Shader/Opaque Unlit" {
	Properties {
		_Snapping ("Vertex Snapping", Range (1, 100)) = 25
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
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
	 
			sampler2D _MainTex;
			float4 _MainTex_ST;
	 
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float persp : TEXCOORD2;
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
	 
				return o;
			}
	 
			fixed4 frag (v2f i) : COLOR {
				fixed4 c;
				fixed4 mainTex = tex2D (_MainTex, i.uv_MainTex / i.persp);
				
				c.rgb = mainTex.rgb * _Color.rgb;
				c.a = 1;
	 
				return c;
			}
	 
			ENDCG
		}
	}
	 
	Fallback "VertexLit"
}
