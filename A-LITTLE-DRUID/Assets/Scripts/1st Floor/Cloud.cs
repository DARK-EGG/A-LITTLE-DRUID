using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject cloud;
    public float speed;
    public bool goRight = true;
    private float bLeft = 0.808f;
    private float bRight = 6.888f;
    Vector3 cloudPos;
    public Transform targetL;
    public Transform targetR;
    Animator ani;

    void Start()
    {
        cloudPos = cloud.transform.position;
    }

    void Update()
    {
        
        if (Boss_walk.attack && BossTalk.canBoss) // 보스가 attack 취할 때
        {
            
            if (goRight)
            {
                cloudPos.x += speed * Time.deltaTime;
                if (cloudPos.x >= bRight)
                {
                    goRight = false;
                }
            }
            
            else
            {
                cloudPos.x -= speed * Time.deltaTime;
                if (cloudPos.x <= bLeft)
                {
                    goRight = true;
                }
            }

            cloud.transform.position = new Vector3(cloudPos.x, cloudPos.y, cloudPos.z);

            if(PlayerAttackKeyEvent.BossDead == true)
            {
                cloud.SetActive(false);
            }
        }
        

        // transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        //if (Boss_walk.attack)
        if(!Boss_walk.attack)
        {
            /*
            if (transform.position.x == targetL.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetR.position, speed * Time.deltaTime);
                Boss_walk.attack = false;
            }

            if (transform.position.y == targetL.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetL.position, speed * Time.deltaTime);
                Boss_walk.attack = false;
            }
            */
            //ani.Play("Cloud");
        }
        

    }
}
