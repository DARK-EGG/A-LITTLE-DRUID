using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrans : MonoBehaviour
{
    public float left;
    public float right;
    public float bot;
    public float top;
    public Vector3 playerChange; // player을 얼마나 움직일 건지.
    private playerfollow fcam;

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
        if (other.CompareTag("Player"))
        {
            fcam.leftLimit =left;
            fcam.bottomLimit =bot;
            fcam.rightLimit = right;
            fcam.topLimit = top;
            Debug.Log("실행");
            other.transform.position = playerChange;
        }
    }
}
