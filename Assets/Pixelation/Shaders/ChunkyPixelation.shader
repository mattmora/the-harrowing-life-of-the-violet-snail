Shader "ChunkyPixelation" 
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SprTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;	
			float2 BlockCount;
			float2 BlockSize;

			float4 _Color = float4(1, 1, 1, 1); 
			sampler2D _SprTex; 
			float2 ChunkCount;
			float2 ChunkSize; 

			float Mix;

			float4 chunkTexAt(float2 uv) : COLOR
			{
				float2 chunkPos = floor(uv * ChunkCount);
				float2 chunkCenter = chunkPos * ChunkSize + ChunkSize * 0.5;

				float4 del = float4(1, 1, 1, 1) - _Color;

				float4 centerTex = tex2D(_MainTex, chunkCenter) - del;
				float grayscale = dot(centerTex.rgb, float3(0.3, 0.59, 0.11));
				grayscale = clamp(grayscale, 0.0, 1.0);

				float dx = floor(grayscale * 16.0);

				float2 sprPos = uv;
				sprPos -= chunkPos*ChunkSize;
				sprPos.x /= 16;
				sprPos *= ChunkCount;
				sprPos.x += 1.0 / 16.0 * dx;

				float4 tex = tex2D(_MainTex, uv);
				float4 tex2 = tex2D(_SprTex, sprPos);

				float4 mixTex = lerp(tex, tex2, Mix);
				return mixTex;
			}
	
			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 blockPos = floor(i.uv * BlockCount);
				float2 blockCenter = blockPos * BlockSize + BlockSize * 0.5;

				float4 tex = chunkTexAt(i.uv);
				return tex;
			}
			ENDCG
		}
	}
}
