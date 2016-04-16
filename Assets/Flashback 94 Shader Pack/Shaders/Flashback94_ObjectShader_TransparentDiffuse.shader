
//////////////////////////////////////////////////////////////////////////////////////////
//																						//
// Flashback '94 Shader Pack for Unity 3D												//
// © 2015 George Khandaker-Kokoris														//
//																						//
// Transparent diffuse shader with vertex snapping and affine texture mapping			//
// Based on a Cg implementation of Unity's built-in VertexLit shader					//
// http://wiki.unity3d.com/index.php/CGVertexLit										//
//																						//
//////////////////////////////////////////////////////////////////////////////////////////

Shader "Flashback 94/Object Shader/Transparent Diffuse" {
	Properties {
		_Snapping ("Vertex Snapping", Range (1, 100)) = 25
		_Color ("Main Color", Color) = (1,1,1,1)
		_Emission ("Emissive Color", Color) = (0,0,0,0)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	 
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
	 
		Pass {
			Tags { "LightMode" = "Vertex" }
			
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			half _Snapping;
			fixed4 _Color;
			fixed4 _Emission;
	 
			sampler2D _MainTex;
			float4 _MainTex_ST;
	 
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
				fixed3 diff : COLOR;
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
	 
				float3 viewpos = mul (UNITY_MATRIX_MV, v.vertex).xyz;
	 
				o.diff = UNITY_LIGHTMODEL_AMBIENT.xyz;
	 
				// All calculations are in object space
				for (int i = 0; i < 4; i++) {
					half3 toLight = unity_LightPosition[i].xyz - viewpos.xyz * unity_LightPosition[i].w;
					half lengthSq = dot(toLight, toLight);
					half atten = 1.0 / (1.0 + lengthSq * unity_LightAtten[i].z );
	 
					fixed3 lightDirObj = mul( (float3x3)UNITY_MATRIX_T_MV, toLight);	//View => model
	 
					lightDirObj = normalize(lightDirObj);
	 
					fixed diff = max ( 0, dot (v.normal, lightDirObj) );
					o.diff += unity_LightColor[i].rgb * (diff * atten);
				}
	 
				o.diff = (o.diff * _Color + _Emission.rgb) * 2;
	 
				return o;
			}
	 
			fixed4 frag (v2f i) : COLOR {
				fixed4 c;
				fixed4 mainTex = tex2D (_MainTex, i.uv_MainTex / i.persp);
				
				c.rgb = (mainTex.rgb * i.diff);
				c.a = mainTex.a * _Color.a;
	 
				return c;
			}
	 
			ENDCG
		}
	 
		//Lightmap pass, dLDR;
		Pass {
			Tags { "LightMode" = "VertexLM" }
	 
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			half _Snapping;
	 
			struct v2f {
				float4 pos : SV_POSITION;
				float2 lmap : TEXCOORD0;
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
			    o.lmap.xy = (v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw) * o.persp;
	 
			    return o;
			 }
	 
			fixed4 frag (v2f i) : COLOR {
				fixed4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lmap.xy / i.persp);
				fixed3 lm = (8.0 * lmtex.a) * lmtex.rgb;
				return fixed4(lm, 1);
			}
	 
			ENDCG
		}
	 
		//Lightmap pass, RGBM;
		Pass {
			Tags { "LightMode" = "VertexLMRGBM" }
	 
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			half _Snapping;
	 
			struct v2f {
				float4 pos : SV_POSITION;
				float2 lmap : TEXCOORD0;
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
			    o.lmap.xy = (v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw) * o.persp;
	 
			    return o;
			 }
	 
			fixed4 frag (v2f i) : COLOR {
				fixed4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lmap.xy / i.persp);
				fixed3 lm = (8.0 * lmtex.a) * lmtex.rgb;
				return fixed4(lm, 1);
			}
	 
			ENDCG
		}
	}
	 
	Fallback "Transparent/VertexLit"
}
