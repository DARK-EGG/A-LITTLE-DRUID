using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

//Player의 Collision event
//Player_notre
public class ColliderSetting : MonoBehaviour
{
    public static GameObject scanObject;
    public GameObject dialogBtn;
    public bool canAttack = true;

    private GameObject spacePls;
    private GameObject alertSpacePls;
    private PiecesOfMemory piecesOfMemory;
    private DialogueUI ui;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 2)
        piecesOfMemory = GameObject.FindGameObjectWithTag("Manager").GetComponent<PiecesOfMemory>();

        ui = GetComponent<DialogueUI>();
        spacePls = ui.spacePlz;
        alertSpacePls = ui.alertBox.transform.GetChild(1).gameObject;

        if (SaveData.isAndroid)
        {
            spacePls.GetComponent<Text>().text = "Touch the button";
            alertSpacePls.GetComponent<Text>().text = "Touch the button";
        }
    }

    //Press the spacebar 대화 중 제거
    void Update()
    {
        if (DialogueUI.isAction && !BossTalk.canTalk)
        {
            spacePls.SetActive(false);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //기억의 조각과 부딪혔을 경우
        if (collision.gameObject.CompareTag("mPiece"))
        {
            piecesOfMemory.getPiece(collision);
        }
        //대화 가능 object와 부딪혔을 경우 Press the spacebar 띄움
        else
        {
            scanObject = collision.gameObject;
            if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("TalkCollider"))
            {
                if (SaveData.isAndroid == false)
                    spacePls.SetActive(true);
                dialogBtn.GetComponent<Image>().sprite = Resources.Load("images\\Chat", typeof(Sprite)) as Sprite;
                canAttack = false;
            }
        }
    }
    //Press the spacebar 대화 가능 Object와 접촉 멈추면 제거
    private void OnCollisionExit2D(Collision2D collision)
    {
        spacePls.SetActive(false);
        dialogBtn.GetComponent<Image>().sprite = Resources.Load("images\\Attack", typeof(Sprite)) as Sprite;
        canAttack = true;
    }
}
