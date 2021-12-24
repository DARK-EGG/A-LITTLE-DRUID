using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적의 현재 상태 변수 -> stagger 넉백에서 사용(혹시 몰라 다른 상태변수들도 넣음)
public enum EnemyState          
{
    idle,
    walk,
    attack,
    stagger
}
/*
 * 몬스터 요구사항
 * rigidbody2d - kinematic, freeze rotation Z
 * polygon collider2d - istrigger : true 
 */

//Mob 움직임 관련 스크립트
//Mob마다 들어있음
public class MobMove : MonoBehaviour
{
    GameObject player;
    GameObject scanObject;

    /*common*/
    // 1 = 플레이어 따라다니기, 2 = 랜덤 , 3 = 양옆으로만 , 4 = 위아래로 , 5 = 갑자기 튀어나감
    public int mNum = 1; //몹 움직이는 모양
    public float speed; //움직임 속도
    public float range = 3; //몹이 움직이기 시작하는 플레이어와 몹의 거리
    public Animator anim;
    public bool isAnim = false; //애니매이션, 애니매이션 존재 여부
    public Vector2 leftUp;
    public Vector2 rightDown; //몬스터 움직임 범위 - 좌측상단, 우측하단 좌표 (한방향 제외)

    /*위,아래 좌,우 직진 몹*/
    public float distance; //한 방향으로만 이동 시 거리 설정 가능!
    public bool straightDirection; //true로 설정 시 위, 오른쪽으로 먼저 간다

    /*갑자기 튀어나감에서 사용*/
    public int gotime = 100;

    /*설정 필요 없음*/
    Vector2 homePos;
    Vector2 enemyPos;
    Vector2 playerPos; //적 원래, 적, 플레이어 위치
    public int num;
    public int randx, randy; 
    public EnemyState currentState; 
    private PolygonCollider2D col; //??


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        col = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.idle;
        homePos = gameObject.transform.position;
        num = 0;
        randx = 0;
        randy = 0;
    }

    void Update()
    {
        //적(스크립트 가진 본인)의 위치
        enemyPos = gameObject.transform.position;
        //Player 위치
        playerPos = player.transform.position;
        //현재 상태
        currentState = EnemyState.idle;
        //몬스터 이동
        MonsterMove(enemyPos, playerPos);
    }

    public void MonsterMove(Vector2 enemyPos, Vector2 playerPos)
    {
        //대화 중 몬스터 멈춤
        if (DialogueUI.isAction || BossTalk.stopTime) { }

        //범위 내에 플레이어가 있을 때
        else if (Vector2.Distance(playerPos, enemyPos) < range)
        {
            switch (mNum)
            {
                case 1://따라오는 몬스터
                    if (!CheckCollision(enemyPos))
                        FollowMove(playerPos, enemyPos);
                    break;

                case 2://랜덤으로 움직이는 몬스터

                    //방향 변경
                    if (num == 0)
                    {
                        randx = Random.Range(-1, 2);
                        randy = Random.Range(-1, 2);
                    }
                    //따라감
                    if (num >= speed * 700)
                    {
                        if (!CheckCollision(enemyPos))
                            FollowMove(playerPos, enemyPos);
                    }
                    //랜덤
                    else
                    {
                        RandomMove(enemyPos);
                    }

                    num++;

                    //방향 변경 위한 초기화
                    if (num >= speed * 1000)
                        num = 0;

                    break;

                case 3://x축방향 이동
                    MoveStraight(enemyPos, true);
                    break;

                case 4://y축방향 이동
                    MoveStraight(enemyPos, false);
                    break;

                case 5://갑자기 플레이어 방향으로 튀어나가는 몬스터
                    if (num >= 1000)
                        num = 0;
                    else if ((num - gotime > 0 && num - gotime < 50 / speed) || (gotime - num > 0 && gotime - num < 50 / speed))
                    {
                        if (enemyPos.x > leftUp.x && enemyPos.x < rightDown.x && enemyPos.y < leftUp.y && enemyPos.y > rightDown.y)
                        {
                            transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
                            anim.SetBool("Attack", true);
                        }
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(enemyPos, homePos, speed * Time.deltaTime);
                        anim.SetBool("Attack", false);
                    }
                    num++;
                    break;
            }
        }

        //멀리 있는 경우 제자리로 돌아감
        else
        {
            transform.position = Vector2.MoveTowards(enemyPos, homePos, speed * Time.deltaTime);//제자리로
        }
    }

    //따라오는 몬스터
    public Vector2 FollowMove(Vector2 playerPos, Vector2 enemyPos)
    {
        //애니매이션
        if (isAnim)
        {
            if (enemyPos.x < playerPos.x)
                anim.SetBool("isRight", true);
            else if (enemyPos.x > playerPos.x)
                anim.SetBool("isLeft", true);
            else
            {
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
            }
        }
        /*//움직임 범위 내에서만 플레이어 따라감
        if (enemyPos.x > leftUp.x && enemyPos.x < rightDown.x && enemyPos.y < leftUp.y && enemyPos.y > rightDown.y)
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
        }
        //움직임 범위 밖에서는 제자리로 이동
        else transform.position = Vector2.MoveTowards(enemyPos, homePos, speed * Time.deltaTime);*/
        transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
        return enemyPos;
    }

    //랜덤으로 움직이는 몬스터
    public void RandomMove(Vector2 enemyPos)
    {
        //애니매이션
        if (isAnim)
        {
            if (randx == 1)
                anim.SetBool("isRight", true);
            else if (randx == -1)
                anim.SetBool("isLeft", true);
            else
            {
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
            }
        }
        //범위 나가면 randx randy 부호 변경
        if (enemyPos.x <= leftUp.x || enemyPos.x >= rightDown.x || enemyPos.y >= leftUp.y || enemyPos.y <= rightDown.y)
        {
            randx = -randx; randy = -randy;
        }
        transform.position = Vector2.MoveTowards(enemyPos, enemyPos + new Vector2(randx, randy), speed * Time.deltaTime);
    }

    //직진 몬스터
    public void MoveStraight(Vector2 enemyPos, bool checkMoveDirection)
    {
        //x축 방향(가로)
        if (checkMoveDirection)
        {
            if (straightDirection)
            {
                enemyPos.x += speed;
                if (isAnim)
                    anim.SetBool("isRight", true);
            }
            else
            {
                enemyPos.x -= speed;
                if (isAnim)
                    anim.SetBool("isRight", false);
            }
            transform.position = new Vector2(enemyPos.x, homePos.y);
        }
        //y축방향(세로)
        else
        {
            if (straightDirection)
                enemyPos.y += speed;
            else
                enemyPos.y -= speed;
            transform.position = new Vector2(homePos.x, enemyPos.y);
        }
        num++;
        if (speed * num >= distance)
        {
            num = 0;
            straightDirection = !straightDirection;
        }
    }

    //콜라이더에 닿았는지 확인
    public bool CheckCollision(Vector2 enemyPos)
    {
        if (scanObject && scanObject.CompareTag("Collider"))
        {
            //집 방향으로 이동
            Vector2 difference = - homePos + (Vector2)scanObject.transform.position;
            difference = difference.normalized;
            transform.position = Vector2.MoveTowards(enemyPos, homePos, speed * Time.deltaTime);
            if (enemyPos == homePos)
                scanObject = null;
            return true;
        }
        return false;
    }

    /* KnockBack.cs에서 사용하는 메소드: 충격량을 얼마나 유지시키는가에 대한 내용 */
    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(getBack(myRigidbody, knockTime));
    }

    public IEnumerator getBack(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);                            // knockTime만큼 넉백되도록 함
            myRigidbody.velocity = Vector2.zero;                                   // knockTime이 지나면 다시 원래대로
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.isKinematic = true;
        }

    }

    //콜라이더 관련
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Collider"))
            scanObject = collision.gameObject;
        if (scanObject && scanObject.CompareTag("Collider"))
        {
            col.isTrigger = false;
        }
    }*/
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collider"))
            scanObject = collision.gameObject;
        if (scanObject && scanObject.CompareTag("Collider"))
        {
            col.isTrigger = false;
        }
    }
    /*public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collider"))
            scanObject = collision.gameObject;
        if (scanObject && scanObject.CompareTag("Collider"))
        {
            col.isTrigger = false;
        }
    }*/
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collider"))
            scanObject = collision.gameObject;
        if (scanObject && scanObject.CompareTag("Collider"))
        {
            col.isTrigger = false;
        }
    }
    /*public void OnCollisionExit2D(Collision2D collision)
    {
        scanObject = null;
        col.isTrigger = true;
    }*/
    public void OnTriggerExit2D(Collider2D collision)
    {
        scanObject = null;
        col.isTrigger = true;
    }
    }
