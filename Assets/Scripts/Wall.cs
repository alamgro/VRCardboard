using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    /*
     * Este script detecta un objeto que ya pasó la barrera.
     * Cuando el objeto que pasa es un Soul, suma un punto (porque el player NO debe de detenerlas)
     * Si el objeto que pasa es un Demon, se resta un punto (porque el player Sí debe de detenerlos)
     */
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip audGrowl;
    [SerializeField] private AudioClip audLoli;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Demon"))
        {
            audioSource.PlayOneShot(audGrowl);
            GameManager.Manager.GetDamage(); //Quitar 1 corazón
        }
        else if (other.CompareTag("Soul"))
        {
            audioSource.PlayOneShot(audLoli);
            GameManager.Manager.AddToScore(1); //añadir punto
        }

        Destroy(other.gameObject);
    }
}
