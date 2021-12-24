using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class CloseTheDoor : MonoBehaviour
{
    public GameObject outDoorClose;
    public GameObject inDoorClose;
    public GameObject battleCollider;
    public int roomNumber = 0;
    public bool[] doorCloseSoundPlayed = new bool[4];
    AudioEffect audioEffect;
    resetMoveableArea rMA;

    void Start()
    {
        for (int i = 0; i < doorCloseSoundPlayed.Length; i++)
        {
            doorCloseSoundPlayed[i] = false;
        }

        if (SceneManager.GetActiveScene().buildIndex != 6)
        {
            inDoorClose = null;
        }
        audioEffect = FindObjectOfType<AudioEffect>();
        rMA = FindObjectOfType<resetMoveableArea>();
    }

    // GateCloseColliders에서 지정된 Collider의 영역을 벗어나게 되면, 방을 나가는 문과 들어오는 문이 모두 닫힙니다.
    public void EveryDoorClosed()
    {
        outDoorClose.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 6)
            inDoorClose.SetActive(true);
        battleCollider.SetActive(true);
        audioEffect.DoorCloseSoundPlay();
        //doorCloseSound.Play();
    }

    public void battleEnded()
    {
        outDoorClose.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 6)
            inDoorClose.SetActive(false);
        battleCollider.SetActive(false);
        //doorOpenSound.Play();
        audioEffect.DoorOpenSoundPlay();
    }

    public void PlayerRoomCheck()
    {
        switch (rMA.currentRoomNum)
        {
            case (1):
                roomNumber = 0;
                break;
            case (3):
                roomNumber = 1;
                break;
            case (5):
                roomNumber = 2;
                break;
            case (6):
                roomNumber = 3;
                break;

        }
    }
}
