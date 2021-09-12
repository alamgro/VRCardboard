using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region SINGLETON MANAGER
    private static GameManager _instance;

    public static GameManager Manager { get { return _instance; } }

    #endregion

    public float obstacleSpeed;

    [SerializeField] private float rewardSpeed;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private GameObject[] heartsHP;
    [SerializeField] private GameObject heartParticles;
    [SerializeField] private int health;
    private readonly int maxhealth = 3;
    private int score = 0;
    private int highScore;
    

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        health = maxhealth;

        highScoreUI.text = string.Empty + highScore.ToString("0000"); //Modificar UI

        SetUpHeartsHP(maxhealth); //Prepare 3D heart models

    }

    public void AddToScore(int _amount)
    {
        score += _amount;
        scoreUI.text = string.Empty + score.ToString("0000"); //Para mantener el formato 0000
    }

    public void CheckForHighScore(int _score)
    {
        if (_score < highScore)
            return;

        highScore = _score;
        PlayerPrefs.SetInt("highScore", highScore);
        highScoreUI.text = string.Empty + highScore.ToString("0000"); //Modificar UI
    }

    //Adds up the amount of health provided (substracts if it is a negative number)
    public void GetDamage()
    {
        health--;

        if(health <= 0)
        {
            //Game Over
            if(Fade.instance)
                Fade.instance.Fading();
            Invoke(nameof(ResetGame), 1.0f);
        }

        heartsHP[health].SetActive(false);

        SpawnHeartParticles(heartsHP[health].transform.position);
    }

    void SpawnHeartParticles(Vector3 _position)
    {
        Instantiate(heartParticles, _position, Quaternion.identity);
    }

    private void SetUpHeartsHP(int _amount)
    {
        //Make appear the hearts in-game
        for(int i = 0; i < _amount && i < heartsHP.Length; i++)
        {
            heartsHP[i].SetActive(true);
        }
    }

    public void ResetGame()
    {
        CheckForHighScore(score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
