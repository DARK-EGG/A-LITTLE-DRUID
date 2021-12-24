using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBossTouched : MonoBehaviour
{
    PlayerStatus playerStatus;
    public float fakeBossCanHurtYouAmount = 1.5f;

    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
    }

    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision && collision.CompareTag("Player"))
        {
            playerStatus.pStatus.playerCurrentHp -= fakeBossCanHurtYouAmount;
        }
    }
}
