using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * [2층 중간보스 스크립트] (BossBase 상속)
 * 플레이어와의 거리가 range보다 가까우면 빠르게 굴러 공격
 */

public class StoneBoss : BossBase
{
    /* Animator */
    public Animator anim;

    /* 공격, 대기 시간 간격 */
    public int stopTime = 100;
    public int rollTime = 200;
    
    /* 공격 범위 및 속도 */
    public float range = 1;
    public float speed = (float)0.7;

    private Vector2 homePos = new Vector2();
    private Vector2 playerPos = new Vector2();
    private Vector2 curPos = new Vector2();

    private int time = 0;

    void Start()
    {
        homePos = gameObject.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        curPos = gameObject.transform.position;
        playerPos = player.transform.position;

        if (Vector2.Distance(playerPos, curPos) <= range)
        {
            Roll();
        }
        else
        {
            transform.position = Vector2.MoveTowards(curPos, homePos, speed * Time.deltaTime);
        }
    }

    /* 플레이어를 향해 구르는 공격기 */
    private void Roll()
    {
        LookAtPlayer();
        time++;

        if (!anim.GetBool("Roll"))
        {
            if (time >= stopTime)
            {
                time = 0;
                anim.SetBool("Roll", true);
            }
        }
        else
        {
            if (time >= rollTime)
            {
                time = 0;
                anim.SetBool("Roll", false);
            }

            transform.position = Vector2.MoveTowards(curPos, playerPos, speed * Time.deltaTime);
        }
    }
}
