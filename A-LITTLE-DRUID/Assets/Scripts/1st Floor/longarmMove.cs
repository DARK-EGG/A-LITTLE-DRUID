using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class longarmMove : MonoBehaviour
{
    public GameObject longarm;
    public float speed;
    public bool goDown = true;
    public static bool longarmDead = false;
    public float bBottom;
    public float bTop;
    public Animator larmAnim;
    public Animator larmDownAnim;
    Vector3 longarmPos;

    void Start()
    {
        longarmPos = longarm.transform.position;
        longarmDead = false;
    }

    void Update()
    {
        if (longarmDead == false)
        {
            if (goDown)
            {
                
                longarmPos.y -= speed * Time.deltaTime;
                if (longarmPos.y <= bBottom)
                {
                    goDown = false;
                    larmAnim.SetBool("goDown", false);
                    larmDownAnim.SetBool("goDownCo", false);
                }
            }
            else
            {
                longarmPos.y += speed * Time.deltaTime;
                if (longarmPos.y >= bTop)
                {
                    goDown = true;
                    larmAnim.SetBool("goDown", true);
                    larmDownAnim.SetBool("goDownCo", true);
                }
            }
            longarm.transform.position = new Vector3(longarmPos.x, longarmPos.y, longarmPos.z);
        }
    }
}
