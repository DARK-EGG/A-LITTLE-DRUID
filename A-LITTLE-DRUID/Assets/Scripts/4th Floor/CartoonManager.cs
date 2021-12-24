using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* [진실의방 엔딩 매니저 스크립트]
 * LevelChanger.cs의 fadeout, fadein 효과를 이용해 카툰 페이지를 넘기는 역할 수행
 */
public class CartoonManager : MonoBehaviour
{
    /* 엔딩 트리거 start - true로 변경해주면 화면에서 엔딩 시작 */
    public bool start = false;
    bool end = false;

    /* 재생 중인지에 대한 boolean 변수 */
    public bool isPlayed = false;

    /*실행 여부*/
    bool cartooned = false;

    /* 한 글자씩 출력하는 효과 */
    public TypeEffect typeEffect;
    public LevelChanger levelChanger;
    public GameObject cartoonSet;

    public GameObject portalTrtoEr;                 // portal Truthroom to Endroom

    public bool isChecked = true;

    DialogueUI ui;
    public float t = 0;
    int count = 0;

    private void Awake()
    {
        typeEffect.endChat = true;
    }

    private void Start()
    {
        start = false;
        cartooned = false;
        if(SaveData.isAndroid)
        {
            cartoonSet.GetComponentInChildren<Text>().text = "Touch the screen";
        }
        ui = GetComponent<DialogueUI>();

    }

    void Update()
    {

        if (start == true)
        {
            cartoonSet.SetActive(true);
            isPlayed = true;
            start = false;
        }

        if (count == 1)
        {
            ui.Talk(23);
            levelChanger.FadeOut();
            TrToEndPortalActive();
            count++;
        }
        else if (isChecked == false)
        {
            t += Time.deltaTime;
            if (t > 1f)             // 1초가 지나면 사라짐
            {
                isChecked = true;
                t = 0f;
            }
            //StartCoroutine(WaitForIt());
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isPlayed == true && !cartooned)
        {
            isChecked = false;

            Debug.Log("image : " + ui.index + ", cartoon page : " + levelChanger.page);

            if (!typeEffect.endChat)
            {
                typeEffect.EffectEnd();
                return;
            }
            ColliderSetting.scanObject = null;

            /* Dialogue json에서 18번째 배열에 있는 대사 출력 */
            ui.Talk(23);

            /* 카툰 페이지 넘기기 */
            PlayCartoon(ui);

            if (ui.index == 25 && levelChanger.page == 13)
            {
                count++;
            }
        }
    }

    /* 엔딩 카툰 페이지를 넘기는 메소드 */
    private void PlayCartoon(DialogueUI ui)
    {
        if (ui.index == 1 || ui.index == 7 || ui.index == 14 || ui.index == 15)
        {
            levelChanger.FadeOut();
        }

        if (ui.index == 3 || ui.index == 9 || ui.index == 12 || (ui.index > 15 && ui.index < 22))
        {
            levelChanger.OnFadeComplete();
        }
    }

    /* 진실의 방에서 스토리 열람 후 엔딩방으로 가기 위함 */
    public void TrToEndPortalActive()
    {
        cartooned = true;
        portalTrtoEr.SetActive(true);
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(5.0f);
        isChecked = true;
        yield return null;
    }
}
