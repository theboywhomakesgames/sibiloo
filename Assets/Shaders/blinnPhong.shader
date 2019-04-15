Shader "Custom/blinnPhong"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Glossiness("Gloss", Range(0,1)) = 0.5
		_Spec("spec", Range(0,1)) = 0.5
	}

	SubShader
	{

		CGPROGRAM

			#pragma surface surf BlinnPhong

			float4 _Color;
			fixed _Glossiness;
			half _Spec;

			struct Input{
				float2 uv_MainTex;
			};

			void surf (Input IN, inout SurfaceOutput o) {
				o.Albedo = _Color.rgb;
				o.Specular = _Spec;
				o.Gloss = _Glossiness;
			}

        ENDCG
    }
    FallBack "Diffuse"
}
