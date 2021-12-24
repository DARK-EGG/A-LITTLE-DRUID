using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 1층 중간 보스 미라 스크립트
 *  - BossBase 상속
 */
public class Mummy : BossBase
{
    /* Animator */
    public Animator anim;

    /* 공격 범위 및 속도 */
    public float range = 1;
    public float speed = (float)0.7;

    private Vector2 homePos = new Vector2();
    private Vector2 playerPos = new Vector2();
    private Vector2 enemyPos = new Vector2();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        homePos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        enemyPos = gameObject.transform.position;
        playerPos = player.transform.position;

        if (Vector2.Distance(playerPos, enemyPos) < range) //범위 조절
        {
            LookAtPlayer();

            transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else
        {
            //transform.position = Vector2.MoveTowards(enemyPos, homePos, speed * Time.deltaTime);

           anim.SetBool("Run", false);
        }
    }
}
