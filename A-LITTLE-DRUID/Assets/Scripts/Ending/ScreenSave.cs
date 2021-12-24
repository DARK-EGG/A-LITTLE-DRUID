using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSave : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
