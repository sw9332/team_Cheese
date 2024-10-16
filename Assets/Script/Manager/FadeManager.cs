using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private PlayerControl playerControl;
    private DialogueManager dialogueManager;

    public Image fadeImage;
    public float fadeDuration = 1f;
    public bool isFade = false;

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

        isFade = false;
        fadeImage.gameObject.SetActive(false);

        if(playerControl != null && !dialogueManager.DialoguePanel.activeSelf)
            playerControl.isMove = true;
    }

    public IEnumerator FadeOut()
    {
        if(playerControl != null)
            playerControl.isMove = false;

        isFade = true;
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
        playerControl.isMove = false;
        yield return StartCoroutine(FadeOut());
        GameManager.GameState = state;
        yield return StartCoroutine(FadeIn());
        playerControl.isMove = true;
    }

    public IEnumerator ChangeSceneFade(string scene) //Scene 바꾸기
    {
        playerControl.isMove = false;
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(scene);
        yield return StartCoroutine(FadeIn());
        playerControl.isMove = true;
    }

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
}
