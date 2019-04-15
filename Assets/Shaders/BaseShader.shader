Shader "Custom/BaseShader"
{
	Properties
	{
		_Color("albedo color", Color) = (1, 1, 1, 1)

		_StencilRef("stencil ref number", float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _SCompare("stencil compare function", float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _SOp("stencil operation", float) = 0

		[Toggle] _Mask("z mask activation", float) = 0
	}
	SubShader
	{
		Stencil{
			Ref[_StencilRef]
			Comp[_SCompare]
			Pass[_SOp]
		}

		ZWrite [_Mask]
		ColorMask [_Mask]

		CGPROGRAM

		#pragma surface surf Lambert

		sampler2D _myDiffuse;
		float4 _Color;

		struct Input {
			float2 uv_myDiffuse;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
		}

		ENDCG
    }
    FallBack "Diffuse"
}
