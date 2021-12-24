using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 스파이크 스크립트
 * 
 */
public class Spike : MonoBehaviour
{
    Animator anim;

    public int attackWait = 3;          // 대기 시간
    public int idleWait = 3;            // 공격 시간

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Attack");
    }


    IEnumerator Attack()
    {

        yield return new WaitForSeconds(attackWait);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(idleWait);
        anim.ResetTrigger("Attack");
       
        StartCoroutine("Attack");

    }
}
