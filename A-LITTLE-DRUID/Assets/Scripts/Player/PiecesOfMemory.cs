using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//기억의 조각 관리 스크립트
//DialogueManager
public class PiecesOfMemory : MonoBehaviour
{
    //이벤트창
    public GameObject alertBox;
    Text alertText;

    //기억의 조각 UI
    public GameObject piece1;
    public GameObject piece2;
    public GameObject piece3;

    //현재 먹은 기억의 조각 수 
    public static int num;
    //현재 기억의 조각 알림 창 떠있는지 여부
    public bool getPieceAlertOn = true;
    //기억의 조각 전부 다 먹었는지 여부
    public static bool all;

    DatabaseManager dbManager;

    public GameObject dialogBtn;
    bool talk_Down = false;

    void Start()
    {
        num = 0;
        getPieceAlertOn = false;
        all = false;
        dbManager = GetComponent<DatabaseManager>();
        alertText = alertBox.transform.GetChild(0).GetComponent<Text>();
    }

    //이벤트창 떠있는 경우 기억의 조각 알림창 내림
    private void Update()
    {
        if ((Input.GetButtonDown("Jump") || talk_Down) && alertBox.activeSelf && getPieceAlertOn)
        {
            alertBox.SetActive(false);
            getPieceAlertOn = false;
            dialogBtn.GetComponent<Image>().sprite = Resources.Load("images\\Attack", typeof(Sprite)) as Sprite;
        }

        InitMobileVar();
    }

    //기억의 조각 UI update
    void UpdateMemoryPiece()
    {
        //UI 업데이트
        if (num == 1)
            piece1.SetActive(true);
        else if (num == 2)
            piece2.SetActive(true);
        else if (num == 3)
        {
            piece3.SetActive(true);
            all = true;
        }
        alertBox.SetActive(true);
        if (dbManager.en)
            alertText.text = "You got the piece";
        else
            alertText.text = "기억의 조각을 얻었다.";

        //현재 이벤트 창 떠있는 지 여부
        getPieceAlertOn = true;
        dialogBtn.GetComponent<Image>().sprite = Resources.Load("images\\Chat", typeof(Sprite)) as Sprite;
    }

    //먹은 기억의 조각 Scene에서 제거
    public void getPiece(Collision2D other)
    {
        if (other.gameObject.CompareTag("mPiece"))
        {
            other.gameObject.SetActive(false);
            num++;
            UpdateMemoryPiece();
        }
    }

    //기억의 조각 개수 저장
    internal void Save()
    {
        throw new NotImplementedException();
    }

    private void InitMobileVar()
    {
        talk_Down = false;
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "T":
                talk_Down = true;
                break;
        }
    }


}
