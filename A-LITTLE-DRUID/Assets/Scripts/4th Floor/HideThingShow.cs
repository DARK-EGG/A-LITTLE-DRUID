using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideThingShow : MonoBehaviour
{
    public GameObject[] hideThings;
    public bool calledOneTime = false;
    
    void Start()
    {
        for(int i=0; i<hideThings.Length; i++)
        {
            hideThings[i].SetActive(false);
        }
    }

    private void Update()
    {
        if( calledOneTime == false && BossTalk.canBoss == true)
        {
            StartCoroutine(HideThingShower());
            calledOneTime = true;
        }
    }

    public IEnumerator HideThingShower()
    {
        for(int i = 0; i < hideThings.Length; i++)
        {
            hideThings[i].SetActive(true);
        }

        yield return null;
    }
}
