using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// For 2nd Floor Boss Room
public class BossRoomDoor : MonoBehaviour
{
    public GameObject inDoorClosed; // 보스방 내부의 문의 이미지에 대한 정보가 들어있는 타일맵
    public GameObject roomOutTransfer;
    EnemiesHP enemiesHP;
    bool visited;
    bool bossDead;//문열림 소리 update 내부에서 한 번만 재생하기 위함
    AudioEffect audioEffect;
    MeetBoss meetBoss; //보스방 바깥의 문을 들고오기위함

    void Start()
    {
        enemiesHP = FindObjectOfType<EnemiesHP>();
        inDoorClosed.SetActive(true);
        roomOutTransfer.SetActive(true);
        visited = false;
        bossDead = false;
        audioEffect = FindObjectOfType<AudioEffect>();
        meetBoss = FindObjectOfType<MeetBoss>();
    }

    void Update()
    {
        if(enemiesHP.floorLastBossHp <= 0 && bossDead == false)
        {
            SecondFloorBossDead();
            bossDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && visited == false)
        {
            roomOutTransfer.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && visited == false)
        {
            // Debug.Log("Close the 2nd Floor Boss Room's door");
            inDoorClosed.SetActive(false);
            meetBoss.bossDoorBlock.SetActive(false);
            audioEffect.DoorCloseSoundPlay();
            visited = true;
        }
    }

    public void SecondFloorBossDead()
    {
        inDoorClosed.SetActive(true);
        roomOutTransfer.SetActive(true);
        meetBoss.bossDoorBlock.SetActive(true);
        audioEffect.DoorOpenSoundPlay();
    }
}
