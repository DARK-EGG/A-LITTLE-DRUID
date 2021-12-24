using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingChange : MonoBehaviour
{
    public static int whichEnding = 0;
    /* 1 : 전부 다 모았고 두고감
     * 2 : 모름
     * 3 : 전부 다 모았고 들고감*/
    public static void checkEndingNum()
    {
        if (LastBossClear.memoryPieceNum == 9)
            whichEnding = 1;
        else if (LastBossClear.memoryPieceNum < 9)
            whichEnding = 2;

        Debug.Log("################Which Ending" + whichEnding);
    }
}
