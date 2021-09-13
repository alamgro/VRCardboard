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
        speed = GameManager.Manager.globalSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.back * speed;
    }
}
