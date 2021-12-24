using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitState : MonoBehaviour
{
    public int[] roomNumber;
    public bool[] visited;
    resetMoveableArea resetMoveableArea;
    EnemyCertainRadius enemyCertainRadius;

    void Start()
    {
        resetMoveableArea = FindObjectOfType<resetMoveableArea>();
        enemyCertainRadius = FindObjectOfType<EnemyCertainRadius>();
    }

    public void CheckVisited(int room)
    {
        if (roomNumber[room] == resetMoveableArea.currentRoomNum)
        {
            visited[room] = true;
            enemyCertainRadius.EnemyCount(room);
            enemyCertainRadius.MiddleBossCount(room);
        }
    }
}
