using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LastBossClear : MonoBehaviour
{
    public static int memoryPieceNum;
    public GameObject visitTruthRoomPortal;
    public GameObject nonVTRP;
    
    void Start()
    {
        visitTruthRoomPortal.SetActive(false);
        nonVTRP.SetActive(false);
        int mpn_1st = SaveData.Load_1st_MP();
        Debug.Log(mpn_1st);
        int mpn_2nd = SaveData.Load_2nd_MP();
        Debug.Log(mpn_2nd);
        int mpn_3rd = SaveData.Load_3rd_MP();
        Debug.Log(mpn_3rd);
        memoryPieceNum = mpn_1st + mpn_2nd + mpn_3rd;
    }

    //해당 함수는, 최종보스의 체력이 0이 되었을 때 호출됩니다.
    public void LastBossClearPortalOpen()
    {
       
        Debug.Log("기억의 조각: " + memoryPieceNum);

        switch (memoryPieceNum)
        {
            case (9):
                visitTruthRoomPortal.SetActive(true);
                break;
            default:
                nonVTRP.SetActive(true);
                break;
        }
    }
}
