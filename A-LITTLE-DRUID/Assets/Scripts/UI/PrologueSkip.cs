using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueSkip : MonoBehaviour
{
    public TypeEffect typeEffect;
    public Button skip;

    private void Start()
    {
        if (LoadEndingInfo.Load())
        {
            if (EndingInfo.ending1 || EndingInfo.ending2 || EndingInfo.ending3)
            {
                skip.gameObject.SetActive(true);
            }
        }
    }

    public void Skip()
    {
        typeEffect.EffectEnd();
        StartCoroutine(FIFO.FadeOutEffect("2_Village"));
    }
}
