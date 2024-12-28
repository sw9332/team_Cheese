using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Ʃ�丮��";

    public static bool Demo = false;

    public Image DemoClearUI;
    public Image GameOverUI;

    private PlayerControl playerControl;
    private FadeManager fadeManager;

    public IEnumerator DemoClear()
    {
        playerControl.isMove = false;
        yield return StartCoroutine(fadeManager.FadeOut(DemoClearUI, Color.clear));
    }

    public IEnumerator GameOver()
    {
        playerControl.isMove = false;
        yield return StartCoroutine(fadeManager.FadeOut(GameOverUI, Color.black));
    }

    void Start()
    {
        GameState = "Ʃ�丮��";

        playerControl = FindFirstObjectByType<PlayerControl>();
        fadeManager = FindFirstObjectByType<FadeManager>();
    }
}