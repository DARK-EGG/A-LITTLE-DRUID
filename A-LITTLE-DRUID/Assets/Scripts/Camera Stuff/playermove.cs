using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float force = 0.1f;

    void Update()
    {
        if(Input.GetKey("w"))
        {
            rigid.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
        if(Input.GetKey("s"))
        {
            rigid.AddForce(-transform.up * force, ForceMode2D.Impulse);
        }
        if(Input.GetKey("a"))
        {
            rigid.AddForce(-transform.right * force, ForceMode2D.Impulse);
        }
        if (Input.GetKey("d"))
        {
            rigid.AddForce(transform.right * force, ForceMode2D.Impulse);
        }
    }
}
