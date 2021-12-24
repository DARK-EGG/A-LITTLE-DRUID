using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomObj : MonoBehaviour
{
    InjuryFromBossRoom injuryFromBossRoom;

    void Start()
    {
        injuryFromBossRoom = FindObjectOfType<InjuryFromBossRoom>();
    }

    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.name == "SpaceA")
                injuryFromBossRoom.damageFromIdleness();
            else
                injuryFromBossRoom.damageFromObj();
        }
    }
}
