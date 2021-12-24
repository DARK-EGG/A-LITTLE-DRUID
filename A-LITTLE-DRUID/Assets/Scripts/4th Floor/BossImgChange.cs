using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImgChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] standingPosture = new Sprite[4];
    FireAnimation fat;
    public Animator bossAnimator;
    public bool bossStandbyEnd = false;
    public GameObject boss;
    public GameObject barrier;
    public bool scarescrow = true;
    public GameObject firePattern;
    public bool bossDead = false;
    ChangeBossPosition cBP;
    
    void Start()
    {
        Debug.Log(gameObject.name);
        scarescrow = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fat = FindObjectOfType<FireAnimation>();
        bossAnimator = GetComponent<Animator>();
        bossAnimator.enabled = false;
        cBP = FindObjectOfType<ChangeBossPosition>();
    }

    void Update()
    {
        if (scarescrow)
        {
            bossAnimator.enabled = true;
            barrier.SetActive(false);
            firePattern.SetActive(false);
        }
        else if(!scarescrow && !bossDead)
        {
            firePattern.SetActive(true);
            if (bossStandbyEnd == false && bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Standby"))
            {
                if (bossStandbyEnd == false && bossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    bossStandbyEnd = true;
                }
            }
        }
    }

    public void ChangeSprite(int i)

    {
        spriteRenderer.sprite = standingPosture[i-1];
    }

    public void BossStanbyAnimPlay()
    {
        bossAnimator.enabled = true;
        bossAnimator.Play("Standby", -1, 0f);
    }
    public void ChangeLocation(float f)
    {
        Vector2 v = new Vector2(0, f);
        boss.transform.position = v;
    }

    public void LastBossDead()
    {
        Debug.Log("Boss Dead");
        firePattern.SetActive(false);
        bossDead = true;
    }
}
