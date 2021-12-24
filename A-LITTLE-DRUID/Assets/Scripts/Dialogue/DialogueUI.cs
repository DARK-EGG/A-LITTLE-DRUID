using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//대사의 전체적인 흐름
//Dialogue Manager
public class DialogueUI : MonoBehaviour
{
    //대화 관련 objects 
    public GameObject dialogBox;
    public GameObject alertBox;
    public GameObject spacePlz;
    Text alertText;
    public TypeEffect typeEffect;

    //대화중 여부
    public static bool isAction = false;
    //대화 인덱스
    public int index;
    //주인공 애니매이션 관련
    public static int isNext = 0;
    //대화가 바뀌었는가
    bool changed = false;
    //보스 대화 여부
    public static bool endBossTalk = false;
    public static bool bossTalkChanged = false;
    public static bool mainBossChange = false;


    //과거 이야기 관련 변수
    bool cartoonPlayed = false;
    CartoonManager cartoon;

    DatabaseManager databaseManager;
    RemoveMovekeysDialogue moveKey;
    ColliderSetting colliderSetting;

    void Awake()
    {
        isAction = false;
        isNext = 0;
        typeEffect.endChat = true;
        endBossTalk = false;
    }

    private void Start()
    {
        cartoonPlayed = false;
        cartoon = GameObject.FindGameObjectWithTag("Manager").GetComponent<CartoonManager>();
        if(SceneManager.GetActiveScene().buildIndex != 1)
            alertText = alertBox.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        databaseManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DatabaseManager>();
        if (SaveData.isAndroid)
            moveKey = GameObject.FindGameObjectWithTag("Manager").GetComponent<RemoveMovekeysDialogue>();
        colliderSetting = FindObjectOfType<ColliderSetting>();
        bossTalkChanged = false;
    }


    public void OpenDialog(GameObject scanObj)
    {
        //글자가 끝나지 않았는데 이 함수가 다시 호출되었을 경우
        if (typeEffect.endChat == false)
        {
            typeEffect.EffectEnd();
            return;
        }

        //대화 중
        isAction = true;
        //spacePlz.SetActive(false);

        //NPC 말풍선
        ObjData objdata = scanObj.GetComponent<ObjData>();
        if (ColliderSetting.scanObject != null)
        {
            if (ColliderSetting.scanObject.CompareTag("NPC"))
                ColliderSetting.scanObject.transform.GetChild(0).gameObject.SetActive(false);
            if (objdata.id == 5)
                ColliderSetting.scanObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        //대화창 Open
        dialogBox.SetActive(true);
        isAction = true;

        if (SaveData.isAndroid)
            moveKey.HideMoveKey(false);

        Talk(objdata.id);
    }

    public void Talk(int id)
    {

        //변경된 대사인지 확인 
        if (!changed)
            if (ChangeTalk(id)) return;
        string talkData = databaseManager.GetDialogue(id, index);

        //타이핑 효과 시작
        typeEffect.SetMsg(talkData);

        //대사가 끝나면
        if (talkData == null)
        {
            //일반 대화 종료
            isAction = false;
            index = 0;
            dialogBox.SetActive(false);
            ColliderSetting.scanObject = null;
            changed = false;
            typeEffect.endChat = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().InitDialogue();
            
            // 문제가 되는 부분
            colliderSetting.canAttack = true;
            
            // cartoonManager에서 쓰이는 DialogueUI는 Player에 안 들어있고, DialogManager에 들어있어서 colliderSetting에 nullException이 뜸

            if (SaveData.isAndroid)
                moveKey.HideMoveKey(true);

            //각 층 보스와의 인트로 대사 종료
            if ((id == 11 || id == 14 || id == 17 || id == 24) && BossTalk.canTalk && !changed)
            {
                GetComponent<BossTalk>().setNullBossTalkTrigger();
                BossTalk.stopTime = false;
                BossTalk.canBoss = true;
                BossTalk.canTalk = false;
                BossTalk.nowBossTalk = false;
                endBossTalk = true;

                //4층 보스일 경우
                if (id == 24)
                {
                    BossImgChange bossImgChange = FindObjectOfType<BossImgChange>();
                    bossImgChange.scarescrow = false;
                    bossImgChange.ChangeLocation(0);
                }
            }

            //4층 보스 사망 후 대화 종료
            else if (id == 25)
            {
                MainBossDeadTalk.lastBossTalk = false;
                MainBossDeadTalk.bossID.SetActive(false);
                LastBossClear lastBossClear = FindObjectOfType<LastBossClear>();
                lastBossClear.LastBossClearPortalOpen();
            }

            //스파클과 대화 종료 후 엔딩 시작
            else if (id == 26 || id == 27)
            {
                EndingManager em = GameObject.FindGameObjectWithTag("Manager").GetComponent<EndingManager>();
                em.endingStart = true;
                em.StartEnding();
            }
            else if(id == 10)
            {
                BossTalk.nowBossTalk = false;
            }
            return;
        }

        //이벤트 확인
        TalkEvent(id);

        //다음 대화로 연결
        isAction = true;
        index++;
        changed = false;
        return;
    }

    public bool ChangeTalk(int id)
    {
        if(id == 2)
        {
            if (SaveData.isAndroid)
            {
                changed = true;
                Talk(31);
                return true;
            }
        }
        //1층 보스와 대화
        if (id == 11)
        {
            if (!BossTalk.canTalk && !BossTalk.canBoss)
            {
                bossTalkChanged = true;
                changed = true;
                Talk(10);
                return true;
            }
            else
            {
                bossTalkChanged = false;
            }

        }
        //기억의 조각
        else if (id == 9 || id == 13 || id == 16)
        {
            if (!PiecesOfMemory.all)
            {
                changed = true;
                Talk(4);
                return true;
            }

        }
        else if(id == 22)
        {
            if (!cartoonPlayed)
            {
                changed = true;
                Talk(23);
                return true;
            }
        }
        //엔딩마다 대사 변경
        else if (id == 26)
        {
            if (EndingChange.whichEnding == 2)
            {
                changed = true;
                Talk(27);
                return true;
            }
        }
        return false;
    }

    public void TalkEvent(int id)
    {
        //프롤로그
        if(id == 1)
        {
            if(index == 6)
            {
                typeEffect.EffectEnd();
                StartCoroutine(FIFO.FadeOutEffect("2_Village"));
            }
        }
        //다음 층으로
        else if (id == 3)
        {
            typeEffect.EffectEnd();
            SceneChan(SceneManager.GetActiveScene().buildIndex - 2);
        }
        //엄마
        else if (id == 5)
        {
            if (index == 5)
            {
                dialogBox.SetActive(false); 
                typeEffect.EffectEnd();
                if (databaseManager.en)
                    alertText.text = "You got a magic wand.";
                else alertText.text = "지팡이를 얻었습니다.";
                alertBox.SetActive(true);
                if (SaveData.isAndroid)
                    moveKey.HideMoveKey(true);
            }
            else if (index == 6)
            {
                alertBox.SetActive(false);
                if (SaveData.isAndroid)
                    moveKey.HideMoveKey(false);
            }
        }
        //용사
        else if (id == 7)
        {
            if (index == 5)
                isNext = 1;
            else if (index == 6)
                isNext = -1;
            else if (index == 8)
            {
                alertBox.SetActive(true);
                if (SaveData.isAndroid)
                    moveKey.HideMoveKey(true);
                dialogBox.SetActive(false);
                typeEffect.EffectEnd();
                if (databaseManager.en)
                    alertText.text = "Enter the temple.";
                else
                {
                    alertText.text = "신전에 입장합니다.";
                    //StartCoroutine(FIFO.FadeOutEffect("3_1st floor"));
                }
            }
            else if (index == 9)
            {
                StartCoroutine(FIFO.FadeOutEffect("3_1st floor"));
                alertBox.SetActive(false);
                dialogBox.SetActive(false);
                if (SaveData.isAndroid)
                    moveKey.HideMoveKey(true);
                //SceneManager.LoadScene("3_1st floor");
            }
        }
        //진실의 방 이야기
        else if (id == 23)
        {
            if (cartoonPlayed == false && cartoon.isPlayed == false)
            {
                dialogBox.SetActive(false);
                if (SaveData.isAndroid)
                    moveKey.HideMoveKey(true);
                typeEffect.EffectEnd();
                cartoon.start = true;
                cartoonPlayed = true;
            }
        }
        //보스와의 대화
        else if (id == 24)
        {
            BossImgChange bossImgChange = FindObjectOfType<BossImgChange>();
            if (index == 0)
            {
                bossImgChange.ChangeLocation(-1.5f);
            }
            if (index == 5)
            {
                if (SaveData.isAndroid)
                {
                    moveKey.HideMoveKey(true);
                    mainBossChange = true;
                    colliderSetting.canAttack = false;
                    colliderSetting.dialogBtn.GetComponent<Image>().sprite = Resources.Load("images\\Chat", typeof(Sprite)) as Sprite;
                }
                dialogBox.SetActive(false);
                typeEffect.EffectEnd();
                bossImgChange.bossAnimator.SetBool("change", true);
                spacePlz.SetActive(true);
            }
            else if (index == 6)
            {
                if (SaveData.isAndroid)
                {
                    colliderSetting.dialogBtn.GetComponent<Image>().sprite = Resources.Load("images\\Attack", typeof(Sprite)) as Sprite;
                    colliderSetting.canAttack = false;
                    mainBossChange = false;
                }
                spacePlz.SetActive(false);
            }
        }
        //엔딩 버튼 등장
        else if (id == 26)
        {
            if (EndingChange.whichEnding != 2 && index == 4)
            {
                dialogBox.transform.GetChild(1).gameObject.SetActive(true);
                dialogBox.transform.GetChild(2).gameObject.SetActive(false);
            }

        }
        //엔딩 끝 신 변경
        else if (id == 28 || id == 29 || id == 30)
        {
            if (index == 5)
            {
                typeEffect.EffectEnd();
                StartCoroutine(FIFO.FadeOutEffect("7_Ending"));
                //SceneManager.LoadScene("7_Ending");
            }
        }
    }

    //씬 변경
    void SceneChan(int floor)
    {
        if (index == 0)
        {
            Debug.Log("입장문 등장");
            isNext = 1;
            alertBox.SetActive(true);
            dialogBox.SetActive(false);
            if (SaveData.isAndroid)
                moveKey.HideMoveKey(true);
            if (databaseManager.en)
                alertText.text = "Enter floor " + (floor + 1);
            else
                alertText.text = floor + 1 + "층에 입장합니다.";
        }
        else if (index == 1)
        {
            Debug.Log("이동");
            alertBox.SetActive(false);
            dialogBox.SetActive(false);
            if (SaveData.isAndroid)
                moveKey.HideMoveKey(true);
            isAction = false;
            isNext = -1;
            GetComponent<SaveData>().Save();
            switch (floor)
            {
                case 1:
                    StartCoroutine(FIFO.FadeOutEffect("4_2nd floor"));
                    //SceneManager.LoadScene("4_2nd floor");
                    break;
                case 2:
                    StartCoroutine(FIFO.FadeOutEffect("5_3rd floor"));
                    //SceneManager.LoadScene("5_3rd floor");
                    break;
                case 3:
                    StartCoroutine(FIFO.FadeOutEffect("6_4th floor"));
                    //SceneManager.LoadScene("6_4th floor");
                    break;

            }
        }
    }
}