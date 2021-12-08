using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrierControl : MonoBehaviour
{
    public float rotationThreshold; //Umbral en el que se activa la rotación
    public float rotationSpeed; //Velocidad de rotación
    public float speedMultiplier; //Si gira más la cabeza aumenta más la rotación, esto agranda ese efecto

    private Transform cam;
    private float finalSpeed = 0;

    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.DeltaAngle(0, cam.eulerAngles.z) > rotationThreshold)
        {
            //Mirando a la izquierda
            RotateLeft();
        }
        else if (Mathf.DeltaAngle(0, cam.eulerAngles.z) < -rotationThreshold)
        {
            //Mirando a la derecha
            RotateRight();
        }

        //print(finalSpeed);

        ///Debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RotateRight()
    {
        //Calcular velocidad final dependiendo de qué tan girada esté la cámara
        finalSpeed = rotationSpeed + (Mathf.DeltaAngle(cam.eulerAngles.z, -rotationThreshold) * speedMultiplier);
        finalSpeed = Mathf.Clamp(finalSpeed, 0f, 800f);
        transform.Rotate(Vector3.back * finalSpeed * Time.deltaTime);
    }

    public void RotateLeft()
    {
        //Calcular velocidad final dependiendo de qué tan girada esté la cámara
        finalSpeed = rotationSpeed + (Mathf.DeltaAngle(rotationThreshold, cam.eulerAngles.z) * speedMultiplier);
        finalSpeed = Mathf.Clamp(finalSpeed, 0f, 800f);
        transform.Rotate(Vector3.forward * finalSpeed * Time.deltaTime);
    }


}
