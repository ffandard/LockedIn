Shader "Custom/ObjectSelected" {
	Properties {
		_Color ("Emission Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float4 world;
			float4 color: COLOR;
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

		fixed rand3D (float3 position)
		{
			return fixed(frac(sin(dot(position,float3(2347.752,4862.743,7624.7537)))*6542.67));
		}

		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT (Input,o);
			o.world = mul(unity_ObjectToWorld,v.vertex);
			o.color = v.color;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed randPattern = rand3D(floor(IN.world.xyz*10));


			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Emission = _Color*sin(randPattern*_Time[3]*3)*0.5+0.5;
			o.Emission = pow(o.Emission*1.3,7.7);
			//o.Emission -= 1.5;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
