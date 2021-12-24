using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioFIFO : MonoBehaviour
{
    static AudioSource mainTheme; 
    static float audioVolume;
    public static bool playOver;

    private void Awake()
    {
        mainTheme = FindObjectOfType<AudioForCamera>().MainTheme;
        playOver = false;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioVolumeController avc = FindObjectOfType<AudioVolumeController>();
            avc.AudioInitialize();
            audioVolume = mainTheme.volume;
        }
    }

    public static IEnumerator AudioFadeInEffect()
    {
        audioVolume = mainTheme.volume;
        mainTheme.volume = 0.0f;

        float currentTime = 0f;
        float resTime = 2f;

        while (resTime > currentTime)
        {
            currentTime += Time.deltaTime;
            mainTheme.volume = Mathf.Lerp(mainTheme.volume, audioVolume, currentTime / resTime);
            yield return null;
        }

        playOver = true;
        yield break;
    }

    public void AudioFOEffect()
    {
        StartCoroutine(AudioFadeOutEffect());
    }

    public static IEnumerator AudioFadeOutEffect()
    {
        playOver = true;
        audioVolume = 0f;
        float currentTime = 0f;
        float resTime = 1f;

        while (resTime > currentTime)
        {
            currentTime += Time.deltaTime;
            mainTheme.volume = Mathf.Lerp(mainTheme.volume, audioVolume, currentTime / resTime);
            yield return null;
        }
        yield break;
    }
}