
//////////////////////////////////////////////////////////////////////////////////////////
//																						//
// Flashback '94 Shader Pack for Unity 3D												//
// © 2015 George Khandaker-Kokoris														//
//																						//
// Image effect shader with color quantization based on number of steps per channel		//
// Only for use with the 'Flashback94_PostProcess' script								//
//																						//
//////////////////////////////////////////////////////////////////////////////////////////

Shader "Flashback 94/Image Effect/Color Quantize" {
	Properties {
		_MainTex ("Render Input", 2D) = "white" {}
		_ColorSteps ("Color Steps", Float) = 256
	}
	SubShader {
		ZTest Always Cull Off ZWrite Off Fog { Mode Off }
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float _ColorSteps;
			
			float4 frag(v2f_img IN) : COLOR
			{
				// Get color from the main texture
				half4 col = tex2D(_MainTex, IN.uv);
				
				// Create step value from the total number of steps
				half stepvalue = 1 / floor(_ColorSteps + 0.5);
				
				// Get the current stepped color level
				half4 level = floor((col / stepvalue) + 0.5);
				
				// Return the quantized color
				return level * stepvalue;
			}
			ENDCG
		}
	}
}
