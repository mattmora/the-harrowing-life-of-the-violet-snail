using Assets.Pixelation.Example.Scripts;
using UnityEngine;

namespace Assets.Pixelation.Scripts
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Chunky Pixelation")]
    public class ChunkyPixelation : ImageEffectBase
    {
        [Range(16.0f, 1024f)] public float BlockCount = 128;

        public Texture2D SprTex;

        public Color Color = Color.white;

        public float Mix = 0.5f;

        private Camera mainCamera;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            float k = Camera.main.aspect;
            Vector2 count = new Vector2(BlockCount, BlockCount / k);
            Vector2 size = new Vector2(1.0f / count.x, 1.0f / count.y);
            //
            material.SetVector("BlockCount", count);
            material.SetVector("BlockSize", size);

            float w = Camera.main.pixelWidth;
            float h = Camera.main.pixelHeight;
            count = new Vector2(w / SprTex.height, h / SprTex.height);
            size = new Vector2(1.0f / count.x, 1.0f / count.y);
            //
            material.SetVector("ChunkCount", count);
            material.SetVector("ChunkSize", size);
            material.SetColor("_Color", Color);
            material.SetTexture("_SprTex", SprTex);

            material.SetFloat("Mix", Mix);

            Graphics.Blit(source, destination, material);
        }
    }
}