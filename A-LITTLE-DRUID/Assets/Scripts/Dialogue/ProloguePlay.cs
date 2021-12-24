using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//프롤로그 재생용 스크립트
//Manager
public class ProloguePlay : MonoBehaviour
{
    public TypeEffect typeEffect;
    public GameObject message;
    public GameObject notice;
    private DialogueUI ui;

    private void Awake()
    {
        typeEffect.endChat = true;
        ui = GetComponent<DialogueUI>();
    }

    private void Start()
    {
        var noticeT = notice.GetComponent<UnityEngine.UI.Text>();
        if (SaveData.isAndroid)
            noticeT.text = "Touch the screen";

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            if (!typeEffect.endChat)
            {
                typeEffect.EffectEnd();
                return;
            }

            if (ui.index == 6)
            {
                StartCoroutine(FIFO.FadeOutEffect("2_Village"));
            }
            ui.Talk(1);
        }
    }
}
