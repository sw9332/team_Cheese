using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private FadeManager fadeManager;
    public GameObject fadeImage;

    public void ScreenClickButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
        StartCoroutine(fadeManager.FadeIn());
    }
}
