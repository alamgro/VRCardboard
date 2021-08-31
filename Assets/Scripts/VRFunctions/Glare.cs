using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glare : MonoBehaviour
{
    public GvrReticlePointer pointer;
    public float timeToWait;
    float timer;
    bool canInteract;


    // Update is called once per frame
    void Update()
    {
        TimerUpdate();
    }

    public void TimeEnter()
    {
        canInteract = true;
        pointer.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void TimerUpdate()
    {
        if (canInteract)
        {
            timer += Time.deltaTime;
            pointer.progress = Mathf.Lerp(0, pointer.reticleSegments, timer / timeToWait);
            pointer.CreateReticleVertices();
            if(timer >= timeToWait)
            {
                TimerExit();
                gameObject.SetActive(false);
            }
        }
    }

    public void TimerExit()
    {
        canInteract = false;
        timer = 0;

        pointer.progress = 0;
        pointer.CreateReticleVertices();
        pointer.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
