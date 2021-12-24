using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    public Text languageNoticeText;
    public Text languageText;

    public string[] languageNoticeList = { "언어", "Lan" };
    public string[] languageList = { "한국어", "English" };
    
    // Start is called before the first frame update
    void Start()
    {
        languageNoticeText.text = languageNoticeList[LanguageSaver.languageIndex];
        languageText.text = languageList[LanguageSaver.languageIndex];

        AudioVolumeSaver avs = FindObjectOfType<AudioVolumeSaver>();

        if(!avs.awakeGameFirst)
        {
            languageNoticeText.text = languageNoticeList[LanguageSaver.languageIndex];
            languageText.text = languageList[LanguageSaver.languageIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftButtonClick()
    {
        if(LanguageSaver.languageIndex == 0)
        {
            LanguageSaver.languageIndex = languageList.Length - 1;
        }
        else
        {
            LanguageSaver.languageIndex -= 1;
        }
        languageNoticeText.text = languageNoticeList[LanguageSaver.languageIndex];
        languageText.text = languageList[LanguageSaver.languageIndex];
        Debug.Log("현재 언어: " + languageText.text);
    }

    public void RightButtonClick()
    {
        if(LanguageSaver.languageIndex == 1)
        {
            LanguageSaver.languageIndex = 0;
        }
        else
        {
            LanguageSaver.languageIndex += 1;
        }
        languageNoticeText.text = languageNoticeList[LanguageSaver.languageIndex];
        languageText.text = languageList[LanguageSaver.languageIndex];
        Debug.Log("현재 언어: " + languageText.ToString());
    }
}
