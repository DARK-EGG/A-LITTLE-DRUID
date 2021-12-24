using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * [2층 보스 추상클래스]
 * 중간보스, 메인보스에 공통적으로 사용되는 메소드 모음
 */

public abstract class BossBase : MonoBehaviour
{
    protected GameObject player;
    protected Rigidbody2D rigid;
    protected bool isFlipped = false;     // 바라보는 방향 여부

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /* 플레이어를 향해 바라보는 방향 전환 */
    protected void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
