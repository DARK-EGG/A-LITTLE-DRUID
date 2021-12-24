using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{

    public float cameraChangeX;
    public float cameraChangeY;
    public Vector3 playerChange; // player을 얼마나 움직일 건지.
    private cameraMovement cam;
    private playerfollow fcam;
    private Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        fcam = Camera.main.GetComponent<playerfollow>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            fcam.leftLimit += cameraChangeX;
            fcam.bottomLimit += cameraChangeY;
            fcam.rightLimit += cameraChangeX;
            fcam.topLimit += cameraChangeY;

            other.transform.position += playerChange;
        }
    }
}
