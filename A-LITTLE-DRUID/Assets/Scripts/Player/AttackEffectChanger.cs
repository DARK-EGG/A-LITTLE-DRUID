using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectChanger : MonoBehaviour
{
    private Animator attackAnim;
    private Animator effectAnim;
    int killedMobsNum = 0;
    int KillCount = 0;
    int[] changeEffectMobs = { 20, 55, 90 };
    void Start()
    {
        attackAnim = GameObject.FindGameObjectWithTag("AttackEffect").GetComponent<Animator>();
        effectAnim = GameObject.FindGameObjectWithTag("AttackEffect").transform.GetChild(0).GetComponent<Animator>();
        int kmn_1st = SaveData.Load_1st_KM();
        Debug.Log("1층 :"+  kmn_1st);
        int kmn_2nd = SaveData.Load_2nd_KM();
        Debug.Log("2층 :" + kmn_2nd);
        int kmn_3rd = SaveData.Load_3rd_KM();
        Debug.Log("3층 :" + kmn_3rd);
        killedMobsNum = kmn_1st + kmn_2nd + kmn_3rd;
    }

    void Update()
    {
        KillCount = killedMobsNum + PlayerAttackKeyEvent.removedEnemyNum;
        if (KillCount >= changeEffectMobs[2])
        {
            Change(4);
        }
        else if (KillCount >= changeEffectMobs[1])
        {
            Change(3);
        }
        else if (KillCount >= changeEffectMobs[0])
        {
            Change(2);
        }
        else
        {
            Change(1);
        }
    }
    public void Change(int num)
    {
        attackAnim.SetInteger("Skill", num);
        effectAnim.SetInteger("Skill", num);
    }
}
