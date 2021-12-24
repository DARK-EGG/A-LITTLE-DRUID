using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public void MoveScene()
    {
        SceneManager.LoadScene("0_Main Menu");
        SaveSystem.resetFolder();
        SaveData.Init();
    }
}
