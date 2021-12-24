using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contiue : MonoBehaviour
{
    // Start is called before the first frame update
    UnityEngine.UI.Button button;
    void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        Debug.Log(SaveData.Load_1st_JW());
        if (!SaveData.Load_1st_JW())
            button.interactable = false;
    }


}
