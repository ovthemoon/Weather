using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusicSource;
    public float fadeDuration = 3f; // Fade In/Out에 걸리는 시간

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBackgroundMusic(AudioClip newClip)
    {
        StartCoroutine(ChangeMusicCoroutine(newClip));
    }

    private IEnumerator ChangeMusicCoroutine(AudioClip newClip)
    {
        // Fade Out
        float startVolume = backgroundMusicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            backgroundMusicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = newClip;
        backgroundMusicSource.Play();

        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            backgroundMusicSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        backgroundMusicSource.volume = startVolume;
    }
}
