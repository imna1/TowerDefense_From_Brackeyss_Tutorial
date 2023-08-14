using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private GameObject levelLoaderUI;
    [SerializeField] private Button[] buttons;

    private int ButtonIndex;

    private void Start()
    {
        ButtonIndex = PlayerPrefs.GetInt("levelReached", 1);
        while (ButtonIndex < buttons.Length)
        {
            buttons[ButtonIndex].interactable = false;
        }
    }

    public void Play()
    {
        levelLoaderUI.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Exciting");
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
}
