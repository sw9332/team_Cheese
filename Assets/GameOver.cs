using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private FadeManager fadeManager;

    public Image error;

    private static GameOver instance = null;

    public static GameOver Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        else Destroy(instance);
    }

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

    public IEnumerator True()
    {
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        SceneManager.LoadScene("Game Over");
        GameManager.GameEnd = true;
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();
    }
}