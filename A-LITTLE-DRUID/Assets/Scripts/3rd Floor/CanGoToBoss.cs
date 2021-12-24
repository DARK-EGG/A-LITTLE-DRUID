using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manager
//중간보스를 다 죽였을 시 보스전 들어가는 문 열리는 스크립트
public class CanGoToBoss : MonoBehaviour
{
    public GameObject doorOpenTilemap;
    //bool door = false;
    public GameObject audioManger;
    AudioEffect audioEffect;

    void Start()
    {
        doorOpenTilemap.SetActive(false);
        audioEffect = audioManger.GetComponent<AudioEffect>();
    }

    public void OpenBossDoor()
    {
        doorOpenTilemap.SetActive(true);
        for (int i = 0; i < doorOpenTilemap.transform.childCount; i++)
        {
            doorOpenTilemap.transform.GetChild(i).gameObject.SetActive(true);
        }
        audioEffect.DoorOpenSoundPlay();
    }
}
