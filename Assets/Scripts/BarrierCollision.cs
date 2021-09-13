using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollision : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audDemon;
    [SerializeField] private AudioClip audSoul;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Demon"))
        {
            audioSource.PlayOneShot(audDemon);
            GameManager.Manager.AddToScore(1); //añadir punto
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Soul"))
        {
            audioSource.PlayOneShot(audSoul);
            GameManager.Manager.GetDamage(); //Quitar 1 corazón
            Destroy(other.gameObject);
        }
    }
}
