using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//방을 이동할 때마다, 카메라가 다닐 수 있는 영역을 바꿔줄 때 쓰는 스크립트
public class resetMoveableArea : MonoBehaviour
{
    [System.Serializable]
    public class resetArea
    {
        public float[] limits = new float[4];
    }
    playerfollow playerfollow;
    public int howManyRooms;
    public int currentRoomNum = 0;
    VisitState visitState;
    [SerializeField]
    public resetArea[] resetAreas;

    void Start()
    {
        playerfollow = gameObject.GetComponent<playerfollow>();
        visitState = FindObjectOfType<VisitState>();
    }

    void Update()
    {
        
    }
    public void ResettingMoveArea()
    {
        playerfollow.leftLimit = resetAreas[currentRoomNum].limits[0];
        playerfollow.rightLimit = resetAreas[currentRoomNum].limits[1];
        playerfollow.bottomLimit = resetAreas[currentRoomNum].limits[2];
        playerfollow.topLimit = resetAreas[currentRoomNum].limits[3];
            //여닫힘 방으로 VisitState의 roomNumber에 들어가있는 방의 경우에만 실행됨
            for (int i= 0; i < visitState.roomNumber.Length; i++)
            {
                if (currentRoomNum == visitState.roomNumber[i])
                    visitState.CheckVisited(i);
            }
           
    }
}
