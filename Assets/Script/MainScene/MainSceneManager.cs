using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject SettingUI;

    //-------------------------------------------------------------------------------

    //새 게임
    public void StartButton_Fade()
    {
        fadeOut.SetActive(true);
        isFading_out = true;
        fadeTimer_out = 0.0f;

        Invoke("StartFadeOut", 1.5f);
    }

    //설정
    public void SettingButton_true()
    {
        SettingUI.SetActive(true);
    }

    //설정 닫기
    public void SettingButton_false()
    {
        SettingUI.SetActive(false);
    }

    //나가기
    public void ExitButton()
    {
        Application.Quit();
    }

    //-------------------------------------------------------------------------------

    void Start()
    {
        fadein_Start();
        FadeOut_Start();
    }

    void Update()
    {
        Fadein();
        FadeOut();
    }

    //-------------------------------------------------------------------------------

    //게임 시작 버튼 페이드 아웃 처리
    public Image fadeOutImage;
    public GameObject fadeOut;
    public float fadeOut_Duration = 1.0f;
    
    private bool isFading_out = false;
    private float fadeTimer_out = 0.0f;
    
    void FadeOut_Start()
    {
        fadeOutImage.color = Color.clear;
        fadeOut.SetActive(false);
    }

    void FadeOut()
    {
        if (isFading_out)
        {
            fadeTimer_out += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer_out / fadeOut_Duration);

            fadeOutImage.color = Color.Lerp(Color.clear, Color.black, alpha);

            if (fadeTimer_out >= fadeOut_Duration)
            {
                isFading_out = false;
            }
        }
    }

    void StartFadeOut()
    {
        SceneManager.LoadScene("tutorial");
    }

    //-------------------------------------------------------------------------------

    //페이드 인 처리
    public Image fadeImage;
    public GameObject fade;
    public float fadeDuration = 1.0f;
    
    private bool isFading = false;
    private float fadeTimer = 0.0f;
    
    void fadein_Start()
    {
        fade.SetActive(true);
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        StartFade();

    }

    void Fadein()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = 1.0f - Mathf.Clamp01(fadeTimer / fadeDuration);

            fadeImage.canvasRenderer.SetAlpha(alpha);

            if (fadeTimer >= fadeDuration)
            {
                isFading = false;
            }
        }
    }

    void StartFade()
    {
        Invoke("FadeImage_false", 1f);
        isFading = true;
        fadeTimer = 0.0f;
    }

    void FadeImage_false()
    {
        fade.SetActive(false);
    }

    //-------------------------------------------------------------------------------
}
