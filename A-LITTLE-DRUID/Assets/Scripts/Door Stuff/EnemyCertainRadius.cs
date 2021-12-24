using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//여닫힘방에서 적이 전부 사라졌는지를 판단하기 위해서 만든 스크립트
public class EnemyCertainRadius : MonoBehaviour
{
    public Vector2[] center;
    public Vector2[] distance;
    [SerializeField]
    public List<GameObject> enemyInRange; // 여기선 middleBoss와 enemy를 함께 list에 넣습니다.
    public int roomNumm;
    EnemiesHP enemiesHP;
    CloseTheDoor door;

    void Start()
    {
        enemiesHP = FindObjectOfType<EnemiesHP>();
        door = FindObjectOfType<CloseTheDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //영역 내부의 적의 수를 세어줍니다.
    //영역을 지정하는 방법은 방의 중심이 되는 포인트와, 꼭지점의 좌표를 각각 center와 distance에 입력합니다
    //(방의 개수가 많을 경우도 있기에 배열로 구성해, Distance값을 달리 얻을 수 있도록 했습니다.
    public void EnemyCount(int t)
    {
        Debug.Log(t);
        for (int i = 0; i < enemiesHP.Enemies.Length; i++)
        {
            if (!enemyInRange.Contains(enemiesHP.Enemies[i]) && Vector2.Distance(center[t], distance[t]) > Vector2.Distance(center[t], enemiesHP.Enemies[i].transform.position))
            {
                if (enemiesHP.Enemies[i].name != "LongarmDS" && enemiesHP.Enemies[i].name != "HitRange")
                {
                    enemyInRange.Add(enemiesHP.Enemies[i]);
                }
            }
        }
        Debug.Log(enemyInRange.Count);
    }
    public void MiddleBossCount(int r)
    {
        for (int i = 0; i < enemiesHP.MiddleBoss.Length; i++)
        {
            if (!enemyInRange.Contains(enemiesHP.MiddleBoss[i]) && Vector2.Distance(center[r], distance[r]) > Vector2.Distance(center[r], enemiesHP.MiddleBoss[i].transform.position))
            {
                enemyInRange.Add(enemiesHP.MiddleBoss[i]);
            }
        }
    }

    public void EnemyCrushed(int t)
    {
        for(int i=0; i<enemyInRange.Count; i++)
        {
            if (enemyInRange[i].Equals(enemiesHP.Enemies[t]))
            {
                enemyInRange.RemoveAt(i);
            }
        }
        if (enemyInRange.Count.Equals(0))
        {
            door.battleEnded();
        }
    }

    public void MiddleBossCrushed(int t)
    {
        for (int i = 0; i < enemyInRange.Count; i++)
        {
            if (enemyInRange[i].Equals(enemiesHP.MiddleBoss[t]))
            {
              enemyInRange.RemoveAt(i);
            }
        }
        if (enemyInRange.Count.Equals(0))
        {
            door.battleEnded();
        }
    }
}
