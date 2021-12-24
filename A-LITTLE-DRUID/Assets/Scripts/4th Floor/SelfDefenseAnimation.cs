using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDefenseAnimation : MonoBehaviour
{
    public Animator realBossAnimator;
    public Animator fakeBossAnimator;
    public string[] realBossAnimList = { "SDPatternForB1", "SDPatternForB2", "SDPatternForB3" };
    public string[] fakeBossAnimList = { "SDPattern1", "SDPattern2", "SDPattern3" };
    public bool selfDefenseAttackEnd = false;
    public bool routineEnd = false;
    public int number;
    public GameObject[] fakeBosses;
    FireAnimation fat;
    BossImgChange bossImgChange;
    MainBossDeadTalk mbdt;
    
    void Start()
    {
        fat = FindObjectOfType<FireAnimation>();
        bossImgChange = FindObjectOfType<BossImgChange>();
        mbdt = FindObjectOfType<MainBossDeadTalk>();

        fakeBossAnimator = GetComponent<Animator>();
        realBossAnimator.enabled = false;
        for(int i=0; i<fakeBosses.Length; i++)
        {
            fakeBosses[i].SetActive(false);
        }
    }

    void Update()
    {
        //if(bossImgChange.bossDead == true && mbdt.fakeBoss == true)
        //{
        //    //gameObject.SetActive(false);
        //}
        if (selfDefenseAttackEnd == false && bossImgChange.bossStandbyEnd == true)
        {
            number = Random.Range(0, 3);
            for (int i = 0; i < fakeBosses.Length; i++)
            {
                fakeBosses[i].SetActive(true);
            }
            realBossAnimator.enabled = true;
            fakeBossAnimator.enabled = true;
            ChangeBossPosition cBP;
            cBP = FindObjectOfType<ChangeBossPosition>();
            cBP.ChangeLocation();
            SelfDefenseAnimReal(number);
            SelfDefenseAnimFake(number);
            selfDefenseAttackEnd = true;
        }
        if (realBossAnimator.GetCurrentAnimatorStateInfo(0).IsName(realBossAnimList[number])
            && realBossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            routineEnd = true;
            
            realBossAnimator.Play("BossAppear", -1, 0f);
        }

        if(selfDefenseAttackEnd && !routineEnd && !fat.fireAttackStarted && fat.selfDefenseStarted)
        {
            for (int i = 0; i < fakeBosses.Length; i++)
            {
                fakeBosses[i].SetActive(false);
            }
        }
        if (routineEnd == true && realBossAnimator.GetCurrentAnimatorStateInfo(0).IsName("BossAppear")
            && realBossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            fat.fireAttackStarted = true;
            realBossAnimator.enabled = false;
            fakeBossAnimator.enabled = false;
            routineEnd = false;
        }
    }

    public void SelfDefenseAnimReal(int t)
    {
        // Debug.Log("Self Defense Real's Anim Started");
        realBossAnimator.Play(realBossAnimList[t], -1, 0f);
    }
    public void SelfDefenseAnimFake(int t)
    {
        // Debug.Log("Self Defense Fake's Anim Started");
        fakeBossAnimator.Play(fakeBossAnimList[t], -1, 0f); 
    }

    public void FakeBossesSetActiveFalse()
    {
        for (int i = 0; i < fakeBosses.Length; i++)
        {
            fakeBosses[i].SetActive(false);
        }
    }
}
