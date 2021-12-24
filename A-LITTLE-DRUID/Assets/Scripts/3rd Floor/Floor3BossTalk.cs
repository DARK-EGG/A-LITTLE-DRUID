using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manager
//보스와의 대화 3층 특화 스크립트
public class Floor3BossTalk : MonoBehaviour
{
    public GameObject talkTrigger;
    public int numBoss;
    bool done = false;
    private void Start()
    {
        done = false;
    }
    void Update()
    {
        if (PlayerAttackKeyEvent.removedMBNum >= numBoss && !done)
            BossTalk.canTalk = false;
        if (DarkByButton.isLight && !done)
        {
            talkTrigger.SetActive(true);
            BossTalk.canTalk = true;
            done = true;
            enabled = false;
        }
    }
}
