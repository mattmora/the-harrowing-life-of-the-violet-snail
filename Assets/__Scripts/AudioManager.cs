using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> sounds;

    public void PlaySound(int i)
    {
        sounds[i].Stop();
        sounds[i].volume = 0.25f;
        sounds[i].Play();
    }

    public void FadeSound(float encodedDurationIndexVolume)
    {
        float duration = Mathf.Floor(encodedDurationIndexVolume / 10f) * 10f;
        int i = Mathf.FloorToInt(encodedDurationIndexVolume - duration);
        float volume = encodedDurationIndexVolume - duration - i;
        duration /= 1000f;

        DOTween.To(() => sounds[i].volume, x => sounds[i].volume = x, volume, duration).SetEase(volume > sounds[i].volume ? Ease.InQuad : Ease.OutQuad);
    }

    public void FadeAllSounds(float encodedDurationVolume)
    {
        float duration = Mathf.Floor(encodedDurationVolume);
        float volume = encodedDurationVolume - duration;
        duration /= 1000f;

        foreach (var sound in sounds)
            DOTween.To(() => sound.volume, x => sound.volume = x, volume, duration).SetEase(volume > sound.volume ? Ease.InQuad : Ease.OutQuad);
    }
}
