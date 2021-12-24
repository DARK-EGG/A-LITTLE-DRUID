using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEscapedFromDoor : MonoBehaviour
{
    CloseTheDoor door;//부모 객체에 있는 문 여닫힘 함수를 불러오기 위함
    EnemyCertainRadius enemyCertainRadius;//리스트 내부에 있는 적의 수를 불러오기 위함

    void Start()
    { 
        door = this.transform.parent.GetComponent<CloseTheDoor>();
        enemyCertainRadius = FindObjectOfType<EnemyCertainRadius>();
    }

    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        door.PlayerRoomCheck();
        if(door.doorCloseSoundPlayed[door.roomNumber] == false && collision.gameObject.CompareTag("Player") && !enemyCertainRadius.enemyInRange.Count.Equals(0))
        {
            if(SceneManager.GetActiveScene().buildIndex == 6)
            {
                door.EveryDoorClosed();
            }
            door.doorCloseSoundPlayed[door.roomNumber] = true;
        }
    }
}
