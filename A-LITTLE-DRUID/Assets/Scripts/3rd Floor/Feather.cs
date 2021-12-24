using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [3층 중간보스 독수리의 깃털 스크립트
 * - MobMove 클래스 상속받으려고 했지만 실패..
 */

public class Feather : MonoBehaviour
{
    public float speed;         // 속도

    private GameObject player;
    private Vector2 enemyPos;
    private Vector2 playerPos;

    private MakeFeather script = null;

    float t = 0f;               // 공격 주기(시간)

    private void Awake()
    {
        script = GameObject.Find("Eagle").GetComponent<MakeFeather>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        enemyPos = gameObject.transform.position;
        playerPos = player.transform.position;
        transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);  // 플레이어를 따라 이동
        
        t += Time.deltaTime;
        if (t > 3f)             // 3초가 지나면 사라짐
        {
            script.ReturnPool(this.gameObject);
            t = 0f;
        }
    }
}
