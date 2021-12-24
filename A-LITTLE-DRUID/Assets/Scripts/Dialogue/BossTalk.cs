using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스와의 대화 관련 script
//Player_notre
public class BossTalk : MonoBehaviour
{
    private GameObject scanObject;
    //보스랑 대화 가능한가
    public static bool canTalk = false;
    //보스랑 싸움 가능한가
    public static bool canBoss = false;
    //시간이 멈춰있는가
    public static bool stopTime = false;

    public static bool nowBossTalk = false;
    private GameObject talkTrigger;
    public static int id;

    public void Awake()
    {
        canTalk = false;
        canBoss = false;
        stopTime = false;
        nowBossTalk = false;
    }

    private void Start()
    {
        talkTrigger = GameObject.FindGameObjectWithTag("TalkTrigger");
    }

    //BossTalk Trigger와 부딪혔을 때
    public void OnTriggerEnter2D(Collider2D collision)
    {
        scanObject = collision.gameObject;
        if (scanObject.CompareTag("TalkTrigger"))
        {
            //보스와의 싸움 시작 전
            if (!canBoss)
            {
                ColliderSetting colliderSetting = GetComponent<ColliderSetting>();
                if (SaveData.isAndroid)
                    colliderSetting.canAttack = false;
                DialogueUI ui = GetComponent<DialogueUI>();
                id = scanObject.GetComponent<ObjData>().id;
                ui.OpenDialog(scanObject);
                if (!DialogueUI.bossTalkChanged || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 3) {
                    talkTrigger = GameObject.FindGameObjectWithTag("TalkTrigger");
                    Debug.Log(talkTrigger.name + " 멀리 이동");
                    talkTrigger.transform.position = new Vector3(2000,0,0);
                }
                nowBossTalk = true;
                scanObject = null;
            }
            //강제 전체 멈춤, 대화 강제 시작
            if (scanObject && canTalk)
            {
                stopTime = true;
            }
        }
    }

    public GameObject getBossTalkTrigger()
    {
        Debug.Log("get "+ talkTrigger);
        return talkTrigger;
    }

    public void setNullBossTalkTrigger()
    {
        scanObject = null;
    }

}
