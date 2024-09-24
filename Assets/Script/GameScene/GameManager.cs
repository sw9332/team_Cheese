using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Tutorial";
    public DialogueManager dialogueManager;

    void Start()
    {
        GameState = "Tutorial"; /* Tutorial -> Tutorial Cut Scene -> InGame */
    }

    void Update()
    {
        if(GameState == "Tutorial Cut Scene")
        {
            if (dialogueManager.dialogue_continue && dialogueManager.is_talking == false)
                if (dialogueManager.button_text.text == "´Ý±â")
                    if (Input.GetKeyDown(KeyCode.Z))
                        StartCoroutine(Fade("InGame"));
        }
    }

    IEnumerator Fade(string state)
    {
        yield return StartCoroutine(FadeOut());
        GameState = state;
        yield return StartCoroutine(FadeIn());
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