using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [3층 중간보스 독수리 스크립트]
 * - 애니메이션, 위치 관련
 * - BossBase 추상클래스 상속
 */
public class EagleAnimation : BossBase
{
    /* 독수리 속도, 범위, 세팅 위치 */
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Vector2 homePos;

    private Animator anim;

    /* 독수리 깃털 생성 공격기 스크립트 - isAttack 부울 변수 사용 */
    private MakeFeather featherScript = null;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        featherScript = GameObject.Find("Eagle").GetComponent<MakeFeather>();
        anim = GetComponent<Animator>();
        homePos = gameObject.transform.position;
    }

    void Update()
    {
        if (featherScript.isAttack)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        Vector2 enemyPos = gameObject.transform.position;   // 독수리 위치
        Vector2 playerPos = player.transform.position;

        if (featherScript.isAttack)                         // 깃털 공격 중에는 제자리에 멈춤
        {
            rigid.velocity = Vector2.zero;
            //rigid.position = enemyPos; 
        }
        else                                                // 깃털 공격 딜레이 중에는 플레이어를 따라감
        {
            FollowPlayer(enemyPos, playerPos);
        }
    }

    /* 플레이어를 따라가는 메소드 */
    public virtual void FollowPlayer(Vector2 enemyPos, Vector2 playerPos)
    {
        if (Vector2.Distance(playerPos, enemyPos) < featherScript.range && !BossTalk.stopTime)
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(enemyPos, homePos, speed * Time.deltaTime);
        }
    }
}
