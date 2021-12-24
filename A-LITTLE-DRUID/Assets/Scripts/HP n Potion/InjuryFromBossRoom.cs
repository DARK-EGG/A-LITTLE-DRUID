using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryFromBossRoom : MonoBehaviour
{
    PlayerStatus playerStatus;

    public int damageFromBossRoomObj = 1;
    float damageFromBossRoomIdleness = 0.5f;

    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
    }

    void Update()
    {
        
    }

    public void damageFromObj()
    {
        playerStatus.pStatus.playerCurrentHp -= damageFromBossRoomObj;
    }

    public void damageFromIdleness()
    {
        playerStatus.pStatus.playerCurrentHp -= damageFromBossRoomIdleness;
    }
}
