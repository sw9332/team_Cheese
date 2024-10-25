using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNameManager : MonoBehaviour
{
    public Text MapNameText;
    private Coroutine fadeCoroutine;
    private float fadeDuration = 1f;

    public void ShowMapNameText(string MapName)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            MapNameText.color = Color.clear;
        }

        fadeCoroutine = StartCoroutine(ShowMapNameTextCoroutine(MapName));
    }

    private IEnumerator ShowMapNameTextCoroutine(string MapName)
    {
        MapNameText.text = MapName;
        yield return StartCoroutine(FadeInText());
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FadeOutText());
    }

    public IEnumerator FadeInText()
    {
        MapNameText.gameObject.SetActive(true);
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            MapNameText.color = Color.Lerp(Color.clear, Color.white, timer / fadeDuration);
            yield return null;
        }
    }

    public IEnumerator FadeOutText()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            MapNameText.color = Color.Lerp(Color.white, Color.clear, timer / fadeDuration);
            yield return null;
        }

        MapNameText.gameObject.SetActive(false);
    }
}
