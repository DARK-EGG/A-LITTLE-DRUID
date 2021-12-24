using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject mainMenuHolder;
    public GameObject optionMenuHolder;
    public GameObject AskStartPanel;

    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWidths;
    int activesScreenResIndex;

    bool actived;

    private void Awake()
    {
        actived = false;
        AskStartPanel.SetActive(false);
    }

    public void CheckNewPlay()
    {
        if (SaveSystem.SavefileExist())
        {
            // 플레이 이력 있음
            AskStartPanel.SetActive(true);
        }
        else
        {
            Play();
        }
    }
    public void Play()
    {
        SaveSystem.resetFolder();
        if (actived)
            return;
        else
        {
            actived = true;
            ButtonSetFalse();
            StartCoroutine(FIFO.FadeOutEffect("1_Prologue"));
        }

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameLaod()
    {
        if (actived)
            return;
        else
        {
            GetComponent<pauseMenu>().paused = false;
            ButtonSetFalse();
            string scene = PlayerPrefs.GetString("SceneNum");
            StartCoroutine(FIFO.FadeOutEffect(scene));
            Debug.Log("로드");
            //PauseUI.SetActive(false);
            //SceneManager.LoadScene(scene);
        }
    }

    public void ButtonSetFalse()
    {
        var buttons = GameObject.Find("Main Menu").GetComponentsInChildren<Button>();

        for(int i =0; i< buttons.Length; i++)
        {
            //buttons[i].enabled = false;
            buttons[i].GetComponent<Image>().raycastTarget = false;
        }
    }
}
