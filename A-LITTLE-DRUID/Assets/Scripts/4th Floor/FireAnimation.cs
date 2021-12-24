using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimation : MonoBehaviour
{
    public Animator animator;
    public int bossPostureNum;
    public string[] fireAnimName = { "FirePattern1", "FirePattern1_2", "FirePattern2", "FirePattern2_2", "FirePattern3", "FirePattern3_2", "FirePattern4", "FirePattern4_2" };
    public int playedNum = 0;
    public int playRequireAnimNum = 0;
    public GameObject firePatterns;
    public static bool fireAttackEnd = false;
    public bool fireAttackStarted = false;
    public bool selfDefenseStarted = false;
    BossImgChange bossImgChange;
    SelfDefenseAnimation selfDefenseAnimation;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        bossImgChange = FindObjectOfType<BossImgChange>();
        selfDefenseAnimation = FindObjectOfType<SelfDefenseAnimation>();
        fireAttackEnd = false;
    }

    void Update()
    {
        
        if (bossImgChange.scarescrow || PlayerAttackKeyEvent.BossDead)
        {
            animator.enabled = false;
        }
        else
        {
            if (fireAttackStarted)
            {
                bossImgChange.bossAnimator.enabled = false;
                bossImgChange.barrier.SetActive(true);
                bossFireAttackImageChangeList();
                animator.enabled = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                Debug.Log("Fire Animation Called??");
                animator.Play(fireAnimName[0], -1, 0f);
                playedNum = 0;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(fireAnimName[0]))
                    fireAttackStarted = false;
                RestOneSecond();
                fireAttackEnd = false;
            }
            if ((fireAttackEnd == false) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                fireAttackAnim(playedNum);
            }
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && fireAttackEnd)
            {
                for(int i=0; i<transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            if ((fireAttackEnd == true) && (selfDefenseStarted == false))
            {
                bossImgChange.BossStanbyAnimPlay();
                selfDefenseStarted = true;
                selfDefenseAnimation.selfDefenseAttackEnd = false;
            }
        }
    }

    void fireAttackAnim(int t)
    {
        if (t == 7)
        {
            fireAttackEnd = true;
            animator.enabled = false;
            selfDefenseStarted = false;
            bossImgChange.bossStandbyEnd = false;
            return;
        }
        firePatterns.SetActive(true);
        playRequireAnimNum = t;
        Debug.Log(fireAnimName[playRequireAnimNum] + " Ended");
        animator.Play(fireAnimName[playRequireAnimNum + 1], -1, 0f);

        playedNum += 1;
        bossFireAttackImageChangeList();
        // Debug.Log("fireAttackBlah " + fireAttackEnd);
        // Debug.Log("Time: " + (animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
    }


    void bossFireAttackImageChangeList()
    {
        if (playedNum == 0 || playedNum == 1)
            bossImgChange.ChangeSprite(1);
        else if (playedNum == 2 || playedNum == 3)
            bossImgChange.ChangeSprite(2);
        else if (playedNum == 4 || playedNum == 5)
            bossImgChange.ChangeSprite(3);
        else
            bossImgChange.ChangeSprite(4);
    }
    public IEnumerator RestOneSecond()
    {
        //firePatterns.SetActive(false);
        yield return new WaitForSeconds(1);

    }
}
