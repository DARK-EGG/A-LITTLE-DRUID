using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFXOnAnimation : MonoBehaviour
{
    public AudioSource lastBossLaughFX;

    public void lastBossLaughPlay()
    {
        lastBossLaughFX.Play();
    }
}
