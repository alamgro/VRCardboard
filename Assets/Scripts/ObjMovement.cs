using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = Vector3.back * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
    
}
