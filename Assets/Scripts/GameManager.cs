using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON MANAGER
    public static GameManager Manager { get { return _instance; } }

    private static GameManager _instance;
    #endregion

    [SerializeField] private float obstacleSpeed;
    [SerializeField] private float rewardSpeed;

    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    pro
}
