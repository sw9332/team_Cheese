using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Æ©Åä¸®¾ó";

    public static bool Save = false;
    public static bool Load = false;

    public static bool GameEnd = false;

    public Image DemoClearUI;
    public Image GameOverUI;

    private PlayerControl playerControl;
    private FadeManager fadeManager;

    public IEnumerator DemoClear()
    {
        playerControl.isMove = false;
        yield return StartCoroutine(fadeManager.FadeOut(DemoClearUI, Color.white));
    }

    public IEnumerator GameOver()
    {
        if (GameEnd)
        {
            playerControl.isMove = false;
            yield return StartCoroutine(fadeManager.FadeOut(GameOverUI, Color.black));
            yield break;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Option", LoadSceneMode.Additive);

        playerControl = FindFirstObjectByType<PlayerControl>();
        fadeManager = FindFirstObjectByType<FadeManager>();
    }
}