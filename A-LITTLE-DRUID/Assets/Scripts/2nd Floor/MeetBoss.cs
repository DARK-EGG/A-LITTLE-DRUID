using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//해당 스크립트는 보스 방에 들어가는 것만을 제한합니다.
//보스 방에 들어간 다음에 특정 영역을 벗어나면 문이 닫히는 변화에 대해서는 해당 스크립트에서 다루지 않습니다.
public class MeetBoss : MonoBehaviour
{
    EnemiesHP enemiesHP;
    public GameObject bossDoorBlock;//보스 방 들어가는게 제한된 닫힌 문 단 하나만을 담고있는 타일맵
    public GameObject roomTransfer;//보스 방 들어가기 직전의 room transfer를 담음
    AudioEffect audioEffect;
    void Start()
    {
        enemiesHP = FindObjectOfType<EnemiesHP>();
        bossDoorBlock.SetActive(false);
        roomTransfer.SetActive(false);

        audioEffect = FindObjectOfType<AudioEffect>();
    }

    

    //해당 함수는 플레이어가 중간보스를 죽여, PlayerHP의 removedMBNum이 증가할 때마다 호출되는데,
    //해당 층의 중간 보스의 수와, 현재 죽은 중간보스의 수가 같은지를 비교한 다음에
    //두 수가 같은 경우에만 보스방으로 접근이 가능하도록 두 변수를 setActive(true)로 바꿔준다.
    
    public void CanWeMeetBoss()
    {
        Debug.Log("$$$$$$");
        if(enemiesHP.middleBossName.Length == PlayerAttackKeyEvent.removedMBNum)
        //if (enemiesHP.middleBossName.Length == PlayerAttackKeyEvent.removedEnemyNum)
        {
            audioEffect.DoorOpenSoundPlay();
            Debug.Log("#######");
            bossDoorBlock.SetActive(true);
            roomTransfer.SetActive(true);
        }
    }
}
