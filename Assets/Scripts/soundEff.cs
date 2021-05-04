using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEff : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip sound;
    public AudioClip soundFX;

    public void HoverSound()
    {
        audioS.PlayOneShot(sound);
    }

    public void ClickSound()
    {
        audioS.clip = soundFX;
        audioS.Play();
    }

}
