using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    PlayerStatus playerStatus;

    //Boss_Weapon Boss_Weapon;
    BossTalk bosstalk;

    public bool monsterAttackingPlayer = false;
    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();

        //Boss_Weapon = GameObject.Find("Boss").GetComponent<Boss_Weapon>();
        monsterAttackingPlayer = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (this.CompareTag("Player") && !this.CompareTag("HitBox"))
        {
            if (collision && collision.CompareTag("MiddleBoss"))
            {
                playerStatus.pStatus.playerCurrentHp -= playerStatus.pStatus.dmgFromMiddleboss;
                if (!monsterAttackingPlayer)
                {
                    StartCoroutine(PlayerAttacked());
                }
            }
            else if (collision && ((collision.CompareTag("Enemy")) || (collision.CompareTag("ClonedEnemy")) || (collision.CompareTag("HitObject"))))
            {
                playerStatus.pStatus.playerCurrentHp -= playerStatus.pStatus.dmgToPlayer;
                if (!monsterAttackingPlayer)
                {
                    StartCoroutine(PlayerAttacked());
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (BossTalk.canBoss == true)
        {
            if (!monsterAttackingPlayer)
            {
                StartCoroutine(PlayerAttacked());
            }
            if(!CompareTag("HitBox") && collision && collision.CompareTag("Boss"))
            {
                playerStatus.pStatus.playerCurrentHp -= playerStatus.pStatus.dmgFromLastboss;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!CompareTag("HitBox"))
        {
            if (collision && collision.CompareTag("MiddleBoss"))
                monsterAttackingPlayer = false;
            else if (collision && ((collision.CompareTag("Enemy") || (collision.CompareTag("ClonedEnemy")))))
                monsterAttackingPlayer = false;
            else if (collision && collision.CompareTag("Boss"))
                monsterAttackingPlayer = false;
        }
    }
    IEnumerator PlayerAttacked()
    {
        monsterAttackingPlayer = true;
        Color normal = new Color(1, 1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = normal;
        yield return new WaitForSeconds(0.2f);
        monsterAttackingPlayer = false;
    }
}
