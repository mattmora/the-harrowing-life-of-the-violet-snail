using Assets.Pixelation.Example.Scripts;
using UnityEngine;

namespace Assets.Pixelation.Scripts
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Chunky")]
    public class Chunky : ImageEffectBase
    {
        public float resolution;

        public Texture2D SprTex;

        public Color Color = Color.white;

        public float Mix;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            float w = resolution;
            float h = resolution * 9f / 16f;

            Vector2 count = new Vector2(w/SprTex.height, h/SprTex.height);
            Vector2 size = new Vector2(1.0f/count.x, 1.0f/count.y);
            //
            material.SetVector("BlockCount", count);
            material.SetVector("BlockSize", size);
            material.SetColor("_Color", Color);
            material.SetTexture("_SprTex", SprTex);
            material.SetFloat("Mix", Mix);
            Graphics.Blit(source, destination, material);
        }
    }
}