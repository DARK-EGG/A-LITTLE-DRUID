using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Boss
//3층 보스 공격 스크립트
public class WindBoss : MonoBehaviour
{
    private int time = 0;
    public int attackTime = 1000;
    bool right = true;
    public float moveRange = 2f;
    public float attackRange = 2f;
    public float mid = 3f;
    public int attackGage = 1;
    public GameObject player;
    public float power = 1f;
    public AudioSource windAudio;
    Animator anim;

    Vector2 bossPos;
    Vector2 playerPos;

    GameObject scanObject;

    void Start()
    {
        anim = GetComponent<Animator>();
        right = true;
    }

    void Update()
    {
        bossPos = transform.position;
        playerPos = player.transform.position;
        if (BossTalk.canBoss)
        {
            //바람 공격, 공격 애니매이션
            if (time > attackTime * 2)
                time = 0;
            else if (time > attackTime && Vector2.Distance(playerPos, bossPos) < attackRange)
            {
                anim.SetBool("Attack", true);
                if(!windAudio.isPlaying)
                    windAudio.Play();
                player.transform.position = Vector2.MoveTowards(playerPos, bossPos, power * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            //이동
            if (right)
                transform.position = new Vector2(gameObject.transform.position.x + 0.005f, gameObject.transform.position.y);
            else
                transform.position = new Vector2(gameObject.transform.position.x - 0.005f, gameObject.transform.position.y);
            if (transform.position.x < mid - moveRange)
                right = true;
            else if (transform.position.x > mid + moveRange)
                right = false;
            time++;
        }
    }

    //몸에 닿았을 시 공격
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerStatus playerStatus;
        playerStatus = FindObjectOfType<PlayerStatus>();

        scanObject = collision.gameObject;
        if (scanObject.CompareTag("Player"))
        {
            playerStatus.pStatus.playerCurrentHp -= attackGage;
        }
    }
}
