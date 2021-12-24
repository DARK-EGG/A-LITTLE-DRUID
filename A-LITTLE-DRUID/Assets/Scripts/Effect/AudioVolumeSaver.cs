using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioVolumeSaver : MonoBehaviour
{
    public GameObject[] sfxList;
    public GameObject[] bgmList;

    public float[] sfxDefaultValue;
    public float[] bgmDefaultValue;


    public float[] sfxCurrentValue;
    public float[] bgmCurrentValue;

    public float sfxSliderValue;
    public float bgmSliderValue;

    public bool awakeGameFirst = true;

    public bool inGameAndReturn = false;

    public float sfxLastValue;
    public float bgmLastValue;

    public void Awake()
    {
        awakeGameFirst = true;
        inGameAndReturn = false;
    }
    public void AudioSaverInfoInitialize()
    {
        if(!inGameAndReturn)
        {
            sfxSliderValue = 1;
            bgmSliderValue = 1;
            sfxLastValue = sfxSliderValue;
            bgmLastValue = bgmSliderValue;
        }
        else
        {
            sfxSliderValue = sfxLastValue; 
            bgmSliderValue = bgmLastValue;
        }

        sfxList = GameObject.FindGameObjectsWithTag("SFX");
        bgmList = GameObject.FindGameObjectsWithTag("BGM");
        sfxDefaultValue = new float[sfxList.Length];
        bgmDefaultValue = new float[bgmList.Length];

        sfxCurrentValue = new float[sfxList.Length];
        bgmCurrentValue = new float[bgmList.Length];

        for (int i = 0; i < sfxList.Length; i++)
        {
            sfxDefaultValue[i] = sfxList[i].GetComponent<AudioSource>().volume;
        }

        for (int i = 0; i < bgmList.Length; i++)
        {
            bgmDefaultValue[i] = bgmList[i].GetComponent<AudioSource>().volume;
        }
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            sfxList = GameObject.FindGameObjectsWithTag("SFX");
            bgmList = GameObject.FindGameObjectsWithTag("BGM");

            for (int i = 0; i < sfxList.Length; i++)
            {
                sfxList[i].GetComponent<AudioSource>().volume = sfxCurrentValue[i];
            }

            for (int i = 0; i < bgmList.Length; i++)
            {
                bgmList[i].GetComponent<AudioSource>().volume = bgmCurrentValue[i];
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
