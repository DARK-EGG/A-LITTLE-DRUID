using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEfficacy : MonoBehaviour
{
    PlayerStatus playerStatus;
    PlayerMove playerMove;
    //public double currentTime = 0d;
    //public int cycleSecond;//몇  초 동안 데미지를 입게 만들지.


    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerMove = FindObjectOfType<PlayerMove>();
        //Debug.Log(playerHP.playerCurrentHp);
    }

    //붉은 포션과 갈색 포션에 해당하는 경우의 function
    public void HealthImmediately(float effect)
    {
        if (playerStatus.pStatus.playerCurrentHp + effect <= playerStatus.pStatus.playerMaxHp)
        {
            playerStatus.pStatus.playerCurrentHp += effect;
        }
        else
        {
            playerStatus.pStatus.playerCurrentHp = playerStatus.pStatus.playerMaxHp;
        }
    }

    public void SpecialHealth(float effect)
    {
        playerStatus.pStatus.playerMaxHp = effect;
        playerStatus.pStatus.playerCurrentHp = effect;
    }
    //특정 초 동안 독에 의한 효과를 받는 것처럼, 플레이어의 체력을 줄여나갑니다.
    public IEnumerator HealthWithTime(float effect)
    {
        for (int i = 0; i < 10; i++)
        {
            playerStatus.pStatus.playerCurrentHp = effect;
            yield return new WaitForSeconds(1);
        }
    }

    public void YourSpeedFeelLike(float effect)
    {
        var beforeSpeed = playerMove.Speed;
        playerMove.Speed += effect;
        Debug.Log("Speed Change: " + beforeSpeed + " >>> " + playerMove.Speed);
    }

}