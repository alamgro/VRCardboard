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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Demon"))
        {
            GameManager.Manager.GetDamage(); //Quitar 1 corazón
        }
        else if (other.CompareTag("Soul"))
        {
            GameManager.Manager.AddToScore(1); //añadir punto
        }

        Destroy(other.gameObject);
    }
}
