using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttackKeyEvent : MonoBehaviour
{
    PlayerStatus playerStatus;
    EnemiesHP enemiesHP;

    Vector3 MonsterOut = new Vector3(-10000, 0, 0);

    AudioSource hitSound;
    AudioSource monsterHitSound;
    AudioSource sorrowSound;

    float delaySceneAfter = 7f;

    static public bool IsDead = false;
    static public bool BossDead = false;

    static public bool middleBossClear = false;

    public static int removedEnemyNum = 0;
    public static int removedMBNum = 0;
    public Animator anim;

    EnemyCertainRadius enemyCertainRadius;
    MeetBoss meetBoss;
    GameObject manager;

    public float restartSceneSec = 7f;

    float dmg;
    float distanceBEP;

    public bool attack_Down;

    ColliderSetting colliderSetting;
    PiecesOfMemory piecesOfMemory;

    private void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
        enemiesHP = GetComponent<EnemiesHP>();

        enemyCertainRadius = FindObjectOfType<EnemyCertainRadius>();
        meetBoss = FindObjectOfType<MeetBoss>();
        manager = GameObject.FindGameObjectWithTag("Manager");
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        
        distanceBEP = 0f;

        colliderSetting = GameObject.FindGameObjectWithTag("Player").GetComponent<ColliderSetting>();
        piecesOfMemory = FindObjectOfType<PiecesOfMemory>();
    }

    private void Start()
    {
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        monsterHitSound = GameObject.Find("MonsterHitSound").GetComponent<AudioSource>();
        sorrowSound = GameObject.Find("SorrowSound").GetComponent<AudioSource>();
        dmg = playerStatus.pStatus.dmgToEnemy;

        removedEnemyNum = 0;
        removedMBNum = 0;

        IsDead = false;
        BossDead = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.T) || attack_Down) && !BossTalk.nowBossTalk)
        {
            var playerPosition = playerStatus.pStatus.player.transform.position;
            if(!piecesOfMemory.getPieceAlertOn && colliderSetting.canAttack)
                HitSoundPlay();
            for (int i = 0; i < enemiesHP.EnemyName.Length; i++)
            {
                //Condition은 특정 범위 내부에 몬스터가 있는지를 판별하기 위한 bool변수입니다.
                var enemy = enemiesHP.Enemies[i];
                distanceBEP = Vector2.Distance(enemy.transform.position, playerPosition);
                if (distanceBEP < playerStatus.pStatus.distance)
                {
                    enemiesHP.EnemyHp[i] -= dmg;
                    //MonsterHitSoundPlay();
                    //StartCoroutine(MonsterHitImg(enemiesHP.Enemies[i]));

                    if(DeadConfirm(enemy, enemiesHP.EnemyHp[i]))
                    {
                        removedEnemyNum += 1;
                        if (SceneManager.GetActiveScene().buildIndex == 6)
                        {
                            enemyCertainRadius.EnemyCrushed(i);
                        }
                    }

                    /*
                    if (enemiesHP.EnemyHp[i] <= 0)
                    {
                        enemiesHP.Enemies[i].transform.position = MonsterOut;
                        removedEnemyNum += 1;
                        SorrowSoundPlay();
                        if (SceneManager.GetActiveScene().buildIndex == 6)
                        {
                            enemyCertainRadius.EnemyCrushed(i);
                        }
                    }
                    */
                }
            }
            for (int t = 0; t < enemiesHP.MiddleBoss.Length; t++)
            {
                var enemy = enemiesHP.MiddleBoss[t];
                distanceBEP = Vector2.Distance(enemy.transform.position, playerPosition);
                if (distanceBEP < playerStatus.pStatus.distance)
                {
                    enemiesHP.middleBossHp[t] -= dmg;
                    MonsterHitSoundPlay();
                    StartCoroutine(MonsterHitImg(enemiesHP.MiddleBoss[t]));

                    if (enemiesHP.middleBossHp[t] <= 0)
                    {
                        enemy.transform.position = MonsterOut;
                        SorrowSoundPlay();
                        removedMBNum += 1;
                        if (enemiesHP.middleBosses.Length == removedMBNum)
                        {
                            middleBossClear = true;
                            if (SceneManager.GetActiveScene().buildIndex != 3)
                            {
                                manager.GetComponent<CanGoToBoss>().OpenBossDoor();
                            }
                        }
                        if (SceneManager.GetActiveScene().buildIndex == 4)
                        {
                            meetBoss.CanWeMeetBoss();
                        }
                        if (SceneManager.GetActiveScene().buildIndex == 6)
                        {
                            enemyCertainRadius.MiddleBossCrushed(t);
                        }
                        SorrowSoundPlay();
                        if (enemiesHP.middleBossName[t].Equals("LongarmIdle"))
                        {
                            longarmMove.longarmDead = true;
                        }
                    }
                }
            }
            distanceBEP = Vector2.Distance(enemiesHP.floorLastBoss.transform.position, playerPosition);
            if ((distanceBEP < playerStatus.pStatus.distance) && (BossTalk.canBoss == true))
            {
                if (SceneManager.GetActiveScene().buildIndex == 6)
                {
                    FloorFourBossAttackConfirm();
                }
                else
                {
                    enemiesHP.floorLastBossHp -= dmg;
                    MonsterHitSoundPlay();
                    StartCoroutine(MonsterHitImg(enemiesHP.floorLastBoss));
                }

                if (enemiesHP.floorLastBossHp <= 0)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 6)
                    {
                        Debug.Log("BossImage Change");
                        MainBossDeadTalk mbdt = FindObjectOfType<MainBossDeadTalk>();
                        mbdt.SetLocation();
                        BossImgChange bossImgChange;
                        bossImgChange = FindObjectOfType<BossImgChange>();
                        bossImgChange.LastBossDead();
                        var bossAll = GameObject.Find("BossAll");
                        bossAll.transform.GetChild(0).gameObject.SetActive(false);
                        bossAll.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    enemiesHP.floorLastBoss.transform.position = MonsterOut;
                    SorrowSoundPlay();
                    BossDead = true;
                }
            }
        }

        if (playerStatus.pStatus.playerCurrentHp <= 0)
        {
            IsDead = true;
            anim.SetBool("die", true);
            StartCoroutine(Waiter());
            //StartCoroutine(PlayerDead());

            //restartSceneSec += 0.005f;//Time.deltaTime;
            if (IsDead == true &&restartSceneSec > delaySceneAfter)
            {
                anim.SetBool("die", false);
                StartCoroutine(FIFO.FadeOutEffect(SceneManager.GetActiveScene().name));
                IsDead = false;
                BossTalk.canTalk = false;
                BossTalk.canBoss = false;
                BossTalk.stopTime = false;
            }
        }

        // Mobile Var Init
        InitMobileVar();
    }

    public IEnumerator Waiter()
    {
        yield return new WaitForSeconds(7f);
        restartSceneSec = 8f;
    }
    public IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(7f);
        StartCoroutine(FIFO.FadeOutEffect(SceneManager.GetActiveScene().name));

    }
    public void HitSoundPlay()
    {
        hitSound.Play();
    }
    public void MonsterHitSoundPlay()
    {
        monsterHitSound.Play();
    }

    public void SorrowSoundPlay()
    {
        sorrowSound.Play();
    }

    public void FloorFourBossAttackConfirm()
    {
        Debug.Log("FireAnimation Stuff");
        //FireAnimation fat = GameObject.Find("Pattern1").GetComponent<FireAnimation>();
        if (FireAnimation.fireAttackEnd == true)
        {
            enemiesHP.floorLastBossHp -= dmg;
            MonsterHitSoundPlay();
        }
        else
            return;
    }

    // Function: Monster Color's 'A'(R,G,B,A's A) Change When Player Attacked
    IEnumerator MonsterHitImg(GameObject hitObj)
    {
        Color normal = hitObj.GetComponent<SpriteRenderer>().color;
        hitObj.GetComponent<SpriteRenderer>().color = new Color(normal.r, normal.g, normal.b, 0.5f);
        yield return new WaitForSeconds(0.05f);
        hitObj.GetComponent<SpriteRenderer>().color = normal;
    }

    public bool DeadConfirm(GameObject obj, float health)
    {
        MonsterHitSoundPlay();
        StartCoroutine(MonsterHitImg(obj));

        if (health <= 0)
        {
            obj.transform.position = MonsterOut;
            SorrowSoundPlay();
            return true;
        }
        else
        {
            return false;
        }
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
