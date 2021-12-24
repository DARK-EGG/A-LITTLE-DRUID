using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleJewel : MonoBehaviour
{
    public GameObject VisibleImg1;
    public GameObject VisibleImg2;
    public GameObject VisibleImg3;
    public bool setVisible1st;
    public bool setVisible2nd;
    public bool setVisible3rd;

    void Start()
    {
        setVisible1st = SaveData.Load_1st_JW();
        setVisible2nd = SaveData.Load_2nd_JW();
        setVisible3rd = SaveData.Load_3rd_JW();
    }

    // Update is called once per frame
    void Update()
    {
        if (setVisible1st)
        {
            VisibleImg1.SetActive(true);
        }
        if (setVisible2nd)
        {
            VisibleImg2.SetActive(true);
        }
        if (setVisible3rd)
        {
            VisibleImg3.SetActive(true);
        }
    }
}
