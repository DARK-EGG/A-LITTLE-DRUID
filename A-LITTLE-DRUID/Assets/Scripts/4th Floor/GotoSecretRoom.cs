using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoSecretRoom : MonoBehaviour
{
    GameObject player;
    public int attackFor = 10;
    public int youAttacked = 0;
    public float distanceNum;
    bool secretRoomDoorOpened = false;
    public GameObject normalStateCollider;
    public GameObject secretRoomOpenCollider;
    public GameObject secretRoomDoor;
    public AudioSource hitWallSound;
    public AudioSource doorOpenSound;

    bool attack_Down = false;

    void Start()
    {
        normalStateCollider.SetActive(true);
        secretRoomOpenCollider.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        secretRoomDoor.SetActive(false);
    }

    void Update()
    {
        if(Vector2.Distance(player.transform.position, this.transform.position) <= distanceNum)
        {
            if((Input.GetKeyDown(KeyCode.T)||attack_Down) && secretRoomDoorOpened == false)
            {
                hitWallSound.Play();
                youAttacked += 1;
            }
        }
        if(secretRoomDoorOpened == false && (attackFor == youAttacked))
        {
            doorOpenSound.Play();
            Debug.Log("Now you can go to secret room");
            secretRoomOpenCollider.SetActive(true);
            normalStateCollider.SetActive(false);
            secretRoomDoor.SetActive(true);
            secretRoomDoorOpened = true;

        }
        InitMobileVar();
    }
    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "A":
                attack_Down = true;
                break;

        }
    }

    private void InitMobileVar()
    {
        attack_Down = false;
    }
}
