using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadEndingInfo : MonoBehaviour
{
    public static bool Load()
    {
        if (File.Exists(Application.dataPath + "/Saves/endingInfo.txt") || File.Exists(Application.persistentDataPath + "/Saves/endingInfo.txt"))
        {
            string playerInfoString;
            if (SaveData.isAndroid == true)
                playerInfoString = File.ReadAllText(Application.persistentDataPath + "/Saves/EndingInfo.txt");
            else
                playerInfoString = File.ReadAllText(Application.dataPath + "/Saves/EndingInfo.txt");

            EndingInfoToSave endingInfoToSave = JsonUtility.FromJson<EndingInfoToSave>(playerInfoString);
            EndingInfo.SetEndingInfo(endingInfoToSave.ending1, endingInfoToSave.ending2, endingInfoToSave.ending3);
            return true;
        }
        else
            return false;
    }

    public static void Save()
    {
        string endingInfoString = JsonUtility.ToJson(new EndingInfoToSave());
        if(SaveData.isAndroid == true)
            File.WriteAllText(Application.persistentDataPath + "/Saves/EndingInfo.txt", endingInfoString);
        else
            File.WriteAllText(Application.dataPath + "/Saves/EndingInfo.txt", endingInfoString);
    }
}

public static class EndingInfo
{
    public static bool ending1;
    public static bool ending2;
    public static bool ending3;

    public static void SetEndingInfo1(bool ending1)
    {
        EndingInfo.ending1 = ending1;
    }
    public static void SetEndingInfo2(bool ending2)
    {
        EndingInfo.ending2 = ending2;
    }
    public static void SetEndingInfo3(bool ending3)
    {
        EndingInfo.ending3 = ending3;
    }
    public static void SetEndingInfo(bool ending1, bool ending2, bool ending3)
    {
        EndingInfo.ending1 = ending1;
        EndingInfo.ending2 = ending2;
        EndingInfo.ending3 = ending3;
    }
}

class EndingInfoToSave
{
    public bool ending1 = true;
    public bool ending2 = true;
    public bool ending3 = true;

    public EndingInfoToSave()
    {
        ending1 = EndingInfo.ending1;
        ending2 = EndingInfo.ending2;
        ending3 = EndingInfo.ending3;
    }
}
