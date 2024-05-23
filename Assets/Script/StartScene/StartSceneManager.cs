using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public Image fadeImage;
    public GameObject fade;
    public float fadeDuration = 1.0f;
    
    private bool isFading = false;
    private float fadeTimer = 0.0f;
    
    void Start()
    {
        fadeImage.color = Color.clear;
        fade.SetActive(false);
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            fadeImage.color = Color.Lerp(Color.clear, Color.black, alpha);

            if (fadeTimer >= fadeDuration)
            {
                isFading = false;
            }
        }
    }

    public void StartFade()
    {
        fade.SetActive(true);
        isFading = true;
        fadeTimer = 0.0f;

        Invoke("StartGame", 1.5f);
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
