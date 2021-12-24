using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//대사 글자 효과
//text내에 들어있음
public class TypeEffect : MonoBehaviour
{
    //목표 대사
    string targetMsg;
    Text msgText;
    public float speed;

    //대사 내 인덱스
    int index;
    //대화 끝남 여부
    public bool endChat = true;

    float t = 0;
    bool start = false;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        endChat = true;
    }

    //타겟 메세지 설정 & 효과 시작
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        endChat = false;
        EffectStart();
    }

    //효과 시작 전 세팅 & 효과 진행 코루틴 시작
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        t = 0;
        StartCoroutine(EffectIng());
    }
    
    IEnumerator EffectIng()
    {
        yield return new WaitUntil(() => speed * (t+=Time.deltaTime) >= 1);

        //대사가 없거나, 다 띄운 후 종료
        if (msgText.text == targetMsg || targetMsg == null)
        {
            if(!endChat)
                EffectEnd();
        }
        //대사 띄우기
        else if(!start)
        {
            start = true;
            msgText.text += targetMsg[index];
            index++;
            t = 0;
            StartCoroutine(EffectIng());
            start = false;
        }
    }

    //효과 끝, 전체 대사 보여줌
    public void EffectEnd()
    {
        msgText.text = targetMsg;
        endChat = true;
        start = false;
    }
}
