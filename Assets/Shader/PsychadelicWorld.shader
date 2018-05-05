Shader "Unlit/PsychadelicWorld"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 world : float4;
			};

			fixed rand3D (float3 position)
			{
				return fixed(frac(sin((position.x*234 + position.y*486 + position.z*762))*654982));
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.world = mul(unity_ObjectToWorld,v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed randPattern = rand3D(floor(i.world.xyz*10));
				fixed4 col = tex2D(_MainTex, i.uv);

				return col *= randPattern * sin(randPattern*_Time[3]*3 +0.5)+0.5;
			}


			ENDCG
		}
	}
}
