using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text MapNameText;
    public Text DateText;
    private Coroutine fadeCoroutine;
    private float fadeDuration = 1f;

    public void ShowMapNameText(string MapName, float delay) //¸Ê ÀÌ¸§
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            MapNameText.color = Color.clear;
        }

        fadeCoroutine = StartCoroutine(ShowMapNameTextCoroutine(MapName, delay));
    }

    public void ShowDateText(string Date, float delay) //³¯Â¥
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            DateText.color = Color.clear;
        }

        fadeCoroutine = StartCoroutine(ShowDateTextCoroutine(Date, delay));
    }

    private IEnumerator ShowMapNameTextCoroutine(string MapName, float delay)
    {
        MapNameText.text = MapName;
        yield return StartCoroutine(FadeInText(MapNameText));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOutText(MapNameText));
    }

    private IEnumerator ShowDateTextCoroutine(string Date, float delay)
    {
        DateText.text = Date;
        yield return StartCoroutine(FadeInText(DateText));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOutText(DateText));
    }

    public IEnumerator FadeInText(Text text)
    {
        text.gameObject.SetActive(true);
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            text.color = Color.Lerp(Color.clear, Color.white, timer / fadeDuration);
            yield return null;
        }
    }

    public IEnumerator FadeOutText(Text text)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            text.color = Color.Lerp(Color.white, Color.clear, timer / fadeDuration);
            yield return null;
        }

        text.gameObject.SetActive(false);
    }
}
