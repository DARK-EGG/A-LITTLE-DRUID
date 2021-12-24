using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;
    public AudioSource getItemSound;
    public AudioSource getSuperItemSound;

    private void Awake()
    {
        doorOpenSound = GameObject.Find("DoorOpenSound").GetComponent<AudioSource>();
        doorCloseSound = GameObject.Find("DoorCloseSound").GetComponent<AudioSource>();
        getItemSound = GameObject.Find("GetItemSound").GetComponent<AudioSource>();
        getSuperItemSound = GameObject.Find("SpecialPotionFX").GetComponent<AudioSource>();
    }

    public void DoorOpenSoundPlay()
    {
        doorOpenSound.Play();
    }

    public void DoorCloseSoundPlay()
    {
        doorCloseSound.Play();
    }

    public void GetItemSoundPlay()
    {
        getItemSound.Play();
    }

    public void GetSuperItemSound()
    {
        getSuperItemSound.Play();
    }
}
