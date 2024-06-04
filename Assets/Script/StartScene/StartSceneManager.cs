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

    public static int Screen_Frame;

    //Screen.currentReolution.refreshRate 현재 모니터 주사율을 가져옴으로써 개인 컴퓨터의 최적화 된 프레임으로 동기화 가능.

    void Awake()
    {
        Screen_Frame = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = Screen_Frame;
    }
    
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
