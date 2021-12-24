using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Jewel : MonoBehaviour
{
    public GameObject floor_jewel;
    public GameObject player;
    Vector3 playerPlace;

    private void Start()
    {

    }
    void Update()
    {
        if (PlayerAttackKeyEvent.BossDead == true) //if(PlayerAttackKeyEvent.BossDead == true)
        {
            Debug.Log("보석 드랍");
            playerPlace = player.gameObject.transform.position;
            floor_jewel.transform.position = new Vector3(playerPlace.x, playerPlace.y + (float)0.3, playerPlace.z);
            floor_jewel.GetComponent<Animator>().SetTrigger("see_jewel");
            
        }
    }

    void enable_jewel()
    {
        floor_jewel.SetActive(false);
    }
}
