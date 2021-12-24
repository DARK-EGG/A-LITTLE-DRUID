using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

/* 플레이어의 움직임을 담당하는 PlayerMove 클래스
 * 
 */
public class PlayerMove : MonoBehaviour
{
    /* 플레이어의 현재 상태 */
    public PlayerState currentState;

    /* 플레이어의 속도 */
    public float Speed;

    private float attackTime = .25f;
    private float attackCounter = .25f;

    /* 플레이어 공격기 애니메이션 */
    private Animator attackAnim;
    private Animator effectAnim;

    /* 플레이어의 애니메이션 */
    Animator anim;
    Rigidbody2D rigid;

    /* Move value */
    float h;
    float v;
    bool isHorizonMove;

    // Mobile Key Var
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool attack_Down;
    bool talk_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    ColliderSetting colliderSetting;
    PiecesOfMemory piecesOfMemory;

    void Awake()
    {
        currentState = PlayerState.walk;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        attackAnim = GameObject.FindGameObjectWithTag("AttackEffect").GetComponent<Animator>();
        effectAnim = GameObject.FindGameObjectWithTag("AttackEffect").transform.GetChild(0).GetComponent<Animator>();
        
        colliderSetting = GetComponent<ColliderSetting>();
        piecesOfMemory = GameObject.FindGameObjectWithTag("Manager").GetComponent<PiecesOfMemory>();
    }

    void Update()
    {
        // 플레이어가 움직일 방향 설정
        isHorizonMove = CheckMoving(isHorizonMove);

        // Player Animation
        MoveAnim();

        // Attack Animation
        AttackAnim();

        //special functions
        if (Input.GetButtonDown("Jump") || talk_Down /* || Input.GetMouseButtonDown(0)*/){
            Debug.Log("TalkButton");
            DialogueUI ui = GetComponent<DialogueUI>();
            BossTalk bossTalk = GetComponent<BossTalk>();
            //NPC 대화
            if (ColliderSetting.scanObject && (ColliderSetting.scanObject.CompareTag("NPC") || ColliderSetting.scanObject.CompareTag("TalkCollider")))
            {
                if (Vector2.Distance(ColliderSetting.scanObject.transform.position, transform.position) < 0.5)
                {
                    ui.OpenDialog(ColliderSetting.scanObject);
                }
            }
            //boss trigger 부딪혔을 때
            if (bossTalk.getBossTalkTrigger())
            { 
                if(bossTalk.getBossTalkTrigger().CompareTag("TalkTrigger") && !BossTalk.canBoss && !DialogueUI.endBossTalk && BossTalk.nowBossTalk)
                {

                    Debug.Log("PlayerMove.BossTrigger");
                    ui.OpenDialog(bossTalk.getBossTalkTrigger());
                }
            }
            //마지막 보스 잡은 후 대화
            if(MainBossDeadTalk.lastBossTalk)
                ui.OpenDialog(MainBossDeadTalk.bossID);
        }

        // 0 : 플레이어 일반, 1 : 플레이어 점점 투명, -1 : 플레이어 투명
        if (DialogueUI.isNext == 1)
        {
            anim.SetInteger("isNext", 1);
        }
        else if(DialogueUI.isNext == -1)
        {
            anim.SetInteger("isNext", -1);
        }
        else
        {
            anim.SetInteger("isNext", 0);
        }

        // Mobile Var Init
        InitMobileVar();
    }


    void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

    }

    /* 플레이어의 움직임 방향 설정*/
    private bool CheckMoving(bool isHorizonMove)
    {
        //Move Value
        h = (DialogueUI.isAction || PlayerAttackKeyEvent.IsDead) ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = (DialogueUI.isAction || PlayerAttackKeyEvent.IsDead) ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;

        //Check Button Down & Up
        //PC
        bool hDown = (DialogueUI.isAction || PlayerAttackKeyEvent.IsDead) ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = (DialogueUI.isAction || PlayerAttackKeyEvent.IsDead) ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = (DialogueUI.isAction || PlayerAttackKeyEvent.IsDead) ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = (DialogueUI.isAction || PlayerAttackKeyEvent.IsDead) ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;

        //Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;  

        return isHorizonMove;
    }

    /* Move Animation */
    private void MoveAnim()
    {
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_left") || anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_left"))
            {
                attackAnim.SetBool("Flip", true);
                effectAnim.SetBool("Flip", true);
            }
            else
            {
                attackAnim.SetBool("Flip", false);
                effectAnim.SetBool("Flip", false);
            }

        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
            //attackAnim.SetBool("Flip", false);
        }
    }

    /* Attack Animation */
    private void AttackAnim()
    {
        if (currentState == PlayerState.attack)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                attackAnim.SetBool("Attack", false);
                effectAnim.SetBool("Attack", false);
                currentState = PlayerState.walk;
            }
        }

        if ((Input.GetKeyDown(KeyCode.T) || attack_Down) && currentState != PlayerState.attack && currentState != PlayerState.stagger && !BossTalk.nowBossTalk)
        {
            attackCounter = attackTime;
            attackAnim.SetBool("Attack", true);
            effectAnim.SetBool("Attack", true);
            currentState = PlayerState.attack;
        }
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
            case "A":
                if (!BossTalk.nowBossTalk)
                {
                    if (piecesOfMemory != null)
                    {
                        if (piecesOfMemory.getPieceAlertOn)
                            talk_Down = true;
                        else if (colliderSetting.canAttack)
                            attack_Down = true;
                        else
                            talk_Down = true;
                    }
                    else
                    {
                        if (colliderSetting.canAttack)
                            attack_Down = true;
                        else
                            talk_Down = true;
                    }
                }
                else
                {
                    if (DialogueUI.mainBossChange)
                        talk_Down = true;
                }
                break;
            case "T":
                talk_Down = true;
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0; 
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;

        }
    }

    private void InitMobileVar()
    {
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        attack_Down = false;
        talk_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
    }

    public void InitDialogue()
    {
        InitMobileVar();
        up_Value = 0;
        down_Value = 0;
        right_Value = 0;
        left_Value = 0;
    }
}
