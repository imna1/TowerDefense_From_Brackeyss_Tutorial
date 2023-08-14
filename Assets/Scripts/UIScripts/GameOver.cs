using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text wavesText;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string menuScene = "MainMenu";
    
    private void OnEnable()
    {
        wavesText.text = PlayerStats.waves.ToString();
    }
    public void Retry()
    {
        sceneFader.FadeTo();
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
    }
}
