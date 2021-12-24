using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    private int health;             // 현재 체력
    private int healthMax;          // 최대 체력

    public HealthSystem(int healthMax)      // 생성자
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()                  // health의 접근자
    {
        return health;
    }

    public void Damage(int damageAmount)    // 데미지를 입혔을 때 메소드
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}

//Golem
//Golem 스폰 스크립트
public class Golem : MonoBehaviour
{
    /* 몬스터 체력*/
    public int health = 40;

    /* 공격 사정거리 */
    public float distance;

    /* 죽은 뒤에 이동될 좌표 */
    public Vector3 MonsterOut;

    /* 부울 변수 */
    protected bool dead = false;

    protected HealthSystem healthSystem;
    private Rigidbody2D rigid;
    private static GameObject player;
    PlayerAttackKeyEvent playerAttackKeyEvent;
    PlayerStatus playerStatus;

    public static bool cloneCheck;

    Vector2 home;
    public int time;
    public int setTime = 200;

    bool attack_Down = false;

    public void Awake()
    {
        time = setTime;
        home = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        playerStatus = FindObjectOfType<PlayerStatus>();

    }
    void Start()
    {
        healthSystem = new HealthSystem(health);        // 40의 체력 부여
        playerAttackKeyEvent = FindObjectOfType<PlayerAttackKeyEvent>();
    }

    public void Update()
    {
        /* T 키를 눌러 공격했을 때 */
        if (Input.GetKeyDown(KeyCode.T) || attack_Down)
        {
            cloneCheck = true;
            var distanceBEP = Vector2.Distance(rigid.transform.position, player.transform.position);
            if (distanceBEP < playerStatus.pStatus.distance)
            {
                Debug.Log("클론 해당 범위 내에 있는 몬스터에게 타격을 입혔습니다.");

                healthSystem.Damage(5);

                if (playerAttackKeyEvent.DeadConfirm(gameObject, healthSystem.GetHealth()))
                    dead = true;
            }
            cloneCheck = false;
        }

        if (time < 0)
        {
            time = setTime;
            healthSystem = new HealthSystem(35);
            transform.position = home;
            dead = false;
        }
        else if (dead)
            time--;

        InitMobileVar();
    }
    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "A":
                attack_Down = true;
                break;

        }
    }

    private void InitMobileVar()
    {
        attack_Down = false;
    }
}
