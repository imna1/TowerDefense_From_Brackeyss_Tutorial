using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AnimationCurve curve;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string name)
    {
        StartCoroutine(FadeOut(name));
    }

    public void FadeTo()
    {
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().name));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            image.color = new Color(0f, 0f, 0f, curve.Evaluate(t));
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FadeOut(string name)
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            image.color = new Color(0f, 0f, 0f, curve.Evaluate(t));
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(name);
    }
}
