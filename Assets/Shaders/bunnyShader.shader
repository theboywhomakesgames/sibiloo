shader "Holistic/HelloShader"{
	Properties{
		_texture("texture", 2D) = "white" {}
		_normalMap("normal map", 2D) = "bump" {}
		_toonShadeMap("toon map", 2D) = "White" {}
		_cubeMap("cube map", CUBE) = "white"{}
		_slider("rim power", Range(0, 10)) = 0.5
		_color("rim color", COLOR) = (0.5, 0.5, 0.5, 1)
		_secondColor("second rim color", COLOR) = (0.5, 0.5, 0.5, 1)

		_StencilRef("stencil ref number", float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _SCompare("stencil compare function", float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _SOp("stencil operation", float) = 0

		[Toggle] _Mask("z mask activation", float) = 0
	}

	SubShader{

		Stencil{
			Ref[_StencilRef]
			Comp[_SCompare]
			Pass[_SOp]
		}

		Pass{
			ZWrite[_Mask]
			ColorMask[_Mask]
		}

		CGPROGRAM

			#pragma surface surf ToonShading alpha:fade

			sampler2D _texture;
			sampler2D _normalMap;
			sampler2D _toonShadeMap;
			samplerCUBE _cubeMap;
			float _slider;
			float3 _color;
			float3 _secondColor;

			half4 LightingToonShading(SurfaceOutput s, half3 lightDir, half atten) {
				half NdotL = dot(s.Normal, lightDir);
				float h = NdotL * 0.5 + 0.5;
				float2 hr = h;
				float3 ramp = tex2D(_toonShadeMap, hr);
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * ramp;
				c.a = s.Alpha;
				return c;
			}
			
			struct Input{
				float2 uv_texture;
				float2 uv_normalMap;
				float3 worldRefl; INTERNAL_DATA
				float3 worldPos;
				float3 viewDir;
			};

			void surf (Input IN, inout SurfaceOutput o){
				half rim = 1 - dot(normalize(IN.viewDir), o.Normal);
				o.Albedo = 1 - _color.rgb;
				o.Emission = rim>0.5?_color.rgb:float3(0, 0, 0);
				o.Alpha = rim > 0.5 ? 0.8 : 0.5;
			}
			
		ENDCG
	}

  Fallback "Diffuse"
}