using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreAudio : MonoBehaviour
{
    AudioForCamera audioForCamera;
    public AudioSource floorBGMSilence;
    public AudioSource truthRoomBGM;
    
    void Start()
    {
        audioForCamera = FindObjectOfType<AudioForCamera>();
    }

    
    void Update()
    {
        if (PlayerAttackKeyEvent.BossDead == true && audioForCamera.bossDeadCount == 0)
        {
            audioForCamera.OnlyBossAudioStop();

            if (LastBossClear.memoryPieceNum == 9)
                TruthRoomBGMPlay();
            else if (LastBossClear.memoryPieceNum != 9)
                SilenceBGMPlay();
            audioForCamera.bossDeadCount++;
        }
    }

    public void SilenceBGMPlay()
    {
        floorBGMSilence.Play();
    }

    public void TruthRoomBGMPlay()
    {
        truthRoomBGM.Play();
    }
}
