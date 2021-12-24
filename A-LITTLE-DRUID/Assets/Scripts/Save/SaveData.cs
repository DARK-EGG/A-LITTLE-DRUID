using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public static bool isAndroid;

    void Awake()
    {

        if (Application.platform != RuntimePlatform.Android)
            isAndroid = false;
        else
            isAndroid = true;
        SaveSystem.PlatformChecker();
        SaveSystem.Init();
        Init();
    }

    public static void Init()
    {
        int killmobs = 0;
        int countMemoryPieces = 0;
        bool checkJewel = false;
        int languageIndex = 0;
    }

    public void Save()
    {
        int killmobs = PlayerAttackKeyEvent.removedEnemyNum;
        //int killmobs = PlayerAttackKeyEvent.removedEnemyNum;
        int countMemoryPieces = PiecesOfMemory.num;
        bool checkJewel = true;
        int languageIndex = LanguageSaver.languageIndex;

        SaveObject saveObject = new SaveObject
        {
            killmobs = killmobs,
            countMemoryPieces = countMemoryPieces,
            checkJewel = checkJewel,
            languageIndex = languageIndex
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);
    }

    public void Load()
    {
        string saveString = SaveSystem.Load();
        if(saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        }
        else
        {
            Debug.Log("No Save");
        }
    }

    // 1층 기억의 조각 갯수 확인
    public static int Load_1st_MP()
    {
        string saveString1 = SaveSystem.Load_1stFloor();
        if (saveString1 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString1);
            return saveObject.countMemoryPieces;
        }
        else
        {
            return 0;
        }
    }

    // 1층 죽인 몹 갯수 확인
    public static int Load_1st_KM()
    {
        string saveString1 = SaveSystem.Load_1stFloor();
        if (saveString1 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString1);
            return saveObject.killmobs;
        }
        else
        {
            return 0;
        }
    }

    // 1층 보석 획득 확인
    public static bool Load_1st_JW()
    {
        string saveString1 = SaveSystem.Load_1stFloor();
        if (saveString1 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString1);
            return saveObject.checkJewel;
        }
        else
        {
            return false;
        }
    }

    // 2층 기억의 조각 갯수 확인
    public static int Load_2nd_MP()
    {
        string saveString2 = SaveSystem.Load_2ndFloor();
        if (saveString2 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString2);
            return saveObject.countMemoryPieces;
        }
        else
        {
            return 0;
        }
    }

    // 2층 죽인 몹 갯수 확인
    public static int Load_2nd_KM()
    {
        string saveString2 = SaveSystem.Load_2ndFloor();
        if (saveString2 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString2);
            return saveObject.killmobs;
        }
        else
        {
            return 0;
        }
    }

    // 2층 보석 획득 확인
    public static bool Load_2nd_JW()
    {
        string saveString2 = SaveSystem.Load_2ndFloor();
        if (saveString2 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString2);
            return saveObject.checkJewel;
        }
        else
        {
            return false;
        }
    }

    // 3층 기억의 조각 갯수 확인
    public static int Load_3rd_MP()
    {
        string saveString3 = SaveSystem.Load_3rdFloor();
        if (saveString3 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString3);
            return saveObject.countMemoryPieces;
        }
        else
        {
            return 0;
        }
    }

    // 3층 죽인 몹 갯수 확인
    public static int Load_3rd_KM()
    {
        string saveString3 = SaveSystem.Load_3rdFloor();
        if (saveString3 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString3);
            return saveObject.killmobs;
        }
        else
        {
            return 0;
        }
    }

    // 3층 보석 획득 확인
    public static bool Load_3rd_JW()
    {
        string saveString3 = SaveSystem.Load_3rdFloor();
        if (saveString3 != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString3);
            return saveObject.checkJewel;
        }
        else
        {
            return false;
        }
    }


    private class SaveObject
    {
        public int killmobs;
        public int countMemoryPieces;
        public bool checkJewel;
        public int languageIndex;
    }

    private class DialogObject
    {
        public int index;
        public string key;
        public string value;
    }
}
