using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPanel : MonoBehaviour
{
    public GameObject endingPanel;
    public GameObject endingBtn;

    public GameObject unlock_end1;
    public GameObject lock_end1;
    public GameObject unlock_end2;
    public GameObject lock_end2;
    public GameObject unlock_end3;
    public GameObject lock_end3;

    void Start()
    {
        endingBtn.SetActive(false);

        endingPanel.SetActive(false);

        unlock_end1.SetActive(false);
        lock_end1.SetActive(true);
        unlock_end2.SetActive(false);
        lock_end2.SetActive(true);
        unlock_end3.SetActive(false);
        lock_end3.SetActive(true);

        OpenBtn();
    }

    public void OpenEndingPanel()
    {
        endingPanel.SetActive(true);
    }

    public void CloseEndingPanel()
    {
        endingPanel.SetActive(false);
    }

    public void OpenBtn()
    {
        if (LoadEndingInfo.Load())
        {
            if (EndingInfo.ending1 || EndingInfo.ending2 || EndingInfo.ending3)
            {
                endingBtn.SetActive(true);
                if (EndingInfo.ending1)
                {
                    unlock_end1.SetActive(true);
                    lock_end1.SetActive(false);
                }
                if (EndingInfo.ending2)
                {
                    unlock_end2.SetActive(true);
                    lock_end2.SetActive(false);
                }
                if (EndingInfo.ending3)
                {
                    unlock_end3.SetActive(true);
                    lock_end3.SetActive(false);
                }
            }
        }
    }


}
