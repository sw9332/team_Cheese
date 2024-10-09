using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private PlayerControl playerControl;

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

    public IEnumerator ChangeStateFade(string state) //State 바꾸기
    {
        yield return StartCoroutine(FadeOut());
        playerControl.isMove = false;
        GameManager.GameState = state;
        yield return StartCoroutine(FadeIn());
        playerControl.isMove = true;
    }

    public IEnumerator ChangeSceneFade(string scene) //Scene 바꾸기
    {
        yield return StartCoroutine(FadeOut());
        playerControl.isMove = false;
        SceneManager.LoadScene(scene);
        yield return StartCoroutine(FadeIn());
        playerControl.isMove = true;
    }

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }
}
