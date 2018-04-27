Shader "Custom/Toon Shader With Outline" {

	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
		//_Cuts ("Cuts", Int) = 1
		//_Smoothing ("Smoothing", Float) = 0.025
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_OutlineSize ("OutlineSize", range (0, 10)) = 0.02
	}

    SubShader {
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}
		LOD 200

		// CEL SHADING
		CGPROGRAM
		#pragma surface surf CelShadingForward
		#pragma target 3.0

		//float _Cuts;
		float _Smoothing;

		half4 LightingCelShadingForward (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			if (NdotL <= 0) {
				NdotL = 0;
			} else if (NdotL <= 0.33) {
				NdotL = 0.33;
			} else if (NdotL <= 0.66) {
				NdotL = 0.66;
			} else {
				NdotL = 1;
			}
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
			c.a = s.Alpha;
			return c;
		}

		half4 _Color;

		struct Input {
			float4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG

		// OUTLINE
		Pass
		{
			Cull Front
			Blend One Zero
			ZWrite On
			ZTest LEqual

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float4 _Color;
			float4 _OutlineColor;
			float _OutlineSize;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
			};
			
			v2f vert (appdata IN)
			{
				v2f OUT;
				IN.vertex.xyz += normalize(IN.normal.xyz) * _OutlineSize;
				OUT.position = UnityObjectToClipPos(IN.vertex);
				return OUT;
			}
			
			fixed4 frag (v2f IN) : SV_Target
			{
				fixed4 col = _OutlineColor;
				return col;
			}
			ENDCG
		}
    }

   	Fallback "diffuse"
}
