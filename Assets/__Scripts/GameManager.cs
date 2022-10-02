using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Pixelation.Scripts;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    public Image overlay;
    public RawImage renderImage;
    public GameObject clingPrompt;
    public Pixelation pixelationEffect;
    public Light sun;

    private float baseSunIntensity;

    // Start is called before the first frame update
    void Start()
    {
        baseSunIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        sun.intensity = Mathf.Sin(Time.time * 0.1f) * 0.03f + Mathf.Sin(Time.time * 1.13f) * 0.01f + baseSunIntensity;

        clingPrompt.SetActive(!Input.GetKey(KeyCode.Space));
    }

    // Dumb workaround for easier unityevents workflow (limits duration precision)
    public void FadeImage(float encodedDurationAlpha)
    {
        float duration = Mathf.Floor(encodedDurationAlpha);
        float alpha = encodedDurationAlpha - duration;
        duration /= 1000f;
        alpha = alpha >= 0.999f ? 1 : alpha;

        renderImage.DOColor(new Color(1, 1, 1, alpha), duration);
    }

    public void FadeOverlay(float encodedDurationAlpha)
    {
        Debug.Log("Fade overlay");

        float duration = Mathf.Floor(encodedDurationAlpha);
        float alpha = encodedDurationAlpha - duration;
        duration /= 1000f;
        alpha = alpha >= 0.999f ? 1 : alpha;

        overlay.DOColor(new Color(0, 0, 0, alpha), duration).SetEase(Ease.InQuad); 
    }

    public void FadePixelation(float encodedDurationCount)
    {
        float duration = Mathf.Floor(encodedDurationCount);
        float count = encodedDurationCount - duration;
        duration /= 1000f;
        count = Mathf.Round((count * 1008) + 16);

        DOTween.To(() => pixelationEffect.BlockCount, x => pixelationEffect.BlockCount = x, count, duration).SetEase(Ease.OutCubic);
    }
}
