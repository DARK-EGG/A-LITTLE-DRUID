using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoockedRoom : MonoBehaviour
{
    public GameObject doorColliderWhenClosed;
    public GameObject roomTransfer;
    public GameObject[] whichNeedDie;
    public int whichNeedDieEnemyCurrentNum = 0;

    void Start()
    {
        doorColliderWhenClosed.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        roomTransfer.SetActive(false);
        doorColliderWhenClosed.SetActive(true);
    }

    public void IsDead(GameObject enemy)
    {
        for(int i=0; i<whichNeedDie.Length; i++)
        {
            if(whichNeedDie[i] == enemy)
            {
                whichNeedDieEnemyCurrentNum += 1;
            }
            if(whichNeedDieEnemyCurrentNum == whichNeedDie.Length)
            {
                roomTransfer.SetActive(true);
                doorColliderWhenClosed.SetActive(false);
            }
        }
    }
}
