using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{


    public static float sfxSliderValue;
    public static float bgmSliderValue;
 
    public Slider sfxSlider;
    public Slider bgmSlider;

    AudioVolumeSaver avs;

    GameObject settingPanel;

    private void Awake()
    {
        settingPanel = GameObject.Find("Setting Panel");
    }

    void Start()
    {
        sfxSlider.maxValue = 1;
        bgmSlider.maxValue = 1;
        sfxSlider.minValue = 0;
        bgmSlider.minValue = 0;

    }

    public void AudioInitialize()
    {

        avs = GameObject.Find("AudioVolumeSaver").GetComponent<AudioVolumeSaver>();
        avs.AudioSaverInfoInitialize();

        if (avs.awakeGameFirst)
        {
            sfxSlider.value = avs.sfxSliderValue;
            bgmSlider.value = avs.bgmSliderValue;

            for (int i = 0; i < avs.sfxList.Length; i++)
            {
                avs.sfxList[i].GetComponent<AudioSource>().volume = (float)(avs.sfxDefaultValue[i] * sfxSlider.value);
            }

            for (int i = 0; i < avs.bgmList.Length; i++)
            {
                avs.bgmList[i].GetComponent<AudioSource>().volume = (float)(avs.bgmDefaultValue[i] * bgmSlider.value);
            }

            VolumeChange();
            avs.awakeGameFirst = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioFIFO.playOver && settingPanel.activeSelf)
        {
            sfxSlider.onValueChanged.AddListener(delegate { StartCoroutine(SFXExamplePlay()); });
            VolumeChange();
        }
    }

    IEnumerator SFXExamplePlay()
    {
        avs.sfxList[1].GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
    }

    public void VolumeChange()
    {
        Debug.Log("Called");
        for (int i = 0; i < avs.sfxList.Length; i++)
        {
            avs.sfxList[i].GetComponent<AudioSource>().volume = (float)(avs.sfxDefaultValue[i] * sfxSlider.value);
            avs.sfxCurrentValue[i] = avs.sfxList[i].GetComponent<AudioSource>().volume;
            avs.sfxSliderValue = sfxSlider.value;
        }
        for (int i = 0; i < avs.bgmList.Length; i++)
        {
            avs.bgmList[i].GetComponent<AudioSource>().volume = (float)(avs.bgmDefaultValue[i] * bgmSlider.value);
            avs.bgmCurrentValue[i] = avs.bgmList[i].GetComponent<AudioSource>().volume;
            avs.bgmSliderValue = bgmSlider.value;
        }
    }
}
