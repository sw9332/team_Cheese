using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private FadeManager fadeManager;

    public void ScreenClickButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
        StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));
    }
}