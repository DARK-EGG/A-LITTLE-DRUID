using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitStateOther : MonoBehaviour
{
    public string[] fightRoomName;//해당 컬라이더를 지나쳐서 들어가게되는 방(GameObject)의 이름들
    public int fightRoomNumber;
    public bool[] fightRoomVisited;
    EnemyCertainRadius enemyCertainRadius;
    void Start()
    {
        fightRoomVisited = new bool[fightRoomName.Length];
        enemyCertainRadius = FindObjectOfType<EnemyCertainRadius>();
    }

    public void CheckVisitedOther(int room)
    {
        enemyCertainRadius.EnemyCount(room);
    }
}
