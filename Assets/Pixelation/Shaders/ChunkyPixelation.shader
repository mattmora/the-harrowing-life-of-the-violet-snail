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
	
			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 blockPos = floor(i.uv * BlockCount);
				float2 blockCenter = blockPos * BlockSize + BlockSize * 0.5;

				float4 pixTex = tex2D(_MainTex, blockCenter);

				// CHUNKY
				// (1)
				float2 chunkPos = floor(i.uv * ChunkCount);
				float2 chunkCenter = chunkPos * ChunkSize + ChunkSize * 0.5;

				float2 blockChunkPos = floor(chunkCenter * BlockCount);
				float2 blockChunkCenter = blockPos * BlockSize + BlockSize * 0.5;

				float2 chunkBlockPos = floor(blockCenter * ChunkCount);
				float2 chunkBlockCenter = chunkPos * ChunkSize + ChunkSize * 0.5;

				// (2)
				float4 del = float4(1, 1, 1, 1) - _Color;

				// (3)
				float4 tex = tex2D(_MainTex, chunkBlockCenter) - del;

				float grayscale = dot(tex.rgb, float3(0.3, 0.59, 0.11));
				grayscale = clamp(grayscale, 0.0, 1.0);

				// (4)
				float dx = floor(grayscale * 16.0);

				// (5)
				float2 sprPos = i.uv;
				sprPos -= chunkPos*ChunkSize;
				sprPos.x *= 0.0625;
				sprPos *= ChunkCount;
				sprPos.x += 0.0625 * dx;

				// (6)
				float4 chunkTex = tex2D(_SprTex, sprPos);
			
				return lerp(pixTex, chunkTex, Mix);
			}
			ENDCG
		}
	}
}
