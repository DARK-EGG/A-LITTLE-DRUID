using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    //엔딩 시작 가능 여부
    public bool endingStart = false;
    bool nextLine = false;
    bool first = true;

    //Objects
    public GameObject endingSet;
    public TypeEffect typeEffect;
    public Text text;
    public Animator anim;

    DialogueUI ui;

    private void Awake()
    {
        typeEffect.endChat = true;
    }
    void Start()
    {
        endingStart = false;
        nextLine = false;
        ui = GetComponent<DialogueUI>();
        first = true;
    }

    public void StartEnding()
    {
        //엔딩 그림 & 텍스트 set true
        endingSet.SetActive(true);
        text.gameObject.SetActive(true);
        typeEffect.endChat = true;

        if (EndingChange.whichEnding == 1)
        {
            EndingInfo.SetEndingInfo1(true);
            LoadEndingInfo.Save();
            endingSet.transform.GetChild(0).gameObject.SetActive(true);
            ui.Talk(28);
        }
        else if (EndingChange.whichEnding == 2)
        {
            EndingInfo.SetEndingInfo2(true);
            LoadEndingInfo.Save();
            endingSet.transform.GetChild(1).gameObject.SetActive(true);
            endingSet.transform.GetChild(2).gameObject.SetActive(true);
            ui.Talk(29);
            anim.SetBool("go2", true);
        }
        else if (EndingChange.whichEnding == 3)
        {
            EndingInfo.SetEndingInfo3(true);
            LoadEndingInfo.Save();
            endingSet.transform.GetChild(3).gameObject.SetActive(true);
            endingSet.transform.GetChild(4).gameObject.SetActive(true);
            endingSet.transform.GetChild(5).gameObject.SetActive(true);
            ui.Talk(30);
            anim.SetBool("go3", true);
        }
        nextLine = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Debug.Log("터치");
            if ((((!(EndingChange.whichEnding == 2) && endingStart)) || !first))
            {
                Debug.Log("들어와");
                if (typeEffect.endChat == false /*&& !(EndingChange.whichEnding == 2 && first)*/)
                {
                    typeEffect.EffectEnd();
                }
                else
                {
                    ui.Talk(27 + EndingChange.whichEnding);
                }
            }
            else if (endingStart)
            {
                first = false;
            }
        }
    }
}
