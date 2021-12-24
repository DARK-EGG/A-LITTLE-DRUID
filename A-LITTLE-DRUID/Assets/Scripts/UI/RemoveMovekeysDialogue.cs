using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMovekeysDialogue : MonoBehaviour
{
    public GameObject movekey;


    public void HideMoveKey(bool hide)
    {
        movekey.SetActive(hide);
    }
}
