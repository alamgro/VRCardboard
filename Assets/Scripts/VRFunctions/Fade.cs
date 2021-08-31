using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    public Animator crlFade;
    public float fadeDuration;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    public void Fading()
    {
        crlFade.SetTrigger("Fade");
    }

}
