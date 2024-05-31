using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    //메인화면 버튼---------------------------------------------------------------------

    //새 게임
    public void StartButton_Fade()
    {
        StartCoroutine(FadeOut());
        Invoke("tutorial_start", 1f);
    }

    public GameObject SettingUI;

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

    void tutorial_start()
    {
        SceneManager.LoadScene("tutorial");
    }

    //-------------------------------------------------------------------------------

    public Image fadeImage;
    public float fadeDuration = 1f;

    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / fadeDuration);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / fadeDuration);
            yield return null;
        }
    }

    //-------------------------------------------------------------------------------

    void Start()
    {
        StartCoroutine(FadeIn());
    }
}