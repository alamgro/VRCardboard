using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGenerator : MonoBehaviour
{
    public GameObject objPrefab;

    #region PRIVATE VARIABLES
    [SerializeField] private float radio;
    [SerializeField] private float initialSpawnRate; //Time to spawn an object
    [SerializeField] private float minSpawnRate; //Time to spawn an object
    [SerializeField] private float timeToSubstract; //Time to substrat to the spawnRate, this increases the difficulty
    [SerializeField] private Transform pivotPosition;
    
    private Collider spawnArea;
    #endregion

    void Start()
    {
        spawnArea = GetComponent<Collider>();
        transform.position = new Vector3(transform.position.x, pivotPosition.position.y, transform.position.z); //Posicionarlo bien en Y
        StartCoroutine(SpawnClampedObject(initialSpawnRate));
    }

    void Update()
    {

    }

    //SE PUEDE OPTIMIZAR SI SOLO INSTANCÍO EN UNA UNIDAD  DE -1 A 1 Y LE SUMO LAS COORDENADAS DEL PIVOTE
    private IEnumerator SpawnClampedObject(float _spawnRate)
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

        print(_spawnRate);
        yield return new WaitForSecondsRealtime(_spawnRate);
        //Call the coroutine again
        AdjustDifficulty(ref _spawnRate, timeToSubstract);
        StartCoroutine(SpawnClampedObject(_spawnRate));
    }

    private Vector3 ClampMagnitude(Vector3 _vectorToClamp, float minMagnitude)
    {
        float vecMagnitude = _vectorToClamp.magnitude;
        Vector3 vecNormalized = _vectorToClamp / vecMagnitude; //equivalent to _vectorToClamp.normalized, but slightly faster in this case
        return vecNormalized * minMagnitude;
    }

    private void AdjustDifficulty(ref float _spawnRate, float _timeToSubstract)
    {
        //Check if the spawnRate reached the minimum rate
        if (_spawnRate <= minSpawnRate)
            return;

        _spawnRate -= _timeToSubstract;
    }
}
