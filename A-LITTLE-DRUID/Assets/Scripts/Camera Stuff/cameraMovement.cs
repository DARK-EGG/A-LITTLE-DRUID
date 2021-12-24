using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Camera now;
    public Camera change;
    public Camera elseCam;
    public GameObject needswitch;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collisioninfo)
    {
        if (collisioninfo && collisioninfo.gameObject.name == "player_notre")
        {
            Debug.Log(now.name);
            Debug.Log(change.name);
            now.enabled = false;
            elseCam.enabled = false;
            change.enabled = true;
            Switch();
        }
    }

    void Switch()
    {
        player.transform.position = needswitch.transform.position;
    }

}
