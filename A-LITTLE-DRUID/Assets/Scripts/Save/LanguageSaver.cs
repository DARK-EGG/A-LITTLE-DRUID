using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageSaver : MonoBehaviour
{
    public static int languageIndex = 0;
    public static string[] korMenu = { "계속하기", "종료하기" };
    public static string[] engMenu = { "Resume", "Quit" }; 

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var sceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (sceneNumber != 0 && sceneNumber != 1 && sceneNumber != 2)
        {
            Text[] escMenu = new Text[korMenu.Length];
            escMenu[0] = GameObject.Find("Resume").GetComponentInChildren<Text>();
            escMenu[1] = GameObject.Find("Quit").GetComponentInChildren<Text>();
            if (languageIndex == 0)
            {
                for (int i = 0; i < korMenu.Length; i++)
                {
                    escMenu[i].text = korMenu[i];
                }
            }
            else if (languageIndex == 1)
            {
                for (int i = 0; i < engMenu.Length; i++)
                {
                    escMenu[i].text = engMenu[i];
                }
            }
        }
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            DatabaseManager dbManager = FindObjectOfType<DatabaseManager>();
            DialogueParser theParser = FindObjectOfType<DialogueParser>();//parser선언
            Dialogue[] dialogues;
            
            if (languageIndex == 0)
            {
                dbManager.en = false;
            }
            else if (languageIndex == 1)
            {
                dbManager.en = true;
            }

            //test 끝나면 복구시켜야 하는 코드
            if (dbManager.en)
                dialogues = theParser.Parse("Dialogue_EN");
            else
                dialogues = theParser.Parse("Dialogue_KR");
            for (int i = 0; i < dialogues.Length; i++)
            {
                dbManager.dialogueDic.Add(i, dialogues[i]);
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
