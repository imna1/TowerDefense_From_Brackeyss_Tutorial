using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private Text livesCounter;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameWon;
    [SerializeField] private Animation subtractLives;
    [SerializeField] private int nextLevelNum;

    public bool gameIsOver;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameIsOver = false;
        livesCounter.text = PlayerStats.lives.ToString();
    }
    public void SubtractLive()
    {
        PlayerStats.lives--;
        livesCounter.text = PlayerStats.lives.ToString();
        subtractLives.Play();
        if(PlayerStats.lives <= 0)
        {
            EndGame();
        }
    }

    public void WonGame()
    {
        PlayerPrefs.SetInt("levelReached", nextLevelNum);
        gameIsOver = true;
        gameWon.SetActive(true);
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOver.SetActive(true);
    }
}
