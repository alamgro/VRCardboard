using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public GvrReticlePointer pointer;
    public Transform goal;

    bool isTeleporting;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportEnter()
    {
        pointer.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public void TeleportClick()
    {
        if(!isTeleporting)
        {
            isTeleporting = true;
            StartCoroutine(IETeleport());
        }
    }

    public void TeleportExit()
    {
        pointer.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    IEnumerator IETeleport()
    {
        Fade.instance.Fading();
        yield return new WaitForSeconds(Fade.instance.fadeDuration);
        player.transform.position = goal.position;
        pointer.GetComponent<MeshRenderer>().material.color = Color.white;
        gameObject.SetActive(false);
        isTeleporting = false;
    }

}
