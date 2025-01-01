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

    public IEnumerator FadeIn(Image ui, Color color)
    {
        ui.gameObject.SetActive(true);
        ui.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            ui.color = Color.Lerp(color, Color.clear, timer / fadeDuration);
            yield return null;
        }

        isFade = false;
        ui.gameObject.SetActive(false);

        if (playerControl != null) playerControl.isMove = true;
    }

    public IEnumerator FadeOut(Image ui, Color color)
    {
        if (playerControl != null) playerControl.isMove = false;

        isFade = true;
        ui.gameObject.SetActive(true);
        ui.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            ui.color = Color.Lerp(Color.clear, color, timer / fadeDuration);
            yield return null;
        }

        ui.color = color;
    }

    public IEnumerator ChangeStateFade(string state) //State 바꾸기
    {
        playerControl.isMove = false;
        yield return StartCoroutine(FadeOut(fadeImage, Color.black));
        GameManager.GameState = state;
        yield return StartCoroutine(FadeIn(fadeImage, Color.black));
        playerControl.isMove = true;
    }

    public IEnumerator ChangeSceneFade(string scene) //Scene 바꾸기
    {
        yield return StartCoroutine(FadeOut(fadeImage, Color.black));
        SceneManager.LoadScene(scene);
    }

    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
}
