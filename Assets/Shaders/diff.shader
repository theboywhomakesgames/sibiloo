Shader "myShaders/diff"
{
    Properties
    {
		_MainTex ("texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }

		Blend DstColor Zero

		Cull Off

		Pass {
			SetTexture [_MainTex] { combine texture }
		}
    }
    FallBack "Diffuse"
}
