Shader "Custom/ARPlaneShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
      Tags { "Queue"="Transparent" "RenderType" = "Transparent" }

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
       #include "UnityCG.cginc"

      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv   : TEXCOORD0;
      };

      struct v2f
      {
        float4 position : SV_POSITION;
        float2 uv     : TEXCOORD0;
      };

      v2f vert (appdata input)
      {
        v2f output;
        output.position = UnityObjectToClipPos(input.vertex);
        output.uv = input.uv;
        return output;
      }

      fixed4 frag (v2f input) : SV_Target
      {
        float2 v = float2(input.uv.x * 200 * 1, input.uv.y * 200) ;

        float  f = 5 * ( (sin(v.x) * 0.5 + 0.5) + (sin(v.y) * 0.5 + 0.5) );
        return fixed4(fixed3(0.1, 0.9, 1.0),  1-f);        
      }
      ENDCG
    }
  }
	FallBack "Diffuse"
}
