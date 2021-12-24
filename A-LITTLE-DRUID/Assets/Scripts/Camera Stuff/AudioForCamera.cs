using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioForCamera : MonoBehaviour
{
    public AudioSource BossTheme;
    public AudioSource MainTheme;
    public AudioSource PlayerDeadSound;
    public GameObject nextDoor;
    PlayerStatus playerStatus;
    int currentDeadCount = 0;
    int currentBossFaceCount = 0;
    public int bossDeadCount = 0;
    bool currentTalk = true;
    public int numBoss; //해당 층의 중간보스의 수를 넣어줘야합니다.
    bool bmStart = true;

    void Start()
    {
        Debug.Log("YEEEEE");

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            playerStatus = FindObjectOfType<PlayerStatus>();
        }
        else
            playerStatus = null;
        MainTheme.Play();
        currentDeadCount = 0;
        currentBossFaceCount = 0;
        bossDeadCount = 0;
    }

    void Update()
    {
        if(playerStatus != null)
        {
            if(PlayerAttackKeyEvent.IsDead == true && currentDeadCount == 0)
                DeadAudio();//죽는음악
            if(PlayerAttackKeyEvent.removedMBNum >= numBoss && currentBossFaceCount == 0)
                MainTheme.Pause();
            if(PlayerAttackKeyEvent.removedMBNum >= numBoss && currentTalk)  
            {
                BossTalk.canTalk = true;//보스와 대화가능상태
                currentTalk = false;
            }
            if (BossTalk.canBoss && bmStart)
            {
                bmStart = false;
                BossAudio();//보스전 시작
            }
            if (PlayerAttackKeyEvent.BossDead == true && bossDeadCount == 0)
            {
                if (SceneManager.GetActiveScene().buildIndex != 6)
                {
                    ReturnAudio();
                    bossDeadCount++;
                }
                nextDoor.SetActive(true);
            }
        }        
    }
    public void DeadAudio()
    {
        MainTheme.Stop();
        BossTheme.Stop();
        PlayerDeadSound.Play();
        currentDeadCount++;
    }

    public void BossAudio()
    {
        MainTheme.Stop();
        BossTheme.Play();
        currentBossFaceCount++;
    }
    public void OnlyBossAudioStop()
    {
        BossTheme.Stop();
    }
    public void ReturnAudio()
    {
        BossTheme.Stop();
        MainTheme.PlayDelayed(2.0f);
    }
}
