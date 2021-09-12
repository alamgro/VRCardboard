using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glare : MonoBehaviour
{
    public GvrReticlePointer pointer;
    public float timeToWait;
    float timer;
    bool canInteract;

    void Update()
    {
        TimerUpdate();
    }

    public void TimeEnter()
    {
        canInteract = true;
        pointer.GetComponent<MeshRenderer>().material.color = Color.green;
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
                GameManager.Manager.ResetGame();
                //gameObject.SetActive(false);
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
