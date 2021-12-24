using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class SaveSystem
{
    private static string SAVE_FOLDER;
    //private static string SAVE_FOLDER = Application.persistentDataPath + "/Saves/"; // < 모바일
    //private static string SAVE_FOLDER = Application.dataPath + "/Saves/";
    //private static string DIALOG_FOLDER = Application.persistentDataPath + "/Resources/"; <모바일
    //private static string DIALOG_FOLDER = Application.dataPath + "/Resources/";

    public static void PlatformChecker()
    {
        if (SaveData.isAndroid) // 안드로이드일 경우
        {
            SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
        }
        else // PC일 경우
        {
            SAVE_FOLDER = Application.dataPath + "/Saves/";
        }
    }

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void resetFolder()
    {
        if (Directory.Exists(SAVE_FOLDER))
        {
            File.Delete(SAVE_FOLDER + "/save_1.txt");
            File.Delete(SAVE_FOLDER + "/save_2.txt");
            File.Delete(SAVE_FOLDER + "/save_3.txt");
        }
    }

    public static void Save(string saveString)
    {

        if(SceneManager.GetActiveScene().name == "3_1st floor")
        {
            File.WriteAllText(SAVE_FOLDER + "save_1.txt", saveString);
        }
        
        else if (SceneManager.GetActiveScene().name == "4_2nd floor")
        {
            File.WriteAllText(SAVE_FOLDER + "save_2.txt", saveString);
        }

        else if (SceneManager.GetActiveScene().name == "5_3rd floor")
        {
            File.WriteAllText(SAVE_FOLDER + "save_3.txt", saveString);
        }

    }

    public static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFIle = null;

        if (mostRecentFIle != null)
        {
            string saveString = File.ReadAllText(mostRecentFIle.FullName);
            return saveString;
        }
        if (File.Exists(SAVE_FOLDER + "save_1.txt"))
        {
            string saveString1 = File.ReadAllText(SAVE_FOLDER + "save_1.txt");
            return saveString1;
        }
        else
        {
            return null;
        }
        
    }

    public static bool SavefileExist()
    {
        if (File.Exists(SAVE_FOLDER + "save_1.txt"))
        {
            return true;
        }
        else return false;
    }
    public static string Load_1stFloor()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFIle = null;

        if (File.Exists(SAVE_FOLDER + "save_1.txt"))
        {
            string saveString1 = File.ReadAllText(SAVE_FOLDER + "save_1.txt");
            return saveString1;
        }
        else
        {
            Debug.Log("1번째 txt 파일 열기 실패");
            return null;
        }
    }

    public static string Load_2ndFloor()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFIle = null;

        if (File.Exists(SAVE_FOLDER + "save_2.txt"))
        {
            string saveString2 = File.ReadAllText(SAVE_FOLDER + "save_2.txt");
            return saveString2;
        }
        else
        {
            Debug.Log("2번째 txt 파일 열기 실패");
            return null;
        }
    }

    public static string Load_3rdFloor()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFIle = null;

        if (File.Exists(SAVE_FOLDER + "save_3.txt"))
        {
            string saveString3 = File.ReadAllText(SAVE_FOLDER  + "save_3.txt");
            return saveString3;
        }
        else
        {
            Debug.Log("3번째 txt 파일 열기 실패");
            return null;
        }
    }
}
