using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Tutorial";

    void Start()
    {
        GameState = "Tutorial"; /* Tutorial -> Tutorial Cut Scene -> InGame */
    }

    public Image fadeImage;
    public float fadeDuration = 1f;

    public IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / fadeDuration);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / fadeDuration);
            yield return null;
        }

        fadeImage.color = Color.black;
    }
}