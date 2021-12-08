using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGenerator : MonoBehaviour
{
    public GameObject demonPrefab;
    public GameObject soulPrefab;

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

    //SE PUEDE OPTIMIZAR SI SOLO INSTANCÍO EN UNA UNIDAD  DE -1 A 1 Y LE SUMO LAS COORDENADAS DEL PIVOTE
    private IEnumerator SpawnClampedObject(float _spawnRate)
    {
        yield return new WaitForSecondsRealtime(_spawnRate);
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

        Spawn(allowedPos);

        //Debug.Log(_spawnRate, gameObject);
        //Call the coroutine again
        AdjustDifficulty(ref _spawnRate, timeToSubstract);
        StartCoroutine(SpawnClampedObject(_spawnRate));
    }

    private void Spawn(Vector3 _allowedPos)
    {
        #region SPAWN RANDOM SOUL OR DEMON
        if (Random.Range(0, 4) == 0)
        {
            //Spawn soul
            Instantiate(soulPrefab, _allowedPos, soulPrefab.transform.rotation);

            //Ramdonly decide if it spawns something else (75 % chances)
            if(Random.Range(0, 4) != 0)
            {
                Vector3 opositePos = (transform.position - _allowedPos).normalized * 6f;
                opositePos = ClampMagnitude(opositePos, radio);
                opositePos.y += transform.position.y;
                opositePos.z = transform.position.z;

                //25% chances of spawn another soul
                if (Random.Range(0, 4) == 0)
                    Instantiate(soulPrefab, opositePos, soulPrefab.transform.rotation);
                else
                    Instantiate(demonPrefab, opositePos, demonPrefab.transform.rotation);
            }
        }
        else
        {
            //Spawn demon
            Instantiate(demonPrefab, _allowedPos, demonPrefab.transform.rotation);

            //Ramdonly decide if it spawns a soul (75 % chances)
            if (Random.Range(0, 4) != 0)
            {
                Vector3 opositePos = (transform.position - _allowedPos).normalized * 6f;
                opositePos = ClampMagnitude(opositePos, radio);
                opositePos.y += transform.position.y;
                opositePos.z = transform.position.z;

                //33% chances of spawn another soul
                if (Random.Range(0, 3) == 0)
                    Instantiate(soulPrefab, opositePos, soulPrefab.transform.rotation);
            }
        }
        #endregion
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
