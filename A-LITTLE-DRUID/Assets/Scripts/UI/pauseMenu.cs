using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    public GameObject PauseUI;


    public bool paused = false;

    bool menu_Down;

    void Start()
    {
        //if (SceneManager.GetActiveScene().name == "0_Main Menu")
        //{
        //    paused = !paused;
        //}

        PauseUI.SetActive(false);
    }

    void Update()
    {
        //Debug.Log("멈춤 " + paused);
        if (Input.GetButtonDown("Cancel") || menu_Down)
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }

        InitMobileVar();
    }

    public void Resume()
    {
        paused = false;
    }

    public void Save()
    {
        Debug.Log("세이브");
        PlayerPrefs.SetString("SceneNum", SceneManager.GetActiveScene().name );
        PlayerPrefs.Save();
        
    }

    public void Quit()
    {
        StartCoroutine(FIFO.FadeOutEffect("0_Main Menu"));
        PlayerPrefs.SetString("SceneNum", SceneManager.GetActiveScene().name);
        paused = false;
        AudioVolumeSaver avc = FindObjectOfType<AudioVolumeSaver>();
        avc.awakeGameFirst = true;
        avc.inGameAndReturn = true;
        avc.sfxLastValue = avc.sfxSliderValue;
        avc.bgmLastValue = avc.bgmSliderValue;
        //SceneManager.LoadScene("0_Main Menu");
    }

    public void ButtonDown(string type)
    {
        switch(type)
        {
            case "M":
                menu_Down = true;
                break;
        }
    }

    private void InitMobileVar()
    {
        menu_Down = false;
    }
}
