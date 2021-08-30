using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGenerator : MonoBehaviour
{
    public GameObject objPrefab;

    [SerializeField]
    private float radio;
    [SerializeField]
    private float spawnRate; //Time to spawn an object
    [SerializeField]
    private Transform pivotPosition;
    private Collider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<Collider>();
        transform.position = new Vector3(transform.position.x, pivotPosition.position.y, transform.position.z); //Posicionarlo bien en Y
        InvokeRepeating(nameof(SpawnClampedObject), 1f, spawnRate);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            print("Objeto instanciado");
            SpawnClampedObject();
        }
    }

    private void SpawnClampedObject()
    {
        Vector3 minCoords, maxCoords;
        minCoords = spawnArea.bounds.min;
        maxCoords = spawnArea.bounds.max;

        float randX, randY;
        randX = Random.Range(minCoords.x, maxCoords.x);
        randY = Random.Range(minCoords.y, maxCoords.y);

        Vector3 allowedPos = new Vector3(randX, randY, transform.position.z) - transform.position;

        allowedPos = ClampMagnitude(allowedPos, radio);
        allowedPos.y += transform.position.y;
        allowedPos.z = transform.position.z;

        Instantiate(objPrefab, allowedPos , Quaternion.identity);
    }

    private Vector3 ClampMagnitude(Vector3 _vectorToClamp, float minMagnitude)
    {
        float vecMagnitude = _vectorToClamp.magnitude;
        Vector3 vecNormalized = _vectorToClamp / vecMagnitude; //equivalent to _vectorToClamp.normalized, but slightly faster in this case
        return vecNormalized * minMagnitude;
    }

}
