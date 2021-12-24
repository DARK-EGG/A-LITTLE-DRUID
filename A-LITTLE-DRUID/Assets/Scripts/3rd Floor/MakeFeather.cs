using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [3층 중간보스 Eagle의 깃털 생성 공격기]
 * - 오브젝트 풀링 기법
 */

public class MakeFeather : MonoBehaviour
{
    private GameObject player;

    /* pool을 담을 부모 Object */
    private Transform parent = null;

    /* 생성할 프리팹 (Feather) */
    [SerializeField]
    private GameObject prefab;

    /* feather을 담을 pool 리스트 */
    [SerializeField]
    private List<GameObject> pool = null;

    public float range = 2;
    public bool isAttack = false;

    public void Awake()
    {
        parent = GameObject.Find("Object Pool").transform;  // 깃털들을 담을 상위 빈 오브젝트
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        pool = new List<GameObject>();
        AddPool(prefab, 3);                                 // pool을 깃털 3개로 초기화

        StartCoroutine(TimeSleep());
    }

    /* 공격기 시간 지연 코루틴 : 2초에 한 번씩 깃털 생성 */
    IEnumerator TimeSleep()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            isAttack = false;
            yield return new WaitForSeconds(2.0f);
            isAttack = true;
            if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= range)
                PopObject(prefab);              // 공격 타이밍에 Pool에서 오브젝트 pop
        }
    }

    /* Pool에 오브젝트를 추가하는 메소드 : length 개수만큼 오브젝트 생성 */
    public void AddPool(GameObject obj, int length)
    {
        for (int i=0; i<length; i++)
        {
            pool.Add(Instantiate(prefab, parent) as GameObject);
            pool[i].SetActive(false);
        }
    }

    /* 꺼낸 오브젝트를 Pool에 다시 넣는 메소드 */
    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);

        /* 위치 초기화 */
        obj.transform.position = gameObject.transform.position;
        obj.transform.SetParent(parent);
    }

    /* Pool에서 오브젝트를 꺼내는 메소드 */
    public void PopObject(GameObject obj)
    {
        GameObject temp;

        if (parent.childCount > 0)
            temp = parent.GetChild(0).gameObject;
        else
        {
            AddPool(obj, 1);                            // Pool 안에 오브젝트가 없다면 생성
            temp = parent.GetChild(0).gameObject;
        }

        /* 위치 초기화 */
        temp.transform.position = gameObject.transform.position;
        temp.transform.SetParent(parent);
        temp.SetActive(true);
    }
}
