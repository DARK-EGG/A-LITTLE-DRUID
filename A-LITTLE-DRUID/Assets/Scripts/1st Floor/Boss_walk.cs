using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_walk : StateMachineBehaviour
{
    public float speed = 1.0f;
    Transform player;
    Rigidbody2D rigid;
    Boss boss;

    public float attackRange = 1f;
    public int num;
    public static bool attack = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        num = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Vector2 target = new Vector2(player.position.x, player.position.y);
        
         //dialog 코드 받고 실행되면 코드 지울 부분
        if (!BossTalk.stopTime && BossTalk.canBoss)//대화중이라 가만히 있어야 될 때가 아니면, 보스 전투 가능하면 움직이게
        {
            
            if (boss.CheckCollision(rigid.position))
            {
                num++;
                if (num == 10)
                {
                    Debug.Log("멈춤");
                    rigid.velocity = Vector2.zero;
                    rigid.position = boss.gameObject.transform.position;
                    boss.scanObject = null;
                    num = 0;
                }
                else
                    return;
                
            }

            Vector2 newPos = Vector2.MoveTowards(rigid.position, target, speed * Time.fixedDeltaTime);
            rigid.MovePosition(newPos);
        }
       

        // attackRange에 플레이어 들어오면 공격 애니메이션 실행
        if (Vector2.Distance(player.position, rigid.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            attack = true;
        }
        
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.ResetTrigger("Attack");
        //attack = false;
    }
    
}
