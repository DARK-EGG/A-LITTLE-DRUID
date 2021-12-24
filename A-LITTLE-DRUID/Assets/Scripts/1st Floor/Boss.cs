using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public GameObject scanObject;
    public Rigidbody2D rigid;
    Animator animator;

    public bool isFlipped = false;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        LookAtPlayer();
    }
    // 플레이어 방향쪽으로 좌우 뒤집기
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }

        }

    }

    public bool CheckCollision(Vector2 enemyPos)
    {
        if (scanObject && scanObject.CompareTag("Collider"))
        {
            Vector2 difference = rigid.transform.position - scanObject.transform.position;
            difference = difference.normalized * 0.01f * 1f;
            transform.position = Vector2.MoveTowards(enemyPos, difference + enemyPos, Time.deltaTime);
            return true;
        }
        return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        scanObject = collision.gameObject;
    }
}
