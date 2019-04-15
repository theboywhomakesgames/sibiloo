Shader "MamatosenLight/light"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_SRef ("Stencil Buffer Ref", float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _SComp("Stencil Comp Func", float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _SOp("stencil Op", float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _SBlendMode1("blend src", float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _SBlendMode2("blend dst", float) = 0

		[Toggle] _ZWrite ("Zwrite", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-100" }
        LOD 200

		ZWrite [_ZWrite]

		Blend [_SBlendMode1] [_SBlendMode2]

		Stencil{
			Ref [_SRef]
			Comp [_SComp]
			Pass [_SOp]
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            o.Albedo = _Color.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
