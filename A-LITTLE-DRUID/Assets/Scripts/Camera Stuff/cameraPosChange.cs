using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPosChange : MonoBehaviour
{
    public Transform cameraTransPos;
    Camera mainCam;
    
    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        resetMoveableArea resetMoveableArea;
        resetMoveableArea = FindObjectOfType<resetMoveableArea>();
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Change Player's Position & Camera's Position");
            if (gameObject.CompareTag("Go Before Gate"))
            {
                resetMoveableArea.currentRoomNum -= 1;
            }
            else if (gameObject.CompareTag("Go After Gate"))
            {
                resetMoveableArea.currentRoomNum += 1;
            }
            else if (gameObject.CompareTag("SecretRoomIn"))
            {
                resetMoveableArea.currentRoomNum += 5;
            }
            else if (gameObject.CompareTag("SecretRoomOut"))
            {
                resetMoveableArea.currentRoomNum -= 5;
            }
            else if (gameObject.CompareTag("GoTruthFrontRoom"))
            {
                resetMoveableArea.currentRoomNum = 11;
            }
            else if (gameObject.CompareTag("GoEndRoom"))
            {
                resetMoveableArea.currentRoomNum = 9;
            }
            resetMoveableArea.ResettingMoveArea();
            collision.gameObject.transform.position = cameraTransPos.position;
            mainCam.transform.position = new Vector3(cameraTransPos.position.x, cameraTransPos.position.y, mainCam.transform.position.z);
            
        }
    }
}
