using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//4층 이외의 층을 위한 스크립
public class FightRoomEnter : MonoBehaviour
{
    VisitStateOther visitStateOther;

    void Start()
    {
        visitStateOther = GetComponent<VisitStateOther>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for(int i=0; i<visitStateOther.fightRoomName.Length; i++)
            {
                if(visitStateOther.fightRoomVisited[i] == false && this.name == visitStateOther.fightRoomName[i])
                {
                    visitStateOther.fightRoomVisited[i] = true;
                    visitStateOther.fightRoomNumber = i;
                    visitStateOther.CheckVisitedOther(i);
                    return;
                }
            }
        }
    }
}
