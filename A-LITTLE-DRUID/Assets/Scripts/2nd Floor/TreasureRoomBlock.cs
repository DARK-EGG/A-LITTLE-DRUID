using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보물상자가 있는 방에 들어갔을 때
// 보물상자의 체력이 0이 되었을 경우, 문은 다시 열리며
// 밖으로 내보내는 RoomTransfer가 동작합니다.
// 보물상자괴물이 만들어낸 n개의 크리쳐를 쓰러트리지 않으면 문은 열리지 않습니다.
public class TreasureRoomBlock : MonoBehaviour
{
    public GameObject treasureRoomBlockTilemap;
    public GameObject roomTransfer;
    bool visited; // 최초 1회 방문했을 때에 한해서만 문이 닫힙니다.
    bool treasureRoomClear;
    int treasureBoxCreatureNum;
    public int destroyedClonedEnemyNum = 0;

    AudioEffect audioEffect;

    private void Awake()
    {
        treasureBoxCreatureNum = 6;
    }
    void Start()
    {
        NonBlockTheTreasureRoom();
        roomTransfer.SetActive(false);
        visited = false;
        treasureRoomClear = false;
        audioEffect = FindObjectOfType<AudioEffect>();
    }

    void Update()
    {
       if(treasureBoxCreatureNum == destroyedClonedEnemyNum && treasureRoomClear == false)
        {
            NonBlockTheTreasureRoom();
            audioEffect.DoorOpenSoundPlay();
            treasureRoomClear = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && visited == false)
        {
            BlockTheTreasureRoom();
            visited = true;
        }
    }

    void BlockTheTreasureRoom()
    {
        treasureRoomBlockTilemap.SetActive(true);
        roomTransfer.SetActive(false);
        audioEffect.DoorCloseSoundPlay();
    }

    void NonBlockTheTreasureRoom()
    {
        treasureRoomBlockTilemap.SetActive(false);
        roomTransfer.SetActive(true);
    }
}
