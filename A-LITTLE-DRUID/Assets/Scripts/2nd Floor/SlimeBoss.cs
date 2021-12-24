using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * [2층 중간보스 슬라임 스크립트] (BossBase 상속)
 */
public class SlimeBoss : BossBase
{
    /* 공격 주기 관련 변수 */
    public int attackWait = 3;
    public int idleWait = 3;
    public int activeWait = 5;

    private Animator anim;

    /* 사용되는 부울 로컬 변수 */
    private static bool isDead = false;
    private bool enableSpawn = false;

    /* 잡몹 생성 코루틴 */
    Coroutine enemyCoroutine;

    /* pool을 담을 부모 Object */
    private Transform parent = null;

    int i = 0;
    int count = 0;

    private void Start()
    {
        isDead = false;
        anim = GetComponent<Animator>();
        enemyCoroutine = StartCoroutine("spawnAnim");
        parent = GameObject.Find("Object Pool").transform;  // 상위 빈 오브젝트

    }

    private void Update()
    {
        if (isDead)
        {
            StopCoroutine(enemyCoroutine);
            //Debug.Log("루틴 중단");
        }
        LookAtPlayer();
    }

    /* 작은 슬라임을 주기적으로 생성하는 코루틴 함수 */
    IEnumerator spawnAnim()
    {
        yield return new WaitForSeconds(attackWait);
        anim.SetTrigger("Idle");
        //Debug.Log("공격중");

        yield return new WaitForSeconds(idleWait);
        anim.ResetTrigger("Idle");
        //Debug.Log("공격끝");

        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= 2)
        {
            PopObject();
            PopObject();
            //Debug.Log("적 등장");
        }

        yield return new WaitForSeconds(activeWait);
        StartCoroutine("spawnAnim");
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
                temp.transform.position = new Vector2(1.73f, 8.87f); ;
            }
            else
            {
                temp.transform.position = new Vector2(4.03f, 8.87f);
            }
            temp.transform.SetParent(parent);
            temp.SetActive(true);
        }

        if (++count > 5) count = 0;
    }
}
