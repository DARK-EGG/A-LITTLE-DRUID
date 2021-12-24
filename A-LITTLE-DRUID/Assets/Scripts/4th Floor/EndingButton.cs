using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingButton : MonoBehaviour
{
    //버튼 눌리기 전/ 후 여부
    //public static bool buttonChk = false;

    //Object
    public GameObject box;
    public Text leave;
    public Text get;

    DatabaseManager db;
    EndingManager em;
    private void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
        em = GameObject.FindGameObjectWithTag("Manager").GetComponent<EndingManager>();
        //buttonChk = false;

        //언어 별로 버튼 글자 변경
        if (db.en)
        {
            leave.text = "Leave them";
            get.text = "Take them";
        }
    }
    public void GetBtnOnClick()
    {
        EndingChange.whichEnding = 3;
        em.StartEnding();
        em.endingStart = true;
        box.SetActive(false);
        //buttonChk = true;
    }
    public void LeaveBtnOnClick()
    {
        em.StartEnding();
        em.endingStart = true;
        //buttonChk = true;
        box.SetActive(false);
    }
}
