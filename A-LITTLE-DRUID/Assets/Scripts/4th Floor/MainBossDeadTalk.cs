using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBossDeadTalk : MonoBehaviour
{
    [SerializeField]
    bool talk = false;
    public static GameObject bossID;
    public GameObject boss;
    public static bool lastBossTalk;
    public GameObject fakeBoss;
    public GameObject galaxy;
    GameObject player;
    DialogueUI ui;
    
    void Start()
    {
        talk = false;
        player = GameObject.FindGameObjectWithTag("Player");
        ui = player.GetComponent<DialogueUI>();
        bossID = boss;
    }
    
    void Update()
    {
        if (PlayerAttackKeyEvent.BossDead && !talk)
        {
            EndingChange.checkEndingNum();
            talk = true;
            lastBossTalk = true;
            galaxy.SetActive(false);
            ui.OpenDialog(bossID);
            fakeBoss.SetActive(true);
        }
        if (PlayerAttackKeyEvent.BossDead && !lastBossTalk)
            fakeBoss.SetActive(false);
    }

    public void SetLocation()
    {
        fakeBoss.transform.position = boss.transform.GetChild(0).position;
    }
}
