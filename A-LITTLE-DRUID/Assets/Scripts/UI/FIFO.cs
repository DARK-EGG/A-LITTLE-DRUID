using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FIFO : MonoBehaviour
{
    public static Image img;
    public bool fadeInOver;
    public bool start;

    private void Awake()
    {
        img = GetComponent<Image>();
        Debug.Log("Current Platform is " + Application.platform);
        
    }

    private void Start()
    {
        if (SaveData.isAndroid == false)
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            if (index != 0 && index != 1 && index != 7)
            {
                GameObject.Find("MoveKeys").SetActive(false);
            }
            else
            {
                Debug.Log("Current Scene Doesn't Have Move Keys");
            }
        }
        fadeInOver = false;
        start = false;
        
    }

    private void Update()
    {
        if(!fadeInOver)
        {
            StartCoroutine(FadeInEffect());
            if (SceneManager.GetActiveScene().buildIndex != 1)
                StartCoroutine(AudioFIFO.AudioFadeInEffect());
            fadeInOver = true;
        }


    }

    public IEnumerator Waiter()
    {
        Debug.Log("Waiter Called");
        yield return new WaitForSeconds(0.1f);
        yield break;
    }

   

    public IEnumerator FadeInEffect()
    {
        float currentTime = 0f;
        float resTime = 0.5f;

        while (resTime > currentTime)
        {
            currentTime += Time.deltaTime;
            img.color = Color32.Lerp(img.color, Color.clear, currentTime / resTime);
            if(img.color == Color.clear)
            {
                Debug.Log("Break!!!!!!");

                yield break;
            }
            yield return null;
        }
    }

    public static IEnumerator FadeOutEffect(string scene)
    {

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            AudioFIFO audioFIFO = FindObjectOfType<AudioFIFO>();
            audioFIFO.AudioFOEffect();
        }
        float currentTime = 0f;
        float resTime = 2f;
        img.color = Color.clear;
        PlayerStatus pS = null;
        var hp = 0f;
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 7)
        {
            pS = FindObjectOfType<PlayerStatus>();
            hp = pS.pStatus.playerCurrentHp;
        }
        while (resTime > currentTime)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 7)
                pS.pStatus.playerCurrentHp = hp;
            currentTime += Time.deltaTime;
            var targetC = Color32.Lerp(img.color, Color.black, currentTime / resTime);
            img.color = targetC;
            yield return null;
        }
        if(img.color == Color.black)
            SceneManager.LoadScene(scene);

    }
}
