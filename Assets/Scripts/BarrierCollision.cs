using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Demon"))
        {
            GameManager.Manager.AddToScore(1); //añadir punto
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Soul"))
        {
            GameManager.Manager.GetDamage(); //Quitar 1 corazón
            Destroy(other.gameObject);
        }
    }
}
