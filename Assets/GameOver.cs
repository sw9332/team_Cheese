using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private FadeManager fadeManager;

    public Image error;

    public void NewGame()
    {
        SaveManager.Instance.DeleteKey();
        StartCoroutine(fadeManager.ChangeSceneFade("GameScene"));
    }

    public void Load()
    {
        if (GameManager.Save)
        {
            GameManager.Load = true;
            StartCoroutine(fadeManager.ChangeSceneFade("GameScene"));
        }

        else error.gameObject.SetActive(true);
    }

    public void Quit()
    {
        StartCoroutine(fadeManager.ChangeSceneFade("MainScene"));
    }

    public void OK()
    {
        error.gameObject.SetActive(false);
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();
        
        StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
    }
}