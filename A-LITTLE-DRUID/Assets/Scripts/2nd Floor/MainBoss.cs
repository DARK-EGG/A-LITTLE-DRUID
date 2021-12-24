using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * [2층 메인보스 공격기 스크립트] (BossBase 상속)
 * + 번개 광역 공격기
 * + water 몬스터 생성 공격기
 */

public class MainBoss : BossBase
{
    /* Animator */
    public Animator anim;

    /* 공격을 시작할 플레이어와의 거리 */
    public int range;

    /* water 몬스터 생성 주기 */
    public int idleWait = 5;
    public int activeWait = 15;

    /* 필요한 bool 변수 */
    private bool bossDie = false;
    private bool isAttack = false;      // 공격 여부

    /* pool을 담을 부모 Object */
    private Transform parent = null;

    int i = 0, count = 0;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        StartCoroutine(StrikeLightning());      // 번개 광역 공격기
        StartCoroutine(SpawnMonster());         // water, waterSmall 몬스터 생성 공격기
        parent = GameObject.Find("Boss Pool").transform;  // 깃털들을 담을 상위 빈 오브젝트
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= range)
        {
            /* 공격 여부에 따른 애니메이션 */
            if (isAttack)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            LookAtPlayer();
        }
    }

    /* 3초마다 1초씩 발생하는 번개 광역 공격기 */
    IEnumerator StrikeLightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            isAttack = false;
            yield return new WaitForSeconds(3.0f);
            isAttack = true;
        }
    }

    /* 몬스터 생성 공격기 */
    IEnumerator SpawnMonster()
    {
        if (!bossDie)
        {

            yield return new WaitForSeconds(idleWait);

            if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= range)
            {
                PopObject();
                PopObject();
            }

            yield return new WaitForSeconds(activeWait);
            StartCoroutine(SpawnMonster());
        }
    }

    /* Pool에서 오브젝트를 꺼내는 메소드 */
    public void PopObject()
    {
        if (++i > 1) i = 0;

        GameObject temp;

        if (parent.childCount > 0)
        {
            temp = parent.GetChild(count).gameObject;

            /* 위치 초기화 */
            if (i == 0)
            {
                temp.transform.position = new Vector2(17.72f, -3.23f);
            }
            else
            {
                temp.transform.position = new Vector2(19.66f, -3.23f);
            }
            temp.transform.SetParent(parent);
            temp.SetActive(true);
        }

        if (++count > 5) count = 0;
    }

}
