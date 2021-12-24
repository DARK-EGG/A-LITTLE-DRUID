using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruthRoomPortalActive : MonoBehaviour
{
    public GameObject portalTrtoEr; // portal Truthroom to Endroom

    void Start()
    {
        portalTrtoEr.SetActive(false);
    }    
}
