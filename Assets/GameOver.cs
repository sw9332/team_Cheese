using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private FadeManager fadeManager;

    public Image image;
    public Image error;

    private static GameOver instance;

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

        else Destroy(this.gameObject);
    }

    public IEnumerator True()
    {
        yield return StartCoroutine(fadeManager.FadeOut(image, Color.black));
        SceneManager.LoadScene("Game Over");
        GameManager.GameEnd = true;
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

        else StartCoroutine(fadeManager.FadeOut(error, Color.black));
    }

    public void Quit()
    {
        StartCoroutine(fadeManager.ChangeSceneFade("MainScene"));
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();
    }
}