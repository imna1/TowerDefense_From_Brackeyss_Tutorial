using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Text enemies;
    [SerializeField] private string menuScene = "MainMenu";
    [SerializeField] private string nextLevel;

    private int value;
    private float sec;

    private void OnEnable()
    {
        StartCoroutine("AnimateText");
    }
    
    private IEnumerator AnimateText()
    {
        value = 0;
        sec = 0.55f / PlayerStats.killingEnemies;
        yield return new WaitForSeconds(0.25f);
        while (value < PlayerStats.killingEnemies)
        {
            value++;
            enemies.text = value.ToString();
            yield return new WaitForSeconds(sec);
        }
    }

    public void NextLevel()
    {
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
    }
}
