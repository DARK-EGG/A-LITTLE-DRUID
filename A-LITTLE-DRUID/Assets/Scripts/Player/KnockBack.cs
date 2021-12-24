using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [넉백 스크립트 KnockBack.cs]
 * Player의 hitBox에 적용되는 스크립트
 */
public class KnockBack : MonoBehaviour
{
    /* 충격량 */
    public float thrust;

    /* 넉백 유지 시간 */
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("ClonedEnemy"))         // 충돌한 오브젝트의 태그가 'Enemy', 'ClonedEnemy'인 경우,
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                hit.isKinematic = false;
                Vector2 difference = hit.transform.position - transform.position;   // Enemy와 Player의 단위 벡터 계산
                difference = difference.normalized * thrust;                        // 단위 벡터 * thrust
                hit.AddForce(difference, ForceMode2D.Impulse);                      // enemy에 충격량 적용
                hit.GetComponent<MobMove>().Knock(hit, knockTime);
            }
        }
      
    }
}
