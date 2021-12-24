using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * [2층 상자 몹 스크립트]
 * 
 * 플레이어 공격 시 몬스터를 생성
 */

public class Chest : MonoBehaviour
{
    /* 상자 공격 시 생성될 몬스터들의 프리팹 */
    public GameObject Skullsmall;
    public GameObject Skull;
    public GameObject Reaper;

    public AudioSource MonsterHitSound;
    public Vector3 MonsterOut;

    private Rigidbody2D rigid;
    private TreasureRoomBlock treasureRoomBlock;
    private int cnt = 0;

    public GameObject[] chestEnemies;
    GameObject player;
    [SerializeField]
    bool[] cEDeadCheck;
    Vector3[] position;

    private void Awake()
    {
        treasureRoomBlock = FindObjectOfType<TreasureRoomBlock>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        cEDeadCheck = new bool[chestEnemies.Length];
        position = new Vector3[chestEnemies.Length];
        for(int i=0; i < chestEnemies.Length; i++)
        {
            cEDeadCheck[i] = false;
            chestEnemies[i].SetActive(false);
            position[i] = chestEnemies[i].transform.position;
            chestEnemies[i].transform.position = new Vector2(-9f, -2.2f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < chestEnemies.Length; i++)
        {
            if (cEDeadCheck[i] == false)
            {
                if (!(chestEnemies[i].transform.position.x > -10f && chestEnemies[i].transform.position.x < -2.5f))
                {
                    cEDeadCheck[i] = true;
                    treasureRoomBlock.destroyedClonedEnemyNum += 1;
                }
            }
            
        }
        //if (trbUse && (pPosition.x > -6.15f && pPosition.x < -2.6f))
        //          trb.destroyedClonedEnemyNum += 1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision && collision.CompareTag("HitBox")) // 플레이어의 HitBox와 접촉 시 몬스터 생성
        {
            cnt++;
            if(cnt == 1)                                // 몬스터는 한 번만 생성하도록 함
            {
                rigid.transform.position = MonsterOut;  // Chest 몬스터 삭제
                MonsterHitSound.Play();

                StartCoroutine("HitDelay");
            }
        }
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(3f);                                         // 3초 딜레이 이후에 몬스터 등장
        for (int i = 0; i < chestEnemies.Length; i++)
        {
            chestEnemies[i].SetActive(true);
            chestEnemies[i].transform.position = position[i];
        }
        //Instantiate(Skullsmall, new Vector3(-3.55f, 8.2f, 0f), Quaternion.identity);
        //Instantiate(Skullsmall, new Vector3(-4.85f, 8.2f, 0f), Quaternion.identity);
        //Instantiate(Skull, new Vector3(-3.15f, 8.5f, 0f), Quaternion.identity);
        //Instantiate(Skull, new Vector3(-5.25f, 8.5f, 0f), Quaternion.identity);
        //Instantiate(Reaper, new Vector3(-4.55f, 8.1f, 0f), Quaternion.identity);
        //Instantiate(Reaper, new Vector3(-3.75f, 8.1f, 0f), Quaternion.identity);
    }
}
